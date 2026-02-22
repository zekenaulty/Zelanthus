# Step: 0070-plan-project-reference-graph-and-architecture-tests

## Goal
- Define project reference updates and architecture-test assertions that enforce Plan 2 dependency boundaries.

## Context
- Plan 2 introduces StoryEngine projects and cross-project references that can drift without an explicit gate.

## Git Branch
- `feature/thin-clients-first-solution-structure`

## Commits
- `952c43f705d35adf53fe53bcd7f6c645cfb6f367`

## Commands Executed
- `dotnet test "Tests/Zelanthus.Architecture.Tests/Zelanthus.Architecture.Tests.csproj"`
- `dotnet test "Zelanthus.slnx" --filter "Category!=LiveGemini"`

## Files Changed
- `Tests/Zelanthus.Architecture.Tests/DependencyDirectionTests.cs`
- `Tests/Zelanthus.Architecture.Tests/Zelanthus.Architecture.Tests.csproj`

## Outputs
- `Plans/InProgress/mvp-chain-runner-local-persistence-baseline/decisions/0003-project-reference-graph-and-boundary-enforcement-v0.md`
  - allowed/forbidden references and enforcement policy are explicit.
- `Plans/InProgress/mvp-chain-runner-local-persistence-baseline/steps/0070-plan-project-reference-graph-and-architecture-tests/artifacts/architecture-test-assertion-matrix.md`
  - required architecture assertions for Domain/Application/Infrastructure/API/provider boundaries.
- `Plans/InProgress/mvp-chain-runner-local-persistence-baseline/artifacts/project-dependency-implementation-matrix.md`
  - implementation boundary matrix is aligned with architecture assertions.

## Tests / Results
- `dotnet test "Tests/Zelanthus.Architecture.Tests/Zelanthus.Architecture.Tests.csproj"` -> `passed` (7 tests)
- `dotnet test "Zelanthus.slnx" --filter "Category!=LiveGemini"` -> `passed` (14 tests)

## Acceptance Evidence
- Architecture assertion matrix covers:
  - domain has no infra/provider/API dependencies,
  - application has no provider implementation dependency,
  - prompting has no story engine/API dependencies,
  - provider implementation has no story engine domain/application dependencies.
- Step output maps each assertion to planned test project coverage.

## Issues
- none

## Decision
- accepted: architecture gate now enforces Plan 2 dependency-direction boundaries for `StoryEngine.Domain`, `StoryEngine.Application`, and `StoryEngine.Infrastructure`.

## Completion
- `completed`

## Next Actions
- Execute step `0060-define-retry-resume-and-reason-code-policy` and then step `0080-define-runner-proof-test-suite-and-evidence-artifacts`.



