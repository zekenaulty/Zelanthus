# Step: 0060-implement-contract-proof-tests-and-local-artifact-persistence

## Goal
- Define implementation boundaries for `Tests/Zelanthus.WorkflowContractProofs.Tests` and local artifact/provenance persistence in Plan 1.

## Context
- Decision `0008` selected the contract-proof test-host baseline, so Plan 1 must prove evidence capture without API-host coupling.

## Git Branch
- `feature/thin-clients-first-solution-structure`

## Commits
- `none`

## Commands Executed
- `none`

## Files Changed
- `none`

## Outputs
- `Plans/InProgress/mvp-prompting-gemini-contract-baseline/steps/0060-implement-contract-proof-tests-and-local-artifact-persistence/artifacts/initial-test-naming-seed.md`
  - Seed test names for placeholder and Gemini adapter contract proofs.
- `Plans/InProgress/mvp-prompting-gemini-contract-baseline/steps/0060-implement-contract-proof-tests-and-local-artifact-persistence/artifacts/contract-proof-evidence-spec.md`
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


