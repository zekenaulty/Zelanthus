namespace Zelanthus.StoryEngine.Domain;

public sealed record PromptReference
{
    public PromptReference(string promptId, int promptVersion)
    {
        if (string.IsNullOrWhiteSpace(promptId))
        {
            throw new ArgumentException("Prompt identifier is required.", nameof(promptId));
        }

        if (promptVersion <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(promptVersion), "PromptVersion must be a positive integer.");
        }

        PromptId = promptId;
        PromptVersion = promptVersion;
    }

    public string PromptId { get; }

    public int PromptVersion { get; }
}
