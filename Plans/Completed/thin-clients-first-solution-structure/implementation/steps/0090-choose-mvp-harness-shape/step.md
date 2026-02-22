# Step: 0090-choose-mvp-harness-shape

## Goal
- Choose MVP harness shape (test-host vs API-host) against locked acceptance criteria.

## Context
- Harness shape must be selected by contract fit and execution reliability, not convenience.

## Git Branch
- `feature/thin-clients-first-solution-structure`

## Commits
- `none`

## Commands Executed
- `Get-Content "Plans/InProgress/thin-clients-first-solution-structure/decisions/0007-mvp-goals-and-first-plan-boundary-v0.md"`
- `Get-Content "Plans/InProgress/thin-clients-first-solution-structure/decisions/0006-call-envelope-and-enforcement-policy-v0.md"`
- `Get-Content "Plans/README.md"`

## Files Changed
- `Plans/InProgress/thin-clients-first-solution-structure/steps/0090-choose-mvp-harness-shape/step.md`
- `Plans/InProgress/thin-clients-first-solution-structure/steps/0090-choose-mvp-harness-shape/artifacts/harness-shape-evaluation-matrix.md`
- `Plans/InProgress/thin-clients-first-solution-structure/decisions/0008-mvp-harness-shape-selection-v0.md`
- `Plans/InProgress/thin-clients-first-solution-structure/steps/index.md`
- `Plans/InProgress/thin-clients-first-solution-structure/plan.md`

## Tests / Results
- `not-run` -> docs-only draft step

## Issues
- none

## Decision
- Select a dedicated test-host contract-proof project (`Tests/Zelanthus.WorkflowContractProofs.Tests`) for Plan 1.
- Defer API-host harness execution unless test-host criteria fail.

## Completion
- `pending`

## Next Actions
- Draft Plan 1 implementation details using the selected test-host harness baseline.


