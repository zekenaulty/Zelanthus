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
