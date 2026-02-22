# Zelanthus Planning Guide

## Purpose
This guide defines how planning is structured in this workspace.

Primary goals:
- Keep plan scope isolated by folder, not by long file names.
- Keep active work easy to navigate.
- Preserve history without polluting active plan areas.
- Make plans clear for humans and LLM/agent workflows.

## Core Rules
- Folder scope is the primary scope boundary.
- Every plan must live in its own plan folder.
- All artifacts for a plan must stay inside that plan folder.
- Never keep in-flight plan artifacts at a stage root.
- If names are getting long to carry scope context, create subfolders instead.

## Plans Folder Model
Root:
- `Plans/Brainstorms`
- `Plans/Drafts`
- `Plans/InProgress`
- `Plans/Completed`
- `Plans/Archived/Brainstorms`
- `Plans/Archived/Drafts`
- `Plans/Archived/CompletedHistory`

Each stage folder contains plan folders, not loose plan files.

Example:
```text
Plans/
  Drafts/
    establish-domain-boundaries/
      plan.md
      notes/
      steps/
      artifacts/
      validation/
      decisions/
      risks/
```

## Folder-First Scope Isolation
- Scope is expressed first by path:
  - stage -> plan folder -> artifact folder -> file
- File names should be concise and semantic.
- Do not encode full scope chains in a single file name.

Bad:
- `step_20260220_1950_domain_service_migration_contract_alignment_pass2_retry3.md`

Good:
- `Plans/InProgress/domain-service-alignment/steps/0010-align-service-boundary/step.md`

## Required Plan Folder Structure
Minimum structure for every plan folder:
- `plan.md` (authoritative plan)
- `steps/` (execution notes, one folder per step)
- `artifacts/` (generated outputs, snapshots, reports)
- `validation/` (test notes, verification evidence)
- `decisions/` (decision log entries or summary)
- `risks/` (risk log entries or summary)

Optional:
- `notes/` (scratch thinking that is still plan-scoped)
- `archive-note.md` (required when moved to Archived)

## Templates
Use the template pack at:
- `Plans/Templates/plan-folder/`

Template files:
- `Plans/Templates/plan-folder/plan.md`
- `Plans/Templates/plan-folder/steps/index.md`
- `Plans/Templates/plan-folder/steps/0010-example-step/step.md`
- `Plans/Templates/plan-folder/archive-note.md`

Template usage:
1. Copy `Plans/Templates/plan-folder/` into the target stage folder.
2. Rename the copied folder to your semantic plan name.
3. Update `plan.md`.
4. Replace `0010-example-step` with real step folders using `NNNN-semantic-step-name`.
5. Keep step-local artifacts inside each step folder.

## Step Organization
Steps are noisy and high-value. Steps must be folderized.

Rule:
- One step = one step folder under `steps/`.
- Each step folder is a scoped sub-workspace for that step only.
- Inside each step folder, keep `step.md` plus step-local artifacts/evidence.
- Do not place step artifacts in the plan root when they belong to one step.

Pattern:
```text
steps/
  0010-align-preflight-contract/
    step.md
    artifacts/
    validation/
    notes/
  0020-harden-domain-events/
    step.md
    artifacts/
```

Do not use:
- flat step files in plan root
- sequence-only names like `0010`, `0020` without semantic slug

## Step Order and Naming
Step folders must encode both order and semantic meaning.

Required pattern:
- `NNNN-semantic-step-name`
- Example: `0010-align-service-boundary`

Rules:
- `NNNN` is a zero-padded order token used for quick visual ordering.
- `semantic-step-name` is the identity and must describe the actual work.
- Prefer order gaps (for example `0010`, `0020`, `0030`) to allow inserts later.
- Inserted work can use an in-between number (for example `0015-add-contract-guard`).
- Order token expresses plan order, not proof of completion.
- Optional: keep `steps/index.md` for dependency/status summaries, not for basic ordering.

## Semantic Naming Standard
Names convey meaning to humans and models. Prefer strong semantics over sequence.

Rules:
- Use meaningful domain names.
- Use intent/outcome language in step names.
- Avoid reused generic names plus numeric suffixes.
- If a sequence token exists, pair it with a semantic slug.
- Semantic slug is the naming identity; sequence token is ordering metadata.

Prefer:
- `enforce-command-handler-boundaries`
- `split-infrastructure-repository-abstractions`
- `add-containerized-integration-test-harness`

