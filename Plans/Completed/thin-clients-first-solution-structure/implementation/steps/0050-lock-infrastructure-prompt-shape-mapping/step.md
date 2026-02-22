# Step: 0050-lock-infrastructure-prompt-shape-mapping

## Goal
- Explicitly lock how `Zelanthus.StoryEngine.Infrastructure` storage relates to `Zelanthus.Prompting` contract shapes.

## Context
- Story engine storage models may need database-optimized structures that are not 1:1 with prompting contracts.
- We still need deterministic transform/mapping to valid prompting shapes for execution and provenance workflows.

## Commands Executed
- `Get-Content -Raw "Plans/InProgress/thin-clients-first-solution-structure/plan.md"`
- `Get-Content -Raw "Plans/InProgress/thin-clients-first-solution-structure/decisions/0003-dependency-direction-rules-v0.md"`
- `Get-Content -Raw "Plans/InProgress/thin-clients-first-solution-structure/decisions/0004-prompting-ownership-and-versioning-policy-v0.md"`

## Files Changed
- `Plans/InProgress/thin-clients-first-solution-structure/plan.md`
- `Plans/InProgress/thin-clients-first-solution-structure/steps/index.md`
- `Plans/InProgress/thin-clients-first-solution-structure/steps/0050-lock-infrastructure-prompt-shape-mapping/step.md`
- `Plans/InProgress/thin-clients-first-solution-structure/decisions/0003-dependency-direction-rules-v0.md`
- `Plans/InProgress/thin-clients-first-solution-structure/decisions/0004-prompting-ownership-and-versioning-policy-v0.md`
- `Plans/InProgress/thin-clients-first-solution-structure/decisions/0005-infrastructure-prompt-shape-mapping-policy-v0.md`
- `Plans/InProgress/thin-clients-first-solution-structure/artifacts/initial-solution-shape.md`

## Tests / Results
- `not-run` -> docs-only brainstorm step

## Issues
- none

## Decision
- `Zelanthus.StoryEngine.Infrastructure` may persist prompt-related data in non-1:1 storage models, but must provide deterministic mapping/transform to/from `Zelanthus.Prompting` contract shapes used by runtime workflows.

## Completion
- `pending`

## Next Actions
- Carry this policy as a non-negotiable acceptance criterion in Draft plans for Infrastructure persistence.


