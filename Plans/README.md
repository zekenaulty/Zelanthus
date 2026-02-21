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
- InProgress-stage plan cancelled before completion -> `Archived/Drafts/<plan-folder-name>`
- Completed plans aged out of recent view -> `Archived/CompletedHistory/<plan-folder-name>`

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
- Direct implementation commits to `main`/trunk are not allowed for InProgress work.

Minor exception:
- Small documentation-only updates can be done without a feature branch only when they are not tied to an active `Plans/InProgress/...` plan.
- Docs-only means no changes under project code folders (`Source/`, `Tests/` when present), build or CI config, container definitions, migrations, or runtime configuration.
- If a change can affect runtime, build, test behavior, or deployment, it requires a feature branch.

In-flight dependency documentation rule:
- If you discover a new dependency while working in a feature branch:
  - capture the smallest possible note in the executing plan folder (`Plans/InProgress/<plan-folder-name>/notes/`) or a minimal scoped step folder,
  - avoid broad planning detours,
  - return to the feature execution flow immediately.
- If the dependency requires a global doctrine/template change:
  - create a separate planning change on `main`/trunk as its own scoped planning update,
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
- Keep recently completed plans in `Plans/Completed`.
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

A plan cannot move to `Completed` unless:
- DoD is satisfied.
- Validation evidence is present.
- Follow-up work is documented (if any).

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
- Use composition and explicit interfaces where they add boundary clarity.
- Keep naming semantic and domain-aligned.
- Keep test project names semantic and purpose-based (for example `Zelanthus.WorkflowContractProofs.Tests`).
- Avoid generic test project names with weak intent (`MvpHarness`, `TempTests`, `MiscTests`).
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
