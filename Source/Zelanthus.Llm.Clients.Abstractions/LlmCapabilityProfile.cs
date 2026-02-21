namespace Zelanthus.Llm.Clients.Abstractions;

public sealed record LlmCapabilityProfile(
    bool SupportsContinuityHandle,
    bool SupportsThinking,
    bool SupportsStructuredOutput,
    bool SupportsJsonMode);
