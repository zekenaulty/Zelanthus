# Step: 0060-implement-contract-proof-tests-and-local-artifact-persistence

## Goal
- Define implementation boundaries for `Tests/Zelanthus.WorkflowContractProofs.Tests` and local artifact/provenance persistence in Plan 1.

## Context
- Decision `0008` selected the contract-proof test-host baseline, so Plan 1 must prove evidence capture without API-host coupling.

## Git Branch
- `feature/thin-clients-first-solution-structure`

## Commits
- `f8454a38ae534c78691921c81d0f30fd4ae94ab7`

## Commands Executed
- `dotnet test "Tests/Zelanthus.WorkflowContractProofs.Tests/Zelanthus.WorkflowContractProofs.Tests.csproj" --filter "Category!=LiveGemini"`
- `dotnet test "Zelanthus.slnx" --filter "Category!=LiveGemini"`

## Files Changed
- `Tests/Zelanthus.WorkflowContractProofs.Tests/ContractProofArtifactWriter.cs`
- `Tests/Zelanthus.WorkflowContractProofs.Tests/GeminiAdapterContractProofTests.cs`
- `Tests/Zelanthus.WorkflowContractProofs.Tests/PromptRenderingContractProofTests.cs`
- `Tests/Zelanthus.WorkflowContractProofs.Tests/TestAssembly.cs`
- `Tests/Zelanthus.WorkflowContractProofs.Tests/UnitTest1.cs` (deleted)
- `artifacts/workflow-contract-proofs/prompt-render-failure-missing-placeholder.json` (generated)
- `artifacts/workflow-contract-proofs/prompt-render-success.json` (generated)
- `artifacts/workflow-contract-proofs/prompt-render-checksum-stability.json` (generated)
- `artifacts/workflow-contract-proofs/gemini-normalized-response.json` (generated)
- `artifacts/workflow-contract-proofs/gemini-provider-protocol-error.json` (generated)
- `artifacts/workflow-contract-proofs/gemini-token-accounting-unknown.json` (generated)

## Outputs
- `Plans/InProgress/mvp-prompting-gemini-contract-baseline/steps/0060-implement-contract-proof-tests-and-local-artifact-persistence/artifacts/initial-test-naming-seed.md`
  - Seed test names for placeholder and Gemini adapter contract proofs.
- `Plans/InProgress/mvp-prompting-gemini-contract-baseline/steps/0060-implement-contract-proof-tests-and-local-artifact-persistence/artifacts/contract-proof-evidence-spec.md`
  - Planned evidence artifact names and required fields for deterministic validation.

## Tests / Results
- `dotnet test "Tests/Zelanthus.WorkflowContractProofs.Tests/Zelanthus.WorkflowContractProofs.Tests.csproj" --filter "Category!=LiveGemini"` -> `passed` (7 tests)
- `dotnet test "Zelanthus.slnx" --filter "Category!=LiveGemini"` -> `passed` (11 tests)

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
- accepted: contract-proof test harness now produces deterministic prompt and Gemini adapter evidence artifacts on default non-live test runs.

## Completion
- `completed`

## Next Actions
- Execute `0070-validate-golden-path-and-reason-coded-failure-path` to lock final acceptance checklist and close Plan 1.


