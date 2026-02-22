# thin-clients-first-solution-structure

## Objective
- Promote accepted thin-client brainstorm outcomes into an implementation-ready Draft package.
- Lock MVP goals and first-plan boundaries before selecting harness transport/host details.

## Scope
- In:
  - MVP goals for prompt contracts, client abstractions, Gemini adapter, and provenance/failure correctness.
  - First-plan boundary for thin implementation work (Plan 1) and explicit non-goals.
  - Harness requirements and decision criteria, without prematurely locking API-host vs test-host.
  - Draft sequencing for Plan 1 and Plan 2 handoff.
- Out:
  - Full story engine domain/application breadth.
  - Production persistence setup (Postgres) and deployment topology.
  - Frontend/chat experience concerns.
  - Prompt sub-template/import composition design and implementation (deferred follow-up scope).

## Definition of Done
- Decision `0007-mvp-goals-and-first-plan-boundary-v0.md` is accepted.
- Decision `0008-mvp-harness-shape-selection-v0.md` is accepted.
- Decision `0009-contract-proof-test-naming-policy-v0.md` is accepted.
- MVP goals are explicit, testable, and mapped to concrete acceptance evidence.
- First-plan scope, non-goals, and deliverables are explicit.
- Harness decision criteria are explicit enough to choose execution shape without redefining goals.
- Draft Plan 1 package exists and is linked for downstream implementation planning.
- Draft step sequencing is explicit for:
  - Plan 1: Prompting + abstractions + Gemini baseline.
  - Plan 2: CognitiveChain/ConversationalChain runner + local artifact/provenance persistence (minimum proof includes one `PLAN_STEP` -> `EXECUTE` pair, but runner is not capped at one pair).
- Thin-clients draft risk log exists and captures current high-risk uncertainties.

## Cross-Plan Dependencies
- Depends on:
  - `Plans/README.md`
  - `Plans/Brainstorms/backbone-doctrine-prompt-first-multi-provider/decisions/0001-backbone-doctrine-v0.md`
  - `Plans/Brainstorms/backbone-doctrine-prompt-first-multi-provider/decisions/0002-chain-continuity-strategy-v0.md`
  - `Plans/Brainstorms/backbone-doctrine-prompt-first-multi-provider/decisions/0003-prompt-governance-and-provenance-v0.md`
  - `Plans/Brainstorms/backbone-doctrine-prompt-first-multi-provider/decisions/0004-mvp-artifact-and-failure-contract-v0.md`
- Supersedes (optional):
  - none

## Interfaces and Contracts Introduced/Changed
- Contract: `Application -> ILlmClient execution envelope`
  - Owner: `Zelanthus.StoryEngine.Application` and `Zelanthus.Llm.Clients.Abstractions`
  - Change Type: `introduced`
  - Notes: Defines canonical request/response contract path used by workflow orchestration.
- Contract: `Provenance assembly ownership`
  - Owner: `Application orchestration`
  - Change Type: `introduced`
  - Notes: Adapter returns protocol metadata; application composes final provenance record.
- Contract: `Architecture boundary enforcement`
  - Owner: `Solution structure`
  - Change Type: `introduced`
  - Notes: Early architecture tests block dependency-direction drift.
- Contract: `MVP goal and acceptance contract`
  - Owner: `Draft plan governance`
  - Change Type: `introduced`
  - Notes: Defines what must be proven in MVP before broad story engine expansion.
- Contract: `MVP harness requirement contract`
  - Owner: `Draft plan governance`
  - Change Type: `introduced`
  - Notes: Defines required harness behavior independent of API-host or test-host execution shape.

## Touchpoint Map
- Code:
  - `Zelanthus.slnx` (current baseline reference)
  - `Source/Zelanthus.API` (current host baseline reference only)
