namespace Zelanthus.StoryEngine.Application.Contracts;

public interface IWorkflowStepExecutor
{
    Task<WorkflowStepExecutionResult> ExecuteAsync(
        WorkflowStepExecutionContext executionContext,
        CancellationToken cancellationToken = default);
}
