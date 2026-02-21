# mvp-prompting-gemini-contract-baseline

## Compiled Plan Metadata

- Plan Scope: `Drafts/mvp-prompting-gemini-contract-baseline`
- Compiled At (UTC): `2026-02-21T06:02:10Z`
- Source Document Count: `14`
- Projection File: `mvp-prompting-gemini-contract-baseline.md`

## Contents

1. `plan.md`
2. `risks/risk-log.md`
3. `steps/index.md`
4. `steps/0010-scaffold-projects-and-references/step.md`
5. `steps/0020-define-prompting-contracts-and-rendering-rules/step.md`
6. `steps/0030-define-llm-client-abstractions-and-capability-profile/step.md`
7. `steps/0040-implement-gemini-adapter-normalization-path/step.md`
8. `steps/0050-add-architecture-boundary-tests/step.md`
9. `steps/0060-implement-contract-proof-tests-and-local-artifact-persistence/step.md`
10. `steps/0070-validate-golden-path-and-reason-coded-failure-path/step.md`
11. `steps/0060-implement-contract-proof-tests-and-local-artifact-persistence/artifacts/contract-proof-evidence-spec.md`
12. `steps/0060-implement-contract-proof-tests-and-local-artifact-persistence/artifacts/initial-test-naming-seed.md`
13. `steps/0070-validate-golden-path-and-reason-coded-failure-path/artifacts/final-acceptance-checklist.md`
14. `archive-note.md`

---

## Source 1: `plan.md`

# mvp-prompting-gemini-contract-baseline

## Objective
- Produce an implementation-ready draft for MVP Plan 1 that proves prompt/client/provider contracts with Gemini-first execution.
- Establish architecture and harness gates needed before broader story engine expansion.

## Scope
- In:
  - Project scaffolding for:
    - `Source/Zelanthus.Prompting`
    - `Source/Zelanthus.Llm.Clients.Abstractions`
    - `Source/Zelanthus.Llm.Clients.Gemini`
    - `Tests/Zelanthus.Architecture.Tests`
    - `Tests/Zelanthus.WorkflowContractProofs.Tests`
  - Prompt contract baseline (identity/version/render/checksum and required placeholder enforcement).
  - Deterministic prompt rendering failure behavior with explicit missing-placeholder reason coding.
  - LLM client abstraction baseline (execution envelope, normalized response, `TokenAccounting`/usage, reason-coded errors).
  - Gemini adapter baseline mapping to abstraction contracts.
  - Architecture boundary assertions and MVP harness proving rules.
- Out:
  - Story engine domain/application/infrastructure implementation breadth.
  - Production persistence setup (Postgres).
  - Frontend/chat experience and long-lived session features.
  - Multi-provider implementation beyond Gemini baseline.
  - Sub-template/import composition semantics beyond single-template rendering.

## Definition of Done
- Plan 1 touchpoints and boundaries are explicit and testable.
- Draft step sequence defines concrete implementation order with dependencies.
- Architecture test gate scope is explicit and aligned to dependency-direction decisions.
- Architecture gate order is explicit in step dependencies:
  - `0050-add-architecture-boundary-tests` must gate `0020`, `0030`, and `0040`.
- MVP harness proving requirements are explicit and aligned to Decision `0008`.
- Prompt rendering contract uses `Placeholder` terminology consistently:
  - `RequiredPlaceholders`
  - `PlaceholderValues`
  - `MissingPlaceholders`
- Prompt identity/version split is explicit:
  - `PromptId` is stable and never embeds version,
  - `PromptVersion` is the only version marker.
- Prompt version format is explicit:
  - `PromptVersion` is a positive integer (`1`, `2`, `3`, ...),
  - version increments by exactly 1 per published template revision.
- Prompt rendering deterministic failure rule is explicit:
  - rendering returns either `RenderedPrompt` or `RenderFailure`,
  - missing required placeholders produce reason code `missing_required_placeholder`,
  - `MissingPlaceholders` is deduplicated and deterministic (lexical sort).
- Placeholder resolution semantics are explicit:
  - placeholder key grammar is `^[a-z][a-z0-9_]*$`,
  - keys are case-sensitive,
  - leading/trailing whitespace in keys is invalid,
  - `RequiredPlaceholders` comes from authored template metadata (source of truth),
  - template-text placeholder parsing is lint-only and not runtime authority,
  - extra `PlaceholderValues` keys are ignored for MVP,
  - missing means key absent or value is `null`,
  - empty string is allowed as an intentional value.
- Checksum canonicalization rules are explicit:
  - hash algorithm is `SHA-256`,
  - UTF-8 encoding,
  - line endings normalized to `\n`,
  - checksum input covers `PromptId`, `PromptVersion`, and `RenderedText` only (in this order).
- Terminology split is explicit:
  - placeholders/template replacement terms are never called model tokens,
  - `TokenAccounting` remains reserved for provider usage accounting.
- Plan includes explicit harness acceptance evidence for:
  - one-missing-placeholder deterministic failure,
  - stable missing-placeholder ordering,
  - successful full-placeholder rendering with stable checksum.
