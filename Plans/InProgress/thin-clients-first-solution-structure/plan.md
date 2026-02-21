# thin-clients-first-solution-structure

## Objective
- Execute the thin-clients-first implementation sequence on one feature branch.
- Coordinate Plan 1 and Plan 2 execution with explicit evidence, dependency discipline, and closeout traceability.

## Scope
- In:
  - Execution coordination for:
    - `Plans/InProgress/mvp-prompting-gemini-contract-baseline`
    - `Plans/InProgress/mvp-chain-runner-local-persistence-baseline`
  - Feature-branch execution governance (`feature/thin-clients-first-solution-structure`).
  - PR-ready handoff packaging and closeout traceability.
- Out:
  - New planning doctrine work unrelated to active execution.
  - Additional MVP expansion beyond Plan 1 and Plan 2 acceptance boundaries.
  - Production persistence setup (Postgres) and deployment topology.
  - Frontend/chat experience concerns.

## Definition of Done
- Plan 1 execution is completed with required validation/evidence.
- Plan 2 execution is completed with required validation/evidence.
- InProgress branch tracking and PR handoff data are current.
- Closeout traceability maps:
  - thin-clients execution coordination -> Plan 1 execution evidence,
  - thin-clients execution coordination -> Plan 2 execution evidence.
- Completion package and cleanup commits are prepared per `Plans/README.md`.

## Cross-Plan Dependencies
- Depends on:
  - `Plans/README.md`
  - `Plans/Brainstorms/backbone-doctrine-prompt-first-multi-provider/decisions/0001-backbone-doctrine-v0.md`
  - `Plans/Brainstorms/backbone-doctrine-prompt-first-multi-provider/decisions/0002-chain-continuity-strategy-v0.md`
  - `Plans/Brainstorms/backbone-doctrine-prompt-first-multi-provider/decisions/0003-prompt-governance-and-provenance-v0.md`
  - `Plans/Brainstorms/backbone-doctrine-prompt-first-multi-provider/decisions/0004-mvp-artifact-and-failure-contract-v0.md`
- Supersedes (optional):
  - none

## Interfaces and Contracts Introduced/Changed
- Contract: `Application -> ILlmClient execution envelope`
  - Owner: `Zelanthus.StoryEngine.Application` and `Zelanthus.Llm.Clients.Abstractions`
  - Change Type: `introduced`
  - Notes: Defines canonical request/response contract path used by workflow orchestration.
- Contract: `Provenance assembly ownership`
  - Owner: `Application orchestration`
  - Change Type: `introduced`
  - Notes: Adapter returns protocol metadata; application composes final provenance record.
- Contract: `Architecture boundary enforcement`
  - Owner: `Solution structure`
  - Change Type: `introduced`
  - Notes: Early architecture tests block dependency-direction drift.
- Contract: `MVP goal and acceptance contract`
  - Owner: `Draft plan governance`
  - Change Type: `introduced`
  - Notes: Defines what must be proven in MVP before broad story engine expansion.
- Contract: `MVP harness requirement contract`
  - Owner: `Draft plan governance`
  - Change Type: `introduced`
  - Notes: Defines required harness behavior independent of API-host or test-host execution shape.

## Git Branch and PR Tracking
- Execution Branch: `feature/thin-clients-first-solution-structure`
- Base Branch: `main`
- PR: `pending (user-owned manual PR)`

## Touchpoint Map
- Code:
  - `Zelanthus.slnx` (current baseline reference)
  - `Source/Zelanthus.API` (current host baseline reference only)
- Docs:
  - `Plans/InProgress/thin-clients-first-solution-structure/plan.md`
  - `Plans/InProgress/thin-clients-first-solution-structure/decisions/0001-solution-project-boundaries-v0.md`
  - `Plans/InProgress/thin-clients-first-solution-structure/decisions/0002-thin-clients-first-sequencing-v0.md`
  - `Plans/InProgress/thin-clients-first-solution-structure/decisions/0003-dependency-direction-rules-v0.md`
  - `Plans/InProgress/thin-clients-first-solution-structure/decisions/0004-prompting-ownership-and-versioning-policy-v0.md`
  - `Plans/InProgress/thin-clients-first-solution-structure/decisions/0005-infrastructure-prompt-shape-mapping-policy-v0.md`
  - `Plans/InProgress/thin-clients-first-solution-structure/decisions/0006-call-envelope-and-enforcement-policy-v0.md`
  - `Plans/InProgress/thin-clients-first-solution-structure/decisions/0007-mvp-goals-and-first-plan-boundary-v0.md`
  - `Plans/InProgress/thin-clients-first-solution-structure/decisions/0008-mvp-harness-shape-selection-v0.md`
  - `Plans/InProgress/thin-clients-first-solution-structure/decisions/0009-contract-proof-test-naming-policy-v0.md`
  - `Plans/InProgress/thin-clients-first-solution-structure/artifacts/initial-solution-shape.md`
  - `Plans/InProgress/thin-clients-first-solution-structure/risks/risk-log.md`
  - `Plans/InProgress/mvp-prompting-gemini-contract-baseline/plan.md`
  - `Plans/InProgress/mvp-chain-runner-local-persistence-baseline/plan.md`
- Infrastructure/Config:
  - local MVP persistence only (path/file-backed baseline; no Postgres setup in this draft).
  - local persistence in this scope means workspace-local storage of:
    - turn artifacts/checkpoints,
    - raw response snapshots,
    - provenance records,
    - validation/failure outputs needed for deterministic resume/replay.
  - local persistence in this scope excludes external database, distributed cache, queue, or service-hosted storage.

## Risks and Mitigations
- Risk: Project split introduces too many assemblies too early.
  - Mitigation: Start with a minimal but explicit package set; expand only when boundary pressure is clear.
- Risk: Story engine couples to provider-specific implementation details.
  - Mitigation: Story engine depends only on thin-client abstractions and prompting contracts.
- Risk: Prompting utilities are duplicated across projects.
  - Mitigation: Keep prompting as its own reusable project boundary.
- Risk: Harness shape decision causes rework.
  - Mitigation: Lock goal/acceptance contract first; decide host shape from explicit decision criteria.

## Validation and Testing
- Automated:
  - not-run (docs-only draft refinement)
- Manual:
  - Verify MVP goals and non-goals are explicit and testable.
  - Verify Plan 1 and Plan 2 boundaries align with accepted backbone decisions.
  - Verify harness criteria are sufficient to evaluate test-host vs API-host without changing MVP goals.

## Rollout / Rollback
- Rollout:
  - Execute Plan 1 and Plan 2 within the active feature branch.
  - Keep execution evidence and step notes current in all three InProgress plan folders.
  - Hand off PR creation/merge to the user once closure package and cleanup commits are ready.
- Rollback:
  - Pause execution, document blocker state in step notes, and re-baseline via Draft revision if execution scope changes materially.

## Status Tracker
- [ ] `0120-execute-mvp-prompting-gemini-contract-baseline`
- [ ] `0130-execute-mvp-chain-runner-local-persistence-baseline`
- [ ] `0140-prepare-closeout-and-pr-handoff`

## Notes
- This InProgress folder is execution-authoritative for the thin-clients top-level coordination plan.
- Draft planning artifacts remain preserved in `Plans/Drafts/thin-clients-first-solution-structure`.
- Existing legacy step folders (`0010`-`0110`) remain as promoted baseline context; execution tracking in this phase uses `0120`-`0140`.

