# Run and Turn State Machine

## Scope
- Plan: `mvp-chain-runner-local-persistence-baseline`
- Step: `0020-define-chain-runner-domain-and-state-model`

## RunState
- `created`
- `running`
- `waiting_retry`
- `succeeded`
- `failed_terminal`
- `cancelled`

## TurnState
- `pending`
- `running`
- `succeeded`
- `failed_retryable`
- `failed_terminal`
- `skipped`

## Run Transitions
| Current State | Event | Next State | Notes |
|---|---|---|---|
| `created` | `start_run` | `running` | initialize first step cursor |
| `running` | `turn_failed_retryable` | `waiting_retry` | reason code persisted |
| `waiting_retry` | `retry_started` | `running` | retry budget must allow |
| `running` | `run_completed` | `succeeded` | all required steps complete |
| `running` | `run_failed_terminal` | `failed_terminal` | deterministic terminal failure |
| `waiting_retry` | `retry_budget_exhausted` | `failed_terminal` | emits `retry_budget_exhausted` |
| `created` / `running` / `waiting_retry` | `cancel_requested` | `cancelled` | explicit cancel path |

## Turn Transitions
| Current State | Event | Next State |
|---|---|---|
| `pending` | `start_turn` | `running` |
| `running` | `turn_completed` | `succeeded` |
| `running` | `turn_failed_retryable` | `failed_retryable` |
| `running` | `turn_failed_terminal` | `failed_terminal` |
| `pending` | `skip_turn` | `skipped` |

## Illegal Transition Rule
- Any state/event pair not listed in this document is illegal.
- Illegal transitions fail deterministically with reason code `invalid_state_transition`.

## Resume Cursor Contract
- Authoritative persisted fields:
  - `workflow_key`
  - `workflow_version`
  - `chain_mode`
  - `current_step_index`
  - `last_success_step_index`
  - `next_turn_index`
  - `next_checkpoint_sequence`
  - `run_state`
  - optional `latest_thinking_persistence_key`
- Mode behavior:
  - `CognitiveChain`: restart from first `PLAN_STEP` (do not continue mid-chain).
  - `ConversationalChain`: resume from `last_success_step_index` + 1.
  - `chain_mode` must match active workflow definition `workflow_kind`; mismatch is deterministic terminal failure `invalid_state_transition`.

## Counter Reservation Rules
- `start_turn` reserves `turn_index` by incrementing `next_turn_index` in `run.json` first, persisting atomically, then writing turn artifacts.
- `write_checkpoint` reserves `checkpoint_sequence` by incrementing `next_checkpoint_sequence` in `run.json` first, persisting atomically, then writing checkpoint artifact.
- persisted checkpoints record concrete `checkpoint_sequence` from pre-increment `next_checkpoint_sequence`.
- reserved indices are never reused after interruption/partial-write; gaps are allowed.
- minimum atomic write strategy:
  - write temp file in same directory,
  - flush/fsync temp content,
  - rename/replace target atomically.
- `current_step_index` increments on successful/skip step transition only.

## Variable-Length Mechanism
- Runner materializes a linear run-local execution queue from workflow definition.
- Route-hook expansion appends additional steps to queue tail only.
- `route_hook_key` must satisfy `^[a-z0-9][a-z0-9-]{0,56}$`.
- Route-hook appended step keys are deterministic: `<route_hook_key>-r<NNNN>` (1-based ordinal per route hook per run).
- Expanded effective queue is persisted in `run.json` before appended-step execution.
- Existing queue order is immutable in Plan 2 (no insertion/reordering).

## Continuity Handle Capability Rule
- If continuity handle capability is supported and expected, null/empty/malformed handle is `continuity_handle_invalid`.
- If continuity handle capability is unsupported, missing handle is non-failure.
