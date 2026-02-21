namespace Zelanthus.StoryEngine.Application;

public interface IWorkflowStepExecutor
{
    Task<WorkflowStepExecutionResult> ExecuteAsync(
        WorkflowStepExecutionContext executionContext,
        CancellationToken cancellationToken = default);
}
