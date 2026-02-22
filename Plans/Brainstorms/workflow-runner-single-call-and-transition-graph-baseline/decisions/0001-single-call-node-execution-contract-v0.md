# 0001-single-call-node-execution-contract-v0

## Status
- `proposed`

## Decision
- `LlmTaskMode.single_call` is a node-level LLM behavior mode and means one provider call for that node attempt.
- Runner invocation control is separate:
  - `single_node_invocation` means execute exactly one node instance and return.
  - absence of `single_node_invocation` means continue process flow according to transition decisions.
- Node execution uses `WorkflowNodeDefinition` as the authoritative contract shape in this plan.
- Node binding requirements are explicit:
  - `llm_task` nodes require `prompt_ref { prompt_id, prompt_version }`, `llm_task_mode`, and `node_config_ref`.
  - `code_task` nodes require `code_task_key` and `node_config_ref`.
- This slice does not introduce a separate single-call request object; single-call behavior is carried by node mode.

## Rationale
- Separates node behavior from invocation policy so each concern can evolve without semantic collisions.
- Prevents recurrence of overloaded single-call terminology.
- Keeps runtime thin by reusing existing runner/request surfaces while tightening contract meaning.

## Invariants
- `single_call` does not imply `single_node_invocation`; these are orthogonal controls.
- `single_node_invocation` does not imply `single_call`; it may execute any node mode.
- `llm_task` nodes without `prompt_ref` fail deterministically with `missing_prompt_reference`.
- Node execution correctness never depends on hidden provider state.
- Continuity handle behavior remains optional and capability-conditioned for LLM modes that can use it.
- One primary reason code per deterministic failure outcome.
- Node attempts persist through existing run/turn/checkpoint/failure artifact boundaries.

## Non-Goals
- No separate `SingleCallExecutionRequest` contract in this slice.
- No new API host or endpoint surface for single-call behavior in this slice.
- No provider-specific execution branching outside normalized contracts.

## Consequences
- Single-call can coexist with cognitive/conversational modes without overloading topology semantics.
- Invocation policy and node behavior can be tested independently in draft/implementation planning.
- Future mode additions remain tractable because axes are no longer conflated.
