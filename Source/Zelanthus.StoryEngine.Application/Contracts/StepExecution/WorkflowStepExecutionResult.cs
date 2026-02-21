namespace Zelanthus.StoryEngine.Application.Contracts.StepExecution;

public sealed class WorkflowStepExecutionResult
{
    private WorkflowStepExecutionResult(
        bool isSuccess,
        bool isRetryableFailure,
        string? reasonCode,
        string? latestThinkingPersistenceKey,
        IReadOnlyList<WorkflowStepDefinition>? appendedSteps)
    {
        IsSuccess = isSuccess;
        IsRetryableFailure = isRetryableFailure;
        ReasonCode = reasonCode;
        LatestThinkingPersistenceKey = latestThinkingPersistenceKey;
        AppendedSteps = appendedSteps ?? Array.Empty<WorkflowStepDefinition>();
    }

    public bool IsSuccess { get; }

    public bool IsRetryableFailure { get; }

    public string? ReasonCode { get; }

    public string? LatestThinkingPersistenceKey { get; }

    public IReadOnlyList<WorkflowStepDefinition> AppendedSteps { get; }

    public static WorkflowStepExecutionResult Succeeded(
        string? latestThinkingPersistenceKey = null,
        IReadOnlyList<WorkflowStepDefinition>? appendedSteps = null)
    {
        return new WorkflowStepExecutionResult(
            true,
            false,
            null,
            latestThinkingPersistenceKey,
            appendedSteps);
    }

    public static WorkflowStepExecutionResult RetryableFailure(string reasonCode)
    {
        return new WorkflowStepExecutionResult(
            false,
            true,
            reasonCode,
            null,
            null);
    }

    public static WorkflowStepExecutionResult TerminalFailure(string reasonCode)
    {
        return new WorkflowStepExecutionResult(
            false,
            false,
            reasonCode,
            null,
            null);
    }
}
