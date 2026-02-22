# terminology-refactor-workflow-enums

## Compiled Plan Metadata

- Plan Scope: `Drafts/terminology-refactor-workflow-enums`
- Compiled At (UTC): `2026-02-22T07:55:20Z`
- Source Document Count: `7`
- Projection File: `terminology-refactor-workflow-enums.md`

## Contents

1. `plan.md`
2. `steps/index.md`
3. `steps/0010-inventory-legacy-usage-and-rename-impact/step.md`
4. `steps/0020-define-code-rename-and-adapter-strategy/step.md`
5. `steps/0030-define-persistence-compatibility-and-migration-path/step.md`
6. `steps/0040-define-proof-updates-and-audit-closeout/step.md`
7. `archive-note.md`

---

## Source 1: `plan.md`

﻿# terminology-refactor-workflow-enums

## Objective
- Execute a controlled terminology refactor across StoryEngine workflow contracts and related tests so canonical axis names are unambiguous.
- Preserve runtime behavior and replay compatibility while replacing overloaded legacy terms in active code and docs.

## Scope
- In:
  - Rename planning/code/test terminology to canonical axes:
    - `WorkflowKind` -> `WorkflowTopology` (or additive introduction + deprecation path where required by compatibility)
    - `StepKind` -> `WorkflowNodeExecutionKind` (phase-role concerns split explicitly)
    - workflow-facing `ChainMode` semantics -> `LlmTaskMode` where mapping is valid
  - Update test/proof naming and assertions to canonical names.
  - Define persistence compatibility behavior for legacy artifact fields.
  - Update active docs to canonical names and retain completed-doc inline rename annotations.
- Out:
  - Behavioral workflow-engine redesign.
  - New runtime features beyond naming/alignment and compatibility adapters.
  - Storage backend expansion (Postgres, distributed queues, cloud stores).

## Definition of Done
- Canonical terms are used in active planning docs and execution docs.
- Code compiles and tests pass with renamed types/fields or explicit compatibility adapters.
- Persistence compatibility policy is implemented and documented (`read legacy + write new` unless a justified alternative is approved).
- Deterministic replay of pre-refactor artifacts remains valid.
- No remaining authoritative usage of `WorkflowKind`/`StepKind` in active docs.
- Completed plans retain historical tokens with inline `RENAMED TO` annotations only.

## Cross-Plan Dependencies
- Depends on:
  - `Plans/README.md`
  - `Plans/terminology-supersession-map.md`
  - `Plans/Brainstorms/workflow-runner-single-call-and-transition-graph-baseline/plan.md`
- Supersedes (optional):
  - none

## Interfaces and Contracts Introduced/Changed
- Contract: `Workflow topology/type naming contract`
  - Owner: `Source/Zelanthus.StoryEngine.Domain`
  - Change Type: `changed`
  - Notes: Canonical topology axis name is `WorkflowTopology`.
- Contract: `Workflow node execution kind contract`
  - Owner: `Source/Zelanthus.StoryEngine.Domain`
  - Change Type: `changed`
  - Notes: Canonical node execution axis name is `WorkflowNodeExecutionKind`; phase role is explicit and separate.
- Contract: `LLM node mode contract`
  - Owner: `Source/Zelanthus.Llm.Clients.Abstractions` and `Source/Zelanthus.StoryEngine.Domain`
  - Change Type: `changed`
  - Notes: Canonical LLM mode axis name is `LlmTaskMode` with `CognitiveChain`, `ConversationalChain`, `SingleCall`.
- Contract: `Persistence compatibility contract`
  - Owner: `Source/Zelanthus.StoryEngine.Infrastructure`
  - Change Type: `introduced`
  - Notes: Legacy artifact field names are readable; new writes use canonical field names.

## Git Branch and PR Tracking
- Execution Branch: `pending (feature/terminology-refactor-workflow-enums)`
- Base Branch: `main`
- PR: `pending`

## Touchpoint Map
- Code:
  - `Source/Zelanthus.StoryEngine.Domain/**`
  - `Source/Zelanthus.StoryEngine.Application/**`
  - `Source/Zelanthus.StoryEngine.Infrastructure/**`
  - `Source/Zelanthus.Llm.Clients.Abstractions/**`
- Tests:
  - `Tests/Zelanthus.WorkflowContractProofs.Tests/**`
  - `Tests/Zelanthus.Architecture.Tests/**`
- Docs:
  - `Plans/README.md`
  - `Plans/terminology-supersession-map.md`
  - `Plans/Brainstorms/workflow-runner-single-call-and-transition-graph-baseline/**`
  - `Plans/Completed/**` (annotation-only updates where legacy terms exist)

## Risks and Mitigations
- Risk: Rename-only work unintentionally changes behavior.
  - Mitigation: lock scope to semantic renames + compatibility adapters; use proof tests and replay checks.
- Risk: Legacy artifacts become unreadable.
  - Mitigation: enforce `read legacy + write new` and add deterministic compatibility tests.
- Risk: Agents reintroduce deprecated names later.
  - Mitigation: keep supersession map authoritative and add architecture/contract checks where feasible.

## Validation and Testing
- Automated:
  - `dotnet build "Zelanthus.slnx"`
  - `dotnet test "Tests/Zelanthus.WorkflowContractProofs.Tests/Zelanthus.WorkflowContractProofs.Tests.csproj"`
  - `dotnet test "Tests/Zelanthus.Architecture.Tests/Zelanthus.Architecture.Tests.csproj"`
