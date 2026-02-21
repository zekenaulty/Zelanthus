using Zelanthus.StoryEngine.Domain;

namespace Zelanthus.StoryEngine.Application;

public sealed record WorkflowStepExecutionContext(
    WorkflowDefinition WorkflowDefinition,
    WorkflowStepDefinition WorkflowStepDefinition,
    int StepIndex,
    WorkflowRunCursor WorkflowRunCursor);
