# Step: 0070-apply-external-review-refinements

## Goal
- Apply external-review refinements to strengthen decision clarity before Draft promotion.

## Context
- External review requested concrete guidance on:
  - call envelope ownership between Prompting and LLM abstractions,
  - early architecture test enforcement,
  - naming alignment policy,
  - thin MVP proving slice.

## Commands Executed
- `Get-Content -Raw "Plans/Brainstorms/thin-clients-first-solution-structure/decisions/0001-solution-project-boundaries-v0.md"`
- `Get-Content -Raw "Plans/Brainstorms/thin-clients-first-solution-structure/decisions/0002-thin-clients-first-sequencing-v0.md"`
- `Get-Content -Raw "Plans/Brainstorms/thin-clients-first-solution-structure/decisions/0003-dependency-direction-rules-v0.md"`
- `Get-Content -Raw "Plans/Brainstorms/thin-clients-first-solution-structure/decisions/0004-prompting-ownership-and-versioning-policy-v0.md"`
- `Get-Content -Raw "Plans/Brainstorms/thin-clients-first-solution-structure/decisions/0005-infrastructure-prompt-shape-mapping-policy-v0.md"`
 - `Get-Content -Raw "Plans/Brainstorms/backbone-doctrine-prompt-first-multi-provider/decisions/0004-mvp-artifact-and-failure-contract-v0.md"`

## Files Changed
- `Plans/README.md`
- `Plans/Templates/README.md`
- `Plans/Templates/plan-folder/plan.md`
- `Plans/Brainstorms/backbone-doctrine-prompt-first-multi-provider/plan.md`
- `Plans/Brainstorms/backbone-doctrine-prompt-first-multi-provider/steps/index.md`
- `Plans/Brainstorms/backbone-doctrine-prompt-first-multi-provider/decisions/0003-prompt-governance-and-provenance-v0.md`
- `Plans/Brainstorms/backbone-doctrine-prompt-first-multi-provider/decisions/0004-mvp-artifact-and-failure-contract-v0.md`
- `Plans/Brainstorms/backbone-doctrine-prompt-first-multi-provider/steps/0050-define-mvp-artifact-and-failure-contract/step.md`
- `Plans/Brainstorms/thin-clients-first-solution-structure/plan.md`
- `Plans/Brainstorms/thin-clients-first-solution-structure/steps/index.md`
- `Plans/Brainstorms/thin-clients-first-solution-structure/steps/0070-apply-external-review-refinements/step.md`
- `Plans/Brainstorms/thin-clients-first-solution-structure/decisions/0001-solution-project-boundaries-v0.md`
- `Plans/Brainstorms/thin-clients-first-solution-structure/decisions/0002-thin-clients-first-sequencing-v0.md`
- `Plans/Brainstorms/thin-clients-first-solution-structure/decisions/0003-dependency-direction-rules-v0.md`
- `Plans/Brainstorms/thin-clients-first-solution-structure/decisions/0004-prompting-ownership-and-versioning-policy-v0.md`
- `Plans/Brainstorms/thin-clients-first-solution-structure/decisions/0005-infrastructure-prompt-shape-mapping-policy-v0.md`
- `Plans/Brainstorms/thin-clients-first-solution-structure/decisions/0006-call-envelope-and-enforcement-policy-v0.md`
- `Plans/Brainstorms/thin-clients-first-solution-structure/artifacts/initial-solution-shape.md`

## Tests / Results
- `not-run` -> docs-only brainstorm step

## Issues
- none

## Decision
- Lock a concrete application call envelope and enforce boundary integrity via early architecture tests and naming policy.

## Completion
- `completed`

## Next Actions
- Use this refined decision set to create two Draft plans:
  - thin contracts/adapter implementation plan,
  - cognitive chain runner/artifact persistence plan.
