namespace Zelanthus.StoryEngine.Infrastructure.Mapping;

public sealed record ProvenanceArtifactRecord(
    string PromptId,
    int PromptVersion,
    string PromptChecksum,
    string ProviderKey,
    string ModelId,
    string? ReasonCode,
    IReadOnlyDictionary<string, string>? Diagnostics);
