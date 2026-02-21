# backbone-doctrine-prompt-first-multi-provider

## Compiled Plan Metadata

- Plan Scope: `Brainstorms/backbone-doctrine-prompt-first-multi-provider`
- Compiled At (UTC): `2026-02-21T08:45:35Z`
- Source Document Count: `13`
- Projection File: `backbone-doctrine-prompt-first-multi-provider.md`

## Contents

1. `plan.md`
2. `decisions/0001-backbone-doctrine-v0.md`
3. `decisions/0002-chain-continuity-strategy-v0.md`
4. `decisions/0003-prompt-governance-and-provenance-v0.md`
5. `decisions/0004-mvp-artifact-and-failure-contract-v0.md`
6. `risks/risk-log.md`
7. `steps/index.md`
8. `steps/0010-define-backbone-doctrine-boundaries/step.md`
9. `steps/0020-define-chain-modes-and-continuity-contract/step.md`
10. `steps/0030-define-provider-capability-and-prompt-governance/step.md`
11. `steps/0040-refine-decision-contract-clarity/step.md`
12. `steps/0050-define-mvp-artifact-and-failure-contract/step.md`
13. `archive-note.md`

---

## Source 1: `plan.md`

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

---

## Source 2: `decisions/0001-backbone-doctrine-v0.md`

# Decision 0001: Backbone Doctrine v0

## Status
- accepted

## Decision Summary
- Zelanthus is prompt-first and workflow-first.
- Chat is an optional interaction surface, not a core runtime dependency.
- Model execution owns semantic generation.
- Deterministic orchestration owns validation, routing, retries, checkpointing, and reporting.

## Context
- Story workflow quality is dominated by prompt contracts and phase execution discipline.
- Prior systems drifted when conversation management and platform concerns grew faster than prompt/runtime contract maturity.
- We need a backbone that stays stable when models/providers change.

## Core Doctrine Rules
- Prompt-first runtime:
  - Core unit of work is `prompt contract -> model execution -> validated artifact`.
  - Prompt/template governance is a required runtime concern, not a documentation afterthought.
- Semantic versus deterministic split:
  - Semantic authoring and semantic correction are model responsibilities.
  - Deterministic runtime enforces schema/invariants, retry policy, decision routing, and resume safety.
- Artifact-first correctness:
  - Durable artifacts are authoritative for resume and replay.
  - Hidden provider state can improve performance, but cannot be the correctness foundation.
- Explicit failures:
  - No silent semantic fallback by orchestrator code.
  - Terminal failures must be reason-coded and persisted with enough context for deterministic replay.

## Scope Boundaries
- In scope:
  - Prompt lifecycle, execution contracts, validation contracts, checkpointing, provenance.
- Out of scope:
  - Chat-first product surfaces, conversation timelines, and user-facing messaging choreography.

## Non-Negotiables
- No deterministic code path may silently generate replacement semantic prose.
- Every accepted execution artifact must trace to prompt ID/version/hash and model/provider metadata.
- Resume behavior must be deterministic and explainable from persisted artifacts.

## Consequences
- Initial implementation planning prioritizes:
  - prompt tooling,
  - provider abstraction contracts,
  - execution chain contracts,
  - artifact/provenance storage.
- Conversation infrastructure can be added later without re-defining core workflow contracts.

---

## Source 3: `decisions/0002-chain-continuity-strategy-v0.md`

# Decision 0002: Chain and Continuity Strategy v0

## Status
- accepted

## Decision Summary
- Zelanthus supports two chain modes:
  - `CognitiveChain`: `PLAN_STEP` followed by `EXECUTE` as a base unit.
  - `ConversationalChain`: interaction-driven multi-turn flow with explicit turn artifacts.
- Provider continuity and thought handles are first-class capabilities.
- Hidden continuity state is never authoritative resume state.
- `PLAN_STEP` -> `EXECUTE` is minimum chain unit semantics, not a hard cap on chain length.

## Context
- Thought-heavy models can consume large token budgets before producing required output artifacts.
- Single-turn execution is insufficient for high-complexity structured phases.
- Continuity support differs by provider/model and can fail across pauses, retries, or model changes.

