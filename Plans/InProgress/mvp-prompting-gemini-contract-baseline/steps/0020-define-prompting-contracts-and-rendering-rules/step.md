# Step: 0020-define-prompting-contracts-and-rendering-rules

## Goal
- Define Plan 1 contract scope for `Zelanthus.Prompting`.

## Context
- Prompt governance and deterministic rendering are core MVP correctness requirements.

## Git Branch
- `feature/thin-clients-first-solution-structure`

## Commits
- `37a32635a968954bdf2d775977e940ee766278f0`

## Commands Executed
- `dotnet build "Zelanthus.slnx"`
- `dotnet test "Tests/Zelanthus.Architecture.Tests/Zelanthus.Architecture.Tests.csproj"`

## Files Changed
- `Source/Zelanthus.Prompting/IPromptRenderer.cs`
- `Source/Zelanthus.Prompting/PlaceholderKeyValidator.cs`
- `Source/Zelanthus.Prompting/PromptReasonCodes.cs`
- `Source/Zelanthus.Prompting/PromptRenderResult.cs`
- `Source/Zelanthus.Prompting/PromptRenderer.cs`
- `Source/Zelanthus.Prompting/PromptTemplateDefinition.cs`
- `Source/Zelanthus.Prompting/RenderFailure.cs`
- `Source/Zelanthus.Prompting/RenderedPrompt.cs`
- `Source/Zelanthus.Prompting/Class1.cs` (deleted)

## Outputs
- `Plans/InProgress/mvp-prompting-gemini-contract-baseline/plan.md`
  - Terminology split section (`Placeholder` vs `TokenAccounting`) is present and explicit.
  - Template definition shape is explicit (`PromptId`, `PromptVersion`, `TemplateText`, `RequiredPlaceholders`).
  - Prompt rendering contract sketch includes `RenderedPrompt` and `RenderFailure`.
  - Prompt identity/version rule is explicit (`PromptId` stable, `PromptVersion` only version marker).
  - Prompt version format rule is explicit (`PromptVersion` is positive integer).
  - Placeholder key grammar/semantics are explicit.
  - Placeholder source-of-truth rule is explicit (authored metadata, not runtime inference).
  - Checksum canonicalization rules are explicit.
  - Deterministic missing-placeholder failure rule is explicit.
- `Plans/InProgress/mvp-prompting-gemini-contract-baseline/steps/index.md`
  - Step description uses placeholder terminology for Prompting scope.

## Tests / Results
- `dotnet build "Zelanthus.slnx"` -> `passed` (0 warnings, 0 errors)
- `dotnet test "Tests/Zelanthus.Architecture.Tests/Zelanthus.Architecture.Tests.csproj"` -> `passed` (4 tests)

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
- accepted: Prompting contract baseline is implemented with deterministic render success/failure outcomes, placeholder validation rules, and SHA-256 checksum canonicalization.

## Completion
- `completed`

## Next Actions
- Implement and validate proof tests in `0060-implement-contract-proof-tests-and-local-artifact-persistence`.


