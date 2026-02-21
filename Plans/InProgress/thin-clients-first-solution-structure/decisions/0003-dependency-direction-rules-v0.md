# Decision 0003: Dependency Direction Rules v0

## Status
- accepted

## Decision Summary
- Enforce one-way dependency direction.
- Separate transformation concerns by layer to avoid orchestration logic leaking into infrastructure or adapters.

## Transformation Responsibility Split
- Semantic transformation (application concern):
  - selecting prompt IDs/versions,
  - building workflow context for phase execution,
  - selecting chain mode and runtime policy.
- Protocol/provider transformation (adapter concern):
  - mapping normalized contracts to provider request shape,
  - mapping provider response/protocol errors into normalized contracts.
- Persistence transformation (infrastructure concern):
  - mapping persisted storage schema to/from canonical runtime prompt/provenance contracts.

## Allowed Dependencies (high level)
- `Zelanthus.API` -> `Zelanthus.StoryEngine.Application`, `Zelanthus.StoryEngine.Infrastructure`, `Zelanthus.Llm.Clients.Gemini`
- `Zelanthus.StoryEngine.Infrastructure` -> `Zelanthus.StoryEngine.Application`, `Zelanthus.StoryEngine.Domain`, `Zelanthus.Llm.Clients.Abstractions`, `Zelanthus.Prompting`
- `Zelanthus.StoryEngine.Application` -> `Zelanthus.StoryEngine.Domain`, `Zelanthus.Llm.Clients.Abstractions`, `Zelanthus.Prompting`
- `Zelanthus.Llm.Clients.Gemini` -> `Zelanthus.Llm.Clients.Abstractions`
- `Zelanthus.Prompting` -> no dependencies on story engine/API/provider implementation projects
- `Zelanthus.StoryEngine.Domain` -> no infrastructure/provider dependencies

## Forbidden Dependencies and Anti-Patterns
- Story engine domain/application referencing provider-specific adapters.
- Prompting package referencing story engine or API projects.
- Provider adapters referencing story engine domain/application.
- Infrastructure persistence shapes used directly as runtime prompt contracts without explicit mapping.
- Infrastructure owning semantic prompt assembly decisions that belong to application workflows.

## Enforcement Guidance
- Add architecture tests for dependency direction in the thin MVP slice (not deferred).
- Add review checklist items for transformation ownership:
  - "Is semantic transform in application?"
  - "Is provider protocol transform in adapter?"
  - "Is storage transform in infrastructure?"
- Minimum architecture assertions:
  - domain has no infrastructure/provider adapter dependencies,
  - prompting has no story engine/API dependencies,
  - provider implementation projects have no story engine domain/application dependencies.

## Rationale
- Keeps abstractions stable and reusable.
- Prevents boundary drift and accidental coupling.
- Reduces refactor risk when adding providers or changing storage schemas.
