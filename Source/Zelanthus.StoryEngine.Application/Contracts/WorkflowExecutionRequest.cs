namespace Zelanthus.StoryEngine.Application.Contracts;

public sealed record WorkflowExecutionRequest(
    WorkflowDefinition WorkflowDefinition,
    WorkflowRunCursor WorkflowRunCursor);
