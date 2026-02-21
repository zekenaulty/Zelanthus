using System.Globalization;
using System.Security.Cryptography;
using System.Text;

namespace Zelanthus.Prompting;

public sealed class PromptRenderer : IPromptRenderer
{
    public PromptRenderResult Render(
        PromptTemplateDefinition templateDefinition,
        IReadOnlyDictionary<string, string?> placeholderValues)
    {
        ArgumentNullException.ThrowIfNull(templateDefinition);
        ArgumentNullException.ThrowIfNull(placeholderValues);

        ValidatePlaceholderValueKeys(placeholderValues);

        var missingPlaceholders = templateDefinition.RequiredPlaceholders
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

        var renderedText = templateDefinition.TemplateText;
        foreach (var requiredPlaceholder in templateDefinition.RequiredPlaceholders)
        {
            var placeholderToken = "{{" + requiredPlaceholder + "}}";
            renderedText = renderedText.Replace(
                placeholderToken,
                placeholderValues[requiredPlaceholder]!,
                StringComparison.Ordinal);
        }

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

    private static void ValidatePlaceholderValueKeys(IReadOnlyDictionary<string, string?> placeholderValues)
    {
        foreach (var placeholderKey in placeholderValues.Keys)
        {
            PlaceholderKeyValidator.Validate(placeholderKey);
        }
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
