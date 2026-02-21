# Decision 0001: Chain Runner Execution Model v0

## Status
- accepted

## Decision Summary
- Plan 2 runner must support variable-length execution for both:
  - `CognitiveChain`
  - `ConversationalChain`
- `PLAN_STEP` -> `EXECUTE` remains a minimum cognitive unit, not a hard cap on chain length.
- Runner correctness is artifact-first: explicit persisted state drives resume and validation.

## Contract Shape
- Chain run contract includes:
  - run identity and correlation IDs,
  - workflow identity (`workflow_key`, `workflow_version`),
  - chain mode,
  - ordered turn metadata,
  - current state and checkpoint references.
- Turn contract includes:
  - turn objective,
  - required inputs/artifacts,
  - output artifact references,
  - validation/failure status.
- Workflow step kinds include:
  - `PLAN_STEP`
  - `EXECUTE`
  - `CONVERSATION_STEP`

## State Enums
- `RunState`:
  - `created`
  - `running`
  - `waiting_retry`
  - `succeeded`
  - `failed_terminal`
  - `cancelled`
- `TurnState`:
  - `pending`
  - `running`
  - `succeeded`
  - `failed_retryable`
  - `failed_terminal`
  - `skipped`

## Transition Table (Plan 2 Minimum)
| Current `RunState` | Event | Next `RunState` |
|---|---|---|
| `created` | `start_run` | `running` |
| `running` | `turn_failed_retryable` | `waiting_retry` |
| `waiting_retry` | `retry_started` | `running` |
| `running` | `run_completed` | `succeeded` |
| `running` | `run_failed_terminal` | `failed_terminal` |
| `waiting_retry` | `retry_budget_exhausted` | `failed_terminal` |
| `created` / `running` / `waiting_retry` | `cancel_requested` | `cancelled` |

| Current `TurnState` | Event | Next `TurnState` |
|---|---|---|
| `pending` | `start_turn` | `running` |
| `running` | `turn_completed` | `succeeded` |
| `running` | `turn_failed_retryable` | `failed_retryable` |
| `running` | `turn_failed_terminal` | `failed_terminal` |
| `pending` | `skip_turn` | `skipped` |

## Illegal Transitions
- Any state/event pair not listed in the transition table is illegal.
- Illegal transition outcome is deterministic terminal failure with reason code `invalid_state_transition`.

## Resume Cursor Contract
- Authoritative resume cursor fields:
  - `workflow_key`
  - `workflow_version`
  - `chain_mode`
  - `current_step_index`
  - `last_success_step_index`
  - `next_turn_index`
  - `next_checkpoint_sequence`
  - `run_state`
  - optional `latest_thinking_persistence_key`
- Counter behavior:
  - `start_turn` reserves `turn_index` first:
    - assign `turn_index = next_turn_index`,
    - increment `next_turn_index`,
    - atomically persist `run.json`,
    - then write turn artifacts.
  - `write_checkpoint` reserves `checkpoint_sequence` first:
    - assign `checkpoint_sequence = next_checkpoint_sequence`,
    - increment `next_checkpoint_sequence`,
    - atomically persist `run.json`,
    - then write checkpoint artifact.
  - reserved indices are never reused after interruption/partial-write; gaps are allowed.
  - `current_step_index` advances on successful/skip transition and is stable during retries.
- Resume behavior:
  - `CognitiveChain` ignores mid-chain cursor for continuation and restarts from first `PLAN_STEP`.
  - `ConversationalChain` uses `last_success_step_index` cursor to continue from the next required step.

## Invariants
- `EXECUTE` cannot execute without required upstream planning artifact(s).
- any step selected for provider invocation must have valid `prompt_ref` (`prompt_id`, `prompt_version`).
- `chain_mode` must match `workflow_kind` for active workflow definition.
- `workflow_kind`/`chain_mode` mismatch is deterministic terminal failure `invalid_state_transition`.
- Cognitive mode may stage multiple `PLAN_STEP` units before one or more `EXECUTE` units.
- Cognitive mode carries forward the latest `thinking_persistence_key` returned by each provider response (rolling continuity handle semantics).
- Continuity handle may rotate on both `PLAN_STEP` and `EXECUTE` responses.
- Continuity capability-conditioned handling:
  - if capability indicates continuity support and handle is expected, null/empty/malformed handle is `continuity_handle_invalid`.
  - if capability indicates no continuity support, missing handle is normal and non-failing.
- Conversational turns still require per-turn validation and persistence.
- Chain routing may expand step count based on workflow need and failure/retry policy.
- Variable-length mechanism uses run-local linear execution queue:
  - initialize queue from `WorkflowDefinition`,
  - append additional steps to queue tail only when route-hook policy requests expansion,
  - route-hook appended steps use deterministic generated keys: `<route_hook_key>-r<NNNN>` (1-based ordinal per route hook per run),
  - `route_hook_key` must satisfy `^[a-z0-9][a-z0-9-]{0,56}$` so generated step keys satisfy step-key path safety,
  - persist expanded effective queue in `run.json` before executing appended steps,
  - no mid-queue insertion or reordering in Plan 2.
- Resume policy:
  - `CognitiveChain` restarts from first `PLAN_STEP`.
  - `ConversationalChain` resumes from last successful persisted step.

## Consequences
- Plan 2 implementation must avoid fixed single-pair assumptions and use `PLAN_STEP/EXECUTE` semantics explicitly.
- Proof tests must include at least one variable-length chain scenario.
- Proof tests must include crash/partial-write reservation scenarios that prove no turn/checkpoint index reuse.
- Proof tests must include missing/invalid step `prompt_ref` deterministic failure behavior.
