# Decision 0003: Project Reference Graph and Boundary Enforcement v0

## Status
- accepted

## Decision Summary
- Plan 2 project reference updates must preserve one-way dependency direction and transformation ownership.
- Architecture tests are required as an early implementation gate.
- Workflow abstraction and route-hook logic remain owned by `StoryEngine.Application`/`StoryEngine.Domain`, not infrastructure or API.

## Allowed Reference Direction (Plan 2 target)
- `Zelanthus.StoryEngine.Domain` -> no runtime project dependencies
- `Zelanthus.StoryEngine.Application` -> `Zelanthus.StoryEngine.Domain`, `Zelanthus.Prompting`, `Zelanthus.Llm.Clients.Abstractions`
- `Zelanthus.StoryEngine.Infrastructure` -> `Zelanthus.StoryEngine.Application`, `Zelanthus.StoryEngine.Domain`, `Zelanthus.Prompting`, `Zelanthus.Llm.Clients.Abstractions`
- `Zelanthus.API` -> composition root references only (`StoryEngine.Application`, `StoryEngine.Infrastructure`, provider adapter)
- `Zelanthus.Llm.Clients.Gemini` -> `Zelanthus.Llm.Clients.Abstractions`

## Forbidden References
- `StoryEngine.Domain/Application` -> provider implementation projects (for example `Gemini`)
- `StoryEngine.Domain` -> `StoryEngine.Infrastructure` or `API`
- `Prompting` -> `StoryEngine.*` or `API`
- Provider implementations -> `StoryEngine.Domain/Application`

## Consequences
- Plan 2 must include architecture test assertions before broad implementation spread.
- Any needed boundary exception requires explicit decision update, not ad-hoc reference changes.
