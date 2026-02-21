namespace Zelanthus.Llm.Clients.Abstractions.Contracts;

public sealed record ExecutionEnvelope(
    RenderedPromptPayload RenderedPrompt,
    ExecutionContext ExecutionContext);