## Chain Mode Contracts
- `CognitiveChain`:
  - `PLAN_STEP` objective: produce explicit compact plan artifact and provider continuity handle (for example provider `thoughtSignature`) when available.
  - `EXECUTE` objective: produce contract-valid output from plan artifact.
  - `EXECUTE` must not run without a valid plan artifact.
  - Additional `PLAN_STEP`/`EXECUTE` units may be executed when workflow scope requires multi-step planning/execution.
  - Cognitive flows may stage multiple `PLAN_STEP` units before one or more `EXECUTE` units.
  - Provider may return a new `thoughtSignature` on each response; Zelanthus tracks the latest value as `thinking_persistence_key`.
  - Cognitive requests should pass forward the latest persisted `thinking_persistence_key` to the next provider call when capability supports it.
  - `thinking_persistence_key` can rotate turn-to-turn and is treated as rolling continuity state, not a fixed per-run key.
- `ConversationalChain`:
  - Every turn is persisted with explicit turn metadata and output artifacts.
  - Conversation context can inform behavior, but contract validity is still checked per turn.

## Continuity Policy
- Continuity handle usage:
  - Allowed as optimization for immediate follow-up turns.
  - Must be treated as opaque provider state.
- Resume rules:
  - Same run window with valid handle: use handle plus persisted turn artifacts.
  - `CognitiveChain` delayed resume or handle failure: restart from first `PLAN_STEP` because provider-side thinking cache durability is not guaranteed.
  - `ConversationalChain` delayed resume: continue from last successful persisted step.
  - Provider/model swap: continuity handle is treated as invalid and ignored.

## Budget and Routing Rules
- Runtime tracks thought token usage and output token usage separately.
- Chain mode router may promote a phase from single-turn to `CognitiveChain` when output starvation risk is detected.
- Retry policy must preserve chain semantics:
  - Prefer `EXECUTE` retry only when upstream `PLAN_STEP` artifact is still valid.
  - Restart at first `PLAN_STEP` when `EXECUTE` failures indicate stale or insufficient planning state.

## Layer Ownership
- Application layer:
  - selects chain mode,
  - defines turn objectives and acceptance criteria.
- Provider adapter layer:
  - translates continuity/thought features to provider protocol.
- Infrastructure layer:
  - persists turn artifacts/checkpoints/provenance and supports deterministic resume.

## Non-Negotiables
- No blind `EXECUTE` execution without explicit valid plan state.
- No correctness dependency on hidden provider state.
- All chain failures and retries must be reason-coded and artifacted.

---

## Source 4: `decisions/0003-prompt-governance-and-provenance-v0.md`

# Decision 0003: Prompt Governance and Provenance v0

## Status
- accepted

## Decision Summary
- Prompt governance is a runtime contract, not a documentation convention.
- Provider integrations are capability-driven through shared abstractions.
- Provenance capture is mandatory for every workflow-relevant model call.

## Context
- Prompt drift and missing provenance are major failure sources in iterative workflow systems.
- Multi-provider support requires normalized capabilities and deterministic metadata, not provider-specific branches in workflow orchestration.

## Prompt Governance Requirements
- Identity and versioning:
  - Prompts use stable semantic IDs and explicit versions.
  - Released versions are immutable; edits require a new version.
- Composition:
  - Composition of prompt blocks/fragments must be deterministic.
  - Composition determinism is verified by checksum consistency.
- Rendering:
  - Required placeholders/tokens must be enforced with hard failures on missing values.
  - Token allowlist is explicit and validated.
- Change control:
  - Prompt updates must leave a traceable change surface (ID, version, checksum delta, reason).

## Provider Capability Contract (minimum)
- Required normalized capability profile fields:
  - `supports_thinking_controls`
  - `supports_continuity_handle`
  - `supports_structured_output`
  - `supports_tool_calls`
  - `supports_json_mode`
- Runtime behavior must branch on capability contract values, not provider-name conditionals in application orchestration code.

## Provenance Contract (minimum)
- Prompt provenance:
  - prompt semantic ID,
  - prompt version,
  - rendered prompt hash/checksum,
  - prompt source locator (pack/template key).
- Execution provenance:
  - provider ID,
  - model ID,
  - chain mode and turn index,
  - request/response timing and token usage (including thought tokens when available).
- Validation provenance:
  - validation outcome,
  - reason codes for retries/failures,
  - normalized output snapshot,
  - raw response snapshot.

## Quality Gates
- A workflow step cannot be considered complete if required provenance fields are missing.
- A prompt-render execution cannot proceed when required token values are unresolved.
- A provider integration is incomplete without a declared capability profile.

