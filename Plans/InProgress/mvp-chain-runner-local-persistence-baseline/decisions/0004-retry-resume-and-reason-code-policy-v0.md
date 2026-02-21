# Decision 0004: Retry, Resume, and Reason-Code Policy v0

## Status
- accepted

## Decision Summary
- Retry and resume behavior is explicit, deterministic, and reason-coded.
- Continuity handles are optional optimization and never authoritative resume state.
- Plan 2 pins baseline reason codes for orchestration and persistence failures.

## Retry/Resume Rules
- Retry current turn only when required upstream artifacts/checkpoints remain valid.
- Restart from planning step when validation indicates stale/insufficient plan artifact.
- `CognitiveChain` resume behavior:
  - always restart from first `PLAN_STEP`,
  - even if prior run had a `thinking_persistence_key` (provider-side thinking cache is not guaranteed).
- `ConversationalChain` resume behavior:
  - resume from last successful persisted step/turn.
- Continuity handle update behavior:
  - advance `latest_thinking_persistence_key` only after successful provider response parse.
  - if turn fails before response parse, do not advance continuity key.
  - capability-conditioned continuity failures:
    - if capability indicates continuity support and handle is expected, null/empty/malformed handle is deterministic failure `continuity_handle_invalid`.
    - if capability indicates no continuity support, missing handle is expected and not a failure.

## Pinned Reason-Code Baseline
- `missing_prompt_reference`
- `missing_required_placeholder`
- `schema_validation_failed`
- `provider_protocol_error`
- `output_starvation`
- `continuity_handle_invalid`
- `cognitive_restart_required`
- `retry_budget_exhausted`
- `checkpoint_write_failed`
- `checkpoint_read_failed`
- `artifact_write_failed`
- `artifact_read_failed`
- `invalid_state_transition`

## Policy Guarantees
- Every deterministic failure outcome emits exactly one reason code.
- Additional failure detail is captured in structured diagnostics payload, not additional reason codes.
- Reason codes are persisted with provenance/failure artifacts.
- Code strings are exact lowercase snake_case values (no aliases).
- Cognitive resume attempts that require restart emit `cognitive_restart_required`.
- Capability-supported null/empty provider continuity handle in cognitive flow emits `continuity_handle_invalid`.
- Continuity-handle failure emission requires capability-supported/expected handle context.
- Capability-unsupported continuity mode proceeds without emitting `continuity_handle_invalid`.

## Consequences
- Plan 2 proof tests must validate reason-code emission and persistence across failure paths.
- Application and infrastructure boundaries must propagate reason codes without mutation.
- Deterministic failure mapping coverage includes:
  - missing step `prompt_ref` failures,
  - workflow-kind/chain-mode mismatch failures,
  - prompt render failures,
  - provider protocol failures,
  - checkpoint/artifact read-write failures,
  - illegal state transition failures.
