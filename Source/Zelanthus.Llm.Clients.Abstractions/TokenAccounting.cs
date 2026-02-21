namespace Zelanthus.Llm.Clients.Abstractions;

public sealed record TokenAccounting(
    TokenUsageValue PromptTokens,
    TokenUsageValue OutputTokens,
    TokenUsageValue TotalTokens,
    TokenUsageValue ThoughtTokens)
{
    public static TokenAccounting Unknown()
    {
        return new TokenAccounting(
            TokenUsageValue.Unknown(),
            TokenUsageValue.Unknown(),
            TokenUsageValue.Unknown(),
            TokenUsageValue.Unknown());
    }
}