- Harness execution mode rules are explicit:
  - default `dotnet test` path uses fixture/recorded-response proofs only,
  - live Gemini integration tests are opt-in and never run by default,
  - live runs require explicit execution intent (env var and/or explicit live test filter).
- Provenance and reason-code acceptance evidence requirements are explicit for implementation phase.

## Cross-Plan Dependencies
- Depends on:
  - `Plans/README.md`
  - `Plans/Drafts/thin-clients-first-solution-structure/plan.md`
  - `Plans/Drafts/thin-clients-first-solution-structure/decisions/0004-prompting-ownership-and-versioning-policy-v0.md`
  - `Plans/Drafts/thin-clients-first-solution-structure/decisions/0006-call-envelope-and-enforcement-policy-v0.md`
  - `Plans/Drafts/thin-clients-first-solution-structure/decisions/0007-mvp-goals-and-first-plan-boundary-v0.md`
  - `Plans/Drafts/thin-clients-first-solution-structure/decisions/0008-mvp-harness-shape-selection-v0.md`
  - `Plans/Drafts/thin-clients-first-solution-structure/decisions/0009-contract-proof-test-naming-policy-v0.md`
  - `Plans/Brainstorms/backbone-doctrine-prompt-first-multi-provider/decisions/0003-prompt-governance-and-provenance-v0.md`
  - `Plans/Brainstorms/backbone-doctrine-prompt-first-multi-provider/decisions/0004-mvp-artifact-and-failure-contract-v0.md`
- Supersedes (optional):
  - none

## Interfaces and Contracts Introduced/Changed
- Contract: `Prompting runtime contract baseline`
  - Owner: `Zelanthus.Prompting`
  - Change Type: `introduced`
  - Notes: Prompt identity/version/render/checksum, placeholder-value input, and deterministic missing-placeholder failure behavior.
- Contract: `LLM client abstraction baseline`
  - Owner: `Zelanthus.Llm.Clients.Abstractions`
  - Change Type: `introduced`
  - Notes: Canonical `ExecutionEnvelope` and `NormalizedResponseEnvelope` plus capability and error shapes.
- Contract: `Gemini adapter normalization baseline`
  - Owner: `Zelanthus.Llm.Clients.Gemini`
  - Change Type: `introduced`
  - Notes: Provider protocol mapping to normalized envelopes and token/provenance metadata.
- Contract: `MVP harness evidence contract`
  - Owner: `Tests/Zelanthus.WorkflowContractProofs.Tests`
  - Change Type: `introduced`
  - Notes: Golden-path and failure-path proof artifacts, including deterministic missing-placeholder failures, provenance, and reason codes.

## Terminology Split (Required)
- Placeholder/template replacement terms:
  - `RequiredPlaceholders`
  - `PlaceholderValues`
  - `MissingPlaceholders`
- Provider usage accounting terms:
  - `TokenAccounting`
  - `PromptTokens`
  - `OutputTokens`
  - `TotalTokens`
  - `unknown` (when provider does not return usage fields)
- Rule:
  - `token` language in Prompting contract text must not refer to placeholders.
  - `PromptId` must not include version fragments (for example `-v1`).
  - `PromptVersion` is the only version field.

## Prompt Version Rules (Required)
- `PromptVersion` is an integer and must be serialized without semantic-version segments.
- `PromptVersion` starts at `1` for first publish and increments by `1` per published revision.
- `PromptVersion` is template-owned metadata and must be carried unchanged into rendered output and failures.

## Template Definition Shape (MVP, Required)
- Prompt templates are represented as a concrete definition shape containing:
  - `PromptId`
  - `PromptVersion`
  - `TemplateText`
  - `RequiredPlaceholders`
- The definition may live in code or embedded resources, but runtime behavior is identical.
- Runtime required-placeholder validation must read `RequiredPlaceholders` from this definition shape.
- Runtime required-placeholder validation must not infer authority from template-text parsing.

## Placeholder Resolution Rules (Required)
- Placeholder key grammar: `^[a-z][a-z0-9_]*$` (lower snake case).
- Placeholder keys are case-sensitive.
- Leading/trailing whitespace in placeholder keys is invalid.
- Placeholder source of truth:
  - `RequiredPlaceholders` is authored metadata persisted with the prompt template/version.
  - Runtime validation uses this metadata list, not inferred placeholder discovery.
  - Template-body placeholder parsing is optional lint/authoring validation only.
- Extra `PlaceholderValues` keys are ignored for MVP.
- Missing placeholder semantics:
  - missing if key is absent,
  - missing if value is `null`,
  - empty string is allowed and not treated as missing.
- Missing list construction:
  - deduplicate keys to a unique set,
  - sort lexically,
  - emit as `MissingPlaceholders`.

## Checksum Canonicalization Rules (Required)
- Hash algorithm: `SHA-256`.
- Normalize rendered text line endings to `\n`.
- Encode checksum input as UTF-8.
- Checksum covers exactly:
  - `PromptId`
  - `PromptVersion`
  - `RenderedText`
- Checksum input field order is fixed and deterministic:
  - `PromptId`, then `PromptVersion`, then `RenderedText`.

