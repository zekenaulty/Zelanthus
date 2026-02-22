# workflow-runner-single-call-and-transition-graph-baseline

## Compiled Plan Metadata

- Plan Scope: `Brainstorms/workflow-runner-single-call-and-transition-graph-baseline`
- Compiled At (UTC): `2026-02-22T07:55:19Z`
- Source Document Count: `23`
- Projection File: `workflow-runner-single-call-and-transition-graph-baseline.md`

## Contents

1. `plan.md`
2. `decisions/0001-single-call-node-execution-contract-v0.md`
3. `decisions/0002-transition-decision-contract-v0.md`
4. `decisions/0003-runner-policy-and-state-deltas-v0.md`
5. `decisions/0004-carry-forward-backlog-contract-v0.md`
6. `risks/risk-log.md`
7. `validation/brainstorm-to-draft-gate-checklist.md`
8. `steps/index.md`
9. `steps/0010-inventory-current-runner-and-proof-surfaces/step.md`
10. `steps/0020-extract-bookforge-phase-and-transition-patterns/step.md`
11. `steps/0030-map-cognition-workflow-concepts-and-anti-patterns/step.md`
12. `steps/0040-define-single-call-and-transition-graph-contracts/step.md`
13. `steps/0050-capture-carry-forward-backlog-and-promotion-gates/step.md`
14. `steps/0010-inventory-current-runner-and-proof-surfaces/artifacts/current-runner-surface-inventory.md`
15. `steps/0010-inventory-current-runner-and-proof-surfaces/notes/proof-vs-runtime-gap-notes.md`
16. `steps/0020-extract-bookforge-phase-and-transition-patterns/artifacts/bookforge-pattern-extract.md`
17. `steps/0020-extract-bookforge-phase-and-transition-patterns/notes/bookforge-non-transferable-patterns.md`
18. `steps/0030-map-cognition-workflow-concepts-and-anti-patterns/artifacts/cognition-concept-map.md`
19. `steps/0030-map-cognition-workflow-concepts-and-anti-patterns/notes/cognition-anti-pattern-guardrails.md`
20. `notes/carry-forward-backlog.md`
21. `notes/terminology-supersession-map.md`
22. `archive-note.md`
23. `artifacts/research-source-index.md`

---

## Source 1: `plan.md`

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

---

## Source 2: `decisions/0001-single-call-node-execution-contract-v0.md`

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

---

## Source 3: `decisions/0002-transition-decision-contract-v0.md`

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

---

## Source 4: `decisions/0003-runner-policy-and-state-deltas-v0.md`

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

---

## Source 5: `decisions/0004-carry-forward-backlog-contract-v0.md`

# 0004-carry-forward-backlog-contract-v0

## Status
- `proposed`

## Decision
- Keep carry-forward backlog plan-scoped for MVP planning cycles, with explicit gate checks in each plan package.
- Enforce `WF-BL-001` as a mandatory continuity item for runner/orchestration evolution.
- Backlog mapping is required before Draft promotion for any plan that:
  - introduces a new execution kind or `LlmTaskMode`,
  - changes transition behavior/contracts,
  - changes runner state/resume/retry policy.

## Rationale
- Prevents loss of high-value hardening work while workspace-level backlog process is not yet formalized.
- Keeps backlog accountability close to planning scope and decision traceability.

## Invariants
- Every active backlog item must include:
  - owner scope,
  - trigger for promotion mapping,
  - explicit exit criteria.
- Draft promotion checklist must include backlog mapping validation.
- Completed package closeout must include backlog status update for affected items.
- One unresolved backlog item cannot be silently dropped by introducing new plan names/terms.

## Non-Goals
- No global cross-repo backlog system in this slice.
- No automated issue tracker integration in this slice.

## Consequences
- Planning remains lightweight while preserving continuity obligations.
- Critical hardening items stay visible across brainstorm -> draft -> in-progress transitions.
- Future workspace-wide backlog model can supersede this with explicit migration notes.

---

## Source 6: `risks/risk-log.md`

# Risk Log

## R-001 Scope Creep Beyond Minimal TransitionGraph
- Risk: TransitionGraph work expands into full orchestration-engine complexity (scheduler, parallelization, graph optimization).
- Impact: High
- Likelihood: Medium
- Mitigation: Keep non-goals explicit; reject scheduler/DAG/parallelization scope in this brainstorm.

