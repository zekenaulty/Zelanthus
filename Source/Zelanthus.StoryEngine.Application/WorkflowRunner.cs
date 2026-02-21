using Zelanthus.StoryEngine.Domain;

namespace Zelanthus.StoryEngine.Application;

public sealed class WorkflowRunner : IWorkflowRunner
{
    private readonly IWorkflowStepExecutor _workflowStepExecutor;

    public WorkflowRunner(IWorkflowStepExecutor workflowStepExecutor)
    {
        _workflowStepExecutor = workflowStepExecutor ?? throw new ArgumentNullException(nameof(workflowStepExecutor));
    }

    public async Task<WorkflowExecutionResult> RunAsync(
        WorkflowExecutionRequest executionRequest,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(executionRequest);

        var workflowDefinition = executionRequest.WorkflowDefinition;
        var runCursor = executionRequest.WorkflowRunCursor;
        var policyReasonCode = default(string?);

        if (!IsChainModeAligned(workflowDefinition.WorkflowKind, runCursor.ChainMode))
        {
            runCursor.MarkTerminalFailure();
            return WorkflowExecutionResult.TerminalFailure(
                RunnerReasonCodes.InvalidStateTransition,
                runCursor,
                workflowDefinition.Steps.Select(step => step.StepKey).ToArray());
        }

        var effectiveSteps = workflowDefinition.Steps.ToList();
        var resumeStart = ResolveStartStepIndex(runCursor);
        var stepIndex = resumeStart.StartStepIndex;
        policyReasonCode = resumeStart.PolicyReasonCode;

        runCursor.TransitionRunState(RunState.Running);

        while (stepIndex < effectiveSteps.Count)
        {
            var step = effectiveSteps[stepIndex];
            if (step.PromptReference is null)
            {
                runCursor.MarkTerminalFailure();
                return WorkflowExecutionResult.TerminalFailure(
                    RunnerReasonCodes.MissingPromptReference,
                    runCursor,
                    effectiveSteps.Select(currentStep => currentStep.StepKey).ToArray(),
                    policyReasonCode);
            }

            var executionContext = new WorkflowStepExecutionContext(
                workflowDefinition,
                step,
                stepIndex,
                runCursor);

            var stepResult = await _workflowStepExecutor.ExecuteAsync(executionContext, cancellationToken).ConfigureAwait(false);
            if (!stepResult.IsSuccess)
            {
                if (stepResult.IsRetryableFailure)
                {
                    runCursor.MarkStepRetryableFailure();
                    return WorkflowExecutionResult.RetryableFailure(
                        stepResult.ReasonCode!,
                        runCursor,
                        effectiveSteps.Select(currentStep => currentStep.StepKey).ToArray(),
                        policyReasonCode);
                }

                runCursor.MarkTerminalFailure();
                return WorkflowExecutionResult.TerminalFailure(
                    stepResult.ReasonCode!,
                    runCursor,
                    effectiveSteps.Select(currentStep => currentStep.StepKey).ToArray(),
                    policyReasonCode);
            }

            if (stepResult.AppendedSteps.Count > 0)
            {
                effectiveSteps.AddRange(stepResult.AppendedSteps);
            }

            runCursor.MarkStepSucceeded(stepIndex, stepResult.LatestThinkingPersistenceKey);
            stepIndex = runCursor.CurrentStepIndex;
        }

        runCursor.TransitionRunState(RunState.Succeeded);
        return WorkflowExecutionResult.Succeeded(
            runCursor,
            effectiveSteps.Select(currentStep => currentStep.StepKey).ToArray(),
            policyReasonCode);
    }

    private static bool IsChainModeAligned(WorkflowKind workflowKind, ChainMode chainMode)
    {
        return workflowKind switch
        {
            WorkflowKind.CognitiveChain => chainMode == ChainMode.CognitiveChain,
            WorkflowKind.ConversationalChain => chainMode == ChainMode.ConversationalChain,
            _ => false,
        };
    }

    private static ResumeStart ResolveStartStepIndex(WorkflowRunCursor runCursor)
    {
        if (runCursor.ChainMode == ChainMode.CognitiveChain && runCursor.CurrentStepIndex > 0)
        {
            // Cognitive chains always restart from the first planning step on resume.
            return new ResumeStart(0, RunnerReasonCodes.CognitiveRestartRequired);
        }

        return new ResumeStart(runCursor.CurrentStepIndex);
    }

    private readonly record struct ResumeStart(int StartStepIndex, string? PolicyReasonCode = null);
}
