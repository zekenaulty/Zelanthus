namespace Zelanthus.Llm.Clients.Abstractions.Models;

public sealed record NormalizedResponse(
    string ContentText,
    IReadOnlyDictionary<string, string>? StructuredFields = null);
