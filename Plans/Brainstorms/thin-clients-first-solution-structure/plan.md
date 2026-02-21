# thin-clients-first-solution-structure

## Objective
- Define initial Zelanthus project/package boundaries and sequencing, with thin LLM clients planned before story engine implementation planning.

## Scope
- In:
  - Solution-level project boundary decisions.
  - Thin clients package-first execution order.
  - Dependency direction rules between API, story engine, prompting, and client packages.
- Out:
  - Actual project scaffolding and implementation.
  - Runtime deployment topology and container orchestration details.

## Definition of Done
- Project boundary decision doc exists and names initial target projects/packages.
- Thin-clients-first sequencing doc exists with phase order and deliverables.
- Dependency-direction rules are explicit enough to drive a Draft implementation plan.
- Prompt ownership/versioning policy is explicit: implementing applications use `Zelanthus.Prompting` shapes/tools to manage prompts and execution provenance.
- Infrastructure storage mapping policy is explicit: `Zelanthus.StoryEngine.Infrastructure` storage may differ from prompting contracts but must map/transform to valid `Zelanthus.Prompting` shapes.
- Canonical application call envelope and provenance ownership are explicit.
- Early architecture test gate and naming alignment policy are explicit.

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

## Touchpoint Map
- Code:
  - `Zelanthus.slnx` (current baseline reference)
- Docs:
  - `Plans/Brainstorms/thin-clients-first-solution-structure/plan.md`
  - `Plans/Brainstorms/thin-clients-first-solution-structure/decisions/0001-solution-project-boundaries-v0.md`
  - `Plans/Brainstorms/thin-clients-first-solution-structure/decisions/0002-thin-clients-first-sequencing-v0.md`
  - `Plans/Brainstorms/thin-clients-first-solution-structure/decisions/0003-dependency-direction-rules-v0.md`
  - `Plans/Brainstorms/thin-clients-first-solution-structure/decisions/0004-prompting-ownership-and-versioning-policy-v0.md`
  - `Plans/Brainstorms/thin-clients-first-solution-structure/decisions/0005-infrastructure-prompt-shape-mapping-policy-v0.md`
  - `Plans/Brainstorms/thin-clients-first-solution-structure/decisions/0006-call-envelope-and-enforcement-policy-v0.md`
  - `Plans/Brainstorms/thin-clients-first-solution-structure/artifacts/initial-solution-shape.md`
- Infrastructure/Config:
  - none in this brainstorm stage

## Risks and Mitigations
- Risk: Project split introduces too many assemblies too early.
  - Mitigation: Start with a minimal but explicit package set; expand only when boundary pressure is clear.
- Risk: Story engine couples to provider-specific implementation details.
  - Mitigation: Story engine depends only on thin-client abstractions and prompting contracts.
- Risk: Prompting utilities are duplicated across projects.
  - Mitigation: Keep prompting as its own reusable project boundary.

## Validation and Testing
- Automated:
  - not-run (docs-only brainstorm)
- Manual:
  - Verify project boundaries align to doctrine from `backbone-doctrine-prompt-first-multi-provider`.
  - Verify thin-clients-first sequence is explicit and actionable.

## Rollout / Rollback
- Rollout:
  - Use this brainstorm as direct input to Draft-stage implementation planning.
  - Draft thin-clients implementation plan before story engine implementation plan.
- Rollback:
  - Supersede with revised brainstorm/draft and archive this plan folder if replaced.

## Status Tracker
- [x] `0010-map-solution-project-boundaries`
- [x] `0020-plan-thin-clients-package-first`
- [x] `0030-sequence-story-engine-dependencies`
- [x] `0040-lock-prompting-ownership-policy`
- [x] `0050-lock-infrastructure-prompt-shape-mapping`
- [x] `0060-refine-decision-layer-responsibility-clarity`
- [x] `0070-apply-external-review-refinements`

## Notes
- This plan intentionally captures structure and ordering only; implementation details belong in Drafts/InProgress.
