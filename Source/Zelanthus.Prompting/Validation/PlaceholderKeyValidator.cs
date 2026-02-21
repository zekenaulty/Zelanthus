using System.Text.RegularExpressions;

namespace Zelanthus.Prompting.Validation;

internal static partial class PlaceholderKeyValidator
{
    private const string PlaceholderKeyPatternExpression = "^[a-z][a-z0-9_]*$";

    [GeneratedRegex(PlaceholderKeyPatternExpression, RegexOptions.CultureInvariant)]
    private static partial Regex PlaceholderKeyPattern();

    public static string Validate(string placeholderKey)
    {
        if (placeholderKey is null)
        {
            throw new ArgumentNullException(nameof(placeholderKey));
        }

        if (placeholderKey.Length == 0)
        {
            throw new ArgumentException("Placeholder key cannot be empty.", nameof(placeholderKey));
        }

        if (!string.Equals(placeholderKey, placeholderKey.Trim(), StringComparison.Ordinal))
        {
            throw new ArgumentException("Placeholder key cannot contain leading or trailing whitespace.", nameof(placeholderKey));
        }

        if (!PlaceholderKeyPattern().IsMatch(placeholderKey))
        {
            throw new ArgumentException(
                $"Placeholder key must match {PlaceholderKeyPatternExpression}.",
                nameof(placeholderKey));
        }

        return placeholderKey;
    }
}
