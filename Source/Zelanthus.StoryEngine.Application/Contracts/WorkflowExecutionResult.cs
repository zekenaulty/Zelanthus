namespace Zelanthus.StoryEngine.Application.Contracts;

public sealed class WorkflowExecutionResult
{
    private WorkflowExecutionResult(
        bool isSuccess,
        bool isRetryableFailure,
        string? reasonCode,
        string? policyReasonCode,
        IReadOnlyList<string> effectiveStepKeys,
        WorkflowRunCursor workflowRunCursor)
    {
        IsSuccess = isSuccess;
        IsRetryableFailure = isRetryableFailure;
        ReasonCode = reasonCode;
        PolicyReasonCode = policyReasonCode;
        EffectiveStepKeys = effectiveStepKeys;
        WorkflowRunCursor = workflowRunCursor;
    }

    public bool IsSuccess { get; }

    public bool IsRetryableFailure { get; }

    public string? ReasonCode { get; }

    public string? PolicyReasonCode { get; }

    public IReadOnlyList<string> EffectiveStepKeys { get; }

    public WorkflowRunCursor WorkflowRunCursor { get; }

    public static WorkflowExecutionResult Succeeded(
        WorkflowRunCursor workflowRunCursor,
        IReadOnlyList<string> effectiveStepKeys,
        string? policyReasonCode = null)
    {
        return new WorkflowExecutionResult(
            true,
            false,
            null,
            policyReasonCode,
            effectiveStepKeys,
            workflowRunCursor);
    }

    public static WorkflowExecutionResult RetryableFailure(
        string reasonCode,
        WorkflowRunCursor workflowRunCursor,
        IReadOnlyList<string> effectiveStepKeys,
        string? policyReasonCode = null)
    {
        return new WorkflowExecutionResult(
            false,
            true,
            reasonCode,
            policyReasonCode,
            effectiveStepKeys,
            workflowRunCursor);
    }

    public static WorkflowExecutionResult TerminalFailure(
        string reasonCode,
        WorkflowRunCursor workflowRunCursor,
        IReadOnlyList<string> effectiveStepKeys,
        string? policyReasonCode = null)
    {
        return new WorkflowExecutionResult(
            false,
            false,
            reasonCode,
            policyReasonCode,
            effectiveStepKeys,
            workflowRunCursor);
    }
}
