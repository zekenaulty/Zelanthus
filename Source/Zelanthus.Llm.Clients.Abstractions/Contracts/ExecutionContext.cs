namespace Zelanthus.Llm.Clients.Abstractions.Contracts;

public sealed record ExecutionContext(
    ChainMode ChainMode,
    int TurnIndex,
    string? WorkflowKey = null,
    string? StepKey = null,
    string? CorrelationId = null,
    string? ContinuityHandle = null,
    IReadOnlyDictionary<string, string>? PolicyFlags = null);
