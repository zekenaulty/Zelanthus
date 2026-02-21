# Decision 0001: Backbone Doctrine v0

## Status
- accepted

## Decision Summary
- Zelanthus is prompt-first and workflow-first.
- Chat is an optional interaction surface, not a core runtime dependency.
- Model execution owns semantic generation.
- Deterministic orchestration owns validation, routing, retries, checkpointing, and reporting.

## Context
- Story workflow quality is dominated by prompt contracts and phase execution discipline.
- Prior systems drifted when conversation management and platform concerns grew faster than prompt/runtime contract maturity.
- We need a backbone that stays stable when models/providers change.

## Core Doctrine Rules
- Prompt-first runtime:
  - Core unit of work is `prompt contract -> model execution -> validated artifact`.
  - Prompt/template governance is a required runtime concern, not a documentation afterthought.
- Semantic versus deterministic split:
  - Semantic authoring and semantic correction are model responsibilities.
  - Deterministic runtime enforces schema/invariants, retry policy, decision routing, and resume safety.
- Artifact-first correctness:
  - Durable artifacts are authoritative for resume and replay.
  - Hidden provider state can improve performance, but cannot be the correctness foundation.
- Explicit failures:
  - No silent semantic fallback by orchestrator code.
  - Terminal failures must be reason-coded and persisted with enough context for deterministic replay.

## Scope Boundaries
- In scope:
  - Prompt lifecycle, execution contracts, validation contracts, checkpointing, provenance.
- Out of scope:
  - Chat-first product surfaces, conversation timelines, and user-facing messaging choreography.

## Non-Negotiables
- No deterministic code path may silently generate replacement semantic prose.
- Every accepted execution artifact must trace to prompt ID/version/hash and model/provider metadata.
- Resume behavior must be deterministic and explainable from persisted artifacts.

## Consequences
- Initial implementation planning prioritizes:
  - prompt tooling,
  - provider abstraction contracts,
  - execution chain contracts,
  - artifact/provenance storage.
- Conversation infrastructure can be added later without re-defining core workflow contracts.
