namespace Zelanthus.Prompting.Contracts;

public interface IPromptRenderer
{
    PromptRenderResult Render(
        PromptTemplateDefinition templateDefinition,
        IReadOnlyDictionary<string, string?> placeholderValues);
}
