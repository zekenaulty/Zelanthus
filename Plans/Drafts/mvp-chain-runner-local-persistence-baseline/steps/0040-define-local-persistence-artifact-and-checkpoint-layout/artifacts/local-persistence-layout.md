# Local Persistence Layout

## Scope
- Plan: `mvp-chain-runner-local-persistence-baseline`
- Step: `0040-define-local-persistence-artifact-and-checkpoint-layout`

## Canonical Layout (Plan 2)
```text
artifacts/workflow-runs/<run-id>/
  run.json
  checkpoints/
    checkpoint-<sequence>.json
  turns/
    <turn-index>-<step-key>/
      turn.json
      raw-response.json
      normalized-response.json
      provenance.json
      validation.json
      accepted-output.json
  failures/
    failure-<turn-index>.json
```

## Required Field Categories
- Run record:
  - run identity
  - workflow identity/version
  - chain mode
  - `next_turn_index`
  - `next_checkpoint_sequence`
  - created/updated timestamps
  - current run state
- Checkpoint record:
  - checkpoint sequence
  - source turn reference
  - resume cursor metadata
  - reason codes (if checkpoint created during failure handling)
- Turn artifact set:
  - canonical turn metadata (`turn.json`)
  - raw snapshot reference/data
  - normalized output reference/data
  - provenance reference/data
  - validation result reference/data
  - objective text stored in `turn.json`/artifact content, not folder name

## Canonical Turn Metadata Contract (`turn.json`)
- `turn.json` is the authoritative turn record.
- Required fields:
  - `workflow_key`
  - `workflow_version`
  - `step_key`
  - `step_index`
  - `turn_index`
  - `attempt_index`
  - `objective`
  - `started_at_utc`
  - `completed_at_utc` (when available)
- Other artifacts reference `turn_index`/`step_key`; they do not redefine authoritative turn identity.

## Path Rules (Deterministic and Cross-Platform)
- Turn folder naming is fixed: `<turn-index>-<step-key>`.
- `step_key` allowed characters: lowercase letters, digits, hyphen.
- `step_key` regex: `^[a-z0-9][a-z0-9-]{0,63}$`.
- Objective text must not be used in path segments.
- Path generation must be deterministic across repeated runs with identical run metadata.

## Rules
- Required fields cannot be silently defaulted.
- Missing/corrupt required fields produce explicit mapping/read failures.
- Layout is local file/path-backed only for Plan 2.
- Counter rules:
  - turn reservation (`start_turn`):
    - assign `turn_index = next_turn_index`,
    - increment `next_turn_index`,
    - atomically persist `run.json`,
    - then write turn artifact set.
  - checkpoint reservation (`write_checkpoint`):
    - assign `checkpoint_sequence = next_checkpoint_sequence`,
    - increment `next_checkpoint_sequence`,
    - atomically persist `run.json`,
    - then write checkpoint artifact.
  - reserved indices are never reused; interruption may create auditable gaps.
- Minimum atomic write strategy:
  - write temp file in same directory,
  - flush/fsync temp content,
  - rename/replace target atomically.
- Failure record rule:
  - failure artifact for a failed turn attempt is `failure-<turn-index>.json`.
- Resume policy alignment:
  - cognitive runs restart from first `PLAN_STEP`,
  - conversational runs resume from last successful persisted step.
