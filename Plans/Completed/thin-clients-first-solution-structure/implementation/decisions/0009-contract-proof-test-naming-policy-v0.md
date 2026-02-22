# Decision 0009: Contract-Proof Test Naming Policy v0

## Status
- accepted

## Decision Summary
- Adopt semantic naming rules for contract-proof test projects and first proof tests.
- Treat these tests as long-lived contract proof assets, not temporary MVP leftovers.

## Project Naming Rule
- Pattern:
  - `Zelanthus.<semantic-purpose>.Tests`
- Selected project name for Plan 1 proof scope:
  - `Zelanthus.WorkflowContractProofs.Tests`
- Avoid:
  - `MvpHarness`, `TempTests`, `MiscTests`, or sequence-only names.

## Test Naming Rule
- Test names must encode:
  - contract surface,
  - scenario,
  - expected outcome.
- Preferred style:
  - `<ContractSurface>_<Scenario>_<ExpectedOutcome>`

## Initial Seed Test Names (Plan 1)
- `PromptRendering_TwoRequiredPlaceholders_OneMissing_ReturnsDeterministicRenderFailure`
- `PromptRendering_TwoRequiredPlaceholders_OneMissing_ReportsMissingPlaceholdersInLexicalOrder`
- `PromptRendering_AllRequiredPlaceholdersProvided_ReturnsRenderedPromptWithStableChecksum`
- `GeminiAdapter_ValidProviderResponse_ProducesNormalizedResponseEnvelope`
- `GeminiAdapter_ProviderProtocolError_EmitsProviderProtocolErrorReasonCode`
- `GeminiAdapter_ResponseWithoutUsage_MapsTokenAccountingToUnknown`
- `ProvenanceRecord_GoldenPathExecution_ContainsRequiredMvpFields`

## Consequences
- Draft Plan 1 references are updated to `Zelanthus.WorkflowContractProofs.Tests`.
- InProgress implementation reviews should reject low-semantic test naming.
