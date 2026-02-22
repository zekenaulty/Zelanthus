# terminology-refactor-workflow-enums

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
