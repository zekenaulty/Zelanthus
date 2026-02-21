namespace Zelanthus.Llm.Clients.Abstractions;

public sealed record LlmFailure(
    string ReasonCode,
    string Message,
    ProviderMetadata ProviderMetadata,
    string? RawSnapshotRef = null,
    IReadOnlyDictionary<string, string>? Diagnostics = null);
