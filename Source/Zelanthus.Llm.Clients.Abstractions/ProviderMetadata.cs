namespace Zelanthus.Llm.Clients.Abstractions;

public sealed record ProviderMetadata(
    string ProviderKey,
    string ModelId,
    IReadOnlyDictionary<string, string>? Metadata = null);
