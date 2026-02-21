using Zelanthus.StoryEngine.Domain;

namespace Zelanthus.StoryEngine.Application;

public sealed class WorkflowExecutionResult
{
    private WorkflowExecutionResult(
        bool isSuccess,
        bool isRetryableFailure,
        string? reasonCode,
        WorkflowRunCursor workflowRunCursor)
    {
        IsSuccess = isSuccess;
        IsRetryableFailure = isRetryableFailure;
        ReasonCode = reasonCode;
        WorkflowRunCursor = workflowRunCursor;
    }

    public bool IsSuccess { get; }

    public bool IsRetryableFailure { get; }

    public string? ReasonCode { get; }

    public WorkflowRunCursor WorkflowRunCursor { get; }

    public static WorkflowExecutionResult Succeeded(WorkflowRunCursor workflowRunCursor)
    {
        return new WorkflowExecutionResult(true, false, null, workflowRunCursor);
    }

    public static WorkflowExecutionResult RetryableFailure(string reasonCode, WorkflowRunCursor workflowRunCursor)
    {
        return new WorkflowExecutionResult(false, true, reasonCode, workflowRunCursor);
    }

    public static WorkflowExecutionResult TerminalFailure(string reasonCode, WorkflowRunCursor workflowRunCursor)
    {
        return new WorkflowExecutionResult(false, false, reasonCode, workflowRunCursor);
    }
}
