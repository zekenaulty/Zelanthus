# Provenance Mapping Contract

## Scope
- Plan: `mvp-chain-runner-local-persistence-baseline`
- Step: `0050-define-infrastructure-mappers-and-store-contracts`

## Contract Rule
- Persisted provenance artifact is a strict superset of `Zelanthus.Prompting` provenance required fields.
- Mapping artifact -> Prompting provenance contract must be deterministic and lossless for required fields.

## Required Prompting Fields (Minimum)
- prompt identity
- prompt version
- prompt checksum/fingerprint
- provider/model identity
- timing metadata
- token/usage metadata
- reason code (failure paths)

## Relationship to Canonical Turn Metadata
- `turn.json` is authoritative for turn identity (`turn_index`, `step_key`, objective).
- Provenance mapping may reference turn identity but must not override canonical values from `turn.json`.

## Failure Behavior
- Missing required provenance fields fail deterministically with one reason code:
  - `artifact_read_failed` (materialization path)
  - `artifact_write_failed` (persistence path)
- No silent defaulting for required provenance fields.
