# workflow-runner-single-call-and-transition-graph-baseline

## Objective
- Define the next thin orchestration baseline by introducing deterministic transition-graph execution with data-defined flow control.
- Lock naming and semantic axes now so future plans and implementation cannot drift on term meaning.
- Preserve artifact-first correctness, deterministic replay, and strict reason-coded failures while keeping runtime scope intentionally small.

## Locked Terms and Axes
- `WorkflowTopology` describes workflow shape/topology only.
- `WorkflowNodeExecutionKind` describes node execution type only (`LlmTask`, `CodeTask`).
- `LlmTaskMode` describes LLM node mode only (`CognitiveChain`, `ConversationalChain`, `SingleCall`).
- These names supersede legacy `WorkflowKind`/`StepKind` usage.
- Canonical crosswalk reference: `Plans/terminology-supersession-map.md`.

## Scope
- In:
  - `WorkflowTopology` contract with explicit `TransitionGraph` semantics for this baseline.
  - Node contract alignment (`WorkflowNodeDefinition`, `node_key`, `node_instance_key`).
  - Transition evaluation contract where ANY node can transition to ANY declared node via data-defined rules.
  - Node execution axis split:
    - `WorkflowNodeExecutionKind` (`llm_task`, `code_task`),
    - `LlmTaskMode` (`single_call`, `cognitive_chain`, `conversational_chain`),
    - invocation option (`single_node_invocation`) as a separate runner concern.
  - Transition decision persistence and resume non-redecision policy.
  - Carry-forward backlog contract updates for transition-graph and node-mode hardening continuity.
- Out:
  - DAG planner, graph optimizer, or parallel/distributed scheduler design.
  - Runtime expression DSL for transition predicates.
  - Postgres persistence implementation.
  - API endpoint expansion or host redesign.
  - Multi-provider transition-strategy expansion.

## Definition of Done
- Decision docs exist and are internally consistent for:
  - single-call node execution,
  - transition decision contract,
  - runner policy/state deltas,
  - carry-forward backlog contract.
- Terminology supersession mapping is explicit and complete.
- No ambiguous authoritative use of legacy terms (`WorkflowKind`, `StepKind`, `route/routing`) outside explicit legacy mapping context.
- Transition decision persistence and resume non-redecision policy are explicit in plan contracts.
- Reason-code baseline for transition failures is pinned and deterministic.

## Cross-Plan Dependencies
- Depends on:
  - `Plans/README.md`
  - `Plans/Brainstorms/backbone-doctrine-prompt-first-multi-provider/plan.md`
  - `Plans/Brainstorms/backbone-doctrine-prompt-first-multi-provider/decisions/0001-backbone-doctrine-v0.md`
  - `Plans/Brainstorms/backbone-doctrine-prompt-first-multi-provider/decisions/0002-chain-continuity-strategy-v0.md`
  - `Plans/Completed/thin-clients-first-solution-structure/implementation/plan.md`
  - `Plans/Completed/mvp-prompting-gemini-contract-baseline/implementation/plan.md`
  - `Plans/Completed/mvp-chain-runner-local-persistence-baseline/implementation/plan.md`
- Supersedes (optional):
  - none

## Interfaces and Contracts Introduced/Changed
- Contract: `Single-call node execution contract (planning-level)`
  - Owner: `Zelanthus.StoryEngine.Application`
  - Change Type: `introduced`
  - Notes: Defines node-level single-call LLM behavior independent from runner invocation policy.
- Contract: `Transition decision contract (planning-level)`
  - Owner: `Zelanthus.StoryEngine.Application`
  - Change Type: `introduced`
  - Notes: Defines deterministic transition table behavior (`goto_node_key`, `complete_run`, `terminal_fail`) with persisted decision evidence.
- Contract: `Runner policy/state delta contract (planning-level)`
  - Owner: `Zelanthus.StoryEngine.Domain` and `Zelanthus.StoryEngine.Application`
  - Change Type: `introduced`
  - Notes: Defines immediate semantic axis split and deterministic resume behavior for transition-graph execution.
- Contract: `Terminology supersession mapping (planning-level)`
  - Owner: `Planning workflow`
  - Change Type: `introduced`
  - Notes: Defines legacy-to-current naming authority and audit-safe mapping rules.
- Contract: `Carry-forward backlog contract (planning-level)`
  - Owner: `Planning workflow`
  - Change Type: `introduced`
  - Notes: Ensures proof-only behavior hardening remains tracked across plan phase transitions.

## Git Branch and PR Tracking
- Execution Branch: `not-applicable (Brainstorm on main)`
- Base Branch: `main`
- PR: `not-applicable`

