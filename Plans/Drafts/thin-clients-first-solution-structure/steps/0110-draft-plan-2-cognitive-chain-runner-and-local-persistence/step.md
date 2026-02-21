# Step: 0110-draft-plan-2-cognitive-chain-runner-and-local-persistence

## Goal
- Produce implementation-ready Draft Plan 2 for chain-length-flexible `CognitiveChain`/`ConversationalChain` runner behavior and local artifact/provenance persistence.

## Context
- Plan 2 depends on Plan 1 contract outputs and should avoid premature production persistence setup.
- Plan 2 must not hard-lock runner flow to two turns; `T1` -> `T2` is minimum proof only.

## Git Branch
- `main`

## Commits
- `none`

## Commands Executed
- `none`

## Files Changed
- `none`

## Tests / Results
- `not-run` -> pending draft step

## Issues
- none

## Decision
- pending

## Completion
- `pending`

## Next Actions
- Define runner acceptance criteria for variable-length `CognitiveChain` and `ConversationalChain` flows.
- Define local persistence boundaries:
  - workspace-local file/path-backed storage for checkpoints/artifacts/provenance/failures,
  - deterministic resume/replay from persisted artifacts,
  - no external database/cache/queue/service storage in Plan 2.
- Define migration constraints toward future Postgres.
