namespace Zelanthus.Prompting.Contracts;

public sealed record RenderedPrompt(
    string PromptId,
    int PromptVersion,
    string RenderedText,
    string Checksum,
    IReadOnlyList<string> RequiredPlaceholders);
