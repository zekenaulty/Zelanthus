namespace Zelanthus.StoryEngine.Domain.Runs;

public enum RunState
{
    Created = 1,
    Running = 2,
    WaitingRetry = 3,
    Succeeded = 4,
    FailedTerminal = 5,
    Cancelled = 6,
}
