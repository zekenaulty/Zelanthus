# Step: 0010-lock-plan-2-scope-and-contract-dependencies

## Goal
- Lock Plan 2 scope boundaries, cross-plan dependencies, and project dependency implementation matrix before drafting lower-level contracts.

## Context
- Plan 2 touches multiple project surfaces and can drift quickly without explicit dependency/ownership constraints.
- Plan 1 contracts are prerequisites and must be treated as fixed upstream inputs.

## Git Branch
- `feature/thin-clients-first-solution-structure`

## Commits
- `952c43f705d35adf53fe53bcd7f6c645cfb6f367`

## Commands Executed
- `dotnet new classlib -n Zelanthus.StoryEngine.Domain -f net10.0 -o "Source/Zelanthus.StoryEngine.Domain"`
- `dotnet new classlib -n Zelanthus.StoryEngine.Application -f net10.0 -o "Source/Zelanthus.StoryEngine.Application"`
- `dotnet new classlib -n Zelanthus.StoryEngine.Infrastructure -f net10.0 -o "Source/Zelanthus.StoryEngine.Infrastructure"`
- `dotnet sln "Zelanthus.slnx" add "Source/Zelanthus.StoryEngine.Domain/Zelanthus.StoryEngine.Domain.csproj" "Source/Zelanthus.StoryEngine.Application/Zelanthus.StoryEngine.Application.csproj" "Source/Zelanthus.StoryEngine.Infrastructure/Zelanthus.StoryEngine.Infrastructure.csproj"`
- `dotnet add "Source/Zelanthus.StoryEngine.Application/Zelanthus.StoryEngine.Application.csproj" reference "Source/Zelanthus.StoryEngine.Domain/Zelanthus.StoryEngine.Domain.csproj" "Source/Zelanthus.Prompting/Zelanthus.Prompting.csproj" "Source/Zelanthus.Llm.Clients.Abstractions/Zelanthus.Llm.Clients.Abstractions.csproj"`
- `dotnet add "Source/Zelanthus.StoryEngine.Infrastructure/Zelanthus.StoryEngine.Infrastructure.csproj" reference "Source/Zelanthus.StoryEngine.Domain/Zelanthus.StoryEngine.Domain.csproj" "Source/Zelanthus.StoryEngine.Application/Zelanthus.StoryEngine.Application.csproj" "Source/Zelanthus.Prompting/Zelanthus.Prompting.csproj" "Source/Zelanthus.Llm.Clients.Abstractions/Zelanthus.Llm.Clients.Abstractions.csproj"`
- `dotnet add "Source/Zelanthus.API/Zelanthus.API.csproj" reference "Source/Zelanthus.StoryEngine.Application/Zelanthus.StoryEngine.Application.csproj" "Source/Zelanthus.StoryEngine.Infrastructure/Zelanthus.StoryEngine.Infrastructure.csproj" "Source/Zelanthus.Llm.Clients.Gemini/Zelanthus.Llm.Clients.Gemini.csproj"`
- `dotnet add "Tests/Zelanthus.Architecture.Tests/Zelanthus.Architecture.Tests.csproj" reference "Source/Zelanthus.StoryEngine.Domain/Zelanthus.StoryEngine.Domain.csproj" "Source/Zelanthus.StoryEngine.Application/Zelanthus.StoryEngine.Application.csproj" "Source/Zelanthus.StoryEngine.Infrastructure/Zelanthus.StoryEngine.Infrastructure.csproj"`
- `dotnet build "Zelanthus.slnx"`
- `dotnet test "Tests/Zelanthus.Architecture.Tests/Zelanthus.Architecture.Tests.csproj"`
- `dotnet test "Zelanthus.slnx" --filter "Category!=LiveGemini"`

## Files Changed
- `Zelanthus.slnx`
- `Source/Zelanthus.StoryEngine.Domain/Zelanthus.StoryEngine.Domain.csproj`
- `Source/Zelanthus.StoryEngine.Application/Zelanthus.StoryEngine.Application.csproj`
- `Source/Zelanthus.StoryEngine.Infrastructure/Zelanthus.StoryEngine.Infrastructure.csproj`
- `Source/Zelanthus.API/Zelanthus.API.csproj`
- `Tests/Zelanthus.Architecture.Tests/Zelanthus.Architecture.Tests.csproj`

## Outputs
- `Plans/InProgress/mvp-chain-runner-local-persistence-baseline/plan.md`
  - scope, non-goals, and cross-plan dependencies are explicit.
- `Plans/InProgress/mvp-chain-runner-local-persistence-baseline/artifacts/project-dependency-implementation-matrix.md`
  - allowed/forbidden project references are explicit.
- `Plans/InProgress/mvp-chain-runner-local-persistence-baseline/validation/plan-2-acceptance-evidence-matrix.md`
  - acceptance points and evidence mapping are explicit.

## Tests / Results
- `dotnet build "Zelanthus.slnx"` -> `passed` (0 warnings, 0 errors)
- `dotnet test "Tests/Zelanthus.Architecture.Tests/Zelanthus.Architecture.Tests.csproj"` -> `passed` (7 tests)
- `dotnet test "Zelanthus.slnx" --filter "Category!=LiveGemini"` -> `passed` (14 tests)

## Acceptance Evidence
- Dependency matrix includes all Plan 2 project touchpoints and direction rules.
- Cross-plan dependency list includes Plan 1 and thin-clients/backbone decision inputs.
- Plan 2 non-goals explicitly exclude Postgres/distributed runtime concerns.

## Issues
- none

## Decision
- accepted: Plan 2 dependency boundaries are now enforced in the live solution/project graph.

## Completion
- `completed`

## Next Actions
- Execute step `0020-define-chain-runner-domain-and-state-model`.



