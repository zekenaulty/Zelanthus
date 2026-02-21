# Step: 0020-define-chain-runner-domain-and-state-model

## Goal
- Define domain-level contracts for chain run lifecycle, turn state, and invariants needed by both chain modes.

## Context
- Application orchestration and persistence mapping cannot be specified safely until run/turn state contracts are explicit.

## Git Branch
- `feature/thin-clients-first-solution-structure`

## Commits
- `none`

## Commands Executed
- `dotnet build "Zelanthus.slnx"`
- `dotnet test "Tests/Zelanthus.Architecture.Tests/Zelanthus.Architecture.Tests.csproj"`

## Files Changed
- `Source/Zelanthus.StoryEngine.Domain/ChainMode.cs`
- `Source/Zelanthus.StoryEngine.Domain/RunState.cs`
- `Source/Zelanthus.StoryEngine.Domain/TurnState.cs`
- `Source/Zelanthus.StoryEngine.Domain/WorkflowRunCursor.cs`
- `Source/Zelanthus.StoryEngine.Domain/RunStateTransitionRules.cs`
- `Source/Zelanthus.StoryEngine.Domain/Class1.cs` (deleted)

## Outputs
- `Plans/InProgress/mvp-chain-runner-local-persistence-baseline/decisions/0001-chain-runner-execution-model-v0.md`
  - chain mode model, run/turn contract shape, and invariants are explicit.
- `Plans/InProgress/mvp-chain-runner-local-persistence-baseline/steps/0020-define-chain-runner-domain-and-state-model/artifacts/run-turn-state-machine.md`
  - explicit `RunState`/`TurnState` enums, transition table, illegal transition handling, and resume cursor contract.
- `Plans/InProgress/mvp-chain-runner-local-persistence-baseline/plan.md`
  - runner execution contract and invariants are explicit.

## Tests / Results
- `dotnet build "Zelanthus.slnx"` -> `passed` (0 warnings, 0 errors)
- `dotnet test "Tests/Zelanthus.Architecture.Tests/Zelanthus.Architecture.Tests.csproj"` -> `passed` (7 tests)

## Acceptance Evidence
- Domain contract includes explicit run identity, chain mode, turn metadata, and run state transitions.
- State machine table explicitly defines legal run/turn transitions and illegal transition behavior (`invalid_state_transition`).
- Resume cursor contract defines authoritative continuation fields for each chain mode.
- Counter reservation rules are explicit for `next_turn_index`/`next_checkpoint_sequence` with persisted `turn_index`/`checkpoint_sequence` fields.
- Crash/partial-write behavior is explicit: reserved indices are never reused and gaps are allowed.
- Continuity-handle failure semantics are capability-conditioned (supported vs unsupported).
- Runner model explicitly supports variable-length execution for both `CognitiveChain` and `ConversationalChain`.
- Route-hook queue expansion rules include deterministic generated step keys and persisted effective queue state.
- `workflow_kind`/`chain_mode` mismatch behavior is explicit and deterministic (`invalid_state_transition`).
- Contract forbids blind `EXECUTE` execution without required upstream planning artifact state.
- Contract distinguishes cognitive restart-from-start resume behavior vs conversational resume-from-last-success behavior.

## Issues
- none

## Decision
- accepted: domain run/turn state model and transition validation contracts are implemented in `Zelanthus.StoryEngine.Domain`.

## Completion
- `completed`

## Next Actions
- Execute step `0025-define-minimal-workflow-abstraction-hooks`.



