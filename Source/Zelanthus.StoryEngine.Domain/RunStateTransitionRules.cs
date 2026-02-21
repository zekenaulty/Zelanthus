namespace Zelanthus.StoryEngine.Domain;

public static class RunStateTransitionRules
{
    private static readonly IReadOnlyDictionary<RunState, IReadOnlySet<RunState>> AllowedTransitions =
        new Dictionary<RunState, IReadOnlySet<RunState>>
        {
            [RunState.Created] = new HashSet<RunState> { RunState.Running, RunState.Cancelled },
            [RunState.Running] = new HashSet<RunState>
            {
                RunState.WaitingRetry,
                RunState.Succeeded,
                RunState.FailedTerminal,
                RunState.Cancelled,
            },
            [RunState.WaitingRetry] = new HashSet<RunState>
            {
                RunState.Running,
                RunState.FailedTerminal,
                RunState.Cancelled,
            },
            [RunState.Succeeded] = new HashSet<RunState>(),
            [RunState.FailedTerminal] = new HashSet<RunState>(),
            [RunState.Cancelled] = new HashSet<RunState>(),
        };

    public static bool IsValidTransition(RunState current, RunState next)
    {
        return AllowedTransitions.TryGetValue(current, out var allowedNextStates) &&
            allowedNextStates.Contains(next);
    }

    public static void EnsureValidTransition(RunState current, RunState next)
    {
        if (!IsValidTransition(current, next))
        {
            throw new InvalidOperationException(
                $"invalid_state_transition: cannot transition run state from '{current}' to '{next}'.");
        }
    }
}
