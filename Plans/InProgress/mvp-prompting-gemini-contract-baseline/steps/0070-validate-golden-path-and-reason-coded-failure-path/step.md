# Step: 0070-validate-golden-path-and-reason-coded-failure-path

## Goal
- Define final Plan 1 acceptance checks for golden-path execution and deterministic reason-coded failures.

## Context
- Plan 1 is complete only when contract behavior is proven with reproducible evidence, not just successful compilation.

## Git Branch
- `feature/thin-clients-first-solution-structure`

## Commits
- `f8454a38ae534c78691921c81d0f30fd4ae94ab7`

## Commands Executed
- `dotnet test "Tests/Zelanthus.WorkflowContractProofs.Tests/Zelanthus.WorkflowContractProofs.Tests.csproj" --filter "Category!=LiveGemini"`
- `dotnet test "Zelanthus.slnx" --filter "Category!=LiveGemini"`

## Files Changed
- `Plans/InProgress/mvp-prompting-gemini-contract-baseline/steps/0070-validate-golden-path-and-reason-coded-failure-path/validation/final-acceptance-checklist.md`
- `artifacts/workflow-contract-proofs/prompt-render-failure-missing-placeholder.json` (validated)
- `artifacts/workflow-contract-proofs/prompt-render-success.json` (validated)
- `artifacts/workflow-contract-proofs/prompt-render-checksum-stability.json` (validated)
- `artifacts/workflow-contract-proofs/gemini-normalized-response.json` (validated)
- `artifacts/workflow-contract-proofs/gemini-provider-protocol-error.json` (validated)
- `artifacts/workflow-contract-proofs/gemini-token-accounting-unknown.json` (validated)

## Outputs
- `Plans/InProgress/mvp-prompting-gemini-contract-baseline/steps/0070-validate-golden-path-and-reason-coded-failure-path/validation/final-acceptance-checklist.md`
  - Final checklist mapping tests and artifacts to Plan 1 DoD acceptance points.

## Tests / Results
- `dotnet test "Tests/Zelanthus.WorkflowContractProofs.Tests/Zelanthus.WorkflowContractProofs.Tests.csproj" --filter "Category!=LiveGemini"` -> `passed` (7 tests)
- `dotnet test "Zelanthus.slnx" --filter "Category!=LiveGemini"` -> `passed` (11 tests)

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
- accepted: Plan 1 golden path and deterministic reason-coded failure path are validated with required proof tests and evidence artifacts.

## Completion
- `completed`

## Next Actions
- Mark top-level thin-clients execution step `0120` complete and proceed to Plan 2 execution step `0130`.