## R-002 Naming Drift Reintroduces Ambiguity
- Risk: Legacy terms are used as authoritative semantics in new docs, causing contract drift.
- Impact: High
- Likelihood: Medium
- Mitigation: Enforce terminology supersession map and explicit acceptance checklist checks.

## R-003 Transition Non-Determinism on Resume
- Risk: Transition decisions are re-evaluated or inferred during resume, creating divergent behavior.
- Impact: High
- Likelihood: Medium
- Mitigation: Persist transition-decision artifacts before next-node execution; disallow re-decision on resume.

## R-004 Proof Drift vs Runtime Process
- Risk: Proof contracts exist without matching runtime process hardening.
- Impact: High
- Likelihood: High
- Mitigation: Maintain explicit carry-forward backlog entries and require Draft mapping before implementation.

## R-005 Overfitting to Reference Systems
- Risk: Importing BookForge/Cognition complexity that is not needed for this baseline.
- Impact: Medium
- Likelihood: Medium
- Mitigation: Accept only patterns with direct contract value and clear minimal implementation path.

---

## Source 7: `validation/brainstorm-to-draft-gate-checklist.md`

# Brainstorm to Draft Gate Checklist

- [ ] Decision docs `0001` through `0004` are populated with accepted/rejected alternatives.
- [ ] Single-call node contract includes explicit non-goals and deterministic failure behavior.
- [ ] Transition decision contract supports data-defined ANY-node to ANY-node flow within declared nodes.
- [ ] Transition failure reason-code baseline is explicitly pinned for MVP (`transition_no_match`, `transition_invalid_action`, `max_iterations_exceeded`).
- [ ] Transition decision persistence and resume non-redecision policy are explicit.
- [ ] Terminology supersession mapping exists and maps legacy terms to current authoritative terms.
- [ ] No ambiguous authoritative uses of `WorkflowKind`, `StepKind`, or `route/routing` remain in this plan bundle.
- [ ] Carry-forward backlog item `WF-BL-001` is mapped to draft implementation planning.
- [ ] Touchpoint map reflects real Zelanthus code surfaces.
- [ ] Risks and mitigations are reviewed and current.

---

## Source 8: `steps/index.md`

# Steps Index

Use this file for dependency and status summaries.
Do not use this file as the only source of step order.
Step order must still be visible in folder names (`NNNN-semantic-step-name`).

## Step Map
| Order | Step Folder | Status | Depends On | Notes |
|---|---|---|---|---|
| 0010 | `0010-inventory-current-runner-and-proof-surfaces` | `pending` | `none` | Inventory current Zelanthus contracts and proof coverage gaps. |
| 0020 | `0020-extract-bookforge-phase-and-transition-patterns` | `pending` | `0010` | Extract reusable phase/transition patterns from BookForge planning evidence. |
| 0030 | `0030-map-cognition-workflow-concepts-and-anti-patterns` | `pending` | `0010` | Map reusable seams and avoid over-complex patterns seen in Cognition. |
| 0040 | `0040-define-single-call-and-transition-graph-contracts` | `pending` | `0020`, `0030` | Draft deterministic single-call + transition-graph contract shape and constraints. |
| 0050 | `0050-capture-carry-forward-backlog-and-promotion-gates` | `pending` | `0040` | Lock backlog carry-forward and Draft promotion gate criteria. |

## Status Values
- `pending`
- `in_progress`
- `blocked`
- `completed`

---

## Source 9: `steps/0010-inventory-current-runner-and-proof-surfaces/step.md`

# Step: 0010-inventory-current-runner-and-proof-surfaces

## Goal
- Document the current execution contract surface in Zelanthus and identify proof-covered vs runtime-hardened gaps.

## Context
- We need a precise baseline before proposing single-call/transition-graph changes.

## Outputs
- `artifacts/current-runner-surface-inventory.md`
- `notes/proof-vs-runtime-gap-notes.md`

## Acceptance Evidence
- Inventory lists runner/domain/infrastructure/test touchpoints with clear gap flags.

## Git Branch
- `main`

## Commits
- `none`

## Commands Executed
- `pending`

## Files Changed
- `pending`

## Tests / Results
- `not-run` -> `docs-only step`

## Issues
- `none`

