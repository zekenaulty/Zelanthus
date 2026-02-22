# 0003-runner-policy-and-state-deltas-v0

## Status
- `proposed`

## Decision
- Naming and semantic axis realignment is immediate in this plan; it is not deferred.
- Authoritative axes for new planning docs in this plan:
  - `WorkflowTopology`: `DaisyChain` | `TransitionGraph`
  - `WorkflowNodeExecutionKind`: `llm_task` | `code_task`
  - `LlmTaskMode` (for `llm_task` nodes only): `single_call` | `cognitive_chain` | `conversational_chain`
- This baseline targets `WorkflowTopology.TransitionGraph` as the flow model.
- Runner invocation policy remains separate from node mode semantics:
  - optional `single_node_invocation` control.
- Legacy terms remain referenceable only for historical traceability mapping; they are not authoritative for new docs in this plan.
- Runner state/resume policy extends existing deterministic model by adding explicit node/transition concerns.

## Rationale
- Eliminates the ambiguity that previously caused semantic drift across plan/code/proof artifacts.
- Aligns contracts with required behavior (ANY-node to ANY-node transitions defined by data).
- Preserves audit continuity by mapping legacy terms rather than rewriting historical completed artifacts.

## Invariants
- New/updated docs in this plan must not use the following as authoritative terms:
  - `WorkflowKind`
  - `StepKind` (as mixed execution/topology meaning)
  - `route`/`routing` when flow-control semantics are intended.
- Node contract validation rules:
  - `WorkflowNodeExecutionKind.llm_task` requires `prompt_ref`, `llm_task_mode`, and `node_config_ref`.
  - `WorkflowNodeExecutionKind.code_task` requires `code_task_key` and `node_config_ref`.
- Topology/definition rules:
  - `WorkflowTopology.TransitionGraph` requires explicit transition rule sets per executable node.
  - transition decisions are committed before next-node execution and are not re-evaluated on resume.
- Resume cursor contract in this slice must remain deterministic and include node progression state sufficient to avoid filesystem inference.
- One primary reason code per deterministic failure outcome.

## Non-Goals
- No immediate repository-wide historical rewrite of completed artifacts.
- No replacement of current run lifecycle with scheduler-centric engine states in this slice.
- No expansion into distributed orchestration concerns.

## Consequences
- New draft/implementation planning can proceed on single-source semantics.
- Refactor/migration work is explicit and auditable instead of implicit and drift-prone.
- TransitionGraph contract work can grow without re-litigating axis naming.
