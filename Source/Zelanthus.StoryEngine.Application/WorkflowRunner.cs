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

        if (!IsChainModeAligned(workflowDefinition.WorkflowKind, runCursor.ChainMode))
        {
            runCursor.MarkTerminalFailure();
            return WorkflowExecutionResult.TerminalFailure(RunnerReasonCodes.InvalidStateTransition, runCursor);
        }

        var effectiveSteps = workflowDefinition.Steps.ToList();
        var stepIndex = ResolveStartStepIndex(runCursor);

        runCursor.TransitionRunState(RunState.Running);

        while (stepIndex < effectiveSteps.Count)
        {
            var step = effectiveSteps[stepIndex];
            if (step.PromptReference is null)
            {
                runCursor.MarkTerminalFailure();
                return WorkflowExecutionResult.TerminalFailure(RunnerReasonCodes.MissingPromptReference, runCursor);
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
                    return WorkflowExecutionResult.RetryableFailure(stepResult.ReasonCode!, runCursor);
                }

                runCursor.MarkTerminalFailure();
                return WorkflowExecutionResult.TerminalFailure(stepResult.ReasonCode!, runCursor);
            }

            if (stepResult.AppendedSteps.Count > 0)
            {
                effectiveSteps.AddRange(stepResult.AppendedSteps);
            }

            runCursor.MarkStepSucceeded(stepIndex, stepResult.LatestThinkingPersistenceKey);
            stepIndex = runCursor.CurrentStepIndex;
        }

        runCursor.TransitionRunState(RunState.Succeeded);
        return WorkflowExecutionResult.Succeeded(runCursor);
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

    private static int ResolveStartStepIndex(WorkflowRunCursor runCursor)
    {
        if (runCursor.ChainMode == ChainMode.CognitiveChain && runCursor.CurrentStepIndex > 0)
        {
            // Cognitive chains always restart from the first planning step on resume.
            return 0;
        }

        return runCursor.CurrentStepIndex;
    }
}
