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