## Prompt Rendering Contract Sketch (Planning-Level)
- Render input:
  - `PlaceholderValues` (map: placeholder key -> value; `null` treated as missing)
- `RenderedPrompt`
  - `PromptId`
  - `PromptVersion`
  - `RenderedText`
  - `Checksum` (`SHA-256`)
  - `RequiredPlaceholders`
- `RenderFailure`
  - `ReasonCode` (must include `missing_required_placeholder`)
  - `MissingPlaceholders` (lexically sorted for deterministic assertions)
  - `PromptId`
  - `PromptVersion`
- Deterministic rule:
  - Prompting returns either `RenderedPrompt` or `RenderFailure`.
  - `PromptId` is stable identity only; version is carried only by `PromptVersion`.
  - `PromptVersion` is a positive integer and not semantic-version formatted.
  - Missing placeholder evaluation uses unique lexically sorted `MissingPlaceholders`.
  - Prompting never emits a partially rendered prompt.

## Reason Codes (Plan 1 Pinned Set)
- Prompt rendering required-placeholder failures:
  - `missing_required_placeholder`
- Gemini adapter protocol/transport mapping failures:
  - `provider_protocol_error`
- Rule:
  - Adapter and harness assertions must use these exact lowercase snake_case values.
  - Gemini protocol-error path must emit `provider_protocol_error` (no aliases).

## Git Branch and PR Tracking
- Execution Branch: `not-applicable (draft planning on main)`
- Base Branch: `main`
- PR: `not-applicable`

## Touchpoint Map
- Code:
  - `Zelanthus.slnx`
  - `Source/Zelanthus.API` (no changes expected for this plan; reference-only composition context)
  - `Source/Zelanthus.Prompting`
  - `Source/Zelanthus.Llm.Clients.Abstractions`
  - `Source/Zelanthus.Llm.Clients.Gemini`
  - `Tests/Zelanthus.Architecture.Tests`
  - `Tests/Zelanthus.WorkflowContractProofs.Tests`
- Docs:
  - `Plans/Drafts/mvp-prompting-gemini-contract-baseline/plan.md`
  - `Plans/Drafts/mvp-prompting-gemini-contract-baseline/steps/index.md`
  - `Plans/Drafts/mvp-prompting-gemini-contract-baseline/risks/risk-log.md`
  - `Plans/Drafts/thin-clients-first-solution-structure/decisions/0008-mvp-harness-shape-selection-v0.md`
- Infrastructure/Config:
  - Plan 1 persistence scope is test proof-artifact output only (repo-local artifacts), not StoryEngine persistence infrastructure.
  - Plan 2 will own runner/persistence infrastructure planning and implementation details.
  - Plan 2 local persistence target is workspace-local file/path-backed storage for runner checkpoints/artifacts/provenance (not external database/cache/queue/service storage).

## Risks and Mitigations
- Risk: Gemini integration variance causes unstable harness outcomes.
  - Mitigation: Validate normalized response contracts and reason-coded failures, not provider prose details; keep live provider tests opt-in.
- Risk: Boundary drift during project scaffolding.
  - Mitigation: Add architecture tests before broader implementation spread.
- Risk: Prompting and abstraction contracts drift during parallel implementation.
  - Mitigation: Lock call-envelope and provenance acceptance criteria first.
- Risk: Sub-template/import requirements appear during implementation and cause ad-hoc composition behavior.
  - Mitigation: Keep Plan 1 single-template only and open a dedicated follow-up plan for template composition/imports.

## Validation and Testing
- Automated:
  - `dotnet build Zelanthus.slnx`
  - default deterministic test path:
    - `dotnet test --filter "Category!=LiveGemini"`
  - live Gemini opt-in path:
    - set environment variable `ZELANTHUS_RUN_LIVE_GEMINI_TESTS=1`
    - run `dotnet test --filter "Category=LiveGemini"`
- Manual:
  - Verify planned contracts map to Decision `0006` envelope and Decision `0008` harness rules.
  - Verify placeholder vs `TokenAccounting` terminology split is preserved across plan artifacts.
  - Verify `RequiredPlaceholders` source-of-truth is authored metadata, not runtime inference.
  - Verify template definition shape includes `PromptId`, `PromptVersion`, `TemplateText`, and `RequiredPlaceholders`.
  - Verify `PromptVersion` is integer-based and carried unchanged.
  - Verify Gemini protocol-error mapping uses reason code `provider_protocol_error`.
  - Verify all required Plan 1 touchpoints are covered by at least one step.

## Harness Acceptance Evidence (Single Project)
- Required test project:
  - `Tests/Zelanthus.WorkflowContractProofs.Tests`
- Required proof tests (exact names):
  - `PromptRendering_TwoRequiredPlaceholders_OneMissing_ReturnsDeterministicRenderFailure`
  - `PromptRendering_TwoRequiredPlaceholders_OneMissing_ReportsMissingPlaceholdersInLexicalOrder`
  - `PromptRendering_AllRequiredPlaceholdersProvided_ReturnsRenderedPromptWithStableChecksum`
  - `GeminiAdapter_ValidProviderResponse_ProducesNormalizedResponseEnvelope`
  - `GeminiAdapter_ProviderProtocolError_EmitsProviderProtocolErrorReasonCode`
  - `GeminiAdapter_ResponseWithoutUsage_MapsTokenAccountingToUnknown`
