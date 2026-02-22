namespace Zelanthus.StoryEngine.Application.Contracts.StepExecution;

public interface IWorkflowStepExecutor
{
    Task<WorkflowStepExecutionResult> ExecuteAsync(
        WorkflowStepExecutionContext executionContext,
        CancellationToken cancellationToken = default);
}
