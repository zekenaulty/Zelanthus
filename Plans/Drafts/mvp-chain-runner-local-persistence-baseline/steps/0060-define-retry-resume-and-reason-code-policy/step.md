# Step: 0060-define-retry-resume-and-reason-code-policy

## Goal
- Define deterministic retry/resume semantics with pinned reason codes across runner, validation, and persistence failure paths.

## Context
- Plan 2 requires reliable recovery and explicit failure diagnostics.
- Reason-code inconsistency quickly erodes replay/debug quality.

## Git Branch
- `main`

## Commits
- `none`

## Commands Executed
- `none`

## Files Changed
- `none`

## Outputs
- `Plans/Drafts/mvp-chain-runner-local-persistence-baseline/decisions/0004-retry-resume-and-reason-code-policy-v0.md`
  - pinned reason codes and retry/resume rules are explicit.
- `Plans/Drafts/mvp-chain-runner-local-persistence-baseline/steps/0060-define-retry-resume-and-reason-code-policy/artifacts/reason-code-and-retry-policy.md`
  - scenario-level policy mapping for retry, restart, and terminal failure outcomes.
- `Plans/Drafts/mvp-chain-runner-local-persistence-baseline/plan.md`
  - Plan 2 reason-code baseline section is explicit.

## Tests / Results
- `not-run` -> draft artifact complete

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
- accepted

## Completion
- `completed`

## Next Actions
- Define architecture test assertions and project reference graph updates.
