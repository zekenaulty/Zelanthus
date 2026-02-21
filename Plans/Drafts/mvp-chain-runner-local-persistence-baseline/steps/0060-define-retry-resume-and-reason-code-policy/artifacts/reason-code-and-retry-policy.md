# Reason Code and Retry Policy

## Scope
- Plan: `mvp-chain-runner-local-persistence-baseline`
- Step: `0060-define-retry-resume-and-reason-code-policy`

## Pinned Reason Codes
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

## Policy Mapping
| Scenario | Action | Required Reason Code |
|---|---|---|
| Required step `prompt_ref` missing/invalid | terminal for turn | `missing_prompt_reference` |
| Required prompt placeholder missing | terminal for turn | `missing_required_placeholder` |
| Structured output schema mismatch | retry-or-terminal by budget/policy | `schema_validation_failed` |
| Provider protocol/transport error | retry-or-terminal by budget/policy | `provider_protocol_error` |
| Output starvation after planning | restart planning unit or terminal by budget | `output_starvation` |
| Continuity handle capability unsupported | continue without continuity handle | `none (non-failure path)` |
| Continuity handle capability supported + expected handle missing/empty/malformed | continue from artifacts or restart planning | `continuity_handle_invalid` |
| Cognitive chain resume requested after interruption | restart from first `PLAN_STEP` | `cognitive_restart_required` |
| Retry budget exhausted | terminal | `retry_budget_exhausted` |
| Checkpoint cannot be written | terminal | `checkpoint_write_failed` |
| Checkpoint cannot be read/materialized | terminal or forced replan path | `checkpoint_read_failed` |
| Non-checkpoint artifact cannot be written | terminal | `artifact_write_failed` |
| Non-checkpoint artifact cannot be read/materialized | terminal | `artifact_read_failed` |
| Illegal run/turn state transition attempted | terminal | `invalid_state_transition` |
| `workflow_kind`/`chain_mode` mismatch detected for active run definition | terminal | `invalid_state_transition` |

## Invariants
- Reason code values are exact lowercase snake_case.
- Deterministic failure outcome maps to exactly one reason code.
- Retry/terminal outcomes persist reason code in provenance/failure artifacts.
- Additional failure detail is stored in diagnostics payload, not extra reason codes.
- Continuity handle is never required for correctness.
- Continuity-handle failure codes apply only when capability-supported handle is expected.
- Cognitive resume semantics restart from chain start due non-guaranteed provider-side thinking cache.
- `latest_thinking_persistence_key` advances only on successful provider response parse.
