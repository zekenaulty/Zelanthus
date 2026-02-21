# Decision 0007: MVP Goals and First Plan Boundary v0

## Status
- accepted

## Decision Summary
- Lock MVP goals before selecting harness host/transport details.
- Keep first implementation plan narrow: contracts and Gemini baseline first.
- Defer Postgres and broader story engine expansion until MVP proof goals are met.

## MVP Goals
1. Prompt contract correctness:
  - `Zelanthus.Prompting` provides deterministic prompt identity/version/render/checksum behavior.
2. Client contract correctness:
  - `Zelanthus.Llm.Clients.Abstractions` defines normalized request/response/capability/error and `TokenAccounting` contracts.
3. Gemini baseline correctness:
  - `Zelanthus.Llm.Clients.Gemini` satisfies abstraction contracts and emits required normalized metadata.
4. Provenance and failure correctness:
  - workflow-relevant calls emit required provenance fields and reason-coded failures.
5. Boundary correctness:
  - architecture tests enforce dependency-direction rules during MVP, not later.

## First Plan Boundary (Draft Plan 1)
- In scope:
  - `Zelanthus.Prompting` baseline implementation.
  - `Zelanthus.Llm.Clients.Abstractions` baseline contracts.
  - `Zelanthus.Llm.Clients.Gemini` baseline adapter.
  - Architecture test gate project for dependency boundaries.
  - Minimal execution harness path capable of proving one golden-path call.
- Out of scope:
  - broad story engine domain modeling,
  - multi-provider implementations,
  - production persistence and Postgres setup,
  - frontend/chat feature development.

## Harness Requirement Contract (host-shape neutral)
- Must execute a deterministic `CognitiveChain` style proof path (`T1` then `T2`) for at least one representative step.
- `T1` -> `T2` is a minimum proof path, not a hard cap on total turns.
- Plan 2 runner design must support variable-length chains for:
  - `CognitiveChain` (multi-step planning/execution with thought-aware checkpoints),
  - `ConversationalChain` (multi-turn chat-style flow with explicit turn artifacts).
- Must run from one repeatable command path (for example `dotnet test` or one explicit host command).
- Must capture raw response snapshot reference plus normalized metadata.
- Must persist provenance and validation/failure outcome artifacts locally.
- Local persistence in MVP means workspace-local file/path-backed storage for artifacts/checkpoints/provenance/failures only.
- Local persistence in MVP excludes external database/cache/queue/service-hosted storage.
- Must support clear failure reason code reporting for MVP baseline codes.

## Candidate Harness Shapes (decision deferred to step `0090`)
- Option A: dedicated MVP integration test project under `Tests/` (recommended starting point).
- Option B: minimal API-hosted endpoint plus integration tests (future-only option, not authorized for Round-1 Plan 1 scope).

## Rationale
- Goal-first planning prevents host-shape discussions from redefining MVP success criteria mid-draft.
- Narrow Plan 1 scope reduces rework and keeps doctrine validation fast.
- Deferring Postgres aligns with current priority: prove runtime contracts first.

## Consequences
- Draft steps must choose harness shape against these requirements, not by preference alone.
- Plan 2 draft depends on Plan 1 contract outputs and acceptance evidence.
