# Runner Proof Evidence Spec

## Scope
- Plan: `mvp-chain-runner-local-persistence-baseline`
- Step: `0080-define-runner-proof-test-suite-and-evidence-artifacts`

## Required Evidence Artifacts
- `artifacts/workflow-runs/<run-id>/run.json`
- `artifacts/workflow-runs/<run-id>/checkpoints/checkpoint-<sequence>.json`
- `artifacts/workflow-runs/<run-id>/turns/<turn-index>-<step-key>/turn.json`
- `artifacts/workflow-runs/<run-id>/turns/<turn-index>-<step-key>/provenance.json`
- `artifacts/workflow-runs/<run-id>/turns/<turn-index>-<step-key>/validation.json`
- `artifacts/workflow-runs/<run-id>/failures/failure-<turn-index>.json` (failure scenarios)

## Required Artifact Assertions
- Run artifact captures chain mode and run state transitions.
- Run artifact captures workflow metadata (`workflow_key`, `workflow_version`, `workflow_kind`, `chain_mode`, `run_state`, `current_step_index`, `last_success_step_index`, `next_turn_index`, `next_checkpoint_sequence`).
- Effective queue metadata captures per-step `prompt_ref` (`prompt_id`, `prompt_version`) for executed/queued steps.
- Run/workflow metadata evidence proves `workflow_kind` and `chain_mode` alignment for valid runs.
- Checkpoint artifact captures deterministic resume cursor metadata and checkpoint counter sequence.
- `turn.json` is canonical turn metadata (`step_key`, `step_index`, `turn_index`, objective, timestamps).
- Effective queue persistence captures deterministic route-hook appends with generated step keys.
- Provenance artifact is strict superset shape with deterministic mapping to Prompting required fields.
- Failure artifact captures pinned reason codes.
- Missing/invalid step `prompt_ref` failure artifact captures `missing_prompt_reference`.
- Cognitive resume evidence shows restart from first `PLAN_STEP` with `cognitive_restart_required`.
- Conversational resume evidence shows continuation from last successful persisted step.
- Cognitive staged-plan evidence captures per-step `thinking_persistence_key` turnover and forward propagation of the latest value.
- Cognitive continuity evidence includes capability-conditioned paths:
  - supported+expected handle missing/invalid -> `continuity_handle_invalid`,
  - unsupported capability -> non-failure path.
- Cognitive continuity failure evidence shows `latest_thinking_persistence_key` is not advanced on pre-parse failure/null handle.
- Artifact path evidence proves deterministic folder naming uses `<turn-index>-<step-key>` only.
- State machine evidence proves illegal transition path emits `invalid_state_transition`.
- State mismatch evidence proves `workflow_kind`/`chain_mode` mismatch emits deterministic terminal `invalid_state_transition`.
- Counter evidence proves deterministic reservation behavior for `next_turn_index`/`next_checkpoint_sequence` and persisted `turn_index`/`checkpoint_sequence`.
- Crash-interruption evidence proves reserved indices are not reused after partial turn/checkpoint artifact writes.
- Failure artifact naming evidence proves deterministic `failure-<turn-index>.json` anchoring.

## Determinism Rules
- Deterministic fixture path is the default execution mode.
- Live-provider tests are opt-in only and cannot be required for baseline Plan 2 acceptance.
- Artifact folder generation must be deterministic for repeated runs with identical run metadata.
- Route-hook generated step keys and effective queue persistence must be deterministic across retry/resume.
- Route-hook key inputs must satisfy pinned grammar so generated step keys remain path-safe.
- Timestamp assertions use presence/format validation (for example ISO-8601 UTC) unless deterministic clock injection is explicitly enabled.
- Cross-run deterministic assertions should use stable identifiers/checksums and not raw timestamp equality.
