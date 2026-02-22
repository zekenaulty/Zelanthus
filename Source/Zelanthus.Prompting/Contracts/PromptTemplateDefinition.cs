namespace Zelanthus.Prompting.Contracts;

public sealed class PromptTemplateDefinition
{
    public PromptTemplateDefinition(
        string promptId,
        int promptVersion,
        string templateText,
        IEnumerable<string> requiredPlaceholders)
    {
        if (string.IsNullOrWhiteSpace(promptId))
        {
            throw new ArgumentException("PromptId is required.", nameof(promptId));
        }

        if (promptVersion <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(promptVersion), "PromptVersion must be a positive integer.");
        }

        if (templateText is null)
        {
            throw new ArgumentNullException(nameof(templateText));
        }

        if (requiredPlaceholders is null)
        {
            throw new ArgumentNullException(nameof(requiredPlaceholders));
        }

        PromptId = promptId;
        PromptVersion = promptVersion;
        TemplateText = templateText;
        RequiredPlaceholders = requiredPlaceholders
            .Select(PlaceholderKeyValidator.Validate)
            .Distinct(StringComparer.Ordinal)
            .ToArray();
    }

    public string PromptId { get; }

    public int PromptVersion { get; }

    public string TemplateText { get; }

    public IReadOnlyList<string> RequiredPlaceholders { get; }
}
