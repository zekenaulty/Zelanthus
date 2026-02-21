namespace Zelanthus.StoryEngine.Infrastructure;

public static class LocalPersistenceReasonCodes
{
    public const string ArtifactWriteFailed = "artifact_write_failed";
    public const string ArtifactReadFailed = "artifact_read_failed";
    public const string CheckpointWriteFailed = "checkpoint_write_failed";
    public const string CheckpointReadFailed = "checkpoint_read_failed";
}
