# Step: 0050-define-infrastructure-mappers-and-store-contracts

## Goal
- Define infrastructure mapper/store contract boundaries for writing and materializing runner artifacts/checkpoints.

## Context
- Infrastructure owns storage transforms, not semantic chain decisions.
- Deterministic mapping guarantees are required to keep runner resume behavior auditable.

## Git Branch
- `feature/thin-clients-first-solution-structure`

## Commits
- `none`

## Commands Executed
- `dotnet build "Zelanthus.slnx"`
- `dotnet test "Tests/Zelanthus.Architecture.Tests/Zelanthus.Architecture.Tests.csproj"`

## Files Changed
- `Source/Zelanthus.StoryEngine.Infrastructure/LocalPersistenceReasonCodes.cs`
- `Source/Zelanthus.StoryEngine.Infrastructure/WorkflowRunStoreRecords.cs`
- `Source/Zelanthus.StoryEngine.Infrastructure/IWorkflowRunStore.cs`
- `Source/Zelanthus.StoryEngine.Infrastructure/LocalFileWorkflowRunStore.cs`
- `Source/Zelanthus.StoryEngine.Infrastructure/Class1.cs` (deleted)

## Outputs
- `Plans/InProgress/mvp-chain-runner-local-persistence-baseline/decisions/0002-local-persistence-contract-and-layout-v0.md`
  - mapping guarantees and failure semantics are explicit.
- `Plans/InProgress/mvp-chain-runner-local-persistence-baseline/steps/0050-define-infrastructure-mappers-and-store-contracts/artifacts/provenance-mapping-contract.md`
  - strict-superset provenance mapping requirements and deterministic failure behavior.
- `Plans/InProgress/mvp-chain-runner-local-persistence-baseline/plan.md`
  - local persistence contract and mapping expectations are explicit.

## Tests / Results
- `dotnet build "Zelanthus.slnx"` -> `passed` (0 warnings, 0 errors)
- `dotnet test "Tests/Zelanthus.Architecture.Tests/Zelanthus.Architecture.Tests.csproj"` -> `passed` (7 tests)

## Acceptance Evidence
- Store interfaces are explicit for:
  - run records,
  - checkpoint records,
  - turn artifact records,
  - failure records.
- Mapping contract includes round-trip preservation for required fields.
- Provenance artifact mapping is explicit as strict superset -> Prompting provenance required fields (deterministic and lossless for required fields).
- Provenance mapping contract preserves canonical turn identity from `turn.json` (no conflicting identity fields).
- Missing required stored fields are defined as explicit mapping failures (never silently defaulted).

## Issues
- none

## Decision
- accepted: infrastructure store and mapper contracts are implemented with deterministic serialization and atomic temp-write replace behavior.

## Completion
- `completed`

## Next Actions
- Execute step `0060-define-retry-resume-and-reason-code-policy`.



