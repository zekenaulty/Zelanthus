namespace Zelanthus.Llm.Clients.Abstractions.Usage;

public readonly record struct TokenUsageValue
{
    private TokenUsageValue(int? value, bool isUnknown)
    {
        Value = value;
        IsUnknown = isUnknown;
    }

    public int? Value { get; }

    public bool IsUnknown { get; }

    public static TokenUsageValue Known(int value)
    {
        if (value < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(value), "Token usage cannot be negative.");
        }

        return new TokenUsageValue(value, false);
    }

    public static TokenUsageValue Unknown()
    {
        return new TokenUsageValue(null, true);
    }

    public override string ToString()
    {
        return IsUnknown ? "unknown" : Value?.ToString() ?? "unknown";
    }
}
