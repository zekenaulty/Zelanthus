namespace Zelanthus.StoryEngine.Application.Orchestration;

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

        if (!IsWorkflowIdentityAligned(
            workflowDefinition.WorkflowKey,
            workflowDefinition.WorkflowVersion,
            runCursor.WorkflowKey,
            runCursor.WorkflowVersion))
        {
            MarkTerminalFailureIfRunnable(runCursor);
            return WorkflowExecutionResult.TerminalFailure(
                RunnerReasonCodes.InvalidStateTransition,
                runCursor,
                workflowDefinition.Steps.Select(step => step.StepKey).ToArray());
        }

        if (!IsWorkflowKindAligned(workflowDefinition.WorkflowKind, runCursor.WorkflowKind))
        {
            MarkTerminalFailureIfRunnable(runCursor);
            return WorkflowExecutionResult.TerminalFailure(
                RunnerReasonCodes.InvalidStateTransition,
                runCursor,
                workflowDefinition.Steps.Select(step => step.StepKey).ToArray());
        }

        if (RequiresConversationalRehydration(runCursor) && executionRequest.EffectiveWorkflowSteps is null)
        {
            MarkTerminalFailureIfRunnable(runCursor);
            return WorkflowExecutionResult.TerminalFailure(
                RunnerReasonCodes.InvalidStateTransition,
                runCursor,
                workflowDefinition.Steps.Select(step => step.StepKey).ToArray());
        }

        var effectiveSteps = (executionRequest.EffectiveWorkflowSteps ?? workflowDefinition.Steps).ToList();
        var resumeStart = ResolveStartStepIndex(runCursor);
        var stepIndex = resumeStart.StartStepIndex;
        policyReasonCode = resumeStart.PolicyReasonCode;

        if (stepIndex >= effectiveSteps.Count)
        {
            MarkTerminalFailureIfRunnable(runCursor);
            return WorkflowExecutionResult.TerminalFailure(
                RunnerReasonCodes.InvalidStateTransition,
                runCursor,
                effectiveSteps.Select(currentStep => currentStep.StepKey).ToArray(),
                policyReasonCode);
        }

        if (!RunStateTransitionRules.IsValidTransition(runCursor.RunState, RunState.Running))
        {
            MarkTerminalFailureIfRunnable(runCursor);
            return WorkflowExecutionResult.TerminalFailure(
                RunnerReasonCodes.InvalidStateTransition,
                runCursor,
                effectiveSteps.Select(currentStep => currentStep.StepKey).ToArray(),
                policyReasonCode);
        }

        runCursor.TransitionRunState(RunState.Running);

        try
        {
            while (stepIndex < effectiveSteps.Count)
            {
                var step = effectiveSteps[stepIndex];
                if (step.PromptReference is null)
                {
                    MarkTerminalFailureIfRunnable(runCursor);
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

                    MarkTerminalFailureIfRunnable(runCursor);
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
        catch (OperationCanceledException)
        {
            if (RunStateTransitionRules.IsValidTransition(runCursor.RunState, RunState.Cancelled))
            {
                runCursor.TransitionRunState(RunState.Cancelled);
            }

            throw;
        }
    }

    private static bool IsWorkflowKindAligned(WorkflowKind workflowKind, WorkflowKind runWorkflowKind)
    {
        return workflowKind == runWorkflowKind;
    }

    private static bool IsWorkflowIdentityAligned(
        string workflowKey,
        int workflowVersion,
        string runWorkflowKey,
        int runWorkflowVersion)
    {
        return string.Equals(workflowKey, runWorkflowKey, StringComparison.Ordinal)
            && workflowVersion == runWorkflowVersion;
    }

    private static bool RequiresConversationalRehydration(WorkflowRunCursor runCursor)
    {
        return runCursor.WorkflowKind == WorkflowKind.ConversationalChain &&
            runCursor.CurrentStepIndex > 0;
    }

    private static void MarkTerminalFailureIfRunnable(WorkflowRunCursor runCursor)
    {
        if (RunStateTransitionRules.IsValidTransition(runCursor.RunState, RunState.FailedTerminal))
        {
            runCursor.MarkTerminalFailure();
        }
    }

    private static ResumeStart ResolveStartStepIndex(WorkflowRunCursor runCursor)
    {
        if (runCursor.WorkflowKind == WorkflowKind.CognitiveChain && runCursor.CurrentStepIndex > 0)
        {
            // Cognitive chains always restart from the first planning step on resume.
            return new ResumeStart(0, RunnerReasonCodes.CognitiveRestartRequired);
        }

        return new ResumeStart(runCursor.CurrentStepIndex);
    }

    private readonly record struct ResumeStart(int StartStepIndex, string? PolicyReasonCode = null);
}
