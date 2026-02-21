# Decision 0004: MVP Artifact and Failure Contract v0

## Status
- accepted

## Decision Summary
- Define a minimal but explicit MVP artifact taxonomy.
- Define a deterministic MVP reason-code baseline for retry and terminal failures.
- Define one canonical structured output contract for MVP.

## Context
- "Artifact-first correctness" requires explicit artifact shape contracts, not only conceptual statements.
- "Reason-coded failure" is only useful if the baseline code set is concrete.
- Structured output drift becomes costly when "close enough" responses are accepted ad hoc.

## MVP Artifact Taxonomy
- `TurnArtifact`
  - Purpose: capture one execution turn outcome.
  - Minimum fields:
    - `chain_mode`
    - `turn_index`
    - `turn_objective`
    - `raw_response_snapshot_ref`
    - `normalized_output_ref`
    - `validation_result_ref`
- `ProvenanceRecord`
  - Purpose: capture prompt and execution lineage.
  - Minimum fields:
    - `prompt_id`
    - `prompt_version`
    - `prompt_hash`
    - `provider_id`
    - `model_id`
    - `timing`
    - `token_accounting`
    - `reason_codes` (if any)
- `AcceptedOutputArtifact`
  - Purpose: canonical artifact consumed by the next workflow step.
  - Minimum fields:
    - `schema_id`
    - `schema_version`
    - `payload`
    - `accepted_at_utc`
    - `source_turn_ref`

## MVP Failure Reason-Code Baseline
- `missing_required_token`
- `schema_validation_failed`
- `provider_protocol_error`
- `output_starvation`
- `continuity_handle_invalid`
- `retry_budget_exhausted`

Reason-code policy:
- Retry and terminal failures must emit one or more codes from the baseline or a declared extension set.
- Codes must be persisted in `ProvenanceRecord` and validation/failure artifacts.

## Canonical Structured Output Rule (MVP)
- Accepted structured output is a JSON object conforming to the declared schema contract for the step.
- Non-conforming outputs are never auto-coerced into accepted artifacts.
- Raw response snapshots are always persisted for forensic review, even on failure.

## Consequences
- Draft plans must include artifacts and reason-code acceptance criteria.
- MVP runner and validators must reject outputs that do not satisfy canonical structured-output contract.