## Decision
- pending

## Completion
- `pending`

## Next Actions
- Feed inventory into BookForge/Cognition pattern mapping steps.

---

## Source 10: `steps/0020-extract-bookforge-phase-and-transition-patterns/step.md`

# Step: 0020-extract-bookforge-phase-and-transition-patterns

## Goal
- Extract concrete, reusable phase/transition process patterns from BookForge plans for Zelanthus contract refinement.

## Context
- BookForge contains high-signal process learnings around phase decomposition, deterministic transitioning, and artifact checkpoints.

## Outputs
- `artifacts/bookforge-pattern-extract.md`
- `notes/bookforge-non-transferable-patterns.md`

## Acceptance Evidence
- Pattern extract includes source links and explicit keep/reject rationale per pattern.

## Git Branch
- `main`

## Commits
- `none`

## Commands Executed
- `pending`

## Files Changed
- `pending`

## Tests / Results
- `not-run` -> `docs-only step`

## Issues
- `none`

## Decision
- pending

## Completion
- `pending`

## Next Actions
- Feed accepted patterns into contract design step.

---

## Source 11: `steps/0030-map-cognition-workflow-concepts-and-anti-patterns/step.md`

# Step: 0030-map-cognition-workflow-concepts-and-anti-patterns

## Goal
- Identify Cognition workflow/planner concepts we should reuse vs avoid in Zelanthus MVP evolution.

## Context
- Cognition has useful seams but also complexity we do not want to import directly.

## Outputs
- `artifacts/cognition-concept-map.md`
- `notes/cognition-anti-pattern-guardrails.md`

## Acceptance Evidence
- Concept map clearly separates reusable seams, deferred seams, and rejected complexity.

## Git Branch
- `main`

## Commits
- `none`

## Commands Executed
- `pending`

## Files Changed
- `pending`

## Tests / Results
- `not-run` -> `docs-only step`

## Issues
- `none`

## Decision
- pending

## Completion
- `pending`

## Next Actions
- Use mapped seams to constrain single-call and transition-graph contract scope.

---

## Source 12: `steps/0040-define-single-call-and-transition-graph-contracts/step.md`

# Step: 0040-define-single-call-and-transition-graph-contracts

## Goal
- Define deterministic contract shapes for single-call execution and transition-graph flow control without broad orchestration-engine expansion.

## Context
- We need explicit contract boundaries before drafting implementation plans.

## Outputs
- `Plans/Brainstorms/workflow-runner-single-call-and-transition-graph-baseline/decisions/0001-single-call-node-execution-contract-v0.md`
- `Plans/Brainstorms/workflow-runner-single-call-and-transition-graph-baseline/decisions/0002-transition-decision-contract-v0.md`
- `Plans/Brainstorms/workflow-runner-single-call-and-transition-graph-baseline/decisions/0003-runner-policy-and-state-deltas-v0.md`
- `Plans/Brainstorms/workflow-runner-single-call-and-transition-graph-baseline/notes/terminology-supersession-map.md`

## Acceptance Evidence
- Contracts include invariants, reason-code expectations, and explicit non-goals.
- Transition contract pins deterministic transition failure codes:
  - `transition_no_match`
  - `transition_invalid_action`
  - `max_iterations_exceeded`
- Terminology supersession mapping is explicit and links legacy terms to current authoritative terms.

## Git Branch
- `main`

## Commits
- `none`

## Commands Executed
- `pending`

## Files Changed
- `pending`

## Tests / Results
- `not-run` -> `docs-only step`

## Issues
- `none`

## Decision
- pending

## Completion
- `pending`

## Next Actions
- Capture carry-forward backlog and draft promotion gates.

---

## Source 13: `steps/0050-capture-carry-forward-backlog-and-promotion-gates/step.md`

# Step: 0050-capture-carry-forward-backlog-and-promotion-gates

## Goal
- Lock explicit carry-forward backlog handling and quality gates required before brainstorming work can promote to Draft.

## Context
- Current workflow has no formal global backlog process; this step creates plan-scoped continuity controls so important hardening work is not dropped.

