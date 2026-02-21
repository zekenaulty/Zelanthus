namespace Zelanthus.StoryEngine.Infrastructure;

public sealed record ProvenanceArtifactRecord(
    string PromptId,
    int PromptVersion,
    string PromptChecksum,
    string ProviderKey,
    string ModelId,
    string? ReasonCode,
    IReadOnlyDictionary<string, string>? Diagnostics);
