# Decision 0002: Thin Clients First Sequencing v0

## Status
- accepted

## Decision Summary
- Sequence implementation planning and delivery with thin client and prompt contracts first, then story engine orchestration and persistence.

## Why This Sequence
- Story engine flows depend on stable contracts for:
  - chain mode behavior,
  - provider capability handling,
  - token accounting and error normalization,
  - prompt provenance and execution metadata.
- Defining orchestration before those contracts stabilizes creates rework and ambiguous boundaries.

## Ordered Delivery Phases
1. Contract phase:
  - define `Zelanthus.Llm.Clients.Abstractions` contracts,
  - define `Zelanthus.Prompting` contracts and provenance requirements.
2. First provider phase:
  - implement `Zelanthus.Llm.Clients.Gemini` against abstractions,
  - validate capability profile wiring and normalized response behavior.
3. Story engine phase:
  - design and implement `Zelanthus.StoryEngine.Domain/Application/Infrastructure` against stabilized contracts.
4. Composition phase:
  - wire runtime composition and entry points in `Zelanthus.API`.

## Thin MVP Proving Slice
- Minimum slice to validate backbone doctrine before broad StoryEngine expansion:
  - `Zelanthus.Prompting` contract implementation (identity/version/render/checksum/provenance hooks),
  - `Zelanthus.Llm.Clients.Abstractions` + `Zelanthus.Llm.Clients.Gemini` with capability profile and normalized metadata,
  - one minimal runner proving at least one `CognitiveChain` step pair (`PLAN_STEP` + `EXECUTE`) with persisted artifacts/provenance.
- Clarification:
  - `PLAN_STEP` + `EXECUTE` is the minimum acceptance proof path, not a runner step-limit.
  - `CognitiveChain` may stage multiple `PLAN_STEP` units before one or more `EXECUTE` units and may repeat `PLAN_STEP` -> `EXECUTE` cycles.
  - `CognitiveChain` resume restarts from chain start to re-establish provider thinking continuity safely.
  - `ConversationalChain` resumes from last successful persisted step.
  - Runner design must allow multi-turn/multi-step execution for both `CognitiveChain` and `ConversationalChain`.
- This slice is the acceptance gate for promoting broad StoryEngine implementation scope.

## Entry and Exit Criteria
- Exit criteria for contract phase:
  - capability profile contract finalized,
  - prompt identity/version/provenance contract finalized.
  - canonical call envelope defined (see `0006-call-envelope-and-enforcement-policy-v0.md`).
- Exit criteria for first provider phase:
  - Gemini adapter passes contract-level tests and emits normalized metadata.
  - architecture dependency tests pass for defined boundaries.
- Entry criteria for story engine phase:
  - contracts are versioned and consumed via abstractions only.

## Non-Goals
- Building full chat/session infrastructure before core prompt/workflow contracts.
- Building multiple provider adapters before Gemini baseline is contract-verified.

## Consequences
- Early effort is spent on contract quality rather than feature breadth.
- Story engine planning can use concrete contract inputs instead of assumptions.
- Draft promotion should remain narrow:
  - Draft Plan 1: Prompting + LLM abstractions + Gemini adapter.
  - Draft Plan 2: MVP `CognitiveChain` runner + artifact/provenance persistence.