- Manual:
  - Verify completed-plan inline rename annotations exist at first legacy-term occurrence.
  - Verify active docs contain canonical terms only (except explicit legacy mapping sections).
  - Verify persisted pre-refactor artifacts replay correctly.

## Rollout / Rollback
- Rollout:
  - Implement in staged commits tied to this plan id.
  - Land contract/type renames with compatibility adapters first.
  - Update tests and docs, then finalize deprecation notes.
- Rollback:
  - Revert refactor commits as a unit.
  - Keep compatibility readers in place if partial rollout is reverted.

## Audit Trail Requirements
- Every rename commit message must include this plan id: `terminology-refactor-workflow-enums`.
- Closeout must record:
  - commit SHAs,
  - affected files,
  - compatibility behavior implemented,
  - deprecations left in place.

## Status Tracker
- [ ] `0010-inventory-legacy-usage-and-rename-impact`
- [ ] `0020-define-code-rename-and-adapter-strategy`
- [ ] `0030-define-persistence-compatibility-and-migration-path`
- [ ] `0040-define-proof-updates-and-audit-closeout`

## Notes
- This draft plans the future code refactor; it does not execute code changes in this docs-retcon pass.

---

## Source 2: `steps/index.md`

﻿# Steps Index

Use this file for dependency and status summaries.
Do not use this file as the only source of step order.
Step order must still be visible in folder names (`NNNN-semantic-step-name`).

## Step Map
| Order | Step Folder | Status | Depends On | Notes |
|---|---|---|---|---|
| 0010 | `0010-inventory-legacy-usage-and-rename-impact` | `pending` | `none` | Inventory all legacy term usage and classify rename/adapter impact. |
| 0020 | `0020-define-code-rename-and-adapter-strategy` | `pending` | `0010` | Lock exact type/field rename strategy and deprecation approach. |
| 0030 | `0030-define-persistence-compatibility-and-migration-path` | `pending` | `0010`, `0020` | Lock legacy artifact compatibility policy and migration behavior. |
| 0040 | `0040-define-proof-updates-and-audit-closeout` | `pending` | `0020`, `0030` | Define proof updates, acceptance evidence, and closeout audit data. |

## Status Values
- `pending`
- `in_progress`
- `blocked`
- `completed`

---

## Source 3: `steps/0010-inventory-legacy-usage-and-rename-impact/step.md`

﻿# Step: 0010-inventory-legacy-usage-and-rename-impact

## Goal
- Produce a complete inventory of legacy term usage and classify each occurrence by required action.

## Context
- Rename work must be precise and auditable to avoid semantic drift or accidental behavior change.

## Git Branch
- `pending (feature/terminology-refactor-workflow-enums)`

## Commits
- `none`

## Commands Executed
- `pending`

## Files Changed
- `pending`

## Tests / Results
- `not-run` -> `planning step`

## Issues
- `none`

## Decision
- pending

## Completion
- `pending`

## Next Actions
- Feed inventory into rename/adaptation strategy definition.

---

## Source 4: `steps/0020-define-code-rename-and-adapter-strategy/step.md`

﻿# Step: 0020-define-code-rename-and-adapter-strategy

## Goal
- Lock exact code-level rename strategy for enums/types/fields and define deprecation/adaptation approach.

## Context
- The implementation pass needs deterministic type mapping rules before touching runtime code.

## Git Branch
- `pending (feature/terminology-refactor-workflow-enums)`

## Commits
- `none`

## Commands Executed
- `pending`

## Files Changed
- `pending`

## Tests / Results
- `not-run` -> `planning step`

## Issues
- `none`

## Decision
- pending

## Completion
- `pending`

## Next Actions
- Use strategy to define persistence compatibility and proof updates.

---

## Source 5: `steps/0030-define-persistence-compatibility-and-migration-path/step.md`

﻿# Step: 0030-define-persistence-compatibility-and-migration-path

## Goal
- Define deterministic compatibility behavior for legacy persisted artifacts and migration/read-write policy.

## Context
- Rename refactors must not break replay of historical runs/artifacts.

## Git Branch
- `pending (feature/terminology-refactor-workflow-enums)`

## Commits
- `none`

## Commands Executed
- `pending`

## Files Changed
- `pending`

## Tests / Results
- `not-run` -> `planning step`

## Issues
- `none`

## Decision
- pending

## Completion
- `pending`

## Next Actions
- Feed compatibility policy into proof/update and closeout requirements.

---

## Source 6: `steps/0040-define-proof-updates-and-audit-closeout/step.md`

﻿# Step: 0040-define-proof-updates-and-audit-closeout

## Goal
- Define proof/test update requirements and final audit traceability expectations for rename rollout.

## Context
- The refactor is complete only when proofs, docs, and closeout evidence align to canonical terms.

## Git Branch
- `pending (feature/terminology-refactor-workflow-enums)`

## Commits
- `none`

## Commands Executed
- `pending`

## Files Changed
- `pending`

## Tests / Results
- `not-run` -> `planning step`

## Issues
- `none`

## Decision
- pending

## Completion
- `pending`

## Next Actions
- Execute refactor implementation plan with commit-level traceability.

---

## Source 7: `archive-note.md`

# Archive Note

## Summary
- Plan: `<plan-folder-name>`
- Archived From: `Plans/<stage>/<plan-folder-name>`
- Archived To: `Plans/Archived/<bucket>/<plan-folder-name>`
- Archived On: `<YYYY-MM-DD>`
- Archived By: `<name-or-agent>`

## Reason
- <Why this plan was archived>

## Completion State
- `<abandoned|superseded|completed-history>`

## Follow-up
- Replacement Plan (optional): `<path>`
- Relevant Notes (optional): <notes>
