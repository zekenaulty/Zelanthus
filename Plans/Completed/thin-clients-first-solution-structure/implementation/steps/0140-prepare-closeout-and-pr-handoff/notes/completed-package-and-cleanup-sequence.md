# Completed Package and Cleanup Sequence

## Purpose
- Document exact closure sequencing for this execution branch so completion is deterministic and auditable.

## Sequence
1. Create completed packages on the feature branch:
   - `Plans/Completed/thin-clients-first-solution-structure/`
   - `Plans/Completed/mvp-prompting-gemini-contract-baseline/`
   - `Plans/Completed/mvp-chain-runner-local-persistence-baseline/`
2. In each completed package, include:
   - `draft-baseline/<plan-slug>-draft.md`
   - `draft-baseline/source-ref.md`
   - `implementation/` (verbatim snapshot of corresponding `Plans/InProgress/<plan-slug>/`)
   - `closeout/closeout.md`
   - `closeout/outcomes.md`
   - `closeout/traceability.md`
   - `closeout/archive-note.md`
3. Commit completed package creation as a standalone commit.
4. Remove active stage folders in a separate cleanup commit:
   - `Plans/Drafts/<plan-slug>/`
   - `Plans/InProgress/<plan-slug>/`
5. Update closure SHA fields (merge, completed-package, cleanup) in completed-package traceability files.
6. Signal PR-ready handoff to the user, then wait for user-owned PR creation/merge.

## Suggested Commit Message Pattern
- Commit A (completed package):
  - `planning(<plan-slug>/closeout): create completed package for pr handoff`
- Commit B (cleanup):
  - `planning(<plan-slug>/cleanup): remove active draft and inprogress folders`

## Guardrails
- Do not combine completed-package creation and active-folder cleanup in one commit.
- Do not mutate draft baseline semantics during closure packaging.
- Keep all closeout evidence path-referenced and SHA-addressable.
