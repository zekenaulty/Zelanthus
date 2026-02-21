namespace Zelanthus.Llm.Clients.Abstractions.Models;

public sealed record ProviderMetadata(
    string ProviderKey,
    string ModelId,
    IReadOnlyDictionary<string, string>? Metadata = null);
