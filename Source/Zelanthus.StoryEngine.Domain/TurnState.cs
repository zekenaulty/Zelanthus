namespace Zelanthus.StoryEngine.Domain;

public enum TurnState
{
    Pending = 1,
    Running = 2,
    Succeeded = 3,
    FailedRetryable = 4,
    FailedTerminal = 5,
    Skipped = 6,
}
