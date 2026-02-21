namespace Zelanthus.StoryEngine.Infrastructure.Storage;

public sealed record WorkflowRunRecord(
    Guid RunId,
    string WorkflowKey,
    int WorkflowVersion,
    string WorkflowKind,
    string RunState,
    int CurrentStepIndex,
    int LastSuccessStepIndex,
    int NextTurnIndex,
    int NextCheckpointSequence,
    string? LatestThinkingPersistenceKey,
    IReadOnlyList<string>? EffectiveStepKeys,
    DateTimeOffset UpdatedUtc);

public sealed record WorkflowTurnRecord(
    int TurnIndex,
    string StepKey,
    int StepIndex,
    string Objective,
    DateTimeOffset CreatedUtc,
    DateTimeOffset UpdatedUtc);

public sealed record WorkflowFailureRecord(
    int TurnIndex,
    string ReasonCode,
    IReadOnlyDictionary<string, string>? Diagnostics,
    DateTimeOffset CreatedUtc);
