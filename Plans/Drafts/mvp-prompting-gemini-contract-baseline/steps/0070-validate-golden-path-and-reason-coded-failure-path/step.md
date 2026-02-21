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
