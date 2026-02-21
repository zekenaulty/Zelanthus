namespace Zelanthus.StoryEngine.Application;

public interface IWorkflowRunner
{
    Task<WorkflowExecutionResult> RunAsync(
        WorkflowExecutionRequest executionRequest,
        CancellationToken cancellationToken = default);
}
