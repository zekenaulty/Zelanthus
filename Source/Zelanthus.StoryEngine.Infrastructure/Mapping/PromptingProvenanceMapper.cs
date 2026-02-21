namespace Zelanthus.StoryEngine.Infrastructure.Mapping;

public static class PromptingProvenanceMapper
{
    public static PromptProvenanceRecord MapToPrompting(ProvenanceArtifactRecord artifactRecord)
    {
        ArgumentNullException.ThrowIfNull(artifactRecord);

        if (string.IsNullOrWhiteSpace(artifactRecord.PromptId))
        {
            throw new InvalidOperationException($"{LocalPersistenceReasonCodes.ArtifactReadFailed}: PromptId is required.");
        }

        if (artifactRecord.PromptVersion <= 0)
        {
            throw new InvalidOperationException($"{LocalPersistenceReasonCodes.ArtifactReadFailed}: PromptVersion must be a positive integer.");
        }

        if (string.IsNullOrWhiteSpace(artifactRecord.PromptChecksum))
        {
            throw new InvalidOperationException($"{LocalPersistenceReasonCodes.ArtifactReadFailed}: PromptChecksum is required.");
        }

        return new PromptProvenanceRecord(
            artifactRecord.PromptId,
            artifactRecord.PromptVersion,
            artifactRecord.PromptChecksum);
    }
}
