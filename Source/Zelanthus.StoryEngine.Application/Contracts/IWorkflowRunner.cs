namespace Zelanthus.StoryEngine.Application.Contracts;

public interface IWorkflowRunner
{
    Task<WorkflowExecutionResult> RunAsync(
        WorkflowExecutionRequest executionRequest,
        CancellationToken cancellationToken = default);
}
