# 0004-carry-forward-backlog-contract-v0

## Status
- `proposed`

## Decision
- Keep carry-forward backlog plan-scoped for MVP planning cycles, with explicit gate checks in each plan package.
- Enforce `WF-BL-001` as a mandatory continuity item for runner/orchestration evolution.
- Backlog mapping is required before Draft promotion for any plan that:
  - introduces a new execution kind or `LlmTaskMode`,
  - changes transition behavior/contracts,
  - changes runner state/resume/retry policy.

## Rationale
- Prevents loss of high-value hardening work while workspace-level backlog process is not yet formalized.
- Keeps backlog accountability close to planning scope and decision traceability.

## Invariants
- Every active backlog item must include:
  - owner scope,
  - trigger for promotion mapping,
  - explicit exit criteria.
- Draft promotion checklist must include backlog mapping validation.
- Completed package closeout must include backlog status update for affected items.
- One unresolved backlog item cannot be silently dropped by introducing new plan names/terms.

## Non-Goals
- No global cross-repo backlog system in this slice.
- No automated issue tracker integration in this slice.

## Consequences
- Planning remains lightweight while preserving continuity obligations.
- Critical hardening items stay visible across brainstorm -> draft -> in-progress transitions.
- Future workspace-wide backlog model can supersede this with explicit migration notes.
