namespace Zelanthus.StoryEngine.Application.Contracts.RunExecution;

public interface IWorkflowRunner
{
    Task<WorkflowExecutionResult> RunAsync(
        WorkflowExecutionRequest executionRequest,
        CancellationToken cancellationToken = default);
}
