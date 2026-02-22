namespace Zelanthus.Llm.Clients.Abstractions.Models;

public sealed record LlmCapabilityProfile(
    bool SupportsContinuityHandle,
    bool SupportsThinking,
    bool SupportsStructuredOutput,
    bool SupportsJsonMode);
