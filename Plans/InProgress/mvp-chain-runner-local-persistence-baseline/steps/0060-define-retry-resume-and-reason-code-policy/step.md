# Step: 0060-define-retry-resume-and-reason-code-policy

## Goal
- Define deterministic retry/resume semantics with pinned reason codes across runner, validation, and persistence failure paths.

## Context
- Plan 2 requires reliable recovery and explicit failure diagnostics.
- Reason-code inconsistency quickly erodes replay/debug quality.

## Git Branch
- `feature/thin-clients-first-solution-structure`

## Commits
- `none`

## Commands Executed
- `dotnet build "Zelanthus.slnx"`
- `dotnet test "Tests/Zelanthus.WorkflowContractProofs.Tests/Zelanthus.WorkflowContractProofs.Tests.csproj" --filter "Category!=LiveGemini"`
- `dotnet test "Tests/Zelanthus.Architecture.Tests/Zelanthus.Architecture.Tests.csproj"`
- `dotnet test "Zelanthus.slnx" --filter "Category!=LiveGemini"`

## Files Changed
- `Source/Zelanthus.StoryEngine.Application/RunnerReasonCodes.cs`
- `Source/Zelanthus.StoryEngine.Application/WorkflowExecutionResult.cs`
- `Source/Zelanthus.StoryEngine.Application/WorkflowRunner.cs`
- `Source/Zelanthus.StoryEngine.Domain/WorkflowStepDefinition.cs`

## Outputs
- `Plans/InProgress/mvp-chain-runner-local-persistence-baseline/decisions/0004-retry-resume-and-reason-code-policy-v0.md`
  - pinned reason codes and retry/resume rules are explicit.
- `Plans/InProgress/mvp-chain-runner-local-persistence-baseline/steps/0060-define-retry-resume-and-reason-code-policy/artifacts/reason-code-and-retry-policy.md`
  - scenario-level policy mapping for retry, restart, and terminal failure outcomes.
- `Plans/InProgress/mvp-chain-runner-local-persistence-baseline/plan.md`
  - Plan 2 reason-code baseline section is explicit.

## Tests / Results
- `dotnet build "Zelanthus.slnx"` -> `passed` (0 warnings, 0 errors)
- `dotnet test "Tests/Zelanthus.WorkflowContractProofs.Tests/Zelanthus.WorkflowContractProofs.Tests.csproj" --filter "Category!=LiveGemini"` -> `passed` (28 tests)
- `dotnet test "Tests/Zelanthus.Architecture.Tests/Zelanthus.Architecture.Tests.csproj"` -> `passed` (7 tests)
- `dotnet test "Zelanthus.slnx" --filter "Category!=LiveGemini"` -> `passed` (35 tests)

## Acceptance Evidence
- Baseline reason-code list is explicit and exact (snake_case).
- Baseline reason-code list includes deterministic missing step `prompt_ref` failure (`missing_prompt_reference`).
- Retry/resume policy identifies when to:
  - retry current turn,
  - restart from planning step,
  - fail terminally.
- Policy explicitly treats continuity handle as optional optimization and not resume authority.
- Policy explicitly differentiates:
  - cognitive resume -> restart from first `PLAN_STEP`,
  - conversational resume -> continue from last successful persisted step.
- Reason-code baseline includes `cognitive_restart_required` for cognitive restart semantics.
- Deterministic failure handling rule is explicit: one failure outcome maps to exactly one reason code.
- Continuity-handle failure behavior is capability-conditioned:
  - supported+expected handle missing/invalid -> `continuity_handle_invalid`,
  - unsupported capability -> non-failure path.

## Issues
- none

## Decision
- accepted: reason-code baseline is implemented in runner contracts, cognitive restart policy emits `cognitive_restart_required`, and deterministic failure mapping is validated by proof tests.

## Completion
- `completed`

## Next Actions
- Execute step `0080-define-runner-proof-test-suite-and-evidence-artifacts`.