## Consequences
- Prompt assets and provider contracts can evolve without rewriting story engine orchestration.
- Gemini-first rollout remains compatible with later providers.
- Replay and debugging quality improves because output artifacts are tied to verifiable prompt and execution metadata.
- MVP artifact taxonomy/reason-code baseline/structured-output canonical rules are defined in `0004-mvp-artifact-and-failure-contract-v0.md`.

---

## Source 5: `decisions/0004-mvp-artifact-and-failure-contract-v0.md`

# Decision 0004: MVP Artifact and Failure Contract v0

## Status
- accepted

## Decision Summary
- Define a minimal but explicit MVP artifact taxonomy.
- Define a deterministic MVP reason-code baseline for retry and terminal failures.
- Define one canonical structured output contract for MVP.

## Context
- "Artifact-first correctness" requires explicit artifact shape contracts, not only conceptual statements.
- "Reason-coded failure" is only useful if the baseline code set is concrete.
- Structured output drift becomes costly when "close enough" responses are accepted ad hoc.

## MVP Artifact Taxonomy
- `TurnArtifact`
  - Purpose: capture one execution turn outcome.
  - Minimum fields:
    - `chain_mode`
    - `turn_index`
    - `turn_objective`
    - `raw_response_snapshot_ref`
    - `normalized_output_ref`
    - `validation_result_ref`
- `ProvenanceRecord`
  - Purpose: capture prompt and execution lineage.
  - Minimum fields:
    - `prompt_id`
    - `prompt_version`
    - `prompt_hash`
    - `provider_id`
    - `model_id`
    - `timing`
    - `token_accounting`
    - `reason_codes` (if any)
- `AcceptedOutputArtifact`
  - Purpose: canonical artifact consumed by the next workflow step.
  - Minimum fields:
    - `schema_id`
    - `schema_version`
    - `payload`
    - `accepted_at_utc`
    - `source_turn_ref`

## MVP Failure Reason-Code Baseline
- `missing_required_token`
- `schema_validation_failed`
- `provider_protocol_error`
- `output_starvation`
- `continuity_handle_invalid`
- `retry_budget_exhausted`

Reason-code policy:
- Retry and terminal failures must emit one or more codes from the baseline or a declared extension set.
- Codes must be persisted in `ProvenanceRecord` and validation/failure artifacts.

## Canonical Structured Output Rule (MVP)
- Accepted structured output is a JSON object conforming to the declared schema contract for the step.
- Non-conforming outputs are never auto-coerced into accepted artifacts.
- Raw response snapshots are always persisted for forensic review, even on failure.

## Consequences
- Draft plans must include artifacts and reason-code acceptance criteria.
- MVP runner and validators must reject outputs that do not satisfy canonical structured-output contract.

---

## Source 6: `risks/risk-log.md`

# Risk Log

## R-001 Hidden Continuity Coupling
- Statement: Provider continuity handles become an implicit dependency for correctness.
- Impact: Resume failures, non-deterministic behavior across retries/model changes.
- Mitigation: Explicit plan artifacts are authoritative; continuity is optional optimization.
- Status: open

## R-002 Premature Abstraction
- Statement: Multi-provider abstraction becomes over-engineered before concrete second provider requirements exist.
- Impact: Slower delivery, unclear boundaries.
- Mitigation: Define minimal capability profile now; implement Gemini adapter first.
- Status: open

## R-003 Chat-First Drift
- Statement: Architecture drifts into conversation state management before prompt/runtime contracts are stable.
- Impact: Complexity growth and lower execution reliability.
- Mitigation: Keep prompt-first doctrine and artifact provenance as stage gate for Draft plans.
- Status: open

---

## Source 7: `steps/index.md`

# Steps Index

Use this file for dependency and status summaries.
Do not use this file as the only source of step order.
Step order must still be visible in folder names (`NNNN-semantic-step-name`).

