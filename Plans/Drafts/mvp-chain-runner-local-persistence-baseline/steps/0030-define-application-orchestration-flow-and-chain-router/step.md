# Step: 0030-define-application-orchestration-flow-and-chain-router

## Goal
- Define application-layer runner orchestration flow and chain router behavior for cognitive and conversational execution.

## Context
- Application layer owns semantic transform, chain-mode choice, and retry/resume policy entry points.
- Provider adapter and infrastructure should not absorb orchestration semantics.

## Git Branch
- `main`

## Commits
- `none`

## Commands Executed
- `none`

## Files Changed
- `none`

## Outputs
- `Plans/Drafts/mvp-chain-runner-local-persistence-baseline/plan.md`
  - application orchestration ownership and runner execution contract are explicit.
- `Plans/Drafts/mvp-chain-runner-local-persistence-baseline/decisions/0003-project-reference-graph-and-boundary-enforcement-v0.md`
  - orchestration boundary ownership is aligned with project references.
- `Plans/Drafts/mvp-chain-runner-local-persistence-baseline/decisions/0005-minimal-workflow-abstraction-v0.md`
  - workflow/step abstraction is integrated into orchestration flow planning.

## Tests / Results
- `not-run` -> draft artifact complete

## Acceptance Evidence
- Orchestration flow explicitly distinguishes:
  - chain-mode selection,
  - workflow step objective assembly (`PLAN_STEP`, `EXECUTE`, `CONVERSATION_STEP`),
  - step-level `prompt_ref` resolution (`prompt_id`, `prompt_version`),
  - `ILlmClient` invocation path,
  - post-call validation and persistence dispatch.
- Flow supports variable step counts and does not encode a fixed single-pair loop.
- Chain routing contract includes explicit upgrade/downgrade conditions between single-turn and chain execution.
- Flow supports staged `PLAN_STEP` sequences followed by one or more `EXECUTE` steps in cognitive mode.
- Variable-length expansion mechanism is explicit: route hooks append steps to run-local queue tail only.
- Route-hook appended step identity is deterministic (`<route_hook_key>-r<NNNN>`) and persisted in effective queue state before appended-step execution.
- Missing/invalid step `prompt_ref` is deterministic terminal failure (`missing_prompt_reference`) before provider invocation.
- `workflow_kind`/`chain_mode` mismatch handling is deterministic and explicitly reason-coded.

## Issues
- none

## Decision
- accepted

## Completion
- `completed`

## Next Actions
- Define local persistence layout and mapping contracts for checkpoint/artifact durability.
