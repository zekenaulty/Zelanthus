# Decision 0002: Chain and Continuity Strategy v0

## Status
- accepted

## Decision Summary
- Zelanthus supports two chain modes:
  - `CognitiveChain`: plan/think turn followed by execute/output turn.
  - `ConversationalChain`: interaction-driven multi-turn flow with explicit turn artifacts.
- Provider continuity and thought handles are first-class capabilities.
- Hidden continuity state is never authoritative resume state.

## Context
- Thought-heavy models can consume large token budgets before producing required output artifacts.
- Single-turn execution is insufficient for high-complexity structured phases.
- Continuity support differs by provider/model and can fail across pauses, retries, or model changes.

## Chain Mode Contracts
- `CognitiveChain`:
  - `T1` objective: produce explicit compact plan artifact and optional continuity handle.
  - `T2` objective: produce contract-valid output from plan artifact.
  - `T2` must not run without a valid plan artifact.
- `ConversationalChain`:
  - Every turn is persisted with explicit turn metadata and output artifacts.
  - Conversation context can inform behavior, but contract validity is still checked per turn.

## Continuity Policy
- Continuity handle usage:
  - Allowed as optimization for immediate follow-up turns.
  - Must be treated as opaque provider state.
- Resume rules:
  - Same run window with valid handle: use handle plus persisted turn artifacts.
  - Delayed resume or handle failure: regenerate plan turn or continue from explicit valid plan artifact.
  - Provider/model swap: continuity handle is treated as invalid and ignored.

## Budget and Routing Rules
- Runtime tracks thought token usage and output token usage separately.
- Chain mode router may promote a phase from single-turn to `CognitiveChain` when output starvation risk is detected.
- Retry policy must preserve chain semantics:
  - Prefer `T2` retry only when `T1` artifact is still valid.
  - Restart at `T1` when `T2` failures indicate stale or insufficient planning state.

## Layer Ownership
- Application layer:
  - selects chain mode,
  - defines turn objectives and acceptance criteria.
- Provider adapter layer:
  - translates continuity/thought features to provider protocol.
- Infrastructure layer:
  - persists turn artifacts/checkpoints/provenance and supports deterministic resume.

## Non-Negotiables
- No blind `T2` execution without explicit valid plan state.
- No correctness dependency on hidden provider state.
- All chain failures and retries must be reason-coded and artifacted.
