namespace Zelanthus.Llm.Clients.Abstractions;

public sealed record NormalizedResponse(
    string ContentText,
    IReadOnlyDictionary<string, string>? StructuredFields = null);