Avoid:
- `step-1`
- `draft-v2-final-final`
- `refactor-part-3`

## Stage Workflow
Default flow:
1. `Brainstorms`
2. `Drafts`
3. `InProgress`
4. `Completed`

Archive flow:
- Brainstorms-stage plan abandoned -> `Archived/Brainstorms/<plan-folder-name>`
- Drafts-stage plan abandoned or reset-before-execution -> `Archived/Drafts/<plan-folder-name>`
- InProgress-stage plan cancelled before completion -> `Archived/Drafts/<plan-folder-name>` (must include `archive-note.md` fields `archived-from: InProgress` and `cancellation-state: cancelled`)
- Completed plans aged out of recent view -> `Archived/CompletedHistory/<plan-folder-name>`

## Stage Transition Protocols
Planning phases and execution phases are different records. Do not collapse them into one mutable folder history.

### Brainstorms -> Drafts
Use this transition when doctrine-level exploration is stable enough to become an implementation-ready draft.

Required promotion behavior:
- Promotion occurs on `main`/trunk as planning work.
- Keep brainstorm context available for audit.
- Create or refresh `Plans/Drafts/<plan-slug>/` with draft-authoritative plan content.
- Carry forward only the validated decisions/contracts/risks needed for execution planning.
- Compile the draft and keep the compiled file inside the draft folder.
- Mark draft step status according to planning completion, not implementation completion.

### Drafts -> InProgress
Use this transition only after explicit user approval.

Required promotion behavior:
- Promotion occurs on `main`/trunk as a planning-only commit.
- Do not move the draft folder into `InProgress`.
- Freeze draft as planning baseline and keep it unchanged except explicit typo-level corrections.
- Typo-level means spelling, grammar, formatting, or link-fix updates only (no semantic planning change).
- Any semantic change after freeze requires a new draft revision folder (for example `<plan-slug>-r2`) and a new promotion.
- Create `Plans/InProgress/<plan-slug>/` as execution-shaped plan content.
- Rebuild execution `steps/` for implementation work (do not reuse planning step tracker as implementation tracker).
- Add promotion trace file in `InProgress` (for example `promotion.md`) with:
  - source draft path,
  - source commit SHA,
  - promotion date,
  - transformation summary (copied vs reshaped content).
- After promotion commit on `main`, create execution branch `feature/<plan-slug>` and run implementation there.

### InProgress -> Completed
Use this transition after implementation finishes and validation evidence is complete.

Required closure behavior:
- Build closure artifacts on the execution feature branch.
- Build a completed package at:
  - `Plans/Completed/<plan-slug>/`
- Completed package standard:
  - `draft-baseline/`
    - `<plan-slug>-draft.md`
    - `source-ref.md`
  - `implementation/`
    - verbatim snapshot of `Plans/InProgress/<plan-slug>/` at merge-target commit, including `plan.md`, `steps/`, `notes/`, `artifacts/`, `validation/`, `decisions/`, and `risks/`
  - `closeout/`
    - `closeout.md`
    - `outcomes.md`
    - `traceability.md` (draft-step -> execution-step -> evidence mapping)
    - `archive-note.md`
- When closure package and cleanup commits are present on the feature branch, explicitly signal PR-ready handoff to the user.
- User manually creates and merges the PR.
- Closure is finalized on `main`/trunk only after PR merge.
- Keep phase history inside `Completed` package. Do not require traversal of old active folders for review.

### Active Folder Cleanup Rule
After completed package is committed, clean active stage folders in a separate commit.

Required cleanup behavior:
- Commit 1: create/update `Plans/Completed/<plan-slug>/...` package.
- Commit 2: delete:
  - `Plans/Drafts/<plan-slug>/`
  - `Plans/InProgress/<plan-slug>/`
- Record required SHAs in `draft-baseline/source-ref.md` and/or `closeout/traceability.md`:
  - source draft commit SHA (frozen draft baseline),
  - promotion commit SHA (`Drafts -> InProgress`),
  - feature branch base commit SHA,
  - merge commit SHA (populate after merge if unknown at PR creation time),
  - completed-package commit SHA,
  - cleanup commit SHA.
- Goal: keep active planning roots clean and avoid long-term repo noise in working stages.

### Status Tracker Semantics by Phase
Do not reuse one status tracker 1:1 across all phases.

