# General Intent Summary

## Why This Reset Exists
This repository was reset to remove semantic drift and implementation noise while keeping the planning process scaffolding.

## Product Intent
Build a story-engine orchestration core that is contract-first and deterministic.

Primary focus:
- Bind prompt templates to execution modes: `CognitiveChain`, `ConversationalChain`, and `SingleCall`.
- Produce a normalized response envelope per execution.
- Dispatch outcomes through data-defined transition rules instead of hard-coded flow logic.

Reference inspirations (non-authoritative):
- `References/cognition/src/Cognition.Clients/Tools/ToolRegistry.cs`
- `References/cognition/src/Cognition.Clients/Tools/ToolDispatcher.cs`
- `References/cognition/src/Cognition.Clients/Tools/Planning/PlannerBase.cs`

## Engineering Principles
- Keep correctness artifact-first and explicit.
- Keep provider-specific behavior behind abstractions.
- Prefer small slices with proof tests before breadth.
- Avoid over-engineering while preserving clean seams.

## Immediate Non-Goals
- No full chat system as the first milestone.
- No workflow mega-engine or graph DSL in the first slice.
- No Postgres or distributed persistence in the first slice.

## Source Of Planning Truth
- `Plans/README.md`
