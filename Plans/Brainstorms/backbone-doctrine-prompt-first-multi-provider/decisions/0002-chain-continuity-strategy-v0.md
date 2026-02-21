# Decision 0002: Chain and Continuity Strategy v0

## Status
- accepted

## Decision Summary
- Zelanthus supports two chain modes:
  - `CognitiveChain`: `PLAN_STEP` followed by `EXECUTE` as a base unit.
  - `ConversationalChain`: interaction-driven multi-turn flow with explicit turn artifacts.
- Provider continuity and thought handles are first-class capabilities.
- Hidden continuity state is never authoritative resume state.
- `PLAN_STEP` -> `EXECUTE` is minimum chain unit semantics, not a hard cap on chain length.

## Context
- Thought-heavy models can consume large token budgets before producing required output artifacts.
- Single-turn execution is insufficient for high-complexity structured phases.
- Continuity support differs by provider/model and can fail across pauses, retries, or model changes.

## Chain Mode Contracts
- `CognitiveChain`:
  - `PLAN_STEP` objective: produce explicit compact plan artifact and provider continuity handle (for example provider `thoughtSignature`) when available.
  - `EXECUTE` objective: produce contract-valid output from plan artifact.
  - `EXECUTE` must not run without a valid plan artifact.
  - Additional `PLAN_STEP`/`EXECUTE` units may be executed when workflow scope requires multi-step planning/execution.
  - Cognitive flows may stage multiple `PLAN_STEP` units before one or more `EXECUTE` units.
  - Provider may return a new `thoughtSignature` on each response; Zelanthus tracks the latest value as `thinking_persistence_key`.
  - Cognitive requests should pass forward the latest persisted `thinking_persistence_key` to the next provider call when capability supports it.
  - `thinking_persistence_key` can rotate turn-to-turn and is treated as rolling continuity state, not a fixed per-run key.
- `ConversationalChain`:
  - Every turn is persisted with explicit turn metadata and output artifacts.
  - Conversation context can inform behavior, but contract validity is still checked per turn.

## Continuity Policy
- Continuity handle usage:
  - Allowed as optimization for immediate follow-up turns.
  - Must be treated as opaque provider state.
- Resume rules:
  - Same run window with valid handle: use handle plus persisted turn artifacts.
  - `CognitiveChain` delayed resume or handle failure: restart from first `PLAN_STEP` because provider-side thinking cache durability is not guaranteed.
  - `ConversationalChain` delayed resume: continue from last successful persisted step.
  - Provider/model swap: continuity handle is treated as invalid and ignored.

## Budget and Routing Rules
- Runtime tracks thought token usage and output token usage separately.
- Chain mode router may promote a phase from single-turn to `CognitiveChain` when output starvation risk is detected.
- Retry policy must preserve chain semantics:
  - Prefer `EXECUTE` retry only when upstream `PLAN_STEP` artifact is still valid.
  - Restart at first `PLAN_STEP` when `EXECUTE` failures indicate stale or insufficient planning state.

## Layer Ownership
- Application layer:
  - selects chain mode,
  - defines turn objectives and acceptance criteria.
- Provider adapter layer:
  - translates continuity/thought features to provider protocol.
- Infrastructure layer:
  - persists turn artifacts/checkpoints/provenance and supports deterministic resume.

## Non-Negotiables
- No blind `EXECUTE` execution without explicit valid plan state.
- No correctness dependency on hidden provider state.
- All chain failures and retries must be reason-coded and artifacted.