## Outputs
- `Plans/Brainstorms/workflow-runner-single-call-and-transition-graph-baseline/notes/carry-forward-backlog.md`
- `Plans/Brainstorms/workflow-runner-single-call-and-transition-graph-baseline/decisions/0004-carry-forward-backlog-contract-v0.md`
- `Plans/Brainstorms/workflow-runner-single-call-and-transition-graph-baseline/validation/brainstorm-to-draft-gate-checklist.md`

## Acceptance Evidence
- Carry-forward backlog includes owner, trigger, and release boundary for each unresolved hardening item.
- Draft promotion gates explicitly require backlog review and mapping.

## Git Branch
- `main`

## Commits
- `none`

## Commands Executed
- `pending`

## Files Changed
- `pending`

## Tests / Results
- `not-run` -> `docs-only step`

## Issues
- `none`

## Decision
- pending

## Completion
- `pending`

## Next Actions
- Request review and decide if brainstorm is ready for `Drafts` promotion.

---

## Source 14: `steps/0010-inventory-current-runner-and-proof-surfaces/artifacts/current-runner-surface-inventory.md`

# Current Runner Surface Inventory

## Domain Surfaces
- `Source/Zelanthus.StoryEngine.Domain/Workflows/WorkflowDefinition.cs`
  - Legacy analysis target that currently models ordered workflow steps.
- `Source/Zelanthus.StoryEngine.Domain/Workflows/WorkflowStepDefinition.cs`
  - Legacy analysis target for node contract evolution (`WorkflowNodeDefinition`).
- `Source/Zelanthus.StoryEngine.Domain/Workflows/WorkflowKind.cs`
  - Legacy analysis target where topology semantics need explicit authority (`WorkflowTopology`).
- `Source/Zelanthus.StoryEngine.Domain/Workflows/StepKind.cs`
  - Legacy analysis target where phase role and execution kind concerns must be separated.
- `Source/Zelanthus.StoryEngine.Domain/Runs/WorkflowRunCursor.cs`
  - Owns run cursor state, counters, and continuity key progression.

## Application Surfaces
- `Source/Zelanthus.StoryEngine.Application/Orchestration/WorkflowRunner.cs`
  - Executes step/node units and applies resume/retry behavior.
  - Current implementation surface that must absorb transition-decision contract semantics.
- `Source/Zelanthus.StoryEngine.Application/Contracts/RunExecution/WorkflowExecutionRequest.cs`
  - Contains workflow definition + run cursor + optional effective-step rehydration.
- `Source/Zelanthus.StoryEngine.Application/Contracts/StepExecution/WorkflowStepExecutionResult.cs`
  - Current result shape that must align with deterministic node outcome summary and transition evaluation.

## Infrastructure Surfaces
- `Source/Zelanthus.StoryEngine.Infrastructure/Storage/Local/WorkflowRunPaths.cs`
  - Deterministic run/turn/checkpoint/failure paths.
- `Source/Zelanthus.StoryEngine.Infrastructure/Storage/Local/LocalFileWorkflowRunStore.cs`
  - Atomic writes for run, turn, checkpoint, and failure records.
- `Source/Zelanthus.StoryEngine.Infrastructure/Storage/Records/WorkflowRunStoreRecords.cs`
  - Record shapes for persisted run/turn/failure data.

## Proof Surfaces
- `Tests/Zelanthus.WorkflowContractProofs.Tests/ChainRunnerContractProofTests.cs`
  - Cognitive and conversational resume semantics.
  - Variable-length process behavior proofs.
  - Path determinism and persistence proofs.
  - Continuity-handle and reason-code behavior proofs.

## Gap Flags
- `GAP-001` naming axis ambiguity:
  - Legacy terms still appear in code surfaces and need explicit mapping to authoritative semantics.
- `GAP-002` transition contract incompleteness:
  - Transition rule object and transition decision artifact semantics are not yet fully hardened in runtime policy.
- `GAP-003` node identity semantics:
  - Stable node identity vs run occurrence identity needs explicit operational hardening.
- `GAP-004` backlog continuity:
  - Proof-backed behavior hardening continuity needs formal enforcement (`WF-BL-001`).

---

## Source 15: `steps/0010-inventory-current-runner-and-proof-surfaces/notes/proof-vs-runtime-gap-notes.md`

# Proof vs Runtime Gap Notes

## Confirmed Proof Coverage
- Cognitive resume restart policy is proof-covered.
- Conversational resume with effective-step rehydration is proof-covered.
- Variable-length execution behavior is proof-covered.
- Deterministic local pathing and atomic persistence behavior is proof-covered.

