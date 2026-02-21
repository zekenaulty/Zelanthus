# Step: 0050-define-infrastructure-mappers-and-store-contracts

## Goal
- Define infrastructure mapper/store contract boundaries for writing and materializing runner artifacts/checkpoints.

## Context
- Infrastructure owns storage transforms, not semantic chain decisions.
- Deterministic mapping guarantees are required to keep runner resume behavior auditable.

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
  - mapping guarantees and failure semantics are explicit.
- `Plans/Drafts/mvp-chain-runner-local-persistence-baseline/steps/0050-define-infrastructure-mappers-and-store-contracts/artifacts/provenance-mapping-contract.md`
  - strict-superset provenance mapping requirements and deterministic failure behavior.
- `Plans/Drafts/mvp-chain-runner-local-persistence-baseline/plan.md`
  - local persistence contract and mapping expectations are explicit.

## Tests / Results
- `not-run` -> draft artifact complete

## Acceptance Evidence
- Store interfaces are explicit for:
  - run records,
  - checkpoint records,
  - turn artifact records,
  - failure records.
- Mapping contract includes round-trip preservation for required fields.
- Provenance artifact mapping is explicit as strict superset -> Prompting provenance required fields (deterministic and lossless for required fields).
- Provenance mapping contract preserves canonical turn identity from `turn.json` (no conflicting identity fields).
- Missing required stored fields are defined as explicit mapping failures (never silently defaulted).

## Issues
- none

## Decision
- accepted

## Completion
- `completed`

## Next Actions
- Define retry/resume reason-code policy and persistence expectations.
