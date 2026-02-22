namespace Zelanthus.StoryEngine.Application.Contracts.RunExecution;

public sealed record WorkflowExecutionRequest(
    WorkflowDefinition WorkflowDefinition,
    WorkflowRunCursor WorkflowRunCursor,
    IReadOnlyList<WorkflowStepDefinition>? EffectiveWorkflowSteps = null);