- Docs:
  - `Plans/Drafts/thin-clients-first-solution-structure/plan.md`
  - `Plans/Drafts/thin-clients-first-solution-structure/decisions/0001-solution-project-boundaries-v0.md`
  - `Plans/Drafts/thin-clients-first-solution-structure/decisions/0002-thin-clients-first-sequencing-v0.md`
  - `Plans/Drafts/thin-clients-first-solution-structure/decisions/0003-dependency-direction-rules-v0.md`
  - `Plans/Drafts/thin-clients-first-solution-structure/decisions/0004-prompting-ownership-and-versioning-policy-v0.md`
  - `Plans/Drafts/thin-clients-first-solution-structure/decisions/0005-infrastructure-prompt-shape-mapping-policy-v0.md`
  - `Plans/Drafts/thin-clients-first-solution-structure/decisions/0006-call-envelope-and-enforcement-policy-v0.md`
  - `Plans/Drafts/thin-clients-first-solution-structure/decisions/0007-mvp-goals-and-first-plan-boundary-v0.md`
  - `Plans/Drafts/thin-clients-first-solution-structure/decisions/0008-mvp-harness-shape-selection-v0.md`
  - `Plans/Drafts/thin-clients-first-solution-structure/decisions/0009-contract-proof-test-naming-policy-v0.md`
  - `Plans/Drafts/thin-clients-first-solution-structure/artifacts/initial-solution-shape.md`
  - `Plans/Drafts/thin-clients-first-solution-structure/risks/risk-log.md`
  - `Plans/Drafts/mvp-prompting-gemini-contract-baseline/plan.md`
  - `Plans/Drafts/mvp-chain-runner-local-persistence-baseline/plan.md`
- Infrastructure/Config:
  - local MVP persistence only (path/file-backed baseline; no Postgres setup in this draft).
  - local persistence in this scope means workspace-local storage of:
    - turn artifacts/checkpoints,
    - raw response snapshots,
    - provenance records,
    - validation/failure outputs needed for deterministic resume/replay.
  - local persistence in this scope excludes external database, distributed cache, queue, or service-hosted storage.

## Risks and Mitigations
- Risk: Project split introduces too many assemblies too early.
  - Mitigation: Start with a minimal but explicit package set; expand only when boundary pressure is clear.
- Risk: Story engine couples to provider-specific implementation details.
  - Mitigation: Story engine depends only on thin-client abstractions and prompting contracts.
- Risk: Prompting utilities are duplicated across projects.
  - Mitigation: Keep prompting as its own reusable project boundary.
- Risk: Harness shape decision causes rework.
  - Mitigation: Lock goal/acceptance contract first; decide host shape from explicit decision criteria.

## Validation and Testing
- Automated:
  - not-run (docs-only draft refinement)
- Manual:
  - Verify MVP goals and non-goals are explicit and testable.
  - Verify Plan 1 and Plan 2 boundaries align with accepted backbone decisions.
  - Verify harness criteria are sufficient to evaluate test-host vs API-host without changing MVP goals.

## Rollout / Rollback
- Rollout:
  - Draft Plan 1 is prepared; keep it in `Drafts/` until explicit promotion approval.
  - Draft Plan 2 using Plan 1 outputs and harness decision `0008` as locked dependencies.
  - Do not promote any draft to `InProgress` without explicit user approval.
- Rollback:
  - Supersede with revised draft and archive this plan folder if replaced.

## Status Tracker
- [x] `0010-map-solution-project-boundaries`
- [x] `0020-plan-thin-clients-package-first`
- [x] `0030-sequence-story-engine-dependencies`
- [x] `0040-lock-prompting-ownership-policy`
- [x] `0050-lock-infrastructure-prompt-shape-mapping`
- [x] `0060-refine-decision-layer-responsibility-clarity`
- [x] `0070-apply-external-review-refinements`
- [x] `0080-lock-mvp-goals-and-first-plan-boundary`
- [x] `0090-choose-mvp-harness-shape`
- [x] `0100-draft-plan-1-prompting-and-gemini-contract-implementation`
- [x] `0105-clarify-contract-proof-test-naming`
- [x] `0110-draft-plan-2-cognitive-chain-runner-and-local-persistence`

## Notes
- Long-term production persistence target is Postgres, but this draft keeps persistence local and minimal to avoid out-of-order setup.
- Plan 2 runner design is chain-length-flexible:
  - `CognitiveChain` can stage multiple `PLAN_STEP` units, then execute one or more `EXECUTE` units, and can run additional `PLAN_STEP` -> `EXECUTE` cycles in the same chain.
  - `CognitiveChain` resume policy restarts from chain start when continuation is interrupted because provider-side thinking state is not durable.
  - `ConversationalChain` supports multi-turn progression using explicit persisted turn artifacts and can resume from last successful step.
