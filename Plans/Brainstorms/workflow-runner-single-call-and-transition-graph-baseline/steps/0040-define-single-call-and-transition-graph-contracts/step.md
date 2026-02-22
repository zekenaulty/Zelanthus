# Step: 0040-define-single-call-and-transition-graph-contracts

## Goal
- Define deterministic contract shapes for single-call execution and transition-graph flow control without broad orchestration-engine expansion.

## Context
- We need explicit contract boundaries before drafting implementation plans.

## Outputs
- `Plans/Brainstorms/workflow-runner-single-call-and-transition-graph-baseline/decisions/0001-single-call-node-execution-contract-v0.md`
- `Plans/Brainstorms/workflow-runner-single-call-and-transition-graph-baseline/decisions/0002-transition-decision-contract-v0.md`
- `Plans/Brainstorms/workflow-runner-single-call-and-transition-graph-baseline/decisions/0003-runner-policy-and-state-deltas-v0.md`
- `Plans/Brainstorms/workflow-runner-single-call-and-transition-graph-baseline/notes/terminology-supersession-map.md`

## Acceptance Evidence
- Contracts include invariants, reason-code expectations, and explicit non-goals.
- Transition contract pins deterministic transition failure codes:
  - `transition_no_match`
  - `transition_invalid_action`
  - `max_iterations_exceeded`
- Terminology supersession mapping is explicit and links legacy terms to current authoritative terms.

## Git Branch
- `main`

## Commits
- `none`

## Commands Executed
- `pending`

## Files Changed
- `pending`

## Tests / Results
- `not-run` -> `docs-only step`

## Issues
- `none`

## Decision
- pending

## Completion
- `pending`

## Next Actions
- Capture carry-forward backlog and draft promotion gates.
