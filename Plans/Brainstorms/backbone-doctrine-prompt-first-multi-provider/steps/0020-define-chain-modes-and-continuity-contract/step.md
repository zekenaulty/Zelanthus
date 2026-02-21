# Step: 0020-define-chain-modes-and-continuity-contract

## Goal
- Define planner-to-execution chain modes that support thought-heavy workloads without making correctness depend on hidden provider state.

## Context
- BookForge findings showed high thought-token usage can starve output in single-turn calls, requiring explicit multi-turn planning/execution abstractions.

## Commands Executed
- `Get-Content -Raw "References/bookforge/resources/plans/proposed/chapter_scoped_two_turn_phase_execution_plan_20260220_0927.md"`
- `Get-Content -Raw "References/cognition/src/Cognition.Clients/Tools/Planning/PlannerBase.cs"`

## Files Changed
- `Plans/Brainstorms/backbone-doctrine-prompt-first-multi-provider/steps/0020-define-chain-modes-and-continuity-contract/step.md`
- `Plans/Brainstorms/backbone-doctrine-prompt-first-multi-provider/decisions/0002-chain-continuity-strategy-v0.md`

## Tests / Results
- `not-run` -> docs-only brainstorm step

## Issues
- none

## Decision
- Support two chain modes (`CognitiveChain`, `ConversationalChain`) with explicit turn artifacts and optional continuity handles.

## Completion
- `completed`

## Next Actions
- Define provider capability profile and prompt governance requirements.
