# Step: 0025-define-minimal-workflow-abstraction-hooks

## Goal
- Define the minimal workflow abstraction required for Plan 2 so runner logic is workflow-aware and future-extensible without over-engineering.

## Context
- Prior attempts showed risk in tackling broad workflow systems too early.
- Plan 2 needs only enough abstraction to support current chain runner correctness plus future route/branch expansion seams.

## Git Branch
- `feature/thin-clients-first-solution-structure`

## Commits
- `952c43f705d35adf53fe53bcd7f6c645cfb6f367`

## Commands Executed
- `dotnet build "Zelanthus.slnx"`
- `dotnet test "Tests/Zelanthus.Architecture.Tests/Zelanthus.Architecture.Tests.csproj"`

## Files Changed
- `Source/Zelanthus.StoryEngine.Domain/WorkflowKeyValidator.cs`
- `Source/Zelanthus.StoryEngine.Domain/PromptReference.cs`
- `Source/Zelanthus.StoryEngine.Domain/StepKind.cs`
- `Source/Zelanthus.StoryEngine.Domain/WorkflowKind.cs`
- `Source/Zelanthus.StoryEngine.Domain/WorkflowStepDefinition.cs`
- `Source/Zelanthus.StoryEngine.Domain/WorkflowDefinition.cs`

## Outputs
- `Plans/InProgress/mvp-chain-runner-local-persistence-baseline/decisions/0005-minimal-workflow-abstraction-v0.md`
  - minimal workflow contracts and route-hook policy are explicit.
- `Plans/InProgress/mvp-chain-runner-local-persistence-baseline/steps/0025-define-minimal-workflow-abstraction-hooks/artifacts/minimal-workflow-abstraction-contract.md`
  - concrete contract sketch and guardrails for implementation.
- `Plans/InProgress/mvp-chain-runner-local-persistence-baseline/plan.md`
  - minimal workflow abstraction section is explicit.

## Tests / Results
- `dotnet build "Zelanthus.slnx"` -> `passed` (0 warnings, 0 errors)
- `dotnet test "Tests/Zelanthus.Architecture.Tests/Zelanthus.Architecture.Tests.csproj"` -> `passed` (7 tests)

## Acceptance Evidence
- Workflow abstraction includes explicit `WorkflowDefinition`, `WorkflowStepDefinition`, and `WorkflowRunContext`.
- Step kinds include `PLAN_STEP`, `EXECUTE`, and `CONVERSATION_STEP`.
- Key semantics are explicit:
  - `workflow_key` stable identity,
  - `workflow_version` integer marker,
  - `step_key` distinct from `prompt_id`,
  - required per-step `prompt_ref` (`prompt_id`, `prompt_version`).
- Workflow run counters (`next_turn_index`, `next_checkpoint_sequence`) are included for deterministic replay/resume.
- Route-hook support is explicitly limited to MVP-safe extension seams (no full branching engine in Plan 2).
- Route-hook appended step keys are deterministic and persisted with effective queue state in `run.json` before execution continues.
- `route_hook_key` grammar is explicit so generated step keys always satisfy `step_key` path-safety constraints.
- `workflow_kind`/`chain_mode` alignment invariant is explicit with deterministic failure behavior for mismatch.

## Issues
- none

## Decision
- accepted: minimal workflow abstraction contracts are now implemented in `Zelanthus.StoryEngine.Domain` with deterministic key validation and required `prompt_ref`.

## Completion
- `completed`

## Next Actions
- Execute step `0030-define-application-orchestration-flow-and-chain-router`.