## Step Map
| Order | Step Folder | Status | Depends On | Notes |
|---|---|---|---|---|
| 0010 | `0010-define-backbone-doctrine-boundaries` | `completed` | `none` | Captured prompt-first doctrine and semantic/deterministic split |
| 0020 | `0020-define-chain-modes-and-continuity-contract` | `completed` | `0010-define-backbone-doctrine-boundaries` | Captured `CognitiveChain` vs `ConversationalChain` and resume contract |
| 0030 | `0030-define-provider-capability-and-prompt-governance` | `completed` | `0010-define-backbone-doctrine-boundaries` | Captured provider capability profile and prompt provenance requirements |
| 0040 | `0040-refine-decision-contract-clarity` | `completed` | `0010-define-backbone-doctrine-boundaries`, `0020-define-chain-modes-and-continuity-contract`, `0030-define-provider-capability-and-prompt-governance` | Expanded all doctrine decisions with explicit contracts, scope boundaries, and layer ownership |
| 0050 | `0050-define-mvp-artifact-and-failure-contract` | `completed` | `0040-refine-decision-contract-clarity` | Defined MVP artifact taxonomy, reason-code baseline, and canonical structured-output rule |

## Status Values
- `pending`
- `in_progress`
- `blocked`
- `completed`

---

## Source 8: `steps/0010-define-backbone-doctrine-boundaries/step.md`

# Step: 0010-define-backbone-doctrine-boundaries

## Goal
- Lock the high-level backbone doctrine for Zelanthus before drafting implementation plans.

## Context
- We need explicit doctrine alignment so future plans do not regress into chat-first architecture or provider lock-in.

## Commands Executed
- `Get-Content -Raw "Plans/README.md"`
- `Get-Content -Raw "Plans/Templates/README.md"`
- `Get-Content -Raw "Zelanthus.slnx"`

## Files Changed
- `Plans/Brainstorms/backbone-doctrine-prompt-first-multi-provider/plan.md`
- `Plans/Brainstorms/backbone-doctrine-prompt-first-multi-provider/steps/index.md`
- `Plans/Brainstorms/backbone-doctrine-prompt-first-multi-provider/decisions/0001-backbone-doctrine-v0.md`

## Tests / Results
- `not-run` -> docs-only brainstorm step

## Issues
- none

## Decision
- Adopt prompt-first backbone doctrine with semantic/deterministic split and artifact-first correctness.

## Completion
- `completed`

## Next Actions
- Capture chain-mode/continuity contract and provider capability governance in follow-up steps.

---

## Source 9: `steps/0020-define-chain-modes-and-continuity-contract/step.md`

# Step: 0020-define-chain-modes-and-continuity-contract

## Goal
- Define planner-to-execution chain modes that support thought-heavy workloads without making correctness depend on hidden provider state.

## Context
- BookForge findings showed high thought-token usage can starve output in single-turn calls, requiring explicit multi-turn planning/execution abstractions.

## Commands Executed
- `Get-Content -Raw "References/bookforge/resources/plans/proposed/chapter_scoped_two_turn_phase_execution_plan_20260220_0927.md"`
- `Get-Content -Raw "References/cognition/src/Cognition.Clients/Tools/Planning/PlannerBase.cs"`

## Files Changed
- `Plans/Brainstorms/backbone-doctrine-prompt-first-multi-provider/steps/0020-define-chain-modes-and-continuity-contract/step.md`
- `Plans/Brainstorms/backbone-doctrine-prompt-first-multi-provider/decisions/0002-chain-continuity-strategy-v0.md`

## Tests / Results
- `not-run` -> docs-only brainstorm step

## Issues
- none

## Decision
- Support two chain modes (`CognitiveChain`, `ConversationalChain`) with explicit turn artifacts and optional continuity handles.

## Completion
- `completed`

## Next Actions
- Define provider capability profile and prompt governance requirements.

---

## Source 10: `steps/0030-define-provider-capability-and-prompt-governance/step.md`

# Step: 0030-define-provider-capability-and-prompt-governance

## Goal
- Define minimal provider abstraction and prompt governance/provenance requirements for a Gemini-first but multi-provider-ready backbone.

## Context
- We need a stable way to compare provider capabilities and preserve prompt-response provenance without building full conversation infrastructure.

## Commands Executed
- `Get-Content -Raw "References/bookforge/src/bookforge/prompt/composition.py"`
- `Get-Content -Raw "References/bookforge/src/bookforge/llm/factory.py"`

## Files Changed
- `Plans/Brainstorms/backbone-doctrine-prompt-first-multi-provider/steps/0030-define-provider-capability-and-prompt-governance/step.md`
- `Plans/Brainstorms/backbone-doctrine-prompt-first-multi-provider/decisions/0003-prompt-governance-and-provenance-v0.md`
- `Plans/Brainstorms/backbone-doctrine-prompt-first-multi-provider/risks/risk-log.md`

