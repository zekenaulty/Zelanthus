# Step: 0105-clarify-contract-proof-test-naming

## Goal
- Remove ambiguous harness naming and lock a semantic naming policy for long-lived contract-proof tests.

## Context
- `MvpHarness` is unclear over time and creates drift risk for future one-off proof tests.

## Git Branch
- `main`

## Commits
- `none`

## Commands Executed
- `Move-Item "Plans/Drafts/mvp-prompting-gemini-contract-baseline/steps/0060-implement-mvp-harness-tests-and-local-artifact-persistence" "Plans/Drafts/mvp-prompting-gemini-contract-baseline/steps/0060-implement-contract-proof-tests-and-local-artifact-persistence"`
- `Get-Content "Plans/Drafts/thin-clients-first-solution-structure/decisions/0008-mvp-harness-shape-selection-v0.md"`
- `Get-Content "Plans/Drafts/mvp-prompting-gemini-contract-baseline/plan.md"`

## Files Changed
- `Plans/Drafts/thin-clients-first-solution-structure/steps/0105-clarify-contract-proof-test-naming/step.md`
- `Plans/Drafts/thin-clients-first-solution-structure/steps/index.md`
- `Plans/Drafts/thin-clients-first-solution-structure/plan.md`
- `Plans/Drafts/thin-clients-first-solution-structure/decisions/0008-mvp-harness-shape-selection-v0.md`
- `Plans/Drafts/thin-clients-first-solution-structure/decisions/0009-contract-proof-test-naming-policy-v0.md`
- `Plans/Drafts/thin-clients-first-solution-structure/steps/0090-choose-mvp-harness-shape/artifacts/harness-shape-evaluation-matrix.md`
- `Plans/Drafts/mvp-prompting-gemini-contract-baseline/plan.md`
- `Plans/Drafts/mvp-prompting-gemini-contract-baseline/steps/index.md`
- `Plans/Drafts/mvp-prompting-gemini-contract-baseline/steps/0060-implement-contract-proof-tests-and-local-artifact-persistence/step.md`
- `AGENTS.md`
- `Plans/README.md`

## Tests / Results
- `not-run` -> docs-only draft refinement step

## Issues
- none

## Decision
- Standardize on long-lived semantic project naming:
  - `Tests/Zelanthus.WorkflowContractProofs.Tests`
- Seed test names must encode contract surface, scenario, and expected outcome.

## Completion
- `completed`

## Next Actions
- Continue `0110` draft with naming policy treated as a dependency.
