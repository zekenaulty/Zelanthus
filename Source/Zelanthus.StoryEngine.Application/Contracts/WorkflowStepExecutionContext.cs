namespace Zelanthus.StoryEngine.Application.Contracts;

public sealed record WorkflowStepExecutionContext(
    WorkflowDefinition WorkflowDefinition,
    WorkflowStepDefinition WorkflowStepDefinition,
    int StepIndex,
    WorkflowRunCursor WorkflowRunCursor);