## Touchpoint Map
- Code:
  - `Source/Zelanthus.StoryEngine.Application/Orchestration/WorkflowRunner.cs` (analysis target)
  - `Source/Zelanthus.StoryEngine.Domain/Workflows/WorkflowDefinition.cs` (legacy analysis target -> topology/transition model alignment)
  - `Source/Zelanthus.StoryEngine.Domain/Workflows/WorkflowStepDefinition.cs` (legacy analysis target -> node contract alignment)
  - `Source/Zelanthus.StoryEngine.Domain/Workflows/WorkflowKind.cs` (legacy analysis target -> topology axis alignment)
  - `Source/Zelanthus.StoryEngine.Domain/Workflows/StepKind.cs` (legacy analysis target -> phase role vs execution kind split)
  - `Source/Zelanthus.StoryEngine.Infrastructure/Storage/Local/WorkflowRunPaths.cs` (analysis target)
  - `Tests/Zelanthus.WorkflowContractProofs.Tests/ChainRunnerContractProofTests.cs` (analysis target)
- Docs:
  - `Plans/Brainstorms/workflow-runner-single-call-and-transition-graph-baseline/plan.md`
  - `Plans/Brainstorms/workflow-runner-single-call-and-transition-graph-baseline/steps/index.md`
  - `Plans/Brainstorms/workflow-runner-single-call-and-transition-graph-baseline/notes/carry-forward-backlog.md`
  - `Plans/Brainstorms/workflow-runner-single-call-and-transition-graph-baseline/notes/terminology-supersession-map.md`
  - `Plans/Brainstorms/workflow-runner-single-call-and-transition-graph-baseline/artifacts/research-source-index.md`
  - `Plans/Brainstorms/workflow-runner-single-call-and-transition-graph-baseline/decisions/0001-single-call-node-execution-contract-v0.md`
  - `Plans/Brainstorms/workflow-runner-single-call-and-transition-graph-baseline/decisions/0002-transition-decision-contract-v0.md`
  - `Plans/Brainstorms/workflow-runner-single-call-and-transition-graph-baseline/decisions/0003-runner-policy-and-state-deltas-v0.md`
  - `Plans/Brainstorms/workflow-runner-single-call-and-transition-graph-baseline/decisions/0004-carry-forward-backlog-contract-v0.md`
  - `Plans/Brainstorms/workflow-runner-single-call-and-transition-graph-baseline/risks/risk-log.md`
- Infrastructure/Config:
  - none in brainstorm stage

## Risks and Mitigations
- Risk: TransitionGraph scope expands into full orchestration-engine complexity.
  - Mitigation: Keep non-goals explicit; reject scheduler/DAG/parallelization scope in this slice.
- Risk: Naming drift between legacy and current terms causes implementation ambiguity.
  - Mitigation: Enforce supersession mapping and ban ambiguous authoritative terms in new docs.
- Risk: Transition decisions become non-deterministic across retries/resume.
  - Mitigation: Persist transition decisions before next-node execution and disallow re-decision on resume.
- Risk: Proof contracts drift away from runtime doctrine hardening.
  - Mitigation: Carry-forward backlog gate remains mandatory (`WF-BL-001`).

## Validation and Testing
- Automated:
  - not-run (docs-only brainstorm)
- Manual:
  - Verify decision docs include accepted/rejected alternatives and deterministic failure behavior.
  - Verify transition contract supports data-defined ANY-node to ANY-node flow within declared node set.
  - Verify terminology audit checklist passes with no ambiguous authoritative terms.
  - Verify carry-forward backlog trigger/exit criteria are explicit.

## Rollout / Rollback
- Rollout:
  - Complete brainstorm decisions and evidence.
  - Promote to `Drafts` only after user approval and terminology/contract consistency review.
  - Use draft output to sequence implementation plans without expanding runtime scope beyond this baseline.
- Rollback:
  - Archive this brainstorm under `Plans/Archived/Brainstorms/` with `archive-note.md` if superseded.

## Status Tracker
- [ ] `0010-inventory-current-runner-and-proof-surfaces`
- [ ] `0020-extract-bookforge-phase-and-transition-patterns`
- [ ] `0030-map-cognition-workflow-concepts-and-anti-patterns`
- [ ] `0040-define-single-call-and-transition-graph-contracts`
- [ ] `0050-capture-carry-forward-backlog-and-promotion-gates`

## Notes
- TransitionGraph in this baseline means deterministic sequential execution with data-defined transition edges, not a DAG planner.
- Flow changes must come from data/metadata edits to transition rules, not orchestration code edits.
- Naming axes are locked for this plan:
  - topology: `WorkflowTopology`
  - node execution kind: `WorkflowNodeExecutionKind`
  - LLM node mode: `LlmTaskMode`
  - node identity: `node_key`
  - node occurrence identity: `node_instance_key`

## Terminology Alignment Update
- This plan was realigned from ambiguous legacy naming to locked canonical axes.
- Flow-control semantics now use `transition` terminology and TransitionGraph contracts.
- Legacy terminology references are preserved only where needed for audit mapping and legacy-surface analysis.
