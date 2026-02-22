# Decision 0001: Solution Project Boundaries v0

## Status
- accepted

## Decision Summary
- Establish explicit package boundaries between prompt tooling, provider clients, story engine layers, and API composition.
- Treat `Zelanthus.Prompting` as required shared prompt contract/governance runtime.
- Keep provider-specific implementations outside story engine domain/application layers.

## Initial Boundary Set
- `Zelanthus.Prompting`
- `Zelanthus.Llm.Clients.Abstractions`
- `Zelanthus.Llm.Clients.Gemini`
- `Zelanthus.StoryEngine.Domain`
- `Zelanthus.StoryEngine.Application`
- `Zelanthus.StoryEngine.Infrastructure`
- `Zelanthus.API`

## Package Responsibilities
- `Zelanthus.Prompting`:
  - prompt identity/versioning contracts,
  - composition/rendering contracts,
  - prompt provenance contract shapes.
- `Zelanthus.Llm.Clients.Abstractions`:
  - provider-agnostic request/response contracts,
  - capability profile contracts,
  - normalized error and token accounting contracts.
- `Zelanthus.Llm.Clients.Gemini`:
  - Gemini protocol implementation against abstractions only.
- `Zelanthus.StoryEngine.Domain`:
  - core domain entities, value objects, invariants.
- `Zelanthus.StoryEngine.Application`:
  - workflow orchestration/use-cases, semantic decision logic, chain-mode selection.
- `Zelanthus.StoryEngine.Infrastructure`:
  - persistence, artifact ledger, checkpoint stores, adapter wiring for runtime contracts.
- `Zelanthus.API`:
  - composition root and external entry points.

## Boundary Invariants
- Story engine domain/application must not reference provider-specific implementation assemblies.
- Prompting runtime contracts are consumed by application/infrastructure, but prompting package does not depend on story engine or API.
- Provider adapters do not depend on story engine domain/application.
- Runtime namespaces/packages use `Zelanthus.*`; `BookForge` naming remains documentation/reference context only.

## Rationale
- Preserves provider portability and prompt-governance discipline.
- Prevents monolithic coupling and supports focused testing.
- Enables future tool/MCP/worker entry points to reuse the same contracts.

## Consequences
- More projects than current baseline, with clearer ownership and test boundaries.
- Architecture tests and review gates are needed to enforce dependency direction.
- Implementing applications integrate prompt registration/versioning/provenance through `Zelanthus.Prompting`.