Rules:
- `Brainstorms`: tracker reflects exploration maturity only.
- `Drafts`: tracker reflects planning completeness and contract clarity.
- `InProgress`: tracker reflects execution/testing/evidence completion.
- `Completed`: closeout checklist confirms DoD, validation, and final outcomes.
- `closeout/traceability.md` is the continuity link across phase-specific trackers.

## Git Branching and Commit Flow
Planning workflow and coding workflow are intentionally different.

Planning default:
- Planning and doctrine work should happen primarily on `main`/trunk.
- `Brainstorms` and `Drafts` updates should stay focused on planning artifacts and decision clarity.

Promotion gate:
- `Drafts -> InProgress` promotion is owner-controlled.
- Do not promote a plan unless the user explicitly approves promotion.
- If promotion intent is unclear, stop and ask before moving a plan to `Plans/InProgress/...`.
- Default promotion flow:
  - make a planning-only promotion commit on `main`/trunk,
  - then create the execution feature branch from that commit.

Execution default:
- Any plan promoted to `Plans/InProgress/...` must execute on a feature branch.
- One active InProgress plan should map to one active feature branch whenever possible.
- Branch naming pattern:
  - `feature/<plan-folder-name>`
  - optional: `feature/<plan-folder-name>-<short-scope>`
  - follow-up/repeat work: `feature/<plan-folder-name>-r2` or `feature/<plan-folder-name>-followup-<slug>`

PR ownership:
- Feature branches are prepared by implementation work and handed to the user for PR creation/merge.
- Agents do not open PRs; they must explicitly signal PR readiness and wait for user PR creation/merge.
- Direct implementation commits to `main`/trunk are not allowed for InProgress work.

Minor exception:
- Small documentation-only updates can be done without a feature branch only when they are not tied to an active `Plans/InProgress/...` plan.
- Docs-only allowlist for this exception: `Plans/**` and `Docs/**` (when present).
- Docs-only means no changes under project code folders (`Source/`, `Tests/` when present), build or CI config, container definitions, migrations, or runtime configuration.
- If a change can affect runtime, build, test behavior, or deployment, it requires a feature branch.

In-flight dependency documentation rule:
- If you discover a new dependency while working in a feature branch:
  - capture the smallest possible note in the executing plan folder (`Plans/InProgress/<plan-folder-name>/notes/`) or a minimal scoped step folder,
  - avoid broad planning detours,
  - return to the feature execution flow immediately.
- If the dependency requires a global doctrine/template change:
  - create a separate planning change on `main`/trunk as its own scoped planning update,
  - add a link-back note in the active `Plans/InProgress/<plan-folder-name>/notes/` with the planning change commit SHA and one-line rationale,
  - do not mix global planning mutations into the active implementation branch unless explicitly requested.
- Goal: document needed context without losing implementation focus.

Commit reliability standard:
- Commits must be atomic and semantically meaningful.
- Avoid noisy or generic commit messages.
- Commits should not knowingly leave touched scope in a broken state.
- Prefer commit message pattern: `<type>(<plan-slug>/<step-slug>): <semantic outcome>`.
- If tests are not run, record that explicitly in the step note.
- Step notes must reference full commit SHAs produced in that step.
- For non-trivial steps, include `git show --name-only <sha>` evidence in step-local artifacts or notes.

## Completed vs CompletedHistory
`Plans/Completed` is a recent work shelf.
`Plans/Archived/CompletedHistory` is long-term history.

Working policy:
- Keep recently completed plan packages in `Plans/Completed`.
- Move older completed plans to `Plans/Archived/CompletedHistory` on a regular cadence.
- When moving, keep folder contents intact and add/update `archive-note.md` with reason and move date.

## Required Content in plan.md
Every `plan.md` must include:
- Objective
- Scope
- Out of scope / non-goals
- Definition of Done
- Cross-plan dependencies (explicit references to required decisions/plans)
- Interfaces and contracts introduced/changed
- Git branch and PR tracking (for InProgress execution)
- Touchpoint map (what will change, where)
- Risks and mitigations
- Validation/test approach
- Rollout/rollback approach
- Status tracker

Dependency note pattern:
- `Depends on: <path-to-plan-or-decision>`
- `Supersedes (optional): <path-to-plan-or-decision>`