## Tests / Results
- `not-run` -> docs-only brainstorm step

## Issues
- none

## Decision
- Adopt capability-profile provider adapters and make prompt hash/version/provenance mandatory for all workflow artifacts.

## Completion
- `completed`

## Next Actions
- Use this doctrine as input to the thin-clients-first solution structure brainstorm and draft plans.

---

## Source 11: `steps/0040-refine-decision-contract-clarity/step.md`

# Step: 0040-refine-decision-contract-clarity

## Goal
- Refine all backbone decision documents for stronger precision, clearer scope boundaries, and more explicit runtime contracts.

## Context
- Earlier decision docs were directionally correct but too compact for downstream implementation planning.
- We need ADR-grade clarity before draft implementation plans.

## Commands Executed
- `Get-Content -Raw "Plans/Brainstorms/backbone-doctrine-prompt-first-multi-provider/decisions/0001-backbone-doctrine-v0.md"`
- `Get-Content -Raw "Plans/Brainstorms/backbone-doctrine-prompt-first-multi-provider/decisions/0002-chain-continuity-strategy-v0.md"`
- `Get-Content -Raw "Plans/Brainstorms/backbone-doctrine-prompt-first-multi-provider/decisions/0003-prompt-governance-and-provenance-v0.md"`

## Files Changed
- `Plans/Brainstorms/backbone-doctrine-prompt-first-multi-provider/plan.md`
- `Plans/Brainstorms/backbone-doctrine-prompt-first-multi-provider/steps/index.md`
- `Plans/Brainstorms/backbone-doctrine-prompt-first-multi-provider/steps/0040-refine-decision-contract-clarity/step.md`
- `Plans/Brainstorms/backbone-doctrine-prompt-first-multi-provider/decisions/0001-backbone-doctrine-v0.md`
- `Plans/Brainstorms/backbone-doctrine-prompt-first-multi-provider/decisions/0002-chain-continuity-strategy-v0.md`
- `Plans/Brainstorms/backbone-doctrine-prompt-first-multi-provider/decisions/0003-prompt-governance-and-provenance-v0.md`

## Tests / Results
- `not-run` -> docs-only brainstorm step

## Issues
- none

## Decision
- Lock detailed doctrine wording for:
  - semantic vs deterministic boundaries,
  - chain and continuity contracts,
  - prompt governance/provenance quality gates.

## Completion
- `completed`

## Next Actions
- Use refined doctrine decisions as explicit input contracts for Draft-stage implementation planning.

---

## Source 12: `steps/0050-define-mvp-artifact-and-failure-contract/step.md`

# Step: 0050-define-mvp-artifact-and-failure-contract

## Goal
- Define explicit MVP artifact taxonomy, failure reason-code baseline, and canonical structured-output rule.

## Context
- External review highlighted that “artifact-first correctness” and “reason-coded failures” needed concrete minimum contracts before Draft.

## Commands Executed
- `Get-Content -Raw "Plans/Brainstorms/backbone-doctrine-prompt-first-multi-provider/decisions/0003-prompt-governance-and-provenance-v0.md"`
- `Get-Content -Raw "Plans/Brainstorms/backbone-doctrine-prompt-first-multi-provider/decisions/0002-chain-continuity-strategy-v0.md"`

## Files Changed
- `Plans/Brainstorms/backbone-doctrine-prompt-first-multi-provider/plan.md`
- `Plans/Brainstorms/backbone-doctrine-prompt-first-multi-provider/steps/index.md`
- `Plans/Brainstorms/backbone-doctrine-prompt-first-multi-provider/steps/0050-define-mvp-artifact-and-failure-contract/step.md`
- `Plans/Brainstorms/backbone-doctrine-prompt-first-multi-provider/decisions/0003-prompt-governance-and-provenance-v0.md`
- `Plans/Brainstorms/backbone-doctrine-prompt-first-multi-provider/decisions/0004-mvp-artifact-and-failure-contract-v0.md`

## Tests / Results
- `not-run` -> docs-only brainstorm step

## Issues
- none

## Decision
- Lock MVP artifact taxonomy and reason-code baseline so downstream implementation plans have concrete contracts.

## Completion
- `completed`

## Next Actions
- Use the MVP artifact/failure contract as an explicit DoD gate in the thin implementation Draft plans.

---

## Source 13: `archive-note.md`

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
