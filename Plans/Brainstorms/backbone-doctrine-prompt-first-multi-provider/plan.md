# backbone-doctrine-prompt-first-multi-provider

## Objective
- Define Zelanthus backbone doctrine for a prompt-first, pipeline-driven system with first-class thought support and multi-provider readiness.

## Scope
- In:
  - Doctrine for semantic vs deterministic responsibilities.
  - Planning/execution chain shapes (`CognitiveChain` and `ConversationalChain`).
  - Provider capability abstraction and prompt governance/provenance requirements.
- Out:
  - Story engine implementation tasks.
  - Chat UX, conversation persistence, and agent messaging architecture.
  - Provider package implementation details.

## Definition of Done
- Doctrine decisions are captured in plan-scoped decision docs and linked from this plan.
- Chain-mode and continuity contracts are explicit, including resume behavior when provider continuity is unavailable.
- Prompt governance/provenance rules are explicit enough to drive next-stage implementation planning.
- MVP artifact taxonomy and failure reason-code baseline are explicit for draft implementation planning.

## Cross-Plan Dependencies
- Depends on:
  - `Plans/README.md`
- Supersedes (optional):
  - none

## Interfaces and Contracts Introduced/Changed
- Contract: `Artifact taxonomy contract (planning-level)`
  - Owner: `Backbone doctrine`
  - Change Type: `introduced`
  - Notes: Defines required artifact classes and minimum fields for MVP.
- Contract: `Failure reason code baseline (planning-level)`
  - Owner: `Backbone doctrine`
  - Change Type: `introduced`
  - Notes: Defines minimum deterministic reason codes for retries/terminal failures.
- Contract: `Structured output canonical rule (planning-level)`
  - Owner: `Backbone doctrine`
  - Change Type: `introduced`
  - Notes: MVP treats schema-validated JSON object output as canonical.

## Touchpoint Map
- Code:
  - `Zelanthus.slnx` (reference-only for current baseline)
- Docs:
  - `Plans/Brainstorms/backbone-doctrine-prompt-first-multi-provider/plan.md`
  - `Plans/Brainstorms/backbone-doctrine-prompt-first-multi-provider/decisions/0001-backbone-doctrine-v0.md`
  - `Plans/Brainstorms/backbone-doctrine-prompt-first-multi-provider/decisions/0002-chain-continuity-strategy-v0.md`
  - `Plans/Brainstorms/backbone-doctrine-prompt-first-multi-provider/decisions/0003-prompt-governance-and-provenance-v0.md`
  - `Plans/Brainstorms/backbone-doctrine-prompt-first-multi-provider/decisions/0004-mvp-artifact-and-failure-contract-v0.md`
  - `Plans/Brainstorms/backbone-doctrine-prompt-first-multi-provider/risks/risk-log.md`
- Infrastructure/Config:
  - none in this brainstorm stage

## Risks and Mitigations
- Risk: Over-abstraction before a second provider is real.
  - Mitigation: Define capability contracts now, implement Gemini-first adapter first.
- Risk: Hidden provider thought state becoming an implicit dependency.
  - Mitigation: Keep explicit turn artifacts as durable source of truth for resume.
- Risk: Drifting into chat-first architecture too early.
  - Mitigation: Enforce prompt-first doctrine and artifact-first provenance.

## Validation and Testing
- Automated:
  - not-run (docs-only brainstorm)
- Manual:
  - Verify each doctrine area has a decision doc and step note.
  - Verify doctrine aligns with `Plans/README.md` quality gates.

## Rollout / Rollback
- Rollout:
  - Use these doctrine decisions as input to Draft-stage implementation plans.
  - Apply in order: thin clients plan first, then story engine plan.
- Rollback:
  - Supersede with a new brainstorm or draft plan and archive this folder with `archive-note.md`.

## Status Tracker
- [x] `0010-define-backbone-doctrine-boundaries`
- [x] `0020-define-chain-modes-and-continuity-contract`
- [x] `0030-define-provider-capability-and-prompt-governance`
- [x] `0040-refine-decision-contract-clarity`
- [x] `0050-define-mvp-artifact-and-failure-contract`

## Notes
- This doctrine intentionally keeps correctness artifact-driven while still treating model thought/continuity as a first-class capability.