Interface/contract note pattern:
- Contract name
- Owner layer/project
- Introduced/changed/removed
- Required invariants or compatibility notes

Git tracking note pattern:
- `Execution Branch: <branch-name-or-not-applicable>`
- `Base Branch: <base-branch>`
- `PR: <url-or-pending-or-not-applicable>`

## Required Content in steps/*/step.md
Each step note must include:
- Goal
- Context
- Git branch
- Commits produced in step (if any)
- Commands executed (exact commands)
- Files changed
- Tests/results
- Issues
- Decision
- Completion state
- Next actions

## Planning Quality Gates
A plan cannot move to `InProgress` unless:
- User explicitly approves promotion.
- Definition of Done is explicit and testable.
- Scope and out-of-scope are explicit.
- Cross-plan dependencies are explicit and valid.
- Interfaces/contracts introduced or changed are explicit.
- Branching and PR approach is explicit.
- Touchpoints are concrete.
- Risks and mitigations are captured.
- Validation approach is defined.
- Draft baseline is frozen and traceable to source commit SHA.
- `InProgress` tracker is reset for execution scope (not copied as completed from draft planning).

A plan cannot move to `Completed` unless:
- DoD is satisfied.
- Validation evidence is present.
- Follow-up work is documented (if any).
- `Plans/Completed/<plan-slug>/` package exists with `draft-baseline/`, `implementation/`, and `closeout/`.
- `closeout/traceability.md` maps draft intent to execution evidence.
- PR-ready handoff was explicitly signaled to the user and the user-owned PR was merged to `main`/trunk.
- Active folder cleanup commit is completed (`Drafts` and `InProgress` plan folders removed).

## .NET/C# Coding Best Practices
This workspace is primarily .NET/C# and should follow DDD and SOLID.

Rules:
- Target framework baseline is `.NET 10` (`net10.0`) unless explicitly approved otherwise.
- New projects should align to `net10.0` to avoid versioning drift.
- Do not mix target frameworks in active implementation scope without an explicit planning decision.
- If dependencies force a framework shift, capture it in plan risks/decisions and request approval before changing targets.
- Keep domain logic in domain layers, not controllers or infrastructure glue.
- Use clear separation of concerns across Domain, Application, Infrastructure, and API.
- Avoid monolithic files/classes; split by responsibility.
- Do not keep feature classes in project root folders.
- Project-root `.cs` allowlist is only `Program.cs` and `GlobalUsings.cs`; all other source files must be folderized by concern.
- Use semantic folders and matching namespaces (for example `Contracts/` -> `.Contracts`, `Workflows/` -> `.Workflows`, `Storage/` -> `.Storage`).
- Treat one-tier folderization as a minimum baseline, not an end state.
- If a folder starts accumulating mixed concerns, split immediately into second-tier semantic folders and namespaces.
- High-change surfaces must use deeper isolation early (for example `Application/Contracts/RunExecution` vs `Application/Contracts/StepExecution`).
- Storage concerns must isolate backend types by folder/namespace (for example `Storage/Local`, `Storage/Postgres`) with shared abstractions and records separated from implementations.
- For Entity Framework-backed persistence, model tracked entities as `class` types (identity-based), not `record` types.
- Use `record` types for value-style contracts/snapshots where value equality semantics are intentional.
- Use composition and explicit interfaces where they add boundary clarity.
- Keep naming semantic and domain-aligned.
- Keep test project names semantic and purpose-based (for example `Zelanthus.WorkflowContractProofs.Tests`).
- Avoid generic test project names with weak intent (`MvpHarness`, `TempTests`, `MiscTests`).
- Use `LlmTaskMode` as the only term for LLM node mode semantics in new plans and code.
- Enforce test coverage for core domain/application behavior.
- Prefer containerized local dependency workflows for repeatable setup and integration testing.
- Keep documentation and plan artifacts aligned with real code behavior.

## Anti-Drift Rules
- Do not rename files to carry extra scope context when a folder split is cleaner.
- Do not leave orphan artifacts outside the owning plan folder.
- Do not mix unrelated initiatives in the same plan folder.
- Do not treat chronological sequence as semantic identity.

## Collaboration and Handoff
- Read `plan.md` plus latest relevant step folders before continuing work.
- Preserve history by adding new step folders, not rewriting old step notes.
- Keep all evidence and artifacts inside the owning plan folder to maintain scope integrity.
