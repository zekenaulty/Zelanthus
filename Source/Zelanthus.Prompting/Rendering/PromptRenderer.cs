using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace Zelanthus.Prompting.Rendering;

public sealed partial class PromptRenderer : IPromptRenderer
{
    public PromptRenderResult Render(
        PromptTemplateDefinition templateDefinition,
        IReadOnlyDictionary<string, string?> placeholderValues)
    {
        ArgumentNullException.ThrowIfNull(templateDefinition);
        ArgumentNullException.ThrowIfNull(placeholderValues);

        ValidatePlaceholderValueKeys(placeholderValues);

        var templatePlaceholderKeys = ExtractTemplatePlaceholderKeys(templateDefinition.TemplateText);
        var missingPlaceholders = templateDefinition.RequiredPlaceholders
            .Concat(templatePlaceholderKeys)
            .Where(requiredPlaceholder =>
                !placeholderValues.TryGetValue(requiredPlaceholder, out var value) ||
                value is null)
            .Distinct(StringComparer.Ordinal)
            .OrderBy(requiredPlaceholder => requiredPlaceholder, StringComparer.Ordinal)
            .ToArray();

        if (missingPlaceholders.Length > 0)
        {
            return PromptRenderResult.Failure(
                new RenderFailure(
                    templateDefinition.PromptId,
                    templateDefinition.PromptVersion,
                    PromptReasonCodes.MissingRequiredPlaceholder,
                    missingPlaceholders));
        }

        var renderedText = ReplaceTemplatePlaceholders(templateDefinition.TemplateText, placeholderValues);

        var normalizedRenderedText = NormalizeLineEndings(renderedText);
        var checksum = ComputeChecksum(
            templateDefinition.PromptId,
            templateDefinition.PromptVersion,
            normalizedRenderedText);

        return PromptRenderResult.Success(
            new RenderedPrompt(
                templateDefinition.PromptId,
                templateDefinition.PromptVersion,
                normalizedRenderedText,
                checksum,
                templateDefinition.RequiredPlaceholders));
    }

    [GeneratedRegex(@"\{\{(?<placeholder>[^{}]+)\}\}", RegexOptions.CultureInvariant)]
    private static partial Regex PlaceholderTokenPattern();

    private static void ValidatePlaceholderValueKeys(IReadOnlyDictionary<string, string?> placeholderValues)
    {
        foreach (var placeholderKey in placeholderValues.Keys)
        {
            PlaceholderKeyValidator.Validate(placeholderKey);
        }
    }

    private static IReadOnlyList<string> ExtractTemplatePlaceholderKeys(string templateText)
    {
        return PlaceholderTokenPattern().Matches(templateText)
            .Select(match => match.Groups["placeholder"].Value.Trim())
            .Where(placeholder => placeholder.Length > 0)
            .Distinct(StringComparer.Ordinal)
            .OrderBy(placeholder => placeholder, StringComparer.Ordinal)
            .ToArray();
    }

    private static string ReplaceTemplatePlaceholders(
        string templateText,
        IReadOnlyDictionary<string, string?> placeholderValues)
    {
        return PlaceholderTokenPattern().Replace(
            templateText,
            match =>
            {
                var placeholder = match.Groups["placeholder"].Value.Trim();
                if (placeholder.Length == 0)
                {
                    return match.Value;
                }

                return placeholderValues.TryGetValue(placeholder, out var value) && value is not null
                    ? value
                    : match.Value;
            });
    }

    private static string ComputeChecksum(string promptId, int promptVersion, string renderedText)
    {
        var canonicalPayload = string.Create(
            CultureInfo.InvariantCulture,
            $"{promptId}\n{promptVersion}\n{renderedText}");

        var payloadBytes = Encoding.UTF8.GetBytes(canonicalPayload);
        var hashBytes = SHA256.HashData(payloadBytes);
        return Convert.ToHexString(hashBytes).ToLowerInvariant();
    }

    private static string NormalizeLineEndings(string value)
    {
        return value.Replace("\r\n", "\n", StringComparison.Ordinal)
            .Replace('\r', '\n');
    }
}