## Runtime Doctrine Gaps
- Transition-rule evaluation contract is not yet fully hardened in runtime implementation policy.
- Node identity (`node_key`) vs occurrence identity (`node_instance_key`) needs complete runtime contract hardening.
- Naming axis split (topology vs execution kind vs LLM mode) must be enforced in implementation.
- Carry-forward backlog gating needs explicit enforcement in draft promotion checklist.

## Planning Implication
- Next draft must promote explicit contracts for transition decisions and node identity semantics before implementation changes are accepted.

---

## Source 16: `steps/0020-extract-bookforge-phase-and-transition-patterns/artifacts/bookforge-pattern-extract.md`

# BookForge Pattern Extract

## Keep Patterns
- Deterministic transition classifier with lane/decision logging.
  - Source: `References/bookforge/resources/plans/lint_repair_split_routing_plan_20260213_183000.md`
  - Why keep: maps directly to bounded transition-graph process control without semantic code generation.
- Retry transitions with reason-coded failure outcomes.
  - Source: `References/bookforge/resources/plans/proposed/outline_phase4_two_step_llm_transition_plan_20260216_0145.md`
  - Why keep: aligns with one-primary-reason-code deterministic policy.
- Artifact-ledger and checkpoint-first resume semantics.
  - Source: `References/bookforge/resources/plans/completed/run_resume_phase_ledger_plan.md`
  - Why keep: directly compatible with Zelanthus artifact-first doctrine.
- Context minimization and chapter-local execution boundaries.
  - Source: `References/bookforge/resources/plans/proposed/chapter_scoped_two_turn_phase_execution_plan_20260220_0927.md`
  - Why keep: reinforces bounded payload and deterministic validation surfaces.

## Reject Patterns for This Slice
- Phase-specific CLI and env control expansion.
  - Why reject now: host/config scope is outside this brainstorm.
- Broad phase 4-6 domain-specific outline workflow details.
  - Why reject now: domain-specific semantics are not needed for transition-graph runner contract baseline.
- Multi-phase template manifest expansion.
  - Why reject now: prompt composition expansion is out of scope for this baseline.

## Adapted Pattern Summary
- Keep BookForge process semantics, not BookForge domain payload shape.
- Keep deterministic transition + retry + evidence model as reusable contract behavior.

---

## Source 17: `steps/0020-extract-bookforge-phase-and-transition-patterns/notes/bookforge-non-transferable-patterns.md`

# BookForge Non-Transferable Patterns

## Rejected for This Baseline
- Domain-specific phase payload contracts tied to BookForge authoring scopes.
  - Reason: this baseline is runner/orchestration contract alignment, not domain payload modeling.
- Broad host/control-plane command surfaces.
  - Reason: execution host expansion is out of scope for this brainstorm.
- Template-manifest expansion and prompt composition system growth.
  - Reason: prompt composition is a separate planning concern and not required for transition-graph semantics.

## Deferred Considerations
- Advanced lane-level orchestration ergonomics can be revisited after transition-graph contract hardening in Draft.

---

## Source 18: `steps/0030-map-cognition-workflow-concepts-and-anti-patterns/artifacts/cognition-concept-map.md`

# Cognition Concept Map

## Reusable Seams
- Explicit runner orchestration boundary as application concern.
- Domain-first contract modeling for execution definitions.
- Separation of provider client concerns from orchestration policy.

## Deferred Seams
- Highly generic workflow/meta-model abstractions.
- Broad graph-engine capabilities (scheduler/planner abstractions).

## Rejected Complexity for This Baseline
- Model-the-system style indirection layers that obscure deterministic runner behavior.
- Implicit state and dynamic behavior that cannot be reconstructed from persisted artifacts.

## Planning Takeaway
- Keep the runner thin and deterministic.
- Use data-defined transition rules for process control.
- Delay generalized engine concerns until transition-graph baseline is proven in execution plans.

---

## Source 19: `steps/0030-map-cognition-workflow-concepts-and-anti-patterns/notes/cognition-anti-pattern-guardrails.md`

# Cognition Anti-Pattern Guardrails

