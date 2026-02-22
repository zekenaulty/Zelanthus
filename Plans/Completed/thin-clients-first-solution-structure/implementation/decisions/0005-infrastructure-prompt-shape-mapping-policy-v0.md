# Decision 0005: Infrastructure Prompt Shape Mapping Policy v0

## Status
- accepted

## Decision Summary
- `Zelanthus.StoryEngine.Infrastructure` may persist prompt and provenance data in storage schemas that are not 1:1 with runtime prompting contracts.
- Infrastructure must implement deterministic mapping to/from canonical runtime contract shapes.

## Context
- Persistence models often optimize for indexing, querying, retention, and storage costs.
- Runtime contract models optimize for execution correctness and workflow validation.
- Forcing identical models in both layers creates unnecessary coupling.

## Required Mapping Guarantees
- Deterministic mapping:
  - same input record and mapper version produce same runtime contract output.
- Round-trip guarantees for required fields:
  - prompt semantic ID,
  - prompt version,
  - rendered prompt hash/checksum,
  - provider/model identity,
  - chain mode and turn metadata,
  - normalized response metadata required by validators.
- Explicit failure semantics:
  - mapping failures are surfaced as explicit errors,
  - required fields cannot be silently dropped or defaulted without reason code.

## Mapping Boundary Rules
- Infrastructure owns storage<->runtime mapping only.
- Application owns semantic assembly decisions (prompt choice, context assembly, chain policy).
- Provider adapters own runtime<->protocol mapping.

## Validation and Test Expectations
- Contract materialization tests:
  - persisted record -> runtime contract passes schema checks.
- Round-trip tests:
  - runtime contract -> storage model -> runtime contract preserves required fields.
- Error-path tests:
  - missing required stored fields produce explicit mapping failures.

## Rationale
- Enables storage flexibility without sacrificing runtime contract integrity.
- Preserves clear layer responsibilities and reduces accidental coupling.

## Consequences
- Infrastructure implementation plans must include mapper contracts and mapping test suites.
- Review gates must reject persistence changes that bypass mapping boundaries.