- Required failure expectations:
  - reason code equals `missing_required_placeholder`,
  - missing list includes exactly the missing placeholder key(s),
  - missing list ordering is stable/deterministic.
- Required prompt contract expectations:
  - `RequiredPlaceholders` validation uses authored metadata as the source of truth,
  - `PromptVersion` is a positive integer,
  - checksum generation uses `SHA-256`.
- Required Gemini expectations:
  - valid provider response maps to normalized envelope shape,
  - provider protocol error maps to explicit reason-coded failure with `ReasonCode == provider_protocol_error`,
  - missing usage fields map `TokenAccounting` values to `unknown`.
- Required execution-mode expectations:
  - default harness runs do not require live provider calls,
  - live provider calls execute only through explicit opt-in path.

## Rollout / Rollback
- Rollout:
  - Finalize draft details and acceptance criteria.
  - Promote this plan only after user approval and branch strategy lock in `Plans/README.md`.
- Rollback:
  - Supersede with revised plan and archive current draft if boundaries materially change.

## Status Tracker
- [ ] `0010-scaffold-projects-and-references`
- [ ] `0020-define-prompting-contracts-and-rendering-rules`
- [ ] `0030-define-llm-client-abstractions-and-capability-profile`
- [ ] `0040-implement-gemini-adapter-normalization-path`
- [ ] `0050-add-architecture-boundary-tests`
- [ ] `0060-implement-contract-proof-tests-and-local-artifact-persistence`
- [ ] `0070-validate-golden-path-and-reason-coded-failure-path`

## Notes
- Workspace baseline remains `.NET 10` (`net10.0`) for all new projects in this plan.

---

## Source 2: `risks/risk-log.md`

# Risk Log

## R-001 Contract Surface Drift
- Statement: Prompting and LLM abstraction contracts drift during early implementation and invalidate adapter/harness assumptions.
- Impact: Rework across multiple projects and delayed MVP proof.
- Mitigation: Lock contract-level acceptance criteria in early steps before adapter and harness implementation.
- Status: open

## R-002 Harness Flakiness
- Statement: External provider behavior variance causes unstable MVP harness tests.
- Impact: Low-confidence acceptance evidence.
- Mitigation: Assert normalized contracts and reason codes; avoid brittle assertions against generated prose; run live provider tests only through explicit opt-in execution path.
- Status: open

## R-003 Boundary Violations
- Statement: Initial project scaffolding introduces forbidden dependencies.
- Impact: Architecture debt and doctrinal drift.
- Mitigation: Add architecture tests as an early gate, not a late cleanup task.
- Status: open

## R-004 Deferred Template Composition
- Statement: Sub-template/import use-cases may appear before a dedicated composition plan is ready.
- Impact: Ad-hoc composition behavior and contract inconsistency.
- Mitigation: Keep Plan 1 single-template rendering only; open a dedicated follow-up plan for composition/import contracts.
- Status: open

---

## Source 3: `steps/index.md`

# Steps Index

Use this file for dependency and status summaries.
Do not use this file as the only source of step order.
Step order must still be visible in folder names (`NNNN-semantic-step-name`).

## Step Map
| Order | Step Folder | Status | Depends On | Notes |
|---|---|---|---|---|
| 0010 | `0010-scaffold-projects-and-references` | `pending` | `none` | Create initial Source/Tests projects and wire solution references |
| 0020 | `0020-define-prompting-contracts-and-rendering-rules` | `pending` | `0010-scaffold-projects-and-references`, `0050-add-architecture-boundary-tests` | Define prompt identity/version/render/checksum and required-placeholder enforcement behavior |
| 0030 | `0030-define-llm-client-abstractions-and-capability-profile` | `pending` | `0010-scaffold-projects-and-references`, `0050-add-architecture-boundary-tests` | Define execution envelope, normalized response, capabilities, and reason-coded errors |
| 0040 | `0040-implement-gemini-adapter-normalization-path` | `pending` | `0030-define-llm-client-abstractions-and-capability-profile`, `0050-add-architecture-boundary-tests` | Map Gemini protocol to abstraction contracts and normalized metadata |
| 0050 | `0050-add-architecture-boundary-tests` | `pending` | `0010-scaffold-projects-and-references` | Add dependency-direction tests as an early enforcement gate; must complete before 0020/0030/0040 |
| 0060 | `0060-implement-contract-proof-tests-and-local-artifact-persistence` | `pending` | `0020-define-prompting-contracts-and-rendering-rules`, `0030-define-llm-client-abstractions-and-capability-profile`, `0040-implement-gemini-adapter-normalization-path` | Build contract-proof tests and artifact/provenance persistence proof path |
| 0070 | `0070-validate-golden-path-and-reason-coded-failure-path` | `pending` | `0050-add-architecture-boundary-tests`, `0060-implement-contract-proof-tests-and-local-artifact-persistence` | Validate success and failure evidence against Plan 1 acceptance criteria |

## Status Values
- `pending`
- `in_progress`
- `blocked`
- `completed`

