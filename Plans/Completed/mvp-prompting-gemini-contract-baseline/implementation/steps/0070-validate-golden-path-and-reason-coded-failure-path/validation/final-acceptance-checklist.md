# Plan 1 Final Acceptance Checklist

## Command Evidence
- `dotnet test "Tests/Zelanthus.WorkflowContractProofs.Tests/Zelanthus.WorkflowContractProofs.Tests.csproj" --filter "Category!=LiveGemini"` -> passed (7 tests)
- `dotnet test "Zelanthus.slnx" --filter "Category!=LiveGemini"` -> passed (11 tests)

## Required Tests
- [x] `PromptRendering_TwoRequiredPlaceholders_OneMissing_ReturnsDeterministicRenderFailure`
- [x] `PromptRendering_TwoRequiredPlaceholders_OneMissing_ReportsMissingPlaceholdersInLexicalOrder`
- [x] `PromptRendering_AllRequiredPlaceholdersProvided_ReturnsRenderedPromptWithStableChecksum`
- [x] `GeminiAdapter_ValidProviderResponse_ProducesNormalizedResponseEnvelope`
- [x] `GeminiAdapter_ProviderProtocolError_EmitsProviderProtocolErrorReasonCode`
- [x] `GeminiAdapter_ResponseWithoutUsage_MapsTokenAccountingToUnknown`

## Required Artifact Outputs
- [x] `artifacts/workflow-contract-proofs/prompt-render-failure-missing-placeholder.json`
- [x] `artifacts/workflow-contract-proofs/prompt-render-success.json`
- [x] `artifacts/workflow-contract-proofs/prompt-render-checksum-stability.json`
- [x] `artifacts/workflow-contract-proofs/gemini-normalized-response.json`
- [x] `artifacts/workflow-contract-proofs/gemini-provider-protocol-error.json`
- [x] `artifacts/workflow-contract-proofs/gemini-token-accounting-unknown.json`

## Assertions Confirmed
- [x] Missing required placeholder failures return `ReasonCode == missing_required_placeholder`.
- [x] Missing placeholder list is deterministic and lexical.
- [x] Prompt versions remain integer-based (`PromptVersion` as positive integer).
- [x] Prompt checksum uses SHA-256 canonicalization.
- [x] Gemini protocol failures map to `provider_protocol_error`.
- [x] Missing Gemini usage maps all token accounting fields to `unknown`.
- [x] Default proof execution path is deterministic and does not require live Gemini calls.
