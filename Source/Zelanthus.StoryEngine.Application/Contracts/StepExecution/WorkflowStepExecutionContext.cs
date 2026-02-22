namespace Zelanthus.StoryEngine.Application.Contracts.StepExecution;

public sealed record WorkflowStepExecutionContext(
    WorkflowDefinition WorkflowDefinition,
    WorkflowStepDefinition WorkflowStepDefinition,
    int StepIndex,
    WorkflowRunCursor WorkflowRunCursor);
