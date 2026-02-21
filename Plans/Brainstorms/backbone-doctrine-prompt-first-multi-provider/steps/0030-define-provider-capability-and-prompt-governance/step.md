# Step: 0030-define-provider-capability-and-prompt-governance

## Goal
- Define minimal provider abstraction and prompt governance/provenance requirements for a Gemini-first but multi-provider-ready backbone.

## Context
- We need a stable way to compare provider capabilities and preserve prompt-response provenance without building full conversation infrastructure.

## Commands Executed
- `Get-Content -Raw "References/bookforge/src/bookforge/prompt/composition.py"`
- `Get-Content -Raw "References/bookforge/src/bookforge/llm/factory.py"`

## Files Changed
- `Plans/Brainstorms/backbone-doctrine-prompt-first-multi-provider/steps/0030-define-provider-capability-and-prompt-governance/step.md`
- `Plans/Brainstorms/backbone-doctrine-prompt-first-multi-provider/decisions/0003-prompt-governance-and-provenance-v0.md`
- `Plans/Brainstorms/backbone-doctrine-prompt-first-multi-provider/risks/risk-log.md`

## Tests / Results
- `not-run` -> docs-only brainstorm step

## Issues
- none

## Decision
- Adopt capability-profile provider adapters and make prompt hash/version/provenance mandatory for all workflow artifacts.

## Completion
- `completed`

## Next Actions
- Use this doctrine as input to the thin-clients-first solution structure brainstorm and draft plans.
