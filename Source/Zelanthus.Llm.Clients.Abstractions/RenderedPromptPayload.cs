namespace Zelanthus.Llm.Clients.Abstractions;

public sealed record RenderedPromptPayload(
    string PromptId,
    int PromptVersion,
    string RenderedText,
    string Checksum,
    IReadOnlyList<string> RequiredPlaceholders);
