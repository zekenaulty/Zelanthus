# Terminology Supersession Map

## Purpose
- Preserve audit clarity by mapping legacy terms to current authoritative terms without rewriting historical completed artifacts.

## Authority
- Current authoritative semantics in this plan are immediate and non-deferred.
- Legacy terms are allowed only when:
  - quoting historical artifacts,
  - referencing existing code surfaces under analysis,
  - documenting migration/supersession context.

## Mapping
| Legacy Term | Historical Meaning | Current Authoritative Term | Current Meaning | Notes |
|---|---|---|---|---|
| `WorkflowKind` | Mixed/overloaded process behavior label | `WorkflowTopology` | Process topology (`DaisyChain`, `TransitionGraph`) | This baseline uses `TransitionGraph`. |
| `WorkflowStepDefinition` | Step-level execution contract | `WorkflowNodeDefinition` | Node-level execution contract | Node semantics are authoritative for new docs in this plan. |
| `step_key` | Step identity | `node_key` | Stable semantic node identity | Remains stable across runs. |
| `step_instance_key` | Step occurrence identity | `node_instance_key` | Per-run node occurrence identity | Required for deterministic replay evidence. |
| `StepKind` (mixed use) | Phase + execution concepts conflated | `WorkflowNodeExecutionKind` + `phase_role` | Execution kind is separate from phase role | Do not merge execution kind and phase role semantics. |
| `ChainMode` | LLM interaction mode | `LlmTaskMode` | `single_call`, `cognitive_chain`, `conversational_chain` | Applies only when node execution kind is `llm_task`. |
| `SingleShot` / `single_shot` | Ambiguous single-turn/single-node/single-call term | `single_call` + `single_node_invocation` | Node behavior mode is distinct from invocation policy | Keep these concerns separate. |
| `route` / `routing` / `route hook` | Ambiguous flow-control naming | `transition` / `transition rule` / `transition hook` | Deterministic edge evaluation in transition graph | Use transition terms for flow-control contracts. |

## Usage Rule
- New planning docs in this plan folder must use current authoritative terms.
- Historical references in completed plans must not be rewritten; map them through this table.
