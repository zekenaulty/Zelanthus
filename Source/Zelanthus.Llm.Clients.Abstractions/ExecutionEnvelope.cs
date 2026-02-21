namespace Zelanthus.Llm.Clients.Abstractions;

public sealed record ExecutionEnvelope(
    RenderedPromptPayload RenderedPrompt,
    ExecutionContext ExecutionContext);
