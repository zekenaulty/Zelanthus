# Minimal Workflow Abstraction Contract

## Scope
- Plan: `mvp-chain-runner-local-persistence-baseline`
- Step: `0025-define-minimal-workflow-abstraction-hooks`

## Contract Sketch
```text
WorkflowDefinition
  workflow_key
  workflow_kind (CognitiveChain | ConversationalChain)
  workflow_version (int)
  steps[] -> WorkflowStepDefinition

WorkflowStepDefinition
  step_key
  step_kind (PLAN_STEP | EXECUTE | CONVERSATION_STEP)
  prompt_ref
    prompt_id
    prompt_version
  input_contract_ref
  output_contract_ref
  route_hook_key? (optional)

WorkflowRunContext
  run_id
  workflow_key
  workflow_version
  chain_mode
  run_state
  current_step_index
  last_success_step_index
  next_turn_index
  next_checkpoint_sequence
  latest_thinking_persistence_key? (optional)
```

## Key Rules
- workflow_kind: enum only (`CognitiveChain` | `ConversationalChain`)
- workflow_key: stable semantic identity; must not include version
- workflow_version: integer version marker
- step_key: workflow-step identity, path-safe (`^[a-z0-9][a-z0-9-]{0,63}$`)
- route_hook_key: path-safe (`^[a-z0-9][a-z0-9-]{0,56}$`) so generated step key `<route_hook_key>-r<NNNN>` remains valid
- prompt_ref: required per step in Plan 2 (`prompt_id`, `prompt_version`)
- prompt_id is separate identity from step_key and remains owned by Prompting
- missing/invalid prompt_ref for required step execution is deterministic terminal failure `missing_prompt_reference`
- chain_mode must match workflow_kind for active workflow definition
- workflow_kind/chain_mode mismatch is deterministic terminal failure `invalid_state_transition`
- next_turn_index/next_checkpoint_sequence: monotonic run counters for deterministic persistence/replay

## Guardrails
- Keep workflow model linear-sequence first.
- Keep route hooks deterministic and minimal for MVP.
- Do not introduce graph-engine orchestration and generic DSL complexity in Plan 2.
- Keep abstraction depth intentionally below prior cognition meta-model scope while preserving clear route-hook seams.
- Track provider continuity handle as rolling step state (`latest_thinking_persistence_key`), not a fixed per-run constant.
- Route-hook expansions append run-local queue tail only; existing step order is immutable for Plan 2.
- Route-hook appended step keys are deterministic: `<route_hook_key>-r<NNNN>` (1-based ordinal per route hook per run).
- Route-hook appended steps include required prompt_ref before persistence/execution.
- Expanded effective queue state is persisted in `run.json` before appended steps execute.

## Future Hook Intent
- Supports future plans for richer routing/branching by extending:
  - `route_hook_key` behavior,
  - step transition policy,
  - optional conditional edges.

## Reference Inputs
- `References/cognition/src/Cognition.Workflows`
- `References/cognition/src/Cognition.Domains`
- `References/bookforge/resources/plans/lint_repair_split_routing_plan_20260213_183000.md`
