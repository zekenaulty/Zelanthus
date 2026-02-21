# Step: 0025-define-minimal-workflow-abstraction-hooks

## Goal
- Define the minimal workflow abstraction required for Plan 2 so runner logic is workflow-aware and future-extensible without over-engineering.

## Context
- Prior attempts showed risk in tackling broad workflow systems too early.
- Plan 2 needs only enough abstraction to support current chain runner correctness plus future route/branch expansion seams.

## Git Branch
- `feature/thin-clients-first-solution-structure`

## Commits
- `none`

## Commands Executed
- `none`

## Files Changed
- `none`

## Outputs
- `Plans/InProgress/mvp-chain-runner-local-persistence-baseline/decisions/0005-minimal-workflow-abstraction-v0.md`
  - minimal workflow contracts and route-hook policy are explicit.
- `Plans/InProgress/mvp-chain-runner-local-persistence-baseline/steps/0025-define-minimal-workflow-abstraction-hooks/artifacts/minimal-workflow-abstraction-contract.md`
  - concrete contract sketch and guardrails for implementation.
- `Plans/InProgress/mvp-chain-runner-local-persistence-baseline/plan.md`
  - minimal workflow abstraction section is explicit.

## Tests / Results
- `not-run` -> pending execution

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
- pending

## Completion
- `pending`

## Next Actions
- Apply workflow abstraction to application orchestration and chain router planning.



