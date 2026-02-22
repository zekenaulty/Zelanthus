namespace Zelanthus.StoryEngine.Domain.Runs;

public sealed class WorkflowRunCursor
{
    public WorkflowRunCursor(
        Guid runId,
        string workflowKey,
        int workflowVersion,
        WorkflowKind workflowKind,
        RunState runState,
        int currentStepIndex,
        int lastSuccessStepIndex,
        int nextTurnIndex,
        int nextCheckpointSequence,
        string? latestThinkingPersistenceKey = null)
    {
        if (string.IsNullOrWhiteSpace(workflowKey))
        {
            throw new ArgumentException("Workflow key is required.", nameof(workflowKey));
        }

        if (workflowVersion <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(workflowVersion), "WorkflowVersion must be a positive integer.");
        }

        if (currentStepIndex < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(currentStepIndex));
        }

        if (lastSuccessStepIndex < -1)
        {
            throw new ArgumentOutOfRangeException(nameof(lastSuccessStepIndex));
        }

        if (nextTurnIndex < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(nextTurnIndex));
        }

        if (nextCheckpointSequence < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(nextCheckpointSequence));
        }

        RunId = runId;
        WorkflowKey = workflowKey;
        WorkflowVersion = workflowVersion;
        WorkflowKind = workflowKind;
        RunState = runState;
        CurrentStepIndex = currentStepIndex;
        LastSuccessStepIndex = lastSuccessStepIndex;
        NextTurnIndex = nextTurnIndex;
        NextCheckpointSequence = nextCheckpointSequence;
        LatestThinkingPersistenceKey = latestThinkingPersistenceKey;
    }

    public Guid RunId { get; }

    public string WorkflowKey { get; }

    public int WorkflowVersion { get; }

    public WorkflowKind WorkflowKind { get; }

    public RunState RunState { get; private set; }

    public int CurrentStepIndex { get; private set; }

    public int LastSuccessStepIndex { get; private set; }

    public int NextTurnIndex { get; private set; }

    public int NextCheckpointSequence { get; private set; }

    public string? LatestThinkingPersistenceKey { get; private set; }

    public int ReserveTurnIndex()
    {
        var reservedTurnIndex = NextTurnIndex;
        NextTurnIndex++;
        return reservedTurnIndex;
    }

    public int ReserveCheckpointSequence()
    {
        var reservedCheckpointSequence = NextCheckpointSequence;
        NextCheckpointSequence++;
        return reservedCheckpointSequence;
    }

    public void TransitionRunState(RunState nextRunState)
    {
        RunStateTransitionRules.EnsureValidTransition(RunState, nextRunState);
        RunState = nextRunState;
    }

    public void MarkStepSucceeded(int stepIndex, string? latestThinkingPersistenceKey = null)
    {
        if (stepIndex < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(stepIndex));
        }

        LastSuccessStepIndex = stepIndex;
        CurrentStepIndex = stepIndex + 1;
        LatestThinkingPersistenceKey = latestThinkingPersistenceKey;
    }

    public void MarkStepRetryableFailure()
    {
        TransitionRunState(RunState.WaitingRetry);
    }

    public void MarkTerminalFailure()
    {
        TransitionRunState(RunState.FailedTerminal);
    }
}
