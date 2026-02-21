namespace Zelanthus.Llm.Clients.Abstractions.Models;

public sealed record RenderedPromptPayload(
    string PromptId,
    int PromptVersion,
    string RenderedText,
    string Checksum,
    IReadOnlyList<string> RequiredPlaceholders);
