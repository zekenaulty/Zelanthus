namespace Zelanthus.Prompting.Contracts;

public sealed record RenderFailure(
    string PromptId,
    int PromptVersion,
    string ReasonCode,
    IReadOnlyList<string> MissingPlaceholders);
