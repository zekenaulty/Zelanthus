# Step: 0030-define-llm-client-abstractions-and-capability-profile

## Goal
- Define Plan 1 baseline abstractions for execution envelope, normalized responses, capabilities, `TokenAccounting`, and reason-coded failures.

## Context
- Provider integrations must conform to shared contracts before adapter implementation begins.

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
  - Plan-level reason code section pins adapter protocol failures to `provider_protocol_error`.
  - Validation/testing section documents deterministic default test path and opt-in live Gemini path.

## Acceptance Evidence
- Required contract assertions:
  - abstraction-level error shapes support canonical `ReasonCode` values,
  - Gemini protocol/transport failures map to `provider_protocol_error`,
  - usage accounting maps absent provider usage fields to `unknown`.

## Tests / Results
- `not-run` -> pending draft step

## Issues
- none

## Decision
- pending

## Completion
- `pending`

## Next Actions
- Finalize abstraction acceptance criteria and boundary rules for Gemini adapter implementation.
