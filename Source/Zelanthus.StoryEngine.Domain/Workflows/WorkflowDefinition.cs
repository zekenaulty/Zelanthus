namespace Zelanthus.StoryEngine.Domain.Workflows;

public sealed class WorkflowDefinition
{
    public WorkflowDefinition(
        string workflowKey,
        WorkflowKind workflowKind,
        int workflowVersion,
        IEnumerable<WorkflowStepDefinition> steps)
    {
        if (string.IsNullOrWhiteSpace(workflowKey))
        {
            throw new ArgumentException("Workflow key is required.", nameof(workflowKey));
        }

        if (workflowVersion <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(workflowVersion), "WorkflowVersion must be a positive integer.");
        }

        if (steps is null)
        {
            throw new ArgumentNullException(nameof(steps));
        }

        var stepList = steps.ToArray();
        if (stepList.Length == 0)
        {
            throw new ArgumentException("Workflow must include at least one step.", nameof(steps));
        }

        var duplicateStepKeys = stepList
            .GroupBy(step => step.StepKey, StringComparer.Ordinal)
            .Where(group => group.Count() > 1)
            .Select(group => group.Key)
            .ToArray();

        if (duplicateStepKeys.Length > 0)
        {
            throw new ArgumentException(
                $"Workflow contains duplicate step keys: {string.Join(", ", duplicateStepKeys)}",
                nameof(steps));
        }

        WorkflowKey = workflowKey;
        WorkflowKind = workflowKind;
        WorkflowVersion = workflowVersion;
        Steps = stepList;
    }

    public string WorkflowKey { get; }

    public WorkflowKind WorkflowKind { get; }

    public int WorkflowVersion { get; }

    public IReadOnlyList<WorkflowStepDefinition> Steps { get; }
}
