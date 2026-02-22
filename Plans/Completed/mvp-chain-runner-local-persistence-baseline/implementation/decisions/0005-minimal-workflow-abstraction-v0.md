# Decision 0005: Minimal Workflow Abstraction v0

## Status
- accepted

## Decision Summary
- Plan 2 introduces a minimal workflow abstraction that is:
  - workflow-aware,
  - linear-sequence first,
  - route-hook capable,
  - not a full meta workflow-engine.
- Abstraction supports current two chain modes and future expansion via data/code without immediate graph-engine complexity.

## Minimal Contract Shape
- `WorkflowDefinition`
  - `workflow_key`
  - `workflow_kind` (`CognitiveChain` or `ConversationalChain`)
  - `workflow_version` (integer)
  - ordered `WorkflowStepDefinition[]`
- `WorkflowStepDefinition`
  - `step_key`
  - `step_kind` (`PLAN_STEP`, `EXECUTE`, `CONVERSATION_STEP`)
  - required `prompt_ref`:
    - `prompt_id`
    - `prompt_version`
  - `input_contract_ref`
  - `output_contract_ref`
  - optional `route_hook_key`
- `WorkflowRunContext`
  - `run_id`
  - `workflow_key`
  - `workflow_version`
  - `chain_mode`
  - `run_state`
  - `current_step_index`
  - `last_success_step_index`
  - `next_turn_index`
  - `next_checkpoint_sequence`
  - optional `latest_thinking_persistence_key`

## Key Semantics
- `workflow_kind` is enum-only and must not be treated as free-form text.
- `workflow_key` is a stable semantic identifier and must not embed version.
- `workflow_version` is the only workflow version marker and is an integer.
- `step_key` identifies workflow step identity and must not be reused as prompt identity.
- `prompt_ref` is required for Plan 2 step execution and directly carries prompt identity/version in each step.
- `prompt_id` remains prompt-template identity owned by `Zelanthus.Prompting`.
- `step_key` path-safe rule: `^[a-z0-9][a-z0-9-]{0,63}$`.
- `route_hook_key` path-safe rule: `^[a-z0-9][a-z0-9-]{0,56}$` so generated step keys remain valid (`<route_hook_key>-r<NNNN>`).
- missing/invalid `prompt_ref` for required step execution is deterministic terminal failure `missing_prompt_reference`.
- `chain_mode` must match `workflow_kind` for the active run definition.
- `workflow_kind`/`chain_mode` mismatch is deterministic terminal failure `invalid_state_transition`.

## Cognitive Continuity Tracking Rule
- `latest_thinking_persistence_key` stores the most recently returned provider continuity handle (for example `thoughtSignature`).
- Provider handles are rolling values and may change on each step.
- Cognitive requests pass forward the latest persisted handle when provider capability supports it.
- If provider capability indicates continuity unsupported, missing handle is non-failure and execution proceeds without continuity state.

## Route Hook Policy
- Route hooks are extension seams only for MVP:
  - simple deterministic routing decisions,
  - no full branching graph orchestration in Plan 2.
- Route-hook expansion behavior in Plan 2:
  - route hooks may append additional steps to run-local execution queue tail only,
  - generated step keys for appended steps are deterministic: `<route_hook_key>-r<NNNN>` (1-based ordinal per route hook per run),
  - appended steps must include required `prompt_ref` before they are persisted/executed,
  - expanded effective queue (including generated step keys) must be persisted in `run.json` before executing appended steps,
  - no insertion/reordering of already-materialized queue steps.
- Future plans may expand route hooks into richer branching/topology support.

## Guardrails
- Avoid generic "system to model systems" abstractions in Plan 2.
- Keep workflow abstraction focused on enabling:
  - current chain runner correctness,
  - deterministic persistence/replay,
  - future workflow extensibility without refactoring core boundaries.

## Consequences
- Plan 2 orchestration contracts must reference workflow definitions and step definitions explicitly.
- Proof tests should include at least one route-hook aware scenario (without full graph branching).
