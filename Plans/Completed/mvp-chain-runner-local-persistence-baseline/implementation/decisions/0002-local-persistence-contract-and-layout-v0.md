# Decision 0002: Local Persistence Contract and Layout v0

## Status
- accepted

## Decision Summary
- Plan 2 uses workspace-local file/path-backed persistence for runner checkpoints and artifacts.
- Persistence contract is deterministic and mapper-driven; required fields cannot be silently dropped.
- External persistence services (Postgres/cache/queue/cloud storage) are explicitly out-of-scope.

## Required Stored Artifacts
- Run-level record (`run.json`)
- Checkpoint records (`checkpoints/checkpoint-<checkpoint-sequence>.json`)
- Turn-level artifacts:
  - canonical turn metadata record (`turn.json`)
  - raw response snapshot
  - normalized response
  - provenance
  - validation
  - accepted output (when present)
- Failure records (`failures/failure-<turn-index>.json`)

## Mapping Guarantees
- Infrastructure mapping must preserve required fields from runtime contracts:
  - prompt identity/version/checksum
  - provider/model identity
  - chain mode/turn index/turn objective
  - workflow/step/turn counters (`current_step_index`, `last_success_step_index`, `next_turn_index`, `next_checkpoint_sequence`)
  - reason codes and validation outcomes
- Missing required stored fields must produce explicit mapping failure.
- Provenance artifact shape is a strict superset of `Zelanthus.Prompting` provenance required fields.
- Mapping from storage artifact -> `Zelanthus.Prompting` provenance contract must be deterministic and lossless for required fields.

## Path Determinism and Portability
- Turn artifact path format is fixed to:
  - `turns/<turn-index>-<step-key>/`
- Objective text is not part of folder names and must be stored in canonical turn metadata (`turn.json`) and related artifact content.
- `turn.json` is canonical source of turn identity/objective/timestamp metadata.
- `step_key` must be platform-safe and deterministic:
  - lowercase letters, digits, hyphen only,
  - regex: `^[a-z0-9][a-z0-9-]{0,63}$`
- Any non-deterministic artifact path generation is a promotion blocker for Plan 2.

## Counter Reservation Durability Rule
- `run.json` is the authoritative counter store for `next_turn_index` and `next_checkpoint_sequence`.
- Reservation order is required:
  - reserve index by incrementing the corresponding `next_*` counter in `run.json`,
  - atomically persist updated `run.json`,
  - then write dependent turn/checkpoint artifacts.
- Minimum atomic write strategy for `run.json`, checkpoints, and turn artifacts:
  - write temp file in same directory,
  - flush/fsync temp content,
  - atomic rename/replace target file.
- Interrupted writes may create index gaps; index reuse is forbidden.

## Failure Record Anchor Rule
- Failure records are keyed by `turn_index` (`failure-<turn-index>.json`) for deterministic traceability.
- Plan 2 stores one failure artifact per failed turn attempt.

## Consequences
- Infrastructure planning and tests must include round-trip and failure-path mapping coverage.
- Local persistence shape must remain explicit and reviewable as a future Postgres migration input.