## Guardrails
- Do not introduce generalized orchestration-engine abstractions in this baseline.
- Do not allow implicit transition behavior that is not persisted as explicit decision artifacts.
- Do not mix naming axes (topology, execution kind, LLM mode, phase role).
- Do not collapse node identity and node occurrence identity.

## Acceptance Reminder
- Transition behavior must be data-edit driven.
- Implementation code changes should not be required for flow edits in this scope.

---

## Source 20: `notes/carry-forward-backlog.md`

# Carry-Forward Backlog (Plan-Scoped)

## Purpose
- Prevent loss of high-value workflow hardening work that is currently codified in proof tests but not yet defined as full runtime process policy.

## Interim Policy
- Until a workspace-wide backlog workflow is formally defined, this brainstorm uses a plan-scoped carry-forward backlog note as the temporary source of truth.
- Any unresolved hardening item in this list must be mapped into Draft planning before implementation work begins.

## Active Items
- `WF-BL-001`
  - Item: Harden `cognitive_chain` and `conversational_chain` behavior from proof-oriented contract validation into concrete, documented runtime process policy.
  - Current State: Contract proofs exist (`Tests/Zelanthus.WorkflowContractProofs.Tests/ChainRunnerContractProofTests.cs`) but full end-to-end operational process semantics are not yet fully codified as implementation doctrine.
  - Owner Scope: `StoryEngine runner planning`
  - Trigger for Promotion: Next Draft that extends node execution modes, transition behavior, or runner orchestration policy.
  - Exit Criteria: Runtime process contract, implementation touchpoints, and validation evidence are documented and accepted.

## Rule for New Execution Modes
- Any newly introduced execution mode or transition behavior in planning must add an entry here if proof contracts can exist before full runtime process hardening is complete.
- Do not close an entry without explicit validation evidence in a completed plan package.

---

## Source 21: `notes/terminology-supersession-map.md`

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

---

## Source 22: `archive-note.md`

# Archive Note

## Summary
- Plan: `<plan-folder-name>`
- Archived From: `Plans/<stage>/<plan-folder-name>`
- Archived To: `Plans/Archived/<bucket>/<plan-folder-name>`
- Archived On: `<YYYY-MM-DD>`
- Archived By: `<name-or-agent>`

## Reason
- <Why this plan was archived>

## Completion State
- `<abandoned|superseded|completed-history>`

## Follow-up
- Replacement Plan (optional): `<path>`
- Relevant Notes (optional): <notes>

---

## Source 23: `artifacts/research-source-index.md`

# Research Source Index

## BookForge Plans
- `References/bookforge/resources/plans/proposed/chapter_scoped_two_turn_phase_execution_plan_20260220_0927.md`
- `References/bookforge/resources/plans/proposed/outline_phase4_two_step_llm_transition_plan_20260216_0145.md`
- `References/bookforge/resources/plans/lint_repair_split_routing_plan_20260213_183000.md`
- `References/bookforge/resources/plans/completed/run_resume_phase_ledger_plan.md`
- `References/bookforge/resources/plans/proposed/outline_phase4_5_6_chapter_scoped_execution_plan_20260216_1053.md`

## Cognition Code
- `References/cognition/src/Cognition.Clients/Tools/Planning/PlannerBase.cs`
- `References/cognition/src/Cognition.Workflows/Definitions/WorkflowDefinition.cs`
- `References/cognition/src/Cognition.Workflows/Definitions/WorkflowNode.cs`
- `References/cognition/src/Cognition.Jobs/FictionWeaverJobs.cs`

## Zelanthus Baseline Targets
- `Source/Zelanthus.StoryEngine.Application/Orchestration/WorkflowRunner.cs`
- `Source/Zelanthus.StoryEngine.Domain/Workflows/WorkflowDefinition.cs` (legacy analysis target)
- `Source/Zelanthus.StoryEngine.Domain/Workflows/WorkflowStepDefinition.cs` (legacy analysis target)
- `Source/Zelanthus.StoryEngine.Domain/Workflows/WorkflowKind.cs` (legacy analysis target)
- `Source/Zelanthus.StoryEngine.Domain/Workflows/StepKind.cs` (legacy analysis target)
- `Source/Zelanthus.StoryEngine.Infrastructure/Storage/Local/WorkflowRunPaths.cs`
- `Tests/Zelanthus.WorkflowContractProofs.Tests/ChainRunnerContractProofTests.cs`
