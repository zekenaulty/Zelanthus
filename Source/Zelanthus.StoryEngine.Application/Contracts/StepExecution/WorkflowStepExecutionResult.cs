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
        var validatedReasonCode = ValidateReasonCode(reasonCode);
        return new WorkflowStepExecutionResult(
            false,
            true,
            validatedReasonCode,
            null,
            null);
    }

    public static WorkflowStepExecutionResult TerminalFailure(string reasonCode)
    {
        var validatedReasonCode = ValidateReasonCode(reasonCode);
        return new WorkflowStepExecutionResult(
            false,
            false,
            validatedReasonCode,
            null,
            null);
    }

    private static string ValidateReasonCode(string reasonCode)
    {
        if (string.IsNullOrWhiteSpace(reasonCode))
        {
            throw new ArgumentException("Reason code is required.", nameof(reasonCode));
        }

        return reasonCode;
    }
}
