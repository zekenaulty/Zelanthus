using System.Text.RegularExpressions;

namespace Zelanthus.StoryEngine.Domain;

internal static partial class WorkflowKeyValidator
{
    [GeneratedRegex("^[a-z0-9][a-z0-9-]{0,63}$", RegexOptions.CultureInvariant)]
    private static partial Regex StepKeyPattern();

    [GeneratedRegex("^[a-z0-9][a-z0-9-]{0,56}$", RegexOptions.CultureInvariant)]
    private static partial Regex RouteHookKeyPattern();

    public static string ValidateStepKey(string stepKey)
    {
        return Validate(stepKey, StepKeyPattern(), nameof(stepKey), "Step key");
    }

    public static string? ValidateRouteHookKey(string? routeHookKey)
    {
        if (routeHookKey is null)
        {
            return null;
        }

        return Validate(routeHookKey, RouteHookKeyPattern(), nameof(routeHookKey), "Route hook key");
    }

    private static string Validate(string value, Regex pattern, string parameterName, string fieldName)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException($"{fieldName} is required.", parameterName);
        }

        if (!pattern.IsMatch(value))
        {
            throw new ArgumentException(
                $"{fieldName} must match {pattern}.",
                parameterName);
        }

        return value;
    }
}
