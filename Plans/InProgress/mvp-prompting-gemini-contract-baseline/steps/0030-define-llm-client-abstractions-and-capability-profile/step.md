# Step: 0030-define-llm-client-abstractions-and-capability-profile

## Goal
- Define Plan 1 baseline abstractions for execution envelope, normalized responses, capabilities, `TokenAccounting`, and reason-coded failures.

## Context
- Provider integrations must conform to shared contracts before adapter implementation begins.

## Git Branch
- `feature/thin-clients-first-solution-structure`

## Commits
- `none`

## Commands Executed
- `dotnet build "Zelanthus.slnx"`
- `dotnet test "Tests/Zelanthus.Architecture.Tests/Zelanthus.Architecture.Tests.csproj"`

## Files Changed
- `Source/Zelanthus.Llm.Clients.Abstractions/ChainMode.cs`
- `Source/Zelanthus.Llm.Clients.Abstractions/ExecutionContext.cs`
- `Source/Zelanthus.Llm.Clients.Abstractions/ExecutionEnvelope.cs`
- `Source/Zelanthus.Llm.Clients.Abstractions/ILlmClient.cs`
- `Source/Zelanthus.Llm.Clients.Abstractions/LlmCapabilityProfile.cs`
- `Source/Zelanthus.Llm.Clients.Abstractions/LlmExecutionResult.cs`
- `Source/Zelanthus.Llm.Clients.Abstractions/LlmFailure.cs`
- `Source/Zelanthus.Llm.Clients.Abstractions/LlmReasonCodes.cs`
- `Source/Zelanthus.Llm.Clients.Abstractions/NormalizedResponse.cs`
- `Source/Zelanthus.Llm.Clients.Abstractions/NormalizedResponseEnvelope.cs`
- `Source/Zelanthus.Llm.Clients.Abstractions/ProviderMetadata.cs`
- `Source/Zelanthus.Llm.Clients.Abstractions/RenderedPromptPayload.cs`
- `Source/Zelanthus.Llm.Clients.Abstractions/TokenAccounting.cs`
- `Source/Zelanthus.Llm.Clients.Abstractions/TokenUsageValue.cs`
- `Source/Zelanthus.Llm.Clients.Abstractions/Class1.cs` (deleted)

## Outputs
- `Plans/InProgress/mvp-prompting-gemini-contract-baseline/plan.md`
  - Plan-level reason code section pins adapter protocol failures to `provider_protocol_error`.
  - Validation/testing section documents deterministic default test path and opt-in live Gemini path.

## Acceptance Evidence
- Required contract assertions:
  - abstraction-level error shapes support canonical `ReasonCode` values,
  - Gemini protocol/transport failures map to `provider_protocol_error`,
  - usage accounting maps absent provider usage fields to `unknown`.

## Tests / Results
- `dotnet build "Zelanthus.slnx"` -> `passed` (0 warnings, 0 errors)
- `dotnet test "Tests/Zelanthus.Architecture.Tests/Zelanthus.Architecture.Tests.csproj"` -> `passed` (4 tests)

## Issues
- none

## Decision
- accepted: abstraction baseline now defines canonical execution envelope, normalized response envelope, capability profile, token accounting, and reason-coded failure contracts.

## Completion
- `completed`

## Next Actions
- Validate abstraction contracts through Gemini adapter proofs in `0040` and `0060`.


