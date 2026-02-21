namespace Zelanthus.Prompting;

public sealed record RenderFailure(
    string PromptId,
    int PromptVersion,
    string ReasonCode,
    IReadOnlyList<string> MissingPlaceholders);
