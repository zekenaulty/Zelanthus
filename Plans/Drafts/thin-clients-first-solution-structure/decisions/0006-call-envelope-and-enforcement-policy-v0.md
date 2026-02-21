# Decision 0006: Call Envelope and Enforcement Policy v0

## Status
- accepted

## Decision Summary
- Define one canonical application-to-client call envelope.
- Define explicit provenance ownership split between application and provider adapters.
- Require architecture boundary tests in the thin MVP slice.
- Lock naming alignment policy for implementation work.

## Canonical Call Envelope (MVP)
- Application constructs `RenderedPrompt` through `Zelanthus.Prompting`.
- Application constructs `ExecutionContext` (chain mode, turn metadata, policy flags, correlation IDs).
- Application invokes `ILlmClient` with `ExecutionEnvelope = { RenderedPrompt, ExecutionContext }`.
- Adapter returns `NormalizedResponseEnvelope` with:
  - `normalized_response`
  - `token_accounting` (including thought tokens when available)
  - `provider_metadata`
  - `raw_snapshot_ref` (or raw snapshot payload reference)
  - `continuity_handle` (optional capability-driven field)

## Provenance Ownership Split
- Adapter owns protocol normalization:
  - provider protocol -> normalized response and metadata.
- Application owns final provenance composition:
  - merges prompt metadata and adapter metadata into `ProvenanceRecord`.
- Infrastructure owns provenance persistence:
  - stores composed record and enforces mapping constraints from decision `0005`.

## Thin MVP Enforcement Gate
- Architecture tests are required in MVP, not deferred:
  - domain does not depend on infrastructure/provider projects,
  - prompting does not depend on story engine/API,
  - provider implementations do not depend on story engine domain/application.
- Build/test gate fails on dependency violations.

## Naming Alignment Policy
- Code/package names in this solution use `Zelanthus.*` as canonical runtime namespace.
- `BookForge` naming is reference-only context in docs and historical artifacts.
- New implementation artifacts must not mix runtime namespaces (`Zelanthus` vs `BookForge`) in project/package naming.

## Rationale
- Prevents duplicate provenance pipelines and ambiguous ownership.
- Forces boundary correctness before code volume increases.
- Avoids package drift caused by naming drift.

## Consequences
- Draft plans must specify call envelope contracts and provenance assembly behavior.
- Draft plans must include architecture test tasks in early steps.
- Naming violations are treated as boundary failures and corrected before InProgress promotion.
