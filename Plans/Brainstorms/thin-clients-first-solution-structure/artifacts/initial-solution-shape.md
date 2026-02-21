# Initial Solution Shape (Brainstorm v0)

## Current Baseline
- Existing solution entry: `Zelanthus.API`

## Target Logical Packages
- `Zelanthus.Prompting`
  - Template registry/composition/rendering/fingerprinting/provenance helpers.
  - Required integration boundary for implementing applications to manage and version prompts.
- `Zelanthus.Llm.Clients.Abstractions`
  - Provider-agnostic request/response contracts, capability profile, normalized errors, token usage contracts.
- `Zelanthus.Llm.Clients.Gemini`
  - Gemini implementation of abstractions (first provider target).
- `Zelanthus.StoryEngine.Domain`
  - Core story domain entities, value objects, invariants.
- `Zelanthus.StoryEngine.Application`
  - Workflow/phase orchestration contracts and use-cases.
- `Zelanthus.StoryEngine.Infrastructure`
  - Persistence/artifact stores and runtime adapters that consume abstractions.
  - May use persistence schemas that differ from `Zelanthus.Prompting` contract shapes.
  - Must own deterministic mapping/transform to and from prompting/runtime contract shapes.
- `Zelanthus.API`
  - Composition root and external API surface.

## Why This Shape
- Keeps provider-specific logic outside story engine.
- Keeps prompt system reusable and testable as a first-class boundary.
- Supports prompt-first workflows without requiring chat subsystem coupling.

## Prompt Ownership Rule
- Implementing applications do not own ad-hoc prompt mechanics.
- Implementing applications own prompt content packs, but they must manage and execute those prompts through `Zelanthus.Prompting` contracts/tooling.

## Infrastructure Mapping Rule
- Storage representation and runtime prompt representation are allowed to differ.
- Required prompt/provenance fields must be mappable without silent data loss.

## Canonical Call Envelope Rule
- Application builds `RenderedPrompt` + `ExecutionContext`.
- Application invokes `ILlmClient` with a single canonical `ExecutionEnvelope`.
- Adapter returns normalized response metadata and raw snapshot references.
- Application composes final `ProvenanceRecord`; Infrastructure persists it.

## Naming Alignment Rule
- Runtime projects and namespaces use `Zelanthus.*`.
- `BookForge` is preserved as reference context in documentation only.
