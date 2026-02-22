# 0002-transition-decision-contract-v0

## Status
- `proposed`

## Decision
- Flow control is modeled as deterministic transition evaluation over declared nodes.
- This baseline uses `WorkflowTopology.TransitionGraph`.
- ANY node can transition to ANY declared node via data-defined transition rules.
- Transition predicates are evaluated only from structured node outcome data.
- Transition actions are bounded to:
  - `goto_node_key`
  - `complete_run`
  - `terminal_fail`
- Transition rule evaluation order is explicit and deterministic (first match wins by configured order/priority).
- A default rule is required per node; if absent or no rule matches, fail deterministically with `transition_no_match`.
- Chosen transition decision must be persisted before next node execution.
- Resume must never re-evaluate already committed transition decisions.

## Rationale
- Matches required process semantics: flow changes come from data edits, not orchestration code edits.
- Supports lint/repair/lint and optional-branch process patterns without introducing scheduler complexity.
- Keeps replay/audit behavior deterministic by pinning decision inputs, actions, and persistence semantics.

## Invariants
- Transition predicates may inspect only:
  - `status`
  - `reason_code`
  - typed `flags`
  - optional `output_contract_id`
- Identity split is mandatory:
  - `node_key` is stable semantic identity,
  - `node_instance_key` is per-run occurrence identity.
- `goto_node_key` target must exist in declared node set; otherwise fail deterministically with `transition_invalid_action`.
- Every executable node definition must be fully bound:
  - `llm_task` node: prompt reference + `llm_task_mode` + node config reference,
  - `code_task` node: code task key + node config reference.
- Transition decision artifact is required per executed node instance:
  - persisted as `transition-decision.json` in the node-instance turn folder,
  - includes evaluated rule id, normalized input summary, selected action, selected target (if any), and effective-definition checksum/hash.
- Loop safety is mandatory:
  - `max_total_nodes_executed_per_run`,
  - `max_visits_per_node_key`,
  - deterministic terminal failure when limits are exceeded (`max_iterations_exceeded`).
- One primary reason code per deterministic transition failure.
- Transition failure baseline in this slice:
  - `transition_no_match`
  - `transition_invalid_action`
  - `max_iterations_exceeded`

## Non-Goals
- No DAG optimizer, distributed scheduler, or parallel-node execution in this slice.
- No dynamic expression DSL for transition predicates in this slice.
- No transition predicate evaluation from raw LLM prose.

## Consequences
- Process flow can be changed by editing transition data/metadata without orchestration code changes.
- Runner implementation can stay as a deterministic sequential loop:
  - execute node,
  - summarize outcome,
  - evaluate transition,
  - persist decision,
  - move pointer.
- Future capabilities can layer on top of this contract without reintroducing naming ambiguity.
