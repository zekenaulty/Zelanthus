# Step: 0040-lock-prompting-ownership-policy

## Goal
- Explicitly lock whether implementing applications must manage and version prompts through `Zelanthus.Prompting`.

## Context
- This policy is critical to avoid prompt/version drift and provenance inconsistency across implementations.

## Commands Executed
- `Get-Content -Raw "Plans/Brainstorms/thin-clients-first-solution-structure/plan.md"`
- `Get-Content -Raw "Plans/Brainstorms/thin-clients-first-solution-structure/decisions/0001-solution-project-boundaries-v0.md"`
- `Get-Content -Raw "Plans/Brainstorms/thin-clients-first-solution-structure/decisions/0002-thin-clients-first-sequencing-v0.md"`

## Files Changed
- `Plans/Brainstorms/thin-clients-first-solution-structure/plan.md`
- `Plans/Brainstorms/thin-clients-first-solution-structure/steps/index.md`
- `Plans/Brainstorms/thin-clients-first-solution-structure/steps/0040-lock-prompting-ownership-policy/step.md`
- `Plans/Brainstorms/thin-clients-first-solution-structure/decisions/0001-solution-project-boundaries-v0.md`
- `Plans/Brainstorms/thin-clients-first-solution-structure/decisions/0004-prompting-ownership-and-versioning-policy-v0.md`
- `Plans/Brainstorms/thin-clients-first-solution-structure/artifacts/initial-solution-shape.md`

## Tests / Results
- `not-run` -> docs-only brainstorm step

## Issues
- none

## Decision
- Implementing applications are required to manage/version prompts and record prompt provenance through `Zelanthus.Prompting` shapes/tools.

## Completion
- `completed`

## Next Actions
- Carry this policy as a non-negotiable requirement into the Draft thin-clients implementation plan.