---

## Source 4: `steps/0010-scaffold-projects-and-references/step.md`

# Step: 0010-scaffold-projects-and-references

## Goal
- Define and scaffold the minimal project set for Plan 1 under `Source/` and `Tests/`.

## Context
- All contract work depends on stable project boundaries and reference direction.

## Git Branch
- `main`

## Commits
- `none`

## Commands Executed
- `none`

## Files Changed
- `none`

## Tests / Results
- `not-run` -> pending draft step

## Issues
- none

## Decision
- pending

## Completion
- `pending`

## Next Actions
- Finalize planned project list and reference rules against dependency-direction policy.

---

## Source 5: `steps/0020-define-prompting-contracts-and-rendering-rules/step.md`

# Step: 0020-define-prompting-contracts-and-rendering-rules

## Goal
- Define Plan 1 contract scope for `Zelanthus.Prompting`.

## Context
- Prompt governance and deterministic rendering are core MVP correctness requirements.

## Git Branch
- `main`

## Commits
- `none`

## Commands Executed
- `none`

## Files Changed
- `none`

## Outputs
- `Plans/Drafts/mvp-prompting-gemini-contract-baseline/plan.md`
  - Terminology split section (`Placeholder` vs `TokenAccounting`) is present and explicit.
  - Template definition shape is explicit (`PromptId`, `PromptVersion`, `TemplateText`, `RequiredPlaceholders`).
  - Prompt rendering contract sketch includes `RenderedPrompt` and `RenderFailure`.
  - Prompt identity/version rule is explicit (`PromptId` stable, `PromptVersion` only version marker).
  - Prompt version format rule is explicit (`PromptVersion` is positive integer).
  - Placeholder key grammar/semantics are explicit.
  - Placeholder source-of-truth rule is explicit (authored metadata, not runtime inference).
  - Checksum canonicalization rules are explicit.
  - Deterministic missing-placeholder failure rule is explicit.
- `Plans/Drafts/mvp-prompting-gemini-contract-baseline/steps/index.md`
  - Step description uses placeholder terminology for Prompting scope.

## Tests / Results
- `not-run` -> pending draft step

## Acceptance Evidence
- Required implementation tests (exact names):
  - `PromptRendering_TwoRequiredPlaceholders_OneMissing_ReturnsDeterministicRenderFailure`
  - `PromptRendering_TwoRequiredPlaceholders_OneMissing_ReportsMissingPlaceholdersInLexicalOrder`
  - `PromptRendering_AllRequiredPlaceholdersProvided_ReturnsRenderedPromptWithStableChecksum`
- Required assertions:
  - missing-required-placeholder scenario returns `RenderFailure`,
  - `ReasonCode == missing_required_placeholder`,
  - `MissingPlaceholders` includes exactly the missing key(s),
  - `MissingPlaceholders` ordering is lexical and deterministic,
  - `MissingPlaceholders` is deduplicated before lexical sorting,
  - `PromptId` remains stable and does not embed version,
  - `PromptVersion` is the only version marker used by the render contract,
  - `PromptVersion` is constrained to positive integers (`1`, `2`, ...),
  - `RequiredPlaceholders` is sourced from authored template metadata,
  - runtime required-placeholder validation reads from template definition metadata (not template text inference),
  - runtime validation does not infer required placeholder list from template text,
  - placeholder key validation uses `^[a-z][a-z0-9_]*$` and rejects leading/trailing whitespace,
  - extra `PlaceholderValues` keys are ignored for MVP and do not fail rendering,
  - missing means absent key or `null` value; empty string is treated as provided,
  - checksum algorithm is `SHA-256`,
  - checksum canonicalization uses UTF-8 with line endings normalized to `\n`,
  - checksum input covers `PromptId`, `PromptVersion`, and `RenderedText` in fixed order,
  - all-placeholders-present scenario returns `RenderedPrompt` with stable checksum.

## Issues
- none

## Decision
- pending

## Completion
- `pending`

## Next Actions
- Lock prompt identity/version/render/checksum and required-placeholder acceptance criteria for InProgress planning.

---

## Source 6: `steps/0030-define-llm-client-abstractions-and-capability-profile/step.md`

# Step: 0030-define-llm-client-abstractions-and-capability-profile

## Goal
- Define Plan 1 baseline abstractions for execution envelope, normalized responses, capabilities, `TokenAccounting`, and reason-coded failures.

## Context
- Provider integrations must conform to shared contracts before adapter implementation begins.

## Git Branch
- `main`

## Commits
- `none`

## Commands Executed
- `none`

## Files Changed
- `none`

## Outputs
- `Plans/Drafts/mvp-prompting-gemini-contract-baseline/plan.md`
  - Plan-level reason code section pins adapter protocol failures to `provider_protocol_error`.
  - Validation/testing section documents deterministic default test path and opt-in live Gemini path.

## Acceptance Evidence
- Required contract assertions:
  - abstraction-level error shapes support canonical `ReasonCode` values,
  - Gemini protocol/transport failures map to `provider_protocol_error`,
  - usage accounting maps absent provider usage fields to `unknown`.

