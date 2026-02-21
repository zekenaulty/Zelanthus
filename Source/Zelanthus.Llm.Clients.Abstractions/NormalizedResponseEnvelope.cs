namespace Zelanthus.Llm.Clients.Abstractions;

public sealed record NormalizedResponseEnvelope(
    NormalizedResponse NormalizedResponse,
    TokenAccounting TokenAccounting,
    ProviderMetadata ProviderMetadata,
    string? RawSnapshotRef,
    string? ContinuityHandle);
