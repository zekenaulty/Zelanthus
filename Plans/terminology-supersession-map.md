# Terminology Supersession Map

## Canonical Axes (Authoritative)
- `WorkflowTopology`
  - Meaning: workflow/process shape only.
  - Scope: process-level topology (for example `DaisyChain`, `TransitionGraph`).
- `WorkflowNodeExecutionKind`
  - Meaning: node execution type only.
  - Scope: node runtime type (`LlmTask`, `CodeTask`).
- `LlmTaskMode`
  - Meaning: LLM node mode only.
  - Scope: only applies when `WorkflowNodeExecutionKind = LlmTask`.
  - Values: `CognitiveChain`, `ConversationalChain`, `SingleCall`.

## Rename Crosswalk
| OldTerm | OldMeaning (as previously used) | NewTerm | NewMeaning | Where It Applies | Effective From (Plan + Commit) |
|---|---|---|---|---|---|
| `WorkflowKind` | Overloaded workflow/chain behavior label | `WorkflowTopology` | Workflow shape/topology only | Legacy docs and prior code references in StoryEngine workflow artifacts | `workflow-runner-single-call-and-transition-graph-baseline` + pending doc-retcon commit |
| `StepKind` | Mixed phase-role and execution-kind label | `WorkflowNodeExecutionKind` + `phase_role` | Execution kind and phase role are separate concerns | Legacy docs and prior code references in StoryEngine workflow artifacts | `workflow-runner-single-call-and-transition-graph-baseline` + pending doc-retcon commit |
| `ChainMode` | Mixed usage across workflow and LLM behavior language | `LlmTaskMode` | LLM node mode only | Legacy docs and prior code references in LLM abstractions and StoryEngine docs | `mvp-prompting-gemini-contract-baseline` (`37a32635a968954bdf2d775977e940ee766278f0`), superseded globally by current retcon |
| `SingleShot` / `single_shot` | Ambiguous single-turn/single-node/single-call meaning | `SingleCall` + `single_node_invocation` | Node behavior mode vs invocation policy are separate | Legacy planning docs and transition discussions | `workflow-runner-single-call-and-transition-graph-baseline` + pending doc-retcon commit |
| `route` / `routing` (flow-control meaning) | Ambiguous list-append and next-step selection wording | `transition` / `phase transition rules` | Deterministic next-node selection rules | Legacy planning docs and chain-runner planning artifacts | `workflow-runner-single-call-and-transition-graph-baseline` + pending doc-retcon commit |

## Do Not Use (Deprecated Ambiguous Terms)
- `WorkflowKind` (legacy overloaded)
- `StepKind` (legacy overloaded)
- `routing` when it means list-append flow control
  - Prefer `transition` or `phase transition rules` when selecting the next node.

## Retcon Policy
- Completed plan artifacts are historical records.
- Do not rewrite historical tokens to new names.
- Add inline `(RENAMED TO: ...)` and `(MEANING NOW: ...)` annotations at first occurrence of legacy terms in completed docs.
- New active planning docs must use canonical terms directly.
