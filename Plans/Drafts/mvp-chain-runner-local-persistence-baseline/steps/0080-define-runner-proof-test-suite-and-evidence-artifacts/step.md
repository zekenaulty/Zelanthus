# Step: 0080-define-runner-proof-test-suite-and-evidence-artifacts

## Goal
- Define deterministic Plan 2 runner proof tests and required evidence artifacts for golden/failure/resume coverage.

## Context
- Plan 2 promotion requires explicit evidence contracts, not only high-level test intent.
- Existing Plan 1 proof harness is the baseline and must be extended without losing deterministic behavior.

## Git Branch
- `main`

## Commits
- `none`

## Commands Executed
- `none`

## Files Changed
- `none`

## Outputs
- `Plans/Drafts/mvp-chain-runner-local-persistence-baseline/steps/0080-define-runner-proof-test-suite-and-evidence-artifacts/artifacts/runner-proof-test-seed.md`
  - exact planned test names for runner proofs.
- `Plans/Drafts/mvp-chain-runner-local-persistence-baseline/steps/0080-define-runner-proof-test-suite-and-evidence-artifacts/artifacts/runner-proof-evidence-spec.md`
  - required evidence artifact names and required fields.
- `Plans/Drafts/mvp-chain-runner-local-persistence-baseline/validation/plan-2-acceptance-evidence-matrix.md`
  - acceptance points mapped to test/evidence requirements.

## Tests / Results
- `not-run` -> draft artifact complete

## Acceptance Evidence
- Proof test list covers:
  - variable-length cognitive flow with staged `PLAN_STEP`/`EXECUTE`,
  - variable-length conversational flow,
  - capability-conditioned continuity-handle behavior,
  - reason-coded failure flow,
  - cognitive restart-from-start resume flow,
  - conversational resume-from-last-success flow,
  - illegal state transition failure flow.
- Evidence spec includes artifact requirements for:
  - run metadata,
  - checkpoint persistence,
  - reason-coded failure,
  - resume result correctness.
- Evidence spec includes workflow metadata and rolling `thinking_persistence_key` propagation checks for active cognitive runs.
- Evidence spec includes capability-conditioned continuity-handle behavior with non-advancing latest-key assertion on supported+expected invalid-handle paths.
- Evidence spec includes deterministic artifact path checks using `<turn-index>-<step-key>` naming only.
- Evidence spec includes canonical `turn.json` checks, deterministic counter reservation assertions, and crash/no-reuse assertions.
- Evidence spec includes deterministic failure artifact naming checks (`failure-<turn-index>.json`).
- Evidence spec includes deterministic route-hook generated step-key and persisted effective-queue checks.
- Evidence spec includes required per-step `prompt_ref` capture and deterministic `missing_prompt_reference` failure proof.
- Evidence spec includes `workflow_kind`/`chain_mode` alignment checks and deterministic mismatch failure proof.
- Evidence spec includes strict provenance superset mapping checks to Prompting required fields.
- Evidence spec includes timestamp determinism rules (presence/format assertions, no cross-run raw timestamp equality by default).
- Default deterministic test execution path excludes live-provider dependency.

## Issues
- none

## Decision
- accepted

## Completion
- `completed`

## Next Actions
- Define API composition boundary integration rules and non-goals.
