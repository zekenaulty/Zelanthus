using System.Text.RegularExpressions;

namespace Zelanthus.StoryEngine.Infrastructure;

public sealed partial class WorkflowRunPaths
{
    private readonly string _workspaceRoot;

    public WorkflowRunPaths(string workspaceRoot)
    {
        if (string.IsNullOrWhiteSpace(workspaceRoot))
        {
            throw new ArgumentException("Workspace root is required.", nameof(workspaceRoot));
        }

        _workspaceRoot = workspaceRoot;
    }

    public string GetRunRoot(Guid runId)
    {
        return Path.Combine(_workspaceRoot, "artifacts", "workflow-runs", runId.ToString("D"));
    }

    public string GetRunRecordPath(Guid runId)
    {
        return Path.Combine(GetRunRoot(runId), "run.json");
    }

    public string GetCheckpointDirectory(Guid runId)
    {
        return Path.Combine(GetRunRoot(runId), "checkpoints");
    }

    public string GetCheckpointPath(Guid runId, int checkpointSequence)
    {
        if (checkpointSequence < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(checkpointSequence));
        }

        return Path.Combine(GetCheckpointDirectory(runId), $"checkpoint-{checkpointSequence}.json");
    }

    public string GetTurnDirectory(Guid runId, int turnIndex, string stepKey)
    {
        if (turnIndex < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(turnIndex));
        }

        ValidateStepKey(stepKey);
        return Path.Combine(GetRunRoot(runId), "turns", $"{turnIndex}-{stepKey}");
    }

    public string GetFailureDirectory(Guid runId)
    {
        return Path.Combine(GetRunRoot(runId), "failures");
    }

    public string GetFailurePath(Guid runId, int turnIndex)
    {
        if (turnIndex < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(turnIndex));
        }

        return Path.Combine(GetFailureDirectory(runId), $"failure-{turnIndex}.json");
    }

    private static void ValidateStepKey(string stepKey)
    {
        if (string.IsNullOrWhiteSpace(stepKey))
        {
            throw new ArgumentException("Step key is required.", nameof(stepKey));
        }

        if (!StepKeyPattern().IsMatch(stepKey))
        {
            throw new ArgumentException("Step key must match ^[a-z0-9][a-z0-9-]{0,63}$.", nameof(stepKey));
        }
    }

    [GeneratedRegex("^[a-z0-9][a-z0-9-]{0,63}$", RegexOptions.CultureInvariant)]
    private static partial Regex StepKeyPattern();
}
