using Zelanthus.StoryEngine.Domain;

namespace Zelanthus.StoryEngine.Application;

public sealed record WorkflowExecutionRequest(
    WorkflowDefinition WorkflowDefinition,
    WorkflowRunCursor WorkflowRunCursor);
