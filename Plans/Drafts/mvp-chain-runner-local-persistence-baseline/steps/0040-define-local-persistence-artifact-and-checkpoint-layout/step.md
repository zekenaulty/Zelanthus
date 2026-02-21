# Step: 0040-define-local-persistence-artifact-and-checkpoint-layout

## Goal
- Define Plan 2 local persistence layout and required artifact/checkpoint records for deterministic resume/replay.

## Context
- Runner behavior must persist enough explicit state to resume without hidden provider continuity state.
- Layout decisions now reduce future migration risk when Postgres is introduced later.

## Git Branch
- `main`

## Commits
- `none`

## Commands Executed
- `none`

## Files Changed
- `none`

## Outputs
- `Plans/Drafts/mvp-chain-runner-local-persistence-baseline/decisions/0002-local-persistence-contract-and-layout-v0.md`
  - local persistence model and required stored artifacts are explicit.
- `Plans/Drafts/mvp-chain-runner-local-persistence-baseline/steps/0040-define-local-persistence-artifact-and-checkpoint-layout/artifacts/local-persistence-layout.md`
  - concrete run/checkpoint/turn/failure file layout and required fields.
- `Plans/Drafts/mvp-chain-runner-local-persistence-baseline/plan.md`
  - local persistence contract section is explicit.

## Tests / Results
- `not-run` -> draft artifact complete

## Acceptance Evidence
- Layout includes explicit run-level, turn-level, checkpoint, and failure records.
- Required fields for checkpoint/resume are listed and mapped to runtime contract fields.
- Turn artifact folder contract is deterministic: `<turn-index>-<step-key>` only.
- Path safety rules and platform-agnostic naming constraints for `step_key` are explicit.
- `turn.json` is explicitly declared as canonical turn metadata record.
- Counter reservation contract (`next_turn_index`, `next_checkpoint_sequence`) and persisted index fields (`turn_index`, `checkpoint_sequence`) are explicit.
- Crash-safe reservation order is explicit: reserve counter in `run.json`, persist atomically, then write dependent artifacts.
- Minimum atomic write strategy is explicit (`temp in same directory -> flush/fsync -> atomic replace`).
- Failure artifact naming anchor is explicit: `failure-<turn-index>.json`.
- Plan explicitly excludes external database/cache/queue/service persistence in Plan 2.

## Issues
- none

## Decision
- accepted

## Completion
- `completed`

## Next Actions
- Define mapper/store contracts and deterministic mapping failure rules.