## Tests / Results
- `not-run` -> pending draft step

## Issues
- none

## Decision
- pending

## Completion
- `pending`

## Next Actions
- Finalize abstraction acceptance criteria and boundary rules for Gemini adapter implementation.

---

## Source 7: `steps/0040-implement-gemini-adapter-normalization-path/step.md`

# Step: 0040-implement-gemini-adapter-normalization-path

## Goal
- Define Plan 1 adapter implementation scope for mapping Gemini protocol responses into normalized contracts.

## Context
- Gemini is the first provider baseline, but contracts must remain provider-agnostic at the application boundary.

## Git Branch
- `main`

## Commits
- `none`

## Commands Executed
- `none`

## Files Changed
- `none`

## Outputs
- `Plans/Drafts/mvp-prompting-gemini-contract-baseline/plan.md`
  - Gemini expectations pin protocol-error mapping to `provider_protocol_error`.
  - Harness execution mode rules clarify live-provider calls are opt-in.

## Acceptance Evidence
- Required implementation tests (exact names):
  - `GeminiAdapter_ValidProviderResponse_ProducesNormalizedResponseEnvelope`
  - `GeminiAdapter_ProviderProtocolError_EmitsProviderProtocolErrorReasonCode`
  - `GeminiAdapter_ResponseWithoutUsage_MapsTokenAccountingToUnknown`
- Required assertions:
  - protocol/transport failures emit `ReasonCode == provider_protocol_error`,
  - missing usage fields map `TokenAccounting` values to `unknown`,
  - adapter proof tests run in default deterministic mode without requiring live-provider calls.

## Tests / Results
- `not-run` -> pending draft step

## Issues
- none

## Decision
- pending

## Completion
- `pending`

## Next Actions
- Specify adapter acceptance tests for normalized metadata, `TokenAccounting`, and failure translation.

---

## Source 8: `steps/0050-add-architecture-boundary-tests/step.md`

# Step: 0050-add-architecture-boundary-tests

## Goal
- Define architecture test coverage that enforces dependency-direction rules during Plan 1 implementation.

## Context
- Boundary drift is highest during initial scaffolding and must be blocked early.

## Git Branch
- `main`

## Commits
- `none`

## Commands Executed
- `none`

## Files Changed
- `none`

## Tests / Results
- `not-run` -> pending draft step

## Issues
- none

## Decision
- pending

## Completion
- `pending`

## Next Actions
- Lock architecture assertions and required pass/fail gates for InProgress execution.

---

## Source 9: `steps/0060-implement-contract-proof-tests-and-local-artifact-persistence/step.md`

# Step: 0060-implement-contract-proof-tests-and-local-artifact-persistence

## Goal
- Define implementation boundaries for `Tests/Zelanthus.WorkflowContractProofs.Tests` and local artifact/provenance persistence in Plan 1.

## Context
- Decision `0008` selected the contract-proof test-host baseline, so Plan 1 must prove evidence capture without API-host coupling.

## Git Branch
- `main`

## Commits
- `none`

## Commands Executed
- `none`

## Files Changed
- `none`

## Outputs
- `Plans/Drafts/mvp-prompting-gemini-contract-baseline/steps/0060-implement-contract-proof-tests-and-local-artifact-persistence/artifacts/initial-test-naming-seed.md`
  - Seed test names for placeholder and Gemini adapter contract proofs.
- `Plans/Drafts/mvp-prompting-gemini-contract-baseline/steps/0060-implement-contract-proof-tests-and-local-artifact-persistence/artifacts/contract-proof-evidence-spec.md`
  - Planned evidence artifact names and required fields for deterministic validation.

## Tests / Results
- `not-run` -> pending draft step

## Acceptance Evidence
- Required implementation test project:
  - `Tests/Zelanthus.WorkflowContractProofs.Tests`
- Required implementation tests (exact names):
  - `PromptRendering_TwoRequiredPlaceholders_OneMissing_ReturnsDeterministicRenderFailure`
  - `PromptRendering_TwoRequiredPlaceholders_OneMissing_ReportsMissingPlaceholdersInLexicalOrder`
  - `PromptRendering_AllRequiredPlaceholdersProvided_ReturnsRenderedPromptWithStableChecksum`
  - `GeminiAdapter_ValidProviderResponse_ProducesNormalizedResponseEnvelope`
  - `GeminiAdapter_ProviderProtocolError_EmitsProviderProtocolErrorReasonCode`
  - `GeminiAdapter_ResponseWithoutUsage_MapsTokenAccountingToUnknown`
- Required implementation evidence artifacts:
  - `artifacts/workflow-contract-proofs/prompt-render-failure-missing-placeholder.json`
  - `artifacts/workflow-contract-proofs/prompt-render-success.json`
  - `artifacts/workflow-contract-proofs/prompt-render-checksum-stability.json`
  - `artifacts/workflow-contract-proofs/gemini-normalized-response.json`
  - `artifacts/workflow-contract-proofs/gemini-provider-protocol-error.json`
  - `artifacts/workflow-contract-proofs/gemini-token-accounting-unknown.json`
- Required artifact assertions:
  - failure artifact captures `ReasonCode == missing_required_placeholder`,
  - failure artifact captures lexically sorted `MissingPlaceholders`,
  - failure/success artifacts carry integer `promptVersion` values,
  - success artifact captures `requiredPlaceholders` from authored template metadata,
  - success artifact captures stable checksum for unchanged input,
  - checksum artifacts record `SHA-256` as the checksum algorithm,
  - normalized-response artifact proves provider response mapping into normalized envelope fields,
  - provider-error artifact proves reason-coded protocol error mapping with `ReasonCode == provider_protocol_error`,
  - token-accounting artifact proves missing usage maps to `unknown`.
- Required execution-mode assertions:
  - default contract-proof test runs exclude live Gemini tests,
  - live Gemini tests require explicit opt-in execution intent.

## Issues
- none

## Decision
- pending

## Completion
- `pending`

## Next Actions
- Define contract-proof test command paths, artifact locations, and evidence schema for golden/failure scenarios.

---

## Source 10: `steps/0070-validate-golden-path-and-reason-coded-failure-path/step.md`

# Step: 0070-validate-golden-path-and-reason-coded-failure-path

## Goal
- Define final Plan 1 acceptance checks for golden-path execution and deterministic reason-coded failures.

## Context
- Plan 1 is complete only when contract behavior is proven with reproducible evidence, not just successful compilation.

## Git Branch
- `main`

## Commits
- `none`

## Commands Executed
- `none`

## Files Changed
- `none`

## Outputs
- `Plans/Drafts/mvp-prompting-gemini-contract-baseline/steps/0070-validate-golden-path-and-reason-coded-failure-path/artifacts/final-acceptance-checklist.md`
  - Final checklist mapping tests and artifacts to Plan 1 DoD acceptance points.

## Tests / Results
- `not-run` -> pending draft step

## Acceptance Evidence
- Required implementation test pass evidence:
  - `PromptRendering_TwoRequiredPlaceholders_OneMissing_ReturnsDeterministicRenderFailure`
  - `PromptRendering_TwoRequiredPlaceholders_OneMissing_ReportsMissingPlaceholdersInLexicalOrder`
  - `PromptRendering_AllRequiredPlaceholdersProvided_ReturnsRenderedPromptWithStableChecksum`
  - `GeminiAdapter_ValidProviderResponse_ProducesNormalizedResponseEnvelope`
  - `GeminiAdapter_ProviderProtocolError_EmitsProviderProtocolErrorReasonCode`
  - `GeminiAdapter_ResponseWithoutUsage_MapsTokenAccountingToUnknown`
- Required implementation artifact evidence:
  - `artifacts/workflow-contract-proofs/prompt-render-failure-missing-placeholder.json`
  - `artifacts/workflow-contract-proofs/prompt-render-success.json`
  - `artifacts/workflow-contract-proofs/prompt-render-checksum-stability.json`
  - `artifacts/workflow-contract-proofs/gemini-normalized-response.json`
  - `artifacts/workflow-contract-proofs/gemini-provider-protocol-error.json`
  - `artifacts/workflow-contract-proofs/gemini-token-accounting-unknown.json`
- Required acceptance assertions:
  - deterministic failure on missing required placeholder,
  - missing list contains exactly missing placeholder keys,
  - missing list order is stable,
  - reason code is `missing_required_placeholder`,
  - prompt `requiredPlaceholders` comes from authored template metadata,
  - prompt `promptVersion` is positive integer format,
  - success path checksum is stable for identical inputs,
  - checksum algorithm is `SHA-256`,
  - Gemini valid-response mapping produces normalized envelope fields,
  - Gemini protocol errors map to explicit reason-coded failures with `ReasonCode == provider_protocol_error`,
  - missing Gemini usage fields map `TokenAccounting` values to `unknown`.
- Required execution-mode assertions:
  - default acceptance run uses deterministic fixture/recorded path without live provider dependency,
  - live Gemini execution path requires explicit opt-in.

## Issues
- none

## Decision
- pending

## Completion
- `pending`

## Next Actions
- Lock final evidence checklist required before Plan 1 promotion to InProgress.

---

## Source 11: `steps/0060-implement-contract-proof-tests-and-local-artifact-persistence/artifacts/contract-proof-evidence-spec.md`

# Contract Proof Evidence Spec

## Scope
- Plan: `mvp-prompting-gemini-contract-baseline`
- Step: `0060-implement-contract-proof-tests-and-local-artifact-persistence`

## Required Evidence Artifacts
- `artifacts/workflow-contract-proofs/prompt-render-failure-missing-placeholder.json`
- `artifacts/workflow-contract-proofs/prompt-render-success.json`
- `artifacts/workflow-contract-proofs/prompt-render-checksum-stability.json`
- `artifacts/workflow-contract-proofs/gemini-normalized-response.json`
- `artifacts/workflow-contract-proofs/gemini-provider-protocol-error.json`
- `artifacts/workflow-contract-proofs/gemini-token-accounting-unknown.json`

## Required Fields
- Failure artifact:
  - `promptId`
  - `promptVersion`
  - `reasonCode`
  - `missingPlaceholders`
- Success artifact:
  - `promptId`
  - `promptVersion`
  - `renderedText`
  - `checksum`
  - `checksumAlgorithm`
  - `requiredPlaceholders`
  - `requiredPlaceholdersSource`
- Checksum stability artifact:
  - `scenario`
  - `checksumRun1`
  - `checksumRun2`
  - `checksumsMatch`
  - `checksumAlgorithm`
- Gemini normalized-response artifact:
  - `providerId`
  - `modelId`
  - `normalizedResponse`
  - `tokenAccounting`
- Gemini provider-error artifact:
  - `providerId`
  - `modelId`
  - `reasonCode`
  - `errorMetadata`
- Gemini token-accounting-unknown artifact:
  - `providerId`
  - `modelId`
  - `tokenAccounting.promptTokens`
  - `tokenAccounting.outputTokens`
  - `tokenAccounting.totalTokens`

## Determinism Rules
- `reasonCode` must equal `missing_required_placeholder` for missing-required-placeholder scenarios.
- `missingPlaceholders` must be lexically sorted for stable assertions.
- `promptVersion` must be a positive integer.
- `requiredPlaceholdersSource` must equal `template_metadata`.
- Stable input must produce stable checksum.
- `checksumAlgorithm` must equal `SHA-256`.
- Gemini protocol error mapping must emit explicit reason-coded failures.
- Gemini protocol error mapping must emit `reasonCode == provider_protocol_error`.
- Missing Gemini usage fields must map to `unknown` token-accounting values.

---

## Source 12: `steps/0060-implement-contract-proof-tests-and-local-artifact-persistence/artifacts/initial-test-naming-seed.md`

# Initial Test Naming Seed

## Project
- `Tests/Zelanthus.WorkflowContractProofs.Tests`

## Naming Pattern
- `<ContractSurface>_<Scenario>_<ExpectedOutcome>`

## Seed Tests
- `PromptRendering_TwoRequiredPlaceholders_OneMissing_ReturnsDeterministicRenderFailure`
- `PromptRendering_TwoRequiredPlaceholders_OneMissing_ReportsMissingPlaceholdersInLexicalOrder`
- `PromptRendering_AllRequiredPlaceholdersProvided_ReturnsRenderedPromptWithStableChecksum`
- `GeminiAdapter_ValidProviderResponse_ProducesNormalizedResponseEnvelope`
- `GeminiAdapter_ProviderProtocolError_EmitsProviderProtocolErrorReasonCode`
- `GeminiAdapter_ResponseWithoutUsage_MapsTokenAccountingToUnknown`
- `GeminiAdapter_LiveProviderCategory_IsOptInOnly`
- `ProvenanceRecord_GoldenPathExecution_ContainsRequiredMvpFields`

---

## Source 13: `steps/0070-validate-golden-path-and-reason-coded-failure-path/artifacts/final-acceptance-checklist.md`

# Final Acceptance Checklist

## Scope
- Plan: `mvp-prompting-gemini-contract-baseline`
- Step: `0070-validate-golden-path-and-reason-coded-failure-path`

## Required Tests
- `PromptRendering_TwoRequiredPlaceholders_OneMissing_ReturnsDeterministicRenderFailure`
- `PromptRendering_TwoRequiredPlaceholders_OneMissing_ReportsMissingPlaceholdersInLexicalOrder`
- `PromptRendering_AllRequiredPlaceholdersProvided_ReturnsRenderedPromptWithStableChecksum`
- `GeminiAdapter_ValidProviderResponse_ProducesNormalizedResponseEnvelope`
- `GeminiAdapter_ProviderProtocolError_EmitsProviderProtocolErrorReasonCode`
- `GeminiAdapter_ResponseWithoutUsage_MapsTokenAccountingToUnknown`

## Required Artifacts
- `artifacts/workflow-contract-proofs/prompt-render-failure-missing-placeholder.json`
- `artifacts/workflow-contract-proofs/prompt-render-success.json`
- `artifacts/workflow-contract-proofs/prompt-render-checksum-stability.json`
- `artifacts/workflow-contract-proofs/gemini-normalized-response.json`
- `artifacts/workflow-contract-proofs/gemini-provider-protocol-error.json`
- `artifacts/workflow-contract-proofs/gemini-token-accounting-unknown.json`

## Acceptance Assertions
- Missing-required-placeholder scenario returns deterministic `RenderFailure`.
- Failure `ReasonCode` equals `missing_required_placeholder`.
- Failure `MissingPlaceholders` includes exactly missing keys and is lexically sorted.
- `RequiredPlaceholders` source is authored template metadata (`template_metadata`).
- `PromptVersion` is a positive integer.
- All-required-placeholders scenario returns `RenderedPrompt`.
- Stable input produces stable checksum.
- Checksum algorithm is `SHA-256`.
- Gemini valid responses map to normalized envelope fields.
- Gemini protocol errors map to explicit reason-coded failures with `ReasonCode == provider_protocol_error`.
- Missing Gemini usage fields map to `unknown` token-accounting values.
- Default acceptance run does not require live-provider calls.
- Live-provider tests run only through explicit opt-in execution path.

---

## Source 14: `archive-note.md`

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
