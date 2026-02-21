# mvp-chain-runner-local-persistence-baseline

## Compiled Plan Metadata

- Plan Scope: `InProgress/mvp-chain-runner-local-persistence-baseline`
- Compiled At (UTC): `2026-02-21T12:09:55Z`
- Source Document Count: `31`
- Projection File: `mvp-chain-runner-local-persistence-baseline.md`

## Contents

1. `plan.md`
2. `decisions/0001-chain-runner-execution-model-v0.md`
3. `decisions/0002-local-persistence-contract-and-layout-v0.md`
4. `decisions/0003-project-reference-graph-and-boundary-enforcement-v0.md`
5. `decisions/0004-retry-resume-and-reason-code-policy-v0.md`
6. `decisions/0005-minimal-workflow-abstraction-v0.md`
7. `risks/risk-log.md`
8. `validation/plan-2-acceptance-evidence-matrix.md`
9. `steps/index.md`
10. `steps/0010-lock-plan-2-scope-and-contract-dependencies/step.md`
11. `steps/0020-define-chain-runner-domain-and-state-model/step.md`
12. `steps/0025-define-minimal-workflow-abstraction-hooks/step.md`
13. `steps/0030-define-application-orchestration-flow-and-chain-router/step.md`
14. `steps/0040-define-local-persistence-artifact-and-checkpoint-layout/step.md`
15. `steps/0050-define-infrastructure-mappers-and-store-contracts/step.md`
16. `steps/0060-define-retry-resume-and-reason-code-policy/step.md`
17. `steps/0070-plan-project-reference-graph-and-architecture-tests/step.md`
18. `steps/0080-define-runner-proof-test-suite-and-evidence-artifacts/step.md`
19. `steps/0090-define-api-composition-boundaries-and-host-integration/step.md`
20. `steps/0100-finalize-plan-2-acceptance-gates-and-promotion-readiness/step.md`
21. `steps/0020-define-chain-runner-domain-and-state-model/artifacts/run-turn-state-machine.md`
22. `steps/0025-define-minimal-workflow-abstraction-hooks/artifacts/minimal-workflow-abstraction-contract.md`
23. `steps/0040-define-local-persistence-artifact-and-checkpoint-layout/artifacts/local-persistence-layout.md`
24. `steps/0050-define-infrastructure-mappers-and-store-contracts/artifacts/provenance-mapping-contract.md`
25. `steps/0060-define-retry-resume-and-reason-code-policy/artifacts/reason-code-and-retry-policy.md`
26. `steps/0070-plan-project-reference-graph-and-architecture-tests/artifacts/architecture-test-assertion-matrix.md`
27. `steps/0080-define-runner-proof-test-suite-and-evidence-artifacts/artifacts/runner-proof-evidence-spec.md`
28. `steps/0080-define-runner-proof-test-suite-and-evidence-artifacts/artifacts/runner-proof-test-seed.md`
29. `steps/0100-finalize-plan-2-acceptance-gates-and-promotion-readiness/artifacts/final-promotion-checklist.md`
30. `artifacts/project-dependency-implementation-matrix.md`
31. `promotion.md`

---

## Source 1: `plan.md`

# mvp-chain-runner-local-persistence-baseline

## Objective
- Execute Plan 2 for chain runner orchestration and local persistence after Plan 1 contracts are established.
- Define exact dependency direction, project reference updates, and acceptance evidence for variable-length `CognitiveChain` and `ConversationalChain` execution.
- Keep the MVP workflow-aware with minimal extensibility hooks so additional workflow shapes can be added later without reworking core runner boundaries.

## Scope
- In:
  - Story engine runner contract and orchestration planning across:
    - `Source/Zelanthus.StoryEngine.Domain`
    - `Source/Zelanthus.StoryEngine.Application`
    - `Source/Zelanthus.StoryEngine.Infrastructure`
  - Plan-level dependency/reference design for integrating:
    - `Source/Zelanthus.Prompting`
    - `Source/Zelanthus.Llm.Clients.Abstractions`
    - `Source/Zelanthus.Llm.Clients.Gemini`
  - Local persistence contract planning for checkpoints, turn artifacts, provenance records, validation/failure evidence.
  - Retry/resume policy planning with explicit reason codes and continuity fallback behavior.
  - Architecture test planning updates for new project boundaries and dependency gates.
  - Runner proof-test planning updates in `Tests/Zelanthus.WorkflowContractProofs.Tests`.
  - Minimal workflow abstraction planning that supports data/code-defined workflow expansion without introducing full meta workflow complexity.
- Out:
  - Production persistence (Postgres) setup and migrations.
  - Multi-provider runtime routing and additional provider adapters.
  - Prompt template import/sub-template implementation details.
  - Frontend/chat product surface and endpoint breadth.
  - Distributed queue/worker orchestration and cloud storage.

## Definition of Done
- Plan 2 scope and non-goals are explicit and aligned to Plan 1 outputs.
- Project reference graph for Plan 2 implementation is explicit, testable, and consistent with dependency-direction decisions.
- Runner execution model is explicit:
  - supports variable-length `CognitiveChain` and `ConversationalChain`,
  - uses `PLAN_STEP` -> `EXECUTE` as a minimum cognitive unit, not a hard turn cap.
- Cognitive and conversational resume policy differences are explicit:
  - `CognitiveChain` resume restarts from chain start (`PLAN_STEP`) for correctness,
  - `ConversationalChain` resume continues from last successful persisted step.
- Local persistence contract is explicit:
  - workspace-local file/path-backed stores only,
  - no external database/cache/queue/service-hosted storage in Plan 2.
- Run/turn state machine is explicit:
  - state enums are pinned,
  - legal and illegal transitions are documented and reason-coded.
- Resume/retry/failure policy is explicit with pinned reason codes and deterministic behavior.
- Required turn/provenance/artifact schemas and mapping boundaries are explicit.
- Test and evidence contract is explicit for:
  - golden-path runner execution,
  - reason-coded failure paths,
  - deterministic resume from persisted artifacts.
- Architecture test gate updates are explicitly scoped to StoryEngine project additions.
- Step sequence, dependencies, and acceptance evidence are implementation-ready.

## Cross-Plan Dependencies
- Depends on:
  - `Plans/README.md`
  - `Plans/InProgress/thin-clients-first-solution-structure/plan.md`
  - `Plans/InProgress/thin-clients-first-solution-structure/decisions/0001-solution-project-boundaries-v0.md`
  - `Plans/InProgress/thin-clients-first-solution-structure/decisions/0002-thin-clients-first-sequencing-v0.md`
  - `Plans/InProgress/thin-clients-first-solution-structure/decisions/0003-dependency-direction-rules-v0.md`
  - `Plans/InProgress/thin-clients-first-solution-structure/decisions/0004-prompting-ownership-and-versioning-policy-v0.md`
  - `Plans/InProgress/thin-clients-first-solution-structure/decisions/0005-infrastructure-prompt-shape-mapping-policy-v0.md`
  - `Plans/InProgress/thin-clients-first-solution-structure/decisions/0006-call-envelope-and-enforcement-policy-v0.md`
  - `Plans/InProgress/thin-clients-first-solution-structure/decisions/0007-mvp-goals-and-first-plan-boundary-v0.md`
  - `Plans/InProgress/mvp-prompting-gemini-contract-baseline/plan.md`
  - `Plans/Brainstorms/backbone-doctrine-prompt-first-multi-provider/decisions/0002-chain-continuity-strategy-v0.md`
  - `Plans/Brainstorms/backbone-doctrine-prompt-first-multi-provider/decisions/0003-prompt-governance-and-provenance-v0.md`
  - `Plans/Brainstorms/backbone-doctrine-prompt-first-multi-provider/decisions/0004-mvp-artifact-and-failure-contract-v0.md`
- Supersedes (optional):
  - none

## Interfaces and Contracts Introduced/Changed
- Contract: `ChainRunner orchestration contract`
  - Owner: `Zelanthus.StoryEngine.Application`
  - Change Type: `introduced`
  - Notes: Defines run lifecycle, chain mode selection, turn sequencing, retry/resume flow, and acceptance gating.
- Contract: `Chain runtime state contract`
  - Owner: `Zelanthus.StoryEngine.Domain`
  - Change Type: `introduced`
  - Notes: Defines run identity, chain/turn state model, and invariants independent from provider protocol details.
- Contract: `Local artifact/checkpoint persistence contract`
  - Owner: `Zelanthus.StoryEngine.Infrastructure`
  - Change Type: `introduced`
  - Notes: Defines deterministic file/path-backed storage model and mapping to runtime contracts.
- Contract: `Provenance artifact mapping contract`
  - Owner: `Zelanthus.StoryEngine.Infrastructure` and `Zelanthus.Prompting`
  - Change Type: `introduced`
  - Notes: Provenance artifact is strict superset shape with deterministic mapping to Prompting required fields.
- Contract: `Workflow definition and step abstraction contract`
  - Owner: `Zelanthus.StoryEngine.Application` and `Zelanthus.StoryEngine.Domain`
  - Change Type: `introduced`
  - Notes: Defines minimal workflow/step metadata and extension hooks for future routing/branching without full graph complexity.
- Contract: `Resume and retry policy contract`
  - Owner: `Zelanthus.StoryEngine.Application`
  - Change Type: `introduced`
  - Notes: Defines when to retry current turn, restart at planning turn, or fail terminally with reason codes.
- Contract: `Plan 2 proof-evidence contract`
  - Owner: `Tests/Zelanthus.WorkflowContractProofs.Tests`
  - Change Type: `changed`
  - Notes: Extends proof artifacts from Plan 1 to include runner state, checkpoint, and resume evidence.

## Runner Execution Contract (Plan 2)
- Supported modes:
  - `CognitiveChain`
  - `ConversationalChain`
- Minimum unit:
  - `PLAN_STEP` -> `EXECUTE` remains the minimum cognitive unit for workflow correctness.
- Flexibility rule:
  - Plan 2 runner is not hard-limited to one plan/execute pair.
  - Cognitive flows may stage multiple `PLAN_STEP` units, then execute one or more `EXECUTE` units.
  - Cognitive flows may run multiple `PLAN_STEP` -> `EXECUTE` cycles in the same chain.
  - Conversational flows may execute variable-length turn sequences with per-turn artifact persistence.
- Thinking persistence rule (Cognitive):
  - provider responses may emit a `thoughtSignature` value each turn; Zelanthus tracks this as `thinking_persistence_key`.
  - continuity handle may rotate on both `PLAN_STEP` and `EXECUTE`.
  - for `CognitiveChain`, the next `PLAN_STEP`/`EXECUTE` request should pass forward the latest persisted `thinking_persistence_key` when provider capability supports it.
  - `thinking_persistence_key` may change turn-to-turn; do not assume a single stable value for an entire chain run.
  - on successful provider response parse, step artifacts persist returned key and advance `latest_thinking_persistence_key`.
  - if turn fails before provider response parse completes, `latest_thinking_persistence_key` is not advanced.
  - cognitive step artifacts persist the latest key as primary provider-continuity evidence; response text may also be persisted for audit/replay.
  - capability-conditioned rule:
    - if provider capability indicates continuity handle support and handle is expected, null/empty/malformed handle is `continuity_handle_invalid`.
    - if provider capability indicates no continuity handle support, missing handle is normal and does not emit a failure reason code.
  - `thinking_persistence_key` remains optimization state and never authoritative correctness state for resume.
- Invariants:
  - No `EXECUTE` step without valid required upstream `PLAN_STEP` artifact(s).
  - No prompted step execution without valid `prompt_ref` (`prompt_id`, `prompt_version`) resolved from `WorkflowStepDefinition`.
  - Chain continuity handle is optional optimization, never authoritative source of truth.
  - `CognitiveChain` interrupted runs restart from first `PLAN_STEP`.
  - `ConversationalChain` interrupted runs resume from last successful persisted step.

## Run and Turn State Contract (Plan 2)
- Run state enum:
  - `created`
  - `running`
  - `waiting_retry`
  - `succeeded`
  - `failed_terminal`
  - `cancelled`
- Turn state enum:
  - `pending`
  - `running`
  - `succeeded`
  - `failed_retryable`
  - `failed_terminal`
  - `skipped`
- Transition contract:
  - explicit legal transitions are documented in:
    - `Plans/InProgress/mvp-chain-runner-local-persistence-baseline/steps/0020-define-chain-runner-domain-and-state-model/artifacts/run-turn-state-machine.md`
  - any undocumented state/event transition is illegal and fails with `invalid_state_transition`.
- Resume cursor authority:
  - `workflow_key`
  - `workflow_version`
  - `chain_mode`
  - `current_step_index`
  - `last_success_step_index`
  - `next_turn_index`
  - `next_checkpoint_sequence`
  - `run_state`
  - optional `latest_thinking_persistence_key`
- Counter reservation rules:
  - turn reservation order (`start_turn`):
    - read `run.json`,
    - assign `turn_index = next_turn_index`,
    - increment `next_turn_index`,
    - atomically persist updated `run.json`,
    - then create/write turn artifact files (`turn.json`, snapshots, provenance, validation).
  - checkpoint reservation order (`write_checkpoint`):
    - read `run.json`,
    - assign `checkpoint_sequence = next_checkpoint_sequence`,
    - increment `next_checkpoint_sequence`,
    - atomically persist updated `run.json`,
    - then write `checkpoint-<checkpoint_sequence>.json`.
  - reserved indices are never reused, including crash/partial-write cases; gaps are allowed and auditable.
  - `current_step_index` advances on successful/explicitly skipped step transition and does not advance on retry of the same step.

## Cognitive vs Conversational Resume Policy
- `CognitiveChain`:
  - Restart from first `PLAN_STEP` on resume.
  - Rationale: provider-side thinking cache is not guaranteed to persist, and `PLAN_STEP` chat output alone is insufficient to deterministically reconstruct provider-side thinking state.
  - Previous cognitive artifacts are still persisted for audit/analysis, but are not treated as enough to continue mid-chain.
- `ConversationalChain`:
  - Resume from last successful persisted step/turn.
  - Rationale: conversational turns are fully persisted to local storage and can be replayed/reconstructed deterministically for continuation.

## Minimal Workflow Abstraction (Plan 2)
- Plan 2 includes a minimal workflow abstraction to support future routing/branching without full workflow-engine complexity.
- Baseline abstraction shape:
  - `WorkflowDefinition`:
    - `workflow_key`
    - `workflow_kind` (`CognitiveChain` or `ConversationalChain`)
    - `workflow_version` (integer)
    - ordered `WorkflowStepDefinition[]`
  - `WorkflowStepDefinition`:
    - `step_key`
    - `step_kind` (`PLAN_STEP`, `EXECUTE`, or `CONVERSATION_STEP`)
    - required `prompt_ref`:
      - `prompt_id`
      - `prompt_version`
    - `input_contract_ref`
    - `output_contract_ref`
    - optional `route_hook_key`
  - `WorkflowRunContext`:
    - `run_id`
    - `workflow_key`
    - `workflow_version`
    - `chain_mode`
    - `run_state`
    - `current_step_index`
    - `last_success_step_index`
    - `next_turn_index`
    - `next_checkpoint_sequence`
    - optional `latest_thinking_persistence_key`
- Key semantics:
  - `workflow_kind` is enum-only (not free-form).
  - `workflow_key` is stable semantic identity and must not embed version.
  - `workflow_version` is the only version marker and is integer.
  - `step_key` is workflow-step identity and must not be conflated with `prompt_id`.
  - `prompt_ref` is prompt-template identity reference owned by Prompting contracts and is required for Plan 2 step execution.
  - Plan 2 intentionally keeps step execution prompt-driven; non-prompt step types are future scope.
  - `step_key` safe regex: `^[a-z0-9][a-z0-9-]{0,63}$`.
  - `route_hook_key` regex: `^[a-z0-9][a-z0-9-]{0,56}$` so generated step keys remain path-safe and satisfy `step_key` regex.
  - `chain_mode` in `WorkflowRunContext` must match `workflow_kind` in `WorkflowDefinition`.
  - `workflow_kind`/`chain_mode` mismatch is deterministic terminal failure `invalid_state_transition`.
- Routing hook policy:
  - Plan 2 supports minimal route-hook seams (for example issue/risk based step routing) without implementing full branching orchestration.
  - Advanced branching and graph execution remain future scope.
- Variable-length execution mechanism:
  - runner uses a linear run-local execution queue initialized from `WorkflowDefinition` steps.
  - route hooks may append additional steps to the queue tail only (no mid-queue insertion or reordering in Plan 2).
  - appended-step key generation is deterministic:
    - `generated_step_key = <route_hook_key>-r<NNNN>` (1-based, zero-padded ordinal per `route_hook_key` within a run).
  - effective queue expansion must be durably persisted in `run.json` before execution continues, so resume never re-derives route-hook expansions.
  - `current_step_index` always points into this effective queue, preserving deterministic replay/resume behavior.
- Step-to-prompt binding rule (Option A):
  - binding lives directly in `WorkflowStepDefinition.prompt_ref`.
  - application orchestration resolves `prompt_ref` per step and invokes Prompting render with step/context placeholders.
  - missing/invalid `prompt_ref` on a required step is deterministic terminal failure `missing_prompt_reference`.

## Local Persistence Contract (Plan 2)
- Local persistence storage is workspace-local file/path-backed.
- Storage model baseline:
```text
artifacts/workflow-runs/<run-id>/
  run.json
  checkpoints/
    checkpoint-<sequence>.json
  turns/
    <turn-index>-<step-key>/
      turn.json
      raw-response.json
      normalized-response.json
      provenance.json
      validation.json
      accepted-output.json
  failures/
    failure-<turn-index>.json
```
- Required persistence behaviors:
  - deterministic write/read for required fields,
  - explicit failure when required fields are missing or corrupted,
  - no silent defaults for required contract fields.
  - deterministic path naming with objective text excluded from folder names.
  - path portability across Windows/Linux naming rules using safe `step_key` tokens.
  - run-counter updates use atomic write/replace semantics for `run.json` before dependent artifact writes.
  - reserved turn/checkpoint indices are not reused after interruption or partial artifact writes.
  - minimum atomic write strategy:
    - write temp file in same directory,
    - flush/fsync temp content,
    - atomic rename/replace target file.
- Canonical turn metadata behavior:
  - `turn.json` is the authoritative turn metadata record.
  - required turn identity fields (`step_key`, `step_index`, `turn_index`, objective, timestamps) must originate from `turn.json`.
  - other artifacts may reference turn metadata, but `turn.json` is the single source of truth.
- Failure record sequencing behavior:
  - failure artifact filename is `failure-<turn-index>.json`.
  - `turn_index` is the deterministic anchor for failure records in Plan 2.
  - one failure artifact exists per failed turn attempt.
- Provenance behavior:
  - provenance artifact is strict superset shape of Prompting required fields.
  - mapper from artifact -> Prompting provenance required fields is deterministic and lossless.
- Explicitly excluded in Plan 2:
  - Postgres tables/migrations,
  - external cache/queue/service-backed persistence.

## Reason Code Baseline (Plan 2)
- Required baseline codes:
  - `missing_prompt_reference`
  - `missing_required_placeholder`
  - `schema_validation_failed`
  - `provider_protocol_error`
  - `output_starvation`
  - `continuity_handle_invalid`
  - `cognitive_restart_required`
  - `retry_budget_exhausted`
  - `checkpoint_write_failed`
  - `checkpoint_read_failed`
  - `artifact_write_failed`
  - `artifact_read_failed`
  - `invalid_state_transition`
- Policy:
  - each deterministic failure outcome emits exactly one reason code,
  - additional detail goes to diagnostics payload (not extra reason codes),
  - reason codes must be persisted in provenance/failure artifacts,
  - no ad-hoc aliasing of baseline codes.

## Project Dependency and Implementation Detail Matrix
| Project | Plan 2 Role | Allowed References | Forbidden References | Implementation Detail |
|---|---|---|---|---|
| `Source/Zelanthus.StoryEngine.Domain` | Runtime state and invariants | `none (BCL only)` | `Infrastructure`, `API`, `Gemini` | Must remain provider/storage agnostic |
| `Source/Zelanthus.StoryEngine.Application` | Runner orchestration and policy | `StoryEngine.Domain`, `Zelanthus.Prompting`, `Zelanthus.Llm.Clients.Abstractions` | `Gemini`, storage-specific IO details | Owns semantic transforms, chain routing, retry/resume policy |
| `Source/Zelanthus.StoryEngine.Infrastructure` | Local persistence and mappers | `StoryEngine.Domain`, `StoryEngine.Application`, `Zelanthus.Prompting`, `Zelanthus.Llm.Clients.Abstractions` | `API` controllers/endpoint behavior | Owns storage-to-runtime mapping, checkpoint/artifact stores |
| `Source/Zelanthus.Llm.Clients.Gemini` | Provider protocol adapter | `Zelanthus.Llm.Clients.Abstractions` | `StoryEngine.*` | Continues Plan 1 normalized envelope responsibilities |
| `Source/Zelanthus.API` | Composition root only | `StoryEngine.Application`, `StoryEngine.Infrastructure`, `Gemini` | domain rule logic and persistence transforms | Plan 2 integration boundary only; endpoint breadth out of scope |
| `Tests/Zelanthus.Architecture.Tests` | Dependency gate enforcement | project-under-test refs only | production coupling bypasses | Must fail build/test on forbidden dependency direction |
| `Tests/Zelanthus.WorkflowContractProofs.Tests` | Proof/evidence production | runner/app contracts and fixture harness | brittle provider-prose assertions | Must produce deterministic evidence artifacts for golden/failure/resume paths |

## Git Branch and PR Tracking
- Execution Branch: `feature/thin-clients-first-solution-structure`
- Base Branch: `main`
- PR: `pending (user-owned manual PR)`

## Touchpoint Map
- Code:
  - `Zelanthus.slnx`
  - `Source/Zelanthus.Prompting` (contract consumer only)
  - `Source/Zelanthus.Llm.Clients.Abstractions` (contract consumer only)
  - `Source/Zelanthus.Llm.Clients.Gemini` (provider adapter reused by runner)
  - `Source/Zelanthus.StoryEngine.Domain`
  - `Source/Zelanthus.StoryEngine.Application`
  - `Source/Zelanthus.StoryEngine.Infrastructure`
  - `Source/Zelanthus.API` (composition/integration boundary only)
  - `Tests/Zelanthus.Architecture.Tests`
  - `Tests/Zelanthus.WorkflowContractProofs.Tests`
- Docs:
  - `Plans/InProgress/mvp-chain-runner-local-persistence-baseline/plan.md`
  - `Plans/InProgress/mvp-chain-runner-local-persistence-baseline/steps/index.md`
  - `Plans/InProgress/mvp-chain-runner-local-persistence-baseline/decisions/`
  - `Plans/InProgress/mvp-chain-runner-local-persistence-baseline/risks/risk-log.md`
  - `Plans/InProgress/mvp-chain-runner-local-persistence-baseline/artifacts/project-dependency-implementation-matrix.md`
  - `Plans/InProgress/mvp-chain-runner-local-persistence-baseline/validation/plan-2-acceptance-evidence-matrix.md`
- Infrastructure/Config:
  - local file/path-backed runner persistence root configuration
  - no external database/cache/queue/service storage in Plan 2

## Risks and Mitigations
- Risk: Runner implementation drifts back into fixed single-pair assumptions instead of `PLAN_STEP/EXECUTE` staged flows.
  - Mitigation: lock `PLAN_STEP/EXECUTE` terminology and staged-flow acceptance tests.
- Risk: Persistence model drops required runtime fields.
  - Mitigation: enforce deterministic mapper tests and explicit failure on missing required fields.
- Risk: Dependency direction violations appear as new StoryEngine projects are added.
  - Mitigation: architecture tests are planned as an early gate before broad implementation.
- Risk: Resume behavior implicitly depends on provider continuity handles.
  - Mitigation: enforce mode-specific resume policy; cognitive resume restarts from chain start and conversational resume continues from last success.
- Risk: Workflow abstraction is either too generic (over-engineered) or too narrow (blocks future workflows).
  - Mitigation: lock a minimal linear workflow abstraction with route-hook seams only; defer advanced branching engine features.
- Risk: Plan 2 scope expands into Postgres/distributed runtime concerns.
  - Mitigation: keep explicit non-goals and reject external persistence/service changes in this plan.
- Risk: Artifact path format is non-deterministic or objective-dependent.
  - Mitigation: lock `<turn-index>-<step-key>` folder naming and add deterministic path proof tests as promotion gates.

## Validation and Testing
- Automated:
  - `dotnet build Zelanthus.slnx`
  - `dotnet test --filter "Category!=LiveGemini"`
  - live-provider opt-in path:
    - set `ZELANTHUS_RUN_LIVE_GEMINI_TESTS=1`
    - run `dotnet test --filter "Category=LiveGemini"`
- Manual:
  - Verify project reference graph matches allowed dependency direction.
  - Verify runner design is variable-length for both chain modes and uses `PLAN_STEP/EXECUTE` semantics for cognitive flows.
  - Verify cognitive resume policy restarts from chain start and does not continue mid-chain.
  - Verify conversational resume policy continues from last successful persisted step.
  - Verify minimal workflow abstraction supports future extension hooks without adding full graph-engine scope now.
  - Verify each executed step uses explicit `WorkflowStepDefinition.prompt_ref` (`prompt_id`, `prompt_version`) and does not infer prompt identity from `step_key`.
  - Verify `workflow_kind`/`chain_mode` mismatch is rejected deterministically with `invalid_state_transition`.
  - Verify `route_hook_key` values satisfy pinned regex and generated step keys remain path-safe.
  - Verify local persistence definition is workspace-local only and excludes external services.
  - Verify artifact folder naming is deterministic and uses `<turn-index>-<step-key>` only.
  - Verify `turn.json` is authoritative for turn identity/objective metadata.
  - Verify provenance artifact mapping is strict superset -> Prompting required fields.
  - Verify deterministic failure outcomes emit exactly one reason code with separate diagnostics payload.
  - Verify timestamp assertions in proof tests are presence/format based unless deterministic clock injection is explicitly enabled.
  - Verify variable-length routing behavior uses deterministic queue-tail append semantics.
  - Verify route-hook generated step keys are deterministic and persisted in `run.json` effective queue state.
  - Verify crash/partial-write simulation does not reuse reserved `turn_index`/`checkpoint_sequence` values.
  - Verify resume/retry behavior is fully reason-coded and deterministic.
  - Verify proof-evidence artifacts cover golden/failure/resume flows.

## Rollout / Rollback
- Rollout:
  - execute steps in order on `feature/thin-clients-first-solution-structure` with evidence captured per step.
  - signal PR-ready handoff to the user after completion package and cleanup commit are prepared.
- Rollback:
  - pause execution, document blocker state, and re-baseline through Draft revision if Plan 2 boundaries materially change.

## Status Tracker
- [ ] `0010-lock-plan-2-scope-and-contract-dependencies`
- [ ] `0020-define-chain-runner-domain-and-state-model`
- [ ] `0025-define-minimal-workflow-abstraction-hooks`
- [ ] `0030-define-application-orchestration-flow-and-chain-router`
- [ ] `0040-define-local-persistence-artifact-and-checkpoint-layout`
- [ ] `0050-define-infrastructure-mappers-and-store-contracts`
- [ ] `0060-define-retry-resume-and-reason-code-policy`
- [ ] `0070-plan-project-reference-graph-and-architecture-tests`
- [ ] `0080-define-runner-proof-test-suite-and-evidence-artifacts`
- [ ] `0090-define-api-composition-boundaries-and-host-integration`
- [ ] `0100-finalize-plan-2-acceptance-gates-and-promotion-readiness`

## Notes
- Workspace baseline is `.NET 10` (`net10.0`) for all planned project additions.
- Plan 2 remains backend/runtime focused and intentionally avoids frontend/chat product scope.
- Plan 2 local persistence is a contract-first bridge to future Postgres, not an implicit production persistence design.
- Reference inputs informing this draft (non-authoritative, concept only):
  - `References/cognition/src/Cognition.Workflows`
  - `References/cognition/src/Cognition.Domains`
  - `References/bookforge/resources/plans/lint_repair_split_routing_plan_20260213_183000.md`

---

## Source 2: `decisions/0001-chain-runner-execution-model-v0.md`

# Decision 0001: Chain Runner Execution Model v0

## Status
- accepted

## Decision Summary
- Plan 2 runner must support variable-length execution for both:
  - `CognitiveChain`
  - `ConversationalChain`
- `PLAN_STEP` -> `EXECUTE` remains a minimum cognitive unit, not a hard cap on chain length.
- Runner correctness is artifact-first: explicit persisted state drives resume and validation.

## Contract Shape
- Chain run contract includes:
  - run identity and correlation IDs,
  - workflow identity (`workflow_key`, `workflow_version`),
  - chain mode,
  - ordered turn metadata,
  - current state and checkpoint references.
- Turn contract includes:
  - turn objective,
  - required inputs/artifacts,
  - output artifact references,
  - validation/failure status.
- Workflow step kinds include:
  - `PLAN_STEP`
  - `EXECUTE`
  - `CONVERSATION_STEP`

## State Enums
- `RunState`:
  - `created`
  - `running`
  - `waiting_retry`
  - `succeeded`
  - `failed_terminal`
  - `cancelled`
- `TurnState`:
  - `pending`
  - `running`
  - `succeeded`
  - `failed_retryable`
  - `failed_terminal`
  - `skipped`

## Transition Table (Plan 2 Minimum)
| Current `RunState` | Event | Next `RunState` |
|---|---|---|
| `created` | `start_run` | `running` |
| `running` | `turn_failed_retryable` | `waiting_retry` |
| `waiting_retry` | `retry_started` | `running` |
| `running` | `run_completed` | `succeeded` |
| `running` | `run_failed_terminal` | `failed_terminal` |
| `waiting_retry` | `retry_budget_exhausted` | `failed_terminal` |
| `created` / `running` / `waiting_retry` | `cancel_requested` | `cancelled` |

| Current `TurnState` | Event | Next `TurnState` |
|---|---|---|
| `pending` | `start_turn` | `running` |
| `running` | `turn_completed` | `succeeded` |
| `running` | `turn_failed_retryable` | `failed_retryable` |
| `running` | `turn_failed_terminal` | `failed_terminal` |
| `pending` | `skip_turn` | `skipped` |

## Illegal Transitions
- Any state/event pair not listed in the transition table is illegal.
- Illegal transition outcome is deterministic terminal failure with reason code `invalid_state_transition`.

## Resume Cursor Contract
- Authoritative resume cursor fields:
  - `workflow_key`
  - `workflow_version`
  - `chain_mode`
  - `current_step_index`
  - `last_success_step_index`
  - `next_turn_index`
  - `next_checkpoint_sequence`
  - `run_state`
  - optional `latest_thinking_persistence_key`
- Counter behavior:
  - `start_turn` reserves `turn_index` first:
    - assign `turn_index = next_turn_index`,
    - increment `next_turn_index`,
    - atomically persist `run.json`,
    - then write turn artifacts.
  - `write_checkpoint` reserves `checkpoint_sequence` first:
    - assign `checkpoint_sequence = next_checkpoint_sequence`,
    - increment `next_checkpoint_sequence`,
    - atomically persist `run.json`,
    - then write checkpoint artifact.
  - reserved indices are never reused after interruption/partial-write; gaps are allowed.
  - `current_step_index` advances on successful/skip transition and is stable during retries.
- Resume behavior:
  - `CognitiveChain` ignores mid-chain cursor for continuation and restarts from first `PLAN_STEP`.
  - `ConversationalChain` uses `last_success_step_index` cursor to continue from the next required step.

## Invariants
- `EXECUTE` cannot execute without required upstream planning artifact(s).
- any step selected for provider invocation must have valid `prompt_ref` (`prompt_id`, `prompt_version`).
- `chain_mode` must match `workflow_kind` for active workflow definition.
- `workflow_kind`/`chain_mode` mismatch is deterministic terminal failure `invalid_state_transition`.
- Cognitive mode may stage multiple `PLAN_STEP` units before one or more `EXECUTE` units.
- Cognitive mode carries forward the latest `thinking_persistence_key` returned by each provider response (rolling continuity handle semantics).
- Continuity handle may rotate on both `PLAN_STEP` and `EXECUTE` responses.
- Continuity capability-conditioned handling:
  - if capability indicates continuity support and handle is expected, null/empty/malformed handle is `continuity_handle_invalid`.
  - if capability indicates no continuity support, missing handle is normal and non-failing.
- Conversational turns still require per-turn validation and persistence.
- Chain routing may expand step count based on workflow need and failure/retry policy.
- Variable-length mechanism uses run-local linear execution queue:
  - initialize queue from `WorkflowDefinition`,
  - append additional steps to queue tail only when route-hook policy requests expansion,
  - route-hook appended steps use deterministic generated keys: `<route_hook_key>-r<NNNN>` (1-based ordinal per route hook per run),
  - `route_hook_key` must satisfy `^[a-z0-9][a-z0-9-]{0,56}$` so generated step keys satisfy step-key path safety,
  - persist expanded effective queue in `run.json` before executing appended steps,
  - no mid-queue insertion or reordering in Plan 2.
- Resume policy:
  - `CognitiveChain` restarts from first `PLAN_STEP`.
  - `ConversationalChain` resumes from last successful persisted step.

## Consequences
- Plan 2 implementation must avoid fixed single-pair assumptions and use `PLAN_STEP/EXECUTE` semantics explicitly.
- Proof tests must include at least one variable-length chain scenario.
- Proof tests must include crash/partial-write reservation scenarios that prove no turn/checkpoint index reuse.
- Proof tests must include missing/invalid step `prompt_ref` deterministic failure behavior.

---

## Source 3: `decisions/0002-local-persistence-contract-and-layout-v0.md`

# Decision 0002: Local Persistence Contract and Layout v0

## Status
- accepted

## Decision Summary
- Plan 2 uses workspace-local file/path-backed persistence for runner checkpoints and artifacts.
- Persistence contract is deterministic and mapper-driven; required fields cannot be silently dropped.
- External persistence services (Postgres/cache/queue/cloud storage) are explicitly out-of-scope.

## Required Stored Artifacts
- Run-level record (`run.json`)
- Checkpoint records (`checkpoints/checkpoint-<sequence>.json`)
- Turn-level artifacts:
  - canonical turn metadata record (`turn.json`)
  - raw response snapshot
  - normalized response
  - provenance
  - validation
  - accepted output (when present)
- Failure records (`failures/failure-<turn-index>.json`)

## Mapping Guarantees
- Infrastructure mapping must preserve required fields from runtime contracts:
  - prompt identity/version/checksum
  - provider/model identity
  - chain mode/turn index/turn objective
  - workflow/step/turn counters (`current_step_index`, `last_success_step_index`, `next_turn_index`, `next_checkpoint_sequence`)
  - reason codes and validation outcomes
- Missing required stored fields must produce explicit mapping failure.
- Provenance artifact shape is a strict superset of `Zelanthus.Prompting` provenance required fields.
- Mapping from storage artifact -> `Zelanthus.Prompting` provenance contract must be deterministic and lossless for required fields.

## Path Determinism and Portability
- Turn artifact path format is fixed to:
  - `turns/<turn-index>-<step-key>/`
- Objective text is not part of folder names and must be stored in canonical turn metadata (`turn.json`) and related artifact content.
- `turn.json` is canonical source of turn identity/objective/timestamp metadata.
- `step_key` must be platform-safe and deterministic:
  - lowercase letters, digits, hyphen only,
  - regex: `^[a-z0-9][a-z0-9-]{0,63}$`
- Any non-deterministic artifact path generation is a promotion blocker for Plan 2.

## Counter Reservation Durability Rule
- `run.json` is the authoritative counter store for `next_turn_index` and `next_checkpoint_sequence`.
- Reservation order is required:
  - reserve index by incrementing the corresponding `next_*` counter in `run.json`,
  - atomically persist updated `run.json`,
  - then write dependent turn/checkpoint artifacts.
- Minimum atomic write strategy for `run.json`, checkpoints, and turn artifacts:
  - write temp file in same directory,
  - flush/fsync temp content,
  - atomic rename/replace target file.
- Interrupted writes may create index gaps; index reuse is forbidden.

## Failure Record Anchor Rule
- Failure records are keyed by `turn_index` (`failure-<turn-index>.json`) for deterministic traceability.
- Plan 2 stores one failure artifact per failed turn attempt.

## Consequences
- Infrastructure planning and tests must include round-trip and failure-path mapping coverage.
- Local persistence shape must remain explicit and reviewable as a future Postgres migration input.

---

## Source 4: `decisions/0003-project-reference-graph-and-boundary-enforcement-v0.md`

# Decision 0003: Project Reference Graph and Boundary Enforcement v0

## Status
- accepted

## Decision Summary
- Plan 2 project reference updates must preserve one-way dependency direction and transformation ownership.
- Architecture tests are required as an early implementation gate.
- Workflow abstraction and route-hook logic remain owned by `StoryEngine.Application`/`StoryEngine.Domain`, not infrastructure or API.

## Allowed Reference Direction (Plan 2 target)
- `Zelanthus.StoryEngine.Domain` -> no runtime project dependencies
- `Zelanthus.StoryEngine.Application` -> `Zelanthus.StoryEngine.Domain`, `Zelanthus.Prompting`, `Zelanthus.Llm.Clients.Abstractions`
- `Zelanthus.StoryEngine.Infrastructure` -> `Zelanthus.StoryEngine.Application`, `Zelanthus.StoryEngine.Domain`, `Zelanthus.Prompting`, `Zelanthus.Llm.Clients.Abstractions`
- `Zelanthus.API` -> composition root references only (`StoryEngine.Application`, `StoryEngine.Infrastructure`, provider adapter)
- `Zelanthus.Llm.Clients.Gemini` -> `Zelanthus.Llm.Clients.Abstractions`

## Forbidden References
- `StoryEngine.Domain/Application` -> provider implementation projects (for example `Gemini`)
- `StoryEngine.Domain` -> `StoryEngine.Infrastructure` or `API`
- `Prompting` -> `StoryEngine.*` or `API`
- Provider implementations -> `StoryEngine.Domain/Application`

## Consequences
- Plan 2 must include architecture test assertions before broad implementation spread.
- Any needed boundary exception requires explicit decision update, not ad-hoc reference changes.

---

## Source 5: `decisions/0004-retry-resume-and-reason-code-policy-v0.md`

# Decision 0004: Retry, Resume, and Reason-Code Policy v0

## Status
- accepted

## Decision Summary
- Retry and resume behavior is explicit, deterministic, and reason-coded.
- Continuity handles are optional optimization and never authoritative resume state.
- Plan 2 pins baseline reason codes for orchestration and persistence failures.

## Retry/Resume Rules
- Retry current turn only when required upstream artifacts/checkpoints remain valid.
- Restart from planning step when validation indicates stale/insufficient plan artifact.
- `CognitiveChain` resume behavior:
  - always restart from first `PLAN_STEP`,
  - even if prior run had a `thinking_persistence_key` (provider-side thinking cache is not guaranteed).
- `ConversationalChain` resume behavior:
  - resume from last successful persisted step/turn.
- Continuity handle update behavior:
  - advance `latest_thinking_persistence_key` only after successful provider response parse.
  - if turn fails before response parse, do not advance continuity key.
  - capability-conditioned continuity failures:
    - if capability indicates continuity support and handle is expected, null/empty/malformed handle is deterministic failure `continuity_handle_invalid`.
    - if capability indicates no continuity support, missing handle is expected and not a failure.

## Pinned Reason-Code Baseline
- `missing_prompt_reference`
- `missing_required_placeholder`
- `schema_validation_failed`
- `provider_protocol_error`
- `output_starvation`
- `continuity_handle_invalid`
- `cognitive_restart_required`
- `retry_budget_exhausted`
- `checkpoint_write_failed`
- `checkpoint_read_failed`
- `artifact_write_failed`
- `artifact_read_failed`
- `invalid_state_transition`

## Policy Guarantees
- Every deterministic failure outcome emits exactly one reason code.
- Additional failure detail is captured in structured diagnostics payload, not additional reason codes.
- Reason codes are persisted with provenance/failure artifacts.
- Code strings are exact lowercase snake_case values (no aliases).
- Cognitive resume attempts that require restart emit `cognitive_restart_required`.
- Capability-supported null/empty provider continuity handle in cognitive flow emits `continuity_handle_invalid`.
- Continuity-handle failure emission requires capability-supported/expected handle context.
- Capability-unsupported continuity mode proceeds without emitting `continuity_handle_invalid`.

## Consequences
- Plan 2 proof tests must validate reason-code emission and persistence across failure paths.
- Application and infrastructure boundaries must propagate reason codes without mutation.
- Deterministic failure mapping coverage includes:
  - missing step `prompt_ref` failures,
  - workflow-kind/chain-mode mismatch failures,
  - prompt render failures,
  - provider protocol failures,
  - checkpoint/artifact read-write failures,
  - illegal state transition failures.

---

## Source 6: `decisions/0005-minimal-workflow-abstraction-v0.md`

# Decision 0005: Minimal Workflow Abstraction v0

## Status
- accepted

## Decision Summary
- Plan 2 introduces a minimal workflow abstraction that is:
  - workflow-aware,
  - linear-sequence first,
  - route-hook capable,
  - not a full meta workflow-engine.
- Abstraction supports current two chain modes and future expansion via data/code without immediate graph-engine complexity.

## Minimal Contract Shape
- `WorkflowDefinition`
  - `workflow_key`
  - `workflow_kind` (`CognitiveChain` or `ConversationalChain`)
  - `workflow_version` (integer)
  - ordered `WorkflowStepDefinition[]`
- `WorkflowStepDefinition`
  - `step_key`
  - `step_kind` (`PLAN_STEP`, `EXECUTE`, `CONVERSATION_STEP`)
  - required `prompt_ref`:
    - `prompt_id`
    - `prompt_version`
  - `input_contract_ref`
  - `output_contract_ref`
  - optional `route_hook_key`
- `WorkflowRunContext`
  - `run_id`
  - `workflow_key`
  - `workflow_version`
  - `chain_mode`
  - `run_state`
  - `current_step_index`
  - `last_success_step_index`
  - `next_turn_index`
  - `next_checkpoint_sequence`
  - optional `latest_thinking_persistence_key`

## Key Semantics
- `workflow_kind` is enum-only and must not be treated as free-form text.
- `workflow_key` is a stable semantic identifier and must not embed version.
- `workflow_version` is the only workflow version marker and is an integer.
- `step_key` identifies workflow step identity and must not be reused as prompt identity.
- `prompt_ref` is required for Plan 2 step execution and directly carries prompt identity/version in each step.
- `prompt_id` remains prompt-template identity owned by `Zelanthus.Prompting`.
- `step_key` path-safe rule: `^[a-z0-9][a-z0-9-]{0,63}$`.
- `route_hook_key` path-safe rule: `^[a-z0-9][a-z0-9-]{0,56}$` so generated step keys remain valid (`<route_hook_key>-r<NNNN>`).
- missing/invalid `prompt_ref` for required step execution is deterministic terminal failure `missing_prompt_reference`.
- `chain_mode` must match `workflow_kind` for the active run definition.
- `workflow_kind`/`chain_mode` mismatch is deterministic terminal failure `invalid_state_transition`.

## Cognitive Continuity Tracking Rule
- `latest_thinking_persistence_key` stores the most recently returned provider continuity handle (for example `thoughtSignature`).
- Provider handles are rolling values and may change on each step.
- Cognitive requests pass forward the latest persisted handle when provider capability supports it.
- If provider capability indicates continuity unsupported, missing handle is non-failure and execution proceeds without continuity state.

## Route Hook Policy
- Route hooks are extension seams only for MVP:
  - simple deterministic routing decisions,
  - no full branching graph orchestration in Plan 2.
- Route-hook expansion behavior in Plan 2:
  - route hooks may append additional steps to run-local execution queue tail only,
  - generated step keys for appended steps are deterministic: `<route_hook_key>-r<NNNN>` (1-based ordinal per route hook per run),
  - appended steps must include required `prompt_ref` before they are persisted/executed,
  - expanded effective queue (including generated step keys) must be persisted in `run.json` before executing appended steps,
  - no insertion/reordering of already-materialized queue steps.
- Future plans may expand route hooks into richer branching/topology support.

## Guardrails
- Avoid generic "system to model systems" abstractions in Plan 2.
- Keep workflow abstraction focused on enabling:
  - current chain runner correctness,
  - deterministic persistence/replay,
  - future workflow extensibility without refactoring core boundaries.

## Consequences
- Plan 2 orchestration contracts must reference workflow definitions and step definitions explicitly.
- Proof tests should include at least one route-hook aware scenario (without full graph branching).

---

## Source 7: `risks/risk-log.md`

# Risk Log

## R-001 PLAN_STEP/EXECUTE Lock-In Risk
- Statement: Runner implementation implicitly assumes a single `PLAN_STEP` -> `EXECUTE` pair and blocks required staged or repeated cognitive cycles.
- Impact: Rework in orchestration logic and failure to support planned chain semantics.
- Mitigation: Lock variable-length `PLAN_STEP/EXECUTE` requirements and include explicit staged-flow acceptance tests.
- Status: open

## R-002 Local Persistence Drift Risk
- Statement: Local persistence layout diverges from required runtime contracts and cannot support deterministic resume.
- Impact: Resume/retry instability and evidence artifacts that cannot be trusted.
- Mitigation: Define required fields/mapping invariants and enforce with persistence contract tests.
- Status: open

## R-003 Resume Correctness Risk
- Statement: Resume logic depends on hidden provider continuity state instead of explicit artifacts/checkpoints.
- Impact: Non-deterministic recovery and correctness regressions across retries/restarts.
- Mitigation: Require resume from persisted artifact state alone; continuity handle is optional optimization only.
- Status: open

## R-004 Dependency Direction Drift Risk
- Statement: New StoryEngine projects introduce forbidden references during scaffolding/refactoring.
- Impact: Architecture debt and boundary violations.
- Mitigation: Add architecture tests as early gate and pin allowed/forbidden reference matrix in this plan.
- Status: open

## R-005 Reason-Code Inconsistency Risk
- Statement: Retry and terminal failure paths emit inconsistent reason codes across layers.
- Impact: Low-quality diagnostics and unreliable failure routing.
- Mitigation: Pin baseline reason-code set and require deterministic persistence in provenance/failure artifacts.
- Status: open

## R-006 Scope Creep Risk
- Statement: Plan 2 work expands into Postgres/distributed-runtime concerns before runner/persistence baseline is proven.
- Impact: Slower delivery and reduced confidence in the MVP spine.
- Mitigation: Keep external persistence and distributed runtime explicitly out-of-scope in Plan 2.
- Status: open

## R-007 Workflow Abstraction Balance Risk
- Statement: Workflow abstraction is either too generic (over-engineered) or too narrow (blocks future workflow routing/branching).
- Impact: Implementation churn in runner orchestration and future plan rework.
- Mitigation: Keep a minimal linear abstraction with explicit route-hook seams; defer full branching engine work.
- Status: open

## R-008 Artifact Path Determinism Risk
- Statement: Artifact folder naming depends on variable objective text or non-deterministic rules and becomes unstable across runs/platforms.
- Impact: Evidence mismatch, path collisions, and cross-platform portability defects.
- Mitigation: Lock turn folder naming to `<turn-index>-<step-key>`, enforce path-safe `step_key` regex, and gate promotion on deterministic-path proof.
- Status: open

## R-009 Continuity Capability Ambiguity Risk
- Statement: Continuity-handle failure behavior is applied without checking provider capability support.
- Impact: False failure routing (`continuity_handle_invalid`) and incorrect retry/restart behavior.
- Mitigation: Condition continuity-handle failures on capability-supported/expected context; treat unsupported capability as non-failure path.
- Status: open

## R-010 Turn Metadata Authority Drift Risk
- Statement: Turn identity/objective fields diverge across artifacts because no canonical turn metadata source is enforced.
- Impact: Replay/debug ambiguity and inconsistent mapping behavior.
- Mitigation: Enforce `turn.json` as canonical turn metadata record and validate references from other artifacts.
- Status: open

## R-011 Counter Reservation Reuse Risk
- Statement: Turn/checkpoint indices are allocated without crash-safe reservation ordering and can be reused after interruption.
- Impact: Artifact collisions, overwritten evidence, and non-deterministic resume behavior.
- Mitigation: Reserve `next_turn_index`/`next_checkpoint_sequence` in `run.json` first, persist atomically, then write dependent artifacts; allow gaps and forbid reuse.
- Status: open

## R-012 Route-Hook Step Identity Drift Risk
- Statement: Route-hook appended steps use non-deterministic keys or are not durably persisted in effective queue state.
- Impact: Retry/resume replays diverge from original execution order and auditability degrades.
- Mitigation: Use deterministic generated step keys (`<route_hook_key>-r<NNNN>`) and persist expanded queue in `run.json` before executing appended steps.
- Status: open

## R-013 Step-to-Prompt Binding Drift Risk
- Statement: Workflow step execution infers prompt identity implicitly (for example from `step_key`) instead of using explicit per-step `prompt_ref`.
- Impact: Non-deterministic prompt selection, provenance drift, and hard-to-debug retry/resume behavior.
- Mitigation: Require `WorkflowStepDefinition.prompt_ref` (`prompt_id`, `prompt_version`) for Plan 2 execution; treat missing/invalid references as deterministic `missing_prompt_reference` failure.
- Status: open

---

## Source 8: `validation/plan-2-acceptance-evidence-matrix.md`

# Plan 2 Acceptance Evidence Matrix

## Scope
- Plan: `mvp-chain-runner-local-persistence-baseline`
- Purpose: map acceptance points to required evidence before promotion to `InProgress`.

## Acceptance Map
| Acceptance Point | Required Evidence Type | Planned Location |
|---|---|---|
| Variable-length `CognitiveChain` support with `PLAN_STEP/EXECUTE` semantics | proof test + run artifact set | `steps/0080-define-runner-proof-test-suite-and-evidence-artifacts/artifacts/` |
| Variable-length `ConversationalChain` support | proof test + run artifact set | `steps/0080-define-runner-proof-test-suite-and-evidence-artifacts/artifacts/` |
| Deterministic local checkpoint persistence | mapper/store tests + checkpoint artifacts | `steps/0040-define-local-persistence-artifact-and-checkpoint-layout/artifacts/` |
| Cognitive resume restarts from first `PLAN_STEP` | failure/resume proof tests + reason-coded artifacts | `steps/0060-define-retry-resume-and-reason-code-policy/artifacts/` |
| Conversational resume continues from last successful step | failure/resume proof tests + checkpoint artifacts | `steps/0080-define-runner-proof-test-suite-and-evidence-artifacts/artifacts/` |
| Cognitive staged flow persists and forwards latest `thinking_persistence_key` (`thoughtSignature`) each step | proof test + run metadata artifacts | `steps/0080-define-runner-proof-test-suite-and-evidence-artifacts/artifacts/` |
| Capability-supported null/empty cognitive continuity handle is deterministic failure (`continuity_handle_invalid`) | failure-path proof test + reason-coded artifact | `steps/0060-define-retry-resume-and-reason-code-policy/artifacts/` |
| Continuity capability unsupported path does not emit failure code | capability-profile proof test + run/failure artifact absence check | `steps/0080-define-runner-proof-test-suite-and-evidence-artifacts/artifacts/` |
| Pinned reason-code emission | proof tests + provenance/failure artifacts | `steps/0060-define-retry-resume-and-reason-code-policy/artifacts/` |
| Deterministic failure maps to exactly one reason code | proof tests + failure diagnostics artifact | `steps/0060-define-retry-resume-and-reason-code-policy/artifacts/` |
| Required step `prompt_ref` missing/invalid emits deterministic terminal failure (`missing_prompt_reference`) | proof test + reason-coded failure artifact | `steps/0060-define-retry-resume-and-reason-code-policy/artifacts/` |
| Illegal run/turn transition emits `invalid_state_transition` | state-machine proof test + failure artifact | `steps/0020-define-chain-runner-domain-and-state-model/artifacts/` |
| Artifact paths are deterministic and use `<turn-index>-<step-key>` only | proof test + run artifact path evidence | `steps/0040-define-local-persistence-artifact-and-checkpoint-layout/artifacts/` |
| Canonical turn metadata is authored in `turn.json` | artifact contract + proof test | `steps/0040-define-local-persistence-artifact-and-checkpoint-layout/artifacts/` |
| Turn/checkpoint counters reserve deterministically and never reuse indices after interruption | counter proof tests + run/checkpoint artifacts | `steps/0020-define-chain-runner-domain-and-state-model/artifacts/` |
| Failure artifacts are deterministically anchored by `turn_index` (`failure-<turn-index>.json`) | failure-path proof tests + artifact path evidence | `steps/0040-define-local-persistence-artifact-and-checkpoint-layout/artifacts/` |
| Route-hook appended steps use deterministic generated keys and persisted effective queue state | route-hook proof tests + run metadata evidence | `steps/0025-define-minimal-workflow-abstraction-hooks/artifacts/` |
| `route_hook_key` grammar is pinned and generated step keys remain path-safe | contract artifact + route-hook proof tests | `steps/0025-define-minimal-workflow-abstraction-hooks/artifacts/` |
| `workflow_kind` and `chain_mode` alignment is enforced with deterministic mismatch failure | state/runner proof test + failure artifact | `steps/0020-define-chain-runner-domain-and-state-model/artifacts/` |
| Timestamp assertions are presence/format based by default | evidence-spec rule + proof test behavior note | `steps/0080-define-runner-proof-test-suite-and-evidence-artifacts/artifacts/` |
| Provenance artifact is strict superset with deterministic Prompting mapping | mapper/proof tests + mapping artifact evidence | `steps/0050-define-infrastructure-mappers-and-store-contracts/artifacts/` |
| Minimal workflow abstraction hooks exist (`WorkflowDefinition`/`WorkflowStepDefinition` with required `prompt_ref`/route hooks) | decision + contract artifact | `steps/0025-define-minimal-workflow-abstraction-hooks/artifacts/` |
| Project dependency boundary integrity | architecture assertions matrix + test pass evidence | `steps/0070-plan-project-reference-graph-and-architecture-tests/artifacts/` |
| Final promotion readiness | checklist showing DoD coverage | `steps/0100-finalize-plan-2-acceptance-gates-and-promotion-readiness/artifacts/` |

## Promotion Gate Rule
- Plan 2 cannot move to `InProgress` until each acceptance point has explicit mapped evidence path and acceptance assertion.

---

## Source 9: `steps/index.md`

# Steps Index

Use this file for dependency and status summaries.
Do not use this file as the only source of step order.
Step order must still be visible in folder names (`NNNN-semantic-step-name`).

## Step Map
| Order | Step Folder | Status | Depends On | Notes |
|---|---|---|---|---|
| 0010 | `0010-lock-plan-2-scope-and-contract-dependencies` | `pending` | `none` | Lock Plan 2 scope, cross-plan dependencies, and dependency implementation matrix |
| 0020 | `0020-define-chain-runner-domain-and-state-model` | `pending` | `0010-lock-plan-2-scope-and-contract-dependencies` | Define run/turn state contracts and domain-level invariants for chain execution |
| 0025 | `0025-define-minimal-workflow-abstraction-hooks` | `pending` | `0010-lock-plan-2-scope-and-contract-dependencies`, `0020-define-chain-runner-domain-and-state-model` | Define minimal workflow/step abstraction and route-hook seams for future expansion |
| 0030 | `0030-define-application-orchestration-flow-and-chain-router` | `pending` | `0010-lock-plan-2-scope-and-contract-dependencies`, `0020-define-chain-runner-domain-and-state-model`, `0025-define-minimal-workflow-abstraction-hooks` | Define chain routing, step sequencing, and orchestration policy contracts |
| 0040 | `0040-define-local-persistence-artifact-and-checkpoint-layout` | `pending` | `0010-lock-plan-2-scope-and-contract-dependencies`, `0020-define-chain-runner-domain-and-state-model` | Define local file/path-backed run, turn, checkpoint, and failure artifact layout |
| 0050 | `0050-define-infrastructure-mappers-and-store-contracts` | `pending` | `0020-define-chain-runner-domain-and-state-model`, `0040-define-local-persistence-artifact-and-checkpoint-layout` | Define storage mapper and store interfaces with deterministic mapping guarantees |
| 0060 | `0060-define-retry-resume-and-reason-code-policy` | `pending` | `0030-define-application-orchestration-flow-and-chain-router`, `0040-define-local-persistence-artifact-and-checkpoint-layout`, `0050-define-infrastructure-mappers-and-store-contracts` | Define retry/resume semantics, pinned reason codes, and continuity fallback policy |
| 0070 | `0070-plan-project-reference-graph-and-architecture-tests` | `pending` | `0010-lock-plan-2-scope-and-contract-dependencies`, `0030-define-application-orchestration-flow-and-chain-router`, `0050-define-infrastructure-mappers-and-store-contracts` | Define project reference updates and architecture test assertions as early implementation gate |
| 0080 | `0080-define-runner-proof-test-suite-and-evidence-artifacts` | `pending` | `0030-define-application-orchestration-flow-and-chain-router`, `0040-define-local-persistence-artifact-and-checkpoint-layout`, `0060-define-retry-resume-and-reason-code-policy`, `0070-plan-project-reference-graph-and-architecture-tests` | Define deterministic proof test set and required evidence artifacts for runner acceptance |
| 0090 | `0090-define-api-composition-boundaries-and-host-integration` | `pending` | `0030-define-application-orchestration-flow-and-chain-router`, `0070-plan-project-reference-graph-and-architecture-tests` | Define API composition responsibilities and explicit non-goals for endpoint scope |
| 0100 | `0100-finalize-plan-2-acceptance-gates-and-promotion-readiness` | `pending` | `0020-define-chain-runner-domain-and-state-model`, `0025-define-minimal-workflow-abstraction-hooks`, `0030-define-application-orchestration-flow-and-chain-router`, `0040-define-local-persistence-artifact-and-checkpoint-layout`, `0050-define-infrastructure-mappers-and-store-contracts`, `0060-define-retry-resume-and-reason-code-policy`, `0070-plan-project-reference-graph-and-architecture-tests`, `0080-define-runner-proof-test-suite-and-evidence-artifacts`, `0090-define-api-composition-boundaries-and-host-integration` | Finalize Plan 2 acceptance matrix and promotion checklist |

## Status Values
- `pending`
- `in_progress`
- `blocked`
- `completed`

---

## Source 10: `steps/0010-lock-plan-2-scope-and-contract-dependencies/step.md`

# Step: 0010-lock-plan-2-scope-and-contract-dependencies

## Goal
- Lock Plan 2 scope boundaries, cross-plan dependencies, and project dependency implementation matrix before drafting lower-level contracts.

## Context
- Plan 2 touches multiple project surfaces and can drift quickly without explicit dependency/ownership constraints.
- Plan 1 contracts are prerequisites and must be treated as fixed upstream inputs.

## Git Branch
- `feature/thin-clients-first-solution-structure`

## Commits
- `none`

## Commands Executed
- `none`

## Files Changed
- `none`

## Outputs
- `Plans/InProgress/mvp-chain-runner-local-persistence-baseline/plan.md`
  - scope, non-goals, and cross-plan dependencies are explicit.
- `Plans/InProgress/mvp-chain-runner-local-persistence-baseline/artifacts/project-dependency-implementation-matrix.md`
  - allowed/forbidden project references are explicit.
- `Plans/InProgress/mvp-chain-runner-local-persistence-baseline/validation/plan-2-acceptance-evidence-matrix.md`
  - acceptance points and evidence mapping are explicit.

## Tests / Results
- `not-run` -> pending execution

## Acceptance Evidence
- Dependency matrix includes all Plan 2 project touchpoints and direction rules.
- Cross-plan dependency list includes Plan 1 and thin-clients/backbone decision inputs.
- Plan 2 non-goals explicitly exclude Postgres/distributed runtime concerns.

## Issues
- none

## Decision
- pending

## Completion
- `pending`

## Next Actions
- Define runner domain/state contracts and turn model details.

---

## Source 11: `steps/0020-define-chain-runner-domain-and-state-model/step.md`

# Step: 0020-define-chain-runner-domain-and-state-model

## Goal
- Define domain-level contracts for chain run lifecycle, turn state, and invariants needed by both chain modes.

## Context
- Application orchestration and persistence mapping cannot be specified safely until run/turn state contracts are explicit.

## Git Branch
- `feature/thin-clients-first-solution-structure`

## Commits
- `none`

## Commands Executed
- `none`

## Files Changed
- `none`

## Outputs
- `Plans/InProgress/mvp-chain-runner-local-persistence-baseline/decisions/0001-chain-runner-execution-model-v0.md`
  - chain mode model, run/turn contract shape, and invariants are explicit.
- `Plans/InProgress/mvp-chain-runner-local-persistence-baseline/steps/0020-define-chain-runner-domain-and-state-model/artifacts/run-turn-state-machine.md`
  - explicit `RunState`/`TurnState` enums, transition table, illegal transition handling, and resume cursor contract.
- `Plans/InProgress/mvp-chain-runner-local-persistence-baseline/plan.md`
  - runner execution contract and invariants are explicit.

## Tests / Results
- `not-run` -> pending execution

## Acceptance Evidence
- Domain contract includes explicit run identity, chain mode, turn metadata, and run state transitions.
- State machine table explicitly defines legal run/turn transitions and illegal transition behavior (`invalid_state_transition`).
- Resume cursor contract defines authoritative continuation fields for each chain mode.
- Counter reservation rules are explicit for `next_turn_index`/`next_checkpoint_sequence` with persisted `turn_index`/`checkpoint_sequence` fields.
- Crash/partial-write behavior is explicit: reserved indices are never reused and gaps are allowed.
- Continuity-handle failure semantics are capability-conditioned (supported vs unsupported).
- Runner model explicitly supports variable-length execution for both `CognitiveChain` and `ConversationalChain`.
- Route-hook queue expansion rules include deterministic generated step keys and persisted effective queue state.
- `workflow_kind`/`chain_mode` mismatch behavior is explicit and deterministic (`invalid_state_transition`).
- Contract forbids blind `EXECUTE` execution without required upstream planning artifact state.
- Contract distinguishes cognitive restart-from-start resume behavior vs conversational resume-from-last-success behavior.

## Issues
- none

## Decision
- pending

## Completion
- `pending`

## Next Actions
- Define application orchestration and chain routing flow on top of this state model.

---

## Source 12: `steps/0025-define-minimal-workflow-abstraction-hooks/step.md`

# Step: 0025-define-minimal-workflow-abstraction-hooks

## Goal
- Define the minimal workflow abstraction required for Plan 2 so runner logic is workflow-aware and future-extensible without over-engineering.

## Context
- Prior attempts showed risk in tackling broad workflow systems too early.
- Plan 2 needs only enough abstraction to support current chain runner correctness plus future route/branch expansion seams.

## Git Branch
- `feature/thin-clients-first-solution-structure`

## Commits
- `none`

## Commands Executed
- `none`

## Files Changed
- `none`

## Outputs
- `Plans/InProgress/mvp-chain-runner-local-persistence-baseline/decisions/0005-minimal-workflow-abstraction-v0.md`
  - minimal workflow contracts and route-hook policy are explicit.
- `Plans/InProgress/mvp-chain-runner-local-persistence-baseline/steps/0025-define-minimal-workflow-abstraction-hooks/artifacts/minimal-workflow-abstraction-contract.md`
  - concrete contract sketch and guardrails for implementation.
- `Plans/InProgress/mvp-chain-runner-local-persistence-baseline/plan.md`
  - minimal workflow abstraction section is explicit.

## Tests / Results
- `not-run` -> pending execution

## Acceptance Evidence
- Workflow abstraction includes explicit `WorkflowDefinition`, `WorkflowStepDefinition`, and `WorkflowRunContext`.
- Step kinds include `PLAN_STEP`, `EXECUTE`, and `CONVERSATION_STEP`.
- Key semantics are explicit:
  - `workflow_key` stable identity,
  - `workflow_version` integer marker,
  - `step_key` distinct from `prompt_id`,
  - required per-step `prompt_ref` (`prompt_id`, `prompt_version`).
- Workflow run counters (`next_turn_index`, `next_checkpoint_sequence`) are included for deterministic replay/resume.
- Route-hook support is explicitly limited to MVP-safe extension seams (no full branching engine in Plan 2).
- Route-hook appended step keys are deterministic and persisted with effective queue state in `run.json` before execution continues.
- `route_hook_key` grammar is explicit so generated step keys always satisfy `step_key` path-safety constraints.
- `workflow_kind`/`chain_mode` alignment invariant is explicit with deterministic failure behavior for mismatch.

## Issues
- none

## Decision
- pending

## Completion
- `pending`

## Next Actions
- Apply workflow abstraction to application orchestration and chain router planning.

---

## Source 13: `steps/0030-define-application-orchestration-flow-and-chain-router/step.md`

# Step: 0030-define-application-orchestration-flow-and-chain-router

## Goal
- Define application-layer runner orchestration flow and chain router behavior for cognitive and conversational execution.

## Context
- Application layer owns semantic transform, chain-mode choice, and retry/resume policy entry points.
- Provider adapter and infrastructure should not absorb orchestration semantics.

## Git Branch
- `feature/thin-clients-first-solution-structure`

## Commits
- `none`

## Commands Executed
- `none`

## Files Changed
- `none`

## Outputs
- `Plans/InProgress/mvp-chain-runner-local-persistence-baseline/plan.md`
  - application orchestration ownership and runner execution contract are explicit.
- `Plans/InProgress/mvp-chain-runner-local-persistence-baseline/decisions/0003-project-reference-graph-and-boundary-enforcement-v0.md`
  - orchestration boundary ownership is aligned with project references.
- `Plans/InProgress/mvp-chain-runner-local-persistence-baseline/decisions/0005-minimal-workflow-abstraction-v0.md`
  - workflow/step abstraction is integrated into orchestration flow planning.

## Tests / Results
- `not-run` -> pending execution

## Acceptance Evidence
- Orchestration flow explicitly distinguishes:
  - chain-mode selection,
  - workflow step objective assembly (`PLAN_STEP`, `EXECUTE`, `CONVERSATION_STEP`),
  - step-level `prompt_ref` resolution (`prompt_id`, `prompt_version`),
  - `ILlmClient` invocation path,
  - post-call validation and persistence dispatch.
- Flow supports variable step counts and does not encode a fixed single-pair loop.
- Chain routing contract includes explicit upgrade/downgrade conditions between single-turn and chain execution.
- Flow supports staged `PLAN_STEP` sequences followed by one or more `EXECUTE` steps in cognitive mode.
- Variable-length expansion mechanism is explicit: route hooks append steps to run-local queue tail only.
- Route-hook appended step identity is deterministic (`<route_hook_key>-r<NNNN>`) and persisted in effective queue state before appended-step execution.
- Missing/invalid step `prompt_ref` is deterministic terminal failure (`missing_prompt_reference`) before provider invocation.
- `workflow_kind`/`chain_mode` mismatch handling is deterministic and explicitly reason-coded.

## Issues
- none

## Decision
- pending

## Completion
- `pending`

## Next Actions
- Define local persistence layout and mapping contracts for checkpoint/artifact durability.

---

## Source 14: `steps/0040-define-local-persistence-artifact-and-checkpoint-layout/step.md`

# Step: 0040-define-local-persistence-artifact-and-checkpoint-layout

## Goal
- Define Plan 2 local persistence layout and required artifact/checkpoint records for deterministic resume/replay.

## Context
- Runner behavior must persist enough explicit state to resume without hidden provider continuity state.
- Layout decisions now reduce future migration risk when Postgres is introduced later.

## Git Branch
- `feature/thin-clients-first-solution-structure`

## Commits
- `none`

## Commands Executed
- `none`

## Files Changed
- `none`

## Outputs
- `Plans/InProgress/mvp-chain-runner-local-persistence-baseline/decisions/0002-local-persistence-contract-and-layout-v0.md`
  - local persistence model and required stored artifacts are explicit.
- `Plans/InProgress/mvp-chain-runner-local-persistence-baseline/steps/0040-define-local-persistence-artifact-and-checkpoint-layout/artifacts/local-persistence-layout.md`
  - concrete run/checkpoint/turn/failure file layout and required fields.
- `Plans/InProgress/mvp-chain-runner-local-persistence-baseline/plan.md`
  - local persistence contract section is explicit.

## Tests / Results
- `not-run` -> pending execution

## Acceptance Evidence
- Layout includes explicit run-level, turn-level, checkpoint, and failure records.
- Required fields for checkpoint/resume are listed and mapped to runtime contract fields.
- Turn artifact folder contract is deterministic: `<turn-index>-<step-key>` only.
- Path safety rules and platform-agnostic naming constraints for `step_key` are explicit.
- `turn.json` is explicitly declared as canonical turn metadata record.
- Counter reservation contract (`next_turn_index`, `next_checkpoint_sequence`) and persisted index fields (`turn_index`, `checkpoint_sequence`) are explicit.
- Crash-safe reservation order is explicit: reserve counter in `run.json`, persist atomically, then write dependent artifacts.
- Minimum atomic write strategy is explicit (`temp in same directory -> flush/fsync -> atomic replace`).
- Failure artifact naming anchor is explicit: `failure-<turn-index>.json`.
- Plan explicitly excludes external database/cache/queue/service persistence in Plan 2.

## Issues
- none

## Decision
- pending

## Completion
- `pending`

## Next Actions
- Define mapper/store contracts and deterministic mapping failure rules.

---

## Source 15: `steps/0050-define-infrastructure-mappers-and-store-contracts/step.md`

# Step: 0050-define-infrastructure-mappers-and-store-contracts

## Goal
- Define infrastructure mapper/store contract boundaries for writing and materializing runner artifacts/checkpoints.

## Context
- Infrastructure owns storage transforms, not semantic chain decisions.
- Deterministic mapping guarantees are required to keep runner resume behavior auditable.

## Git Branch
- `feature/thin-clients-first-solution-structure`

## Commits
- `none`

## Commands Executed
- `none`

## Files Changed
- `none`

## Outputs
- `Plans/InProgress/mvp-chain-runner-local-persistence-baseline/decisions/0002-local-persistence-contract-and-layout-v0.md`
  - mapping guarantees and failure semantics are explicit.
- `Plans/InProgress/mvp-chain-runner-local-persistence-baseline/steps/0050-define-infrastructure-mappers-and-store-contracts/artifacts/provenance-mapping-contract.md`
  - strict-superset provenance mapping requirements and deterministic failure behavior.
- `Plans/InProgress/mvp-chain-runner-local-persistence-baseline/plan.md`
  - local persistence contract and mapping expectations are explicit.

## Tests / Results
- `not-run` -> pending execution

## Acceptance Evidence
- Store interfaces are explicit for:
  - run records,
  - checkpoint records,
  - turn artifact records,
  - failure records.
- Mapping contract includes round-trip preservation for required fields.
- Provenance artifact mapping is explicit as strict superset -> Prompting provenance required fields (deterministic and lossless for required fields).
- Provenance mapping contract preserves canonical turn identity from `turn.json` (no conflicting identity fields).
- Missing required stored fields are defined as explicit mapping failures (never silently defaulted).

## Issues
- none

## Decision
- pending

## Completion
- `pending`

## Next Actions
- Define retry/resume reason-code policy and persistence expectations.

---

## Source 16: `steps/0060-define-retry-resume-and-reason-code-policy/step.md`

# Step: 0060-define-retry-resume-and-reason-code-policy

## Goal
- Define deterministic retry/resume semantics with pinned reason codes across runner, validation, and persistence failure paths.

## Context
- Plan 2 requires reliable recovery and explicit failure diagnostics.
- Reason-code inconsistency quickly erodes replay/debug quality.

## Git Branch
- `feature/thin-clients-first-solution-structure`

## Commits
- `none`

## Commands Executed
- `none`

## Files Changed
- `none`

## Outputs
- `Plans/InProgress/mvp-chain-runner-local-persistence-baseline/decisions/0004-retry-resume-and-reason-code-policy-v0.md`
  - pinned reason codes and retry/resume rules are explicit.
- `Plans/InProgress/mvp-chain-runner-local-persistence-baseline/steps/0060-define-retry-resume-and-reason-code-policy/artifacts/reason-code-and-retry-policy.md`
  - scenario-level policy mapping for retry, restart, and terminal failure outcomes.
- `Plans/InProgress/mvp-chain-runner-local-persistence-baseline/plan.md`
  - Plan 2 reason-code baseline section is explicit.

## Tests / Results
- `not-run` -> pending execution

## Acceptance Evidence
- Baseline reason-code list is explicit and exact (snake_case).
- Baseline reason-code list includes deterministic missing step `prompt_ref` failure (`missing_prompt_reference`).
- Retry/resume policy identifies when to:
  - retry current turn,
  - restart from planning step,
  - fail terminally.
- Policy explicitly treats continuity handle as optional optimization and not resume authority.
- Policy explicitly differentiates:
  - cognitive resume -> restart from first `PLAN_STEP`,
  - conversational resume -> continue from last successful persisted step.
- Reason-code baseline includes `cognitive_restart_required` for cognitive restart semantics.
- Deterministic failure handling rule is explicit: one failure outcome maps to exactly one reason code.
- Continuity-handle failure behavior is capability-conditioned:
  - supported+expected handle missing/invalid -> `continuity_handle_invalid`,
  - unsupported capability -> non-failure path.

## Issues
- none

## Decision
- pending

## Completion
- `pending`

## Next Actions
- Define architecture test assertions and project reference graph updates.

---

## Source 17: `steps/0070-plan-project-reference-graph-and-architecture-tests/step.md`

# Step: 0070-plan-project-reference-graph-and-architecture-tests

## Goal
- Define project reference updates and architecture-test assertions that enforce Plan 2 dependency boundaries.

## Context
- Plan 2 introduces StoryEngine projects and cross-project references that can drift without an explicit gate.

## Git Branch
- `feature/thin-clients-first-solution-structure`

## Commits
- `none`

## Commands Executed
- `none`

## Files Changed
- `none`

## Outputs
- `Plans/InProgress/mvp-chain-runner-local-persistence-baseline/decisions/0003-project-reference-graph-and-boundary-enforcement-v0.md`
  - allowed/forbidden references and enforcement policy are explicit.
- `Plans/InProgress/mvp-chain-runner-local-persistence-baseline/steps/0070-plan-project-reference-graph-and-architecture-tests/artifacts/architecture-test-assertion-matrix.md`
  - required architecture assertions for Domain/Application/Infrastructure/API/provider boundaries.
- `Plans/InProgress/mvp-chain-runner-local-persistence-baseline/artifacts/project-dependency-implementation-matrix.md`
  - implementation boundary matrix is aligned with architecture assertions.

## Tests / Results
- `not-run` -> pending execution

## Acceptance Evidence
- Architecture assertion matrix covers:
  - domain has no infra/provider/API dependencies,
  - application has no provider implementation dependency,
  - prompting has no story engine/API dependencies,
  - provider implementation has no story engine domain/application dependencies.
- Step output maps each assertion to planned test project coverage.

## Issues
- none

## Decision
- pending

## Completion
- `pending`

## Next Actions
- Define runner proof tests and evidence artifact requirements.

---

## Source 18: `steps/0080-define-runner-proof-test-suite-and-evidence-artifacts/step.md`

# Step: 0080-define-runner-proof-test-suite-and-evidence-artifacts

## Goal
- Define deterministic Plan 2 runner proof tests and required evidence artifacts for golden/failure/resume coverage.

## Context
- Plan 2 promotion requires explicit evidence contracts, not only high-level test intent.
- Existing Plan 1 proof harness is the baseline and must be extended without losing deterministic behavior.

## Git Branch
- `feature/thin-clients-first-solution-structure`

## Commits
- `none`

## Commands Executed
- `none`

## Files Changed
- `none`

## Outputs
- `Plans/InProgress/mvp-chain-runner-local-persistence-baseline/steps/0080-define-runner-proof-test-suite-and-evidence-artifacts/artifacts/runner-proof-test-seed.md`
  - exact planned test names for runner proofs.
- `Plans/InProgress/mvp-chain-runner-local-persistence-baseline/steps/0080-define-runner-proof-test-suite-and-evidence-artifacts/artifacts/runner-proof-evidence-spec.md`
  - required evidence artifact names and required fields.
- `Plans/InProgress/mvp-chain-runner-local-persistence-baseline/validation/plan-2-acceptance-evidence-matrix.md`
  - acceptance points mapped to test/evidence requirements.

## Tests / Results
- `not-run` -> pending execution

## Acceptance Evidence
- Proof test list covers:
  - variable-length cognitive flow with staged `PLAN_STEP`/`EXECUTE`,
  - variable-length conversational flow,
  - capability-conditioned continuity-handle behavior,
  - reason-coded failure flow,
  - cognitive restart-from-start resume flow,
  - conversational resume-from-last-success flow,
  - illegal state transition failure flow.
- Evidence spec includes artifact requirements for:
  - run metadata,
  - checkpoint persistence,
  - reason-coded failure,
  - resume result correctness.
- Evidence spec includes workflow metadata and rolling `thinking_persistence_key` propagation checks for active cognitive runs.
- Evidence spec includes capability-conditioned continuity-handle behavior with non-advancing latest-key assertion on supported+expected invalid-handle paths.
- Evidence spec includes deterministic artifact path checks using `<turn-index>-<step-key>` naming only.
- Evidence spec includes canonical `turn.json` checks, deterministic counter reservation assertions, and crash/no-reuse assertions.
- Evidence spec includes deterministic failure artifact naming checks (`failure-<turn-index>.json`).
- Evidence spec includes deterministic route-hook generated step-key and persisted effective-queue checks.
- Evidence spec includes required per-step `prompt_ref` capture and deterministic `missing_prompt_reference` failure proof.
- Evidence spec includes `workflow_kind`/`chain_mode` alignment checks and deterministic mismatch failure proof.
- Evidence spec includes strict provenance superset mapping checks to Prompting required fields.
- Evidence spec includes timestamp determinism rules (presence/format assertions, no cross-run raw timestamp equality by default).
- Default deterministic test execution path excludes live-provider dependency.

## Issues
- none

## Decision
- pending

## Completion
- `pending`

## Next Actions
- Define API composition boundary integration rules and non-goals.

---

## Source 19: `steps/0090-define-api-composition-boundaries-and-host-integration/step.md`

# Step: 0090-define-api-composition-boundaries-and-host-integration

## Goal
- Define Plan 2 API composition responsibilities and explicit non-goals for endpoint/host breadth.

## Context
- API layer should remain a composition boundary and must not absorb orchestration or persistence transform semantics.

## Git Branch
- `feature/thin-clients-first-solution-structure`

## Commits
- `none`

## Commands Executed
- `none`

## Files Changed
- `none`

## Outputs
- `Plans/InProgress/mvp-chain-runner-local-persistence-baseline/plan.md`
  - API composition boundary and non-goals are explicit in scope/touchpoint sections.
- `Plans/InProgress/mvp-chain-runner-local-persistence-baseline/decisions/0003-project-reference-graph-and-boundary-enforcement-v0.md`
  - API reference rules are explicit.

## Tests / Results
- `not-run` -> pending execution

## Acceptance Evidence
- API touchpoint scope is constrained to composition/integration boundaries.
- Endpoint feature breadth is explicitly excluded from Plan 2 scope.
- Dependency direction keeps API as top-level composition root only.

## Issues
- none

## Decision
- pending

## Completion
- `pending`

## Next Actions
- Finalize Plan 2 acceptance checklist and promotion readiness package.

---

## Source 20: `steps/0100-finalize-plan-2-acceptance-gates-and-promotion-readiness/step.md`

# Step: 0100-finalize-plan-2-acceptance-gates-and-promotion-readiness

## Goal
- Finalize Plan 2 promotion-readiness package with explicit acceptance gates, evidence mapping, and dependency implementation detail completeness.

## Context
- Plan 2 moves to execution only when scope, contracts, risk controls, and evidence requirements are all explicit and reviewable in one package.

## Git Branch
- `feature/thin-clients-first-solution-structure`

## Commits
- `none`

## Commands Executed
- `none`

## Files Changed
- `none`

## Outputs
- `Plans/InProgress/mvp-chain-runner-local-persistence-baseline/steps/0100-finalize-plan-2-acceptance-gates-and-promotion-readiness/artifacts/final-promotion-checklist.md`
  - final checklist for Plan 2 promotion review.
- `Plans/InProgress/mvp-chain-runner-local-persistence-baseline/validation/plan-2-acceptance-evidence-matrix.md`
  - acceptance matrix finalized.
- `Plans/InProgress/mvp-chain-runner-local-persistence-baseline/plan.md`
  - DoD and promotion-ready boundaries finalized.

## Tests / Results
- `not-run` -> pending execution

## Acceptance Evidence
- Promotion checklist covers:
  - scope/non-goal lock,
  - dependency reference matrix lock,
  - runner model lock,
  - local persistence contract lock,
  - reason-code policy lock,
  - evidence/test contract lock.
- Checklist explicitly blocks promotion for non-deterministic artifact path contracts.
- Checklist explicitly blocks promotion for missing canonical `turn.json` authority or missing counter-reservation contract.
- Checklist explicitly blocks promotion for missing crash-safe reservation/no-reuse rules and missing failure artifact naming anchor.
- Checklist explicitly blocks promotion when route-hook generated-step identity/persistence rules are incomplete.
- Checklist explicitly blocks promotion for missing required step `prompt_ref` contract and missing `missing_prompt_reference` evidence.
- Checklist explicitly blocks promotion for missing route-hook key grammar, workflow-kind/chain-mode invariant rules, and missing atomic write strategy notes.
- Checklist includes explicit stop-conditions if any required area is incomplete.

## Issues
- none

## Decision
- pending

## Completion
- `pending`

## Next Actions
- Request explicit user review/approval before Draft -> InProgress promotion.

---

## Source 21: `steps/0020-define-chain-runner-domain-and-state-model/artifacts/run-turn-state-machine.md`

# Run and Turn State Machine

## Scope
- Plan: `mvp-chain-runner-local-persistence-baseline`
- Step: `0020-define-chain-runner-domain-and-state-model`

## RunState
- `created`
- `running`
- `waiting_retry`
- `succeeded`
- `failed_terminal`
- `cancelled`

## TurnState
- `pending`
- `running`
- `succeeded`
- `failed_retryable`
- `failed_terminal`
- `skipped`

## Run Transitions
| Current State | Event | Next State | Notes |
|---|---|---|---|
| `created` | `start_run` | `running` | initialize first step cursor |
| `running` | `turn_failed_retryable` | `waiting_retry` | reason code persisted |
| `waiting_retry` | `retry_started` | `running` | retry budget must allow |
| `running` | `run_completed` | `succeeded` | all required steps complete |
| `running` | `run_failed_terminal` | `failed_terminal` | deterministic terminal failure |
| `waiting_retry` | `retry_budget_exhausted` | `failed_terminal` | emits `retry_budget_exhausted` |
| `created` / `running` / `waiting_retry` | `cancel_requested` | `cancelled` | explicit cancel path |

## Turn Transitions
| Current State | Event | Next State |
|---|---|---|
| `pending` | `start_turn` | `running` |
| `running` | `turn_completed` | `succeeded` |
| `running` | `turn_failed_retryable` | `failed_retryable` |
| `running` | `turn_failed_terminal` | `failed_terminal` |
| `pending` | `skip_turn` | `skipped` |

## Illegal Transition Rule
- Any state/event pair not listed in this document is illegal.
- Illegal transitions fail deterministically with reason code `invalid_state_transition`.

## Resume Cursor Contract
- Authoritative persisted fields:
  - `workflow_key`
  - `workflow_version`
  - `chain_mode`
  - `current_step_index`
  - `last_success_step_index`
  - `next_turn_index`
  - `next_checkpoint_sequence`
  - `run_state`
  - optional `latest_thinking_persistence_key`
- Mode behavior:
  - `CognitiveChain`: restart from first `PLAN_STEP` (do not continue mid-chain).
  - `ConversationalChain`: resume from `last_success_step_index` + 1.
  - `chain_mode` must match active workflow definition `workflow_kind`; mismatch is deterministic terminal failure `invalid_state_transition`.

## Counter Reservation Rules
- `start_turn` reserves `turn_index` by incrementing `next_turn_index` in `run.json` first, persisting atomically, then writing turn artifacts.
- `write_checkpoint` reserves `checkpoint_sequence` by incrementing `next_checkpoint_sequence` in `run.json` first, persisting atomically, then writing checkpoint artifact.
- persisted checkpoints record concrete `checkpoint_sequence` from pre-increment `next_checkpoint_sequence`.
- reserved indices are never reused after interruption/partial-write; gaps are allowed.
- minimum atomic write strategy:
  - write temp file in same directory,
  - flush/fsync temp content,
  - rename/replace target atomically.
- `current_step_index` increments on successful/skip step transition only.

## Variable-Length Mechanism
- Runner materializes a linear run-local execution queue from workflow definition.
- Route-hook expansion appends additional steps to queue tail only.
- `route_hook_key` must satisfy `^[a-z0-9][a-z0-9-]{0,56}$`.
- Route-hook appended step keys are deterministic: `<route_hook_key>-r<NNNN>` (1-based ordinal per route hook per run).
- Expanded effective queue is persisted in `run.json` before appended-step execution.
- Existing queue order is immutable in Plan 2 (no insertion/reordering).

## Continuity Handle Capability Rule
- If continuity handle capability is supported and expected, null/empty/malformed handle is `continuity_handle_invalid`.
- If continuity handle capability is unsupported, missing handle is non-failure.

---

## Source 22: `steps/0025-define-minimal-workflow-abstraction-hooks/artifacts/minimal-workflow-abstraction-contract.md`

# Minimal Workflow Abstraction Contract

## Scope
- Plan: `mvp-chain-runner-local-persistence-baseline`
- Step: `0025-define-minimal-workflow-abstraction-hooks`

## Contract Sketch
```text
WorkflowDefinition
  workflow_key
  workflow_kind (CognitiveChain | ConversationalChain)
  workflow_version (int)
  steps[] -> WorkflowStepDefinition

WorkflowStepDefinition
  step_key
  step_kind (PLAN_STEP | EXECUTE | CONVERSATION_STEP)
  prompt_ref
    prompt_id
    prompt_version
  input_contract_ref
  output_contract_ref
  route_hook_key? (optional)

WorkflowRunContext
  run_id
  workflow_key
  workflow_version
  chain_mode
  run_state
  current_step_index
  last_success_step_index
  next_turn_index
  next_checkpoint_sequence
  latest_thinking_persistence_key? (optional)
```

## Key Rules
- workflow_kind: enum only (`CognitiveChain` | `ConversationalChain`)
- workflow_key: stable semantic identity; must not include version
- workflow_version: integer version marker
- step_key: workflow-step identity, path-safe (`^[a-z0-9][a-z0-9-]{0,63}$`)
- route_hook_key: path-safe (`^[a-z0-9][a-z0-9-]{0,56}$`) so generated step key `<route_hook_key>-r<NNNN>` remains valid
- prompt_ref: required per step in Plan 2 (`prompt_id`, `prompt_version`)
- prompt_id is separate identity from step_key and remains owned by Prompting
- missing/invalid prompt_ref for required step execution is deterministic terminal failure `missing_prompt_reference`
- chain_mode must match workflow_kind for active workflow definition
- workflow_kind/chain_mode mismatch is deterministic terminal failure `invalid_state_transition`
- next_turn_index/next_checkpoint_sequence: monotonic run counters for deterministic persistence/replay

## Guardrails
- Keep workflow model linear-sequence first.
- Keep route hooks deterministic and minimal for MVP.
- Do not introduce graph-engine orchestration and generic DSL complexity in Plan 2.
- Keep abstraction depth intentionally below prior cognition meta-model scope while preserving clear route-hook seams.
- Track provider continuity handle as rolling step state (`latest_thinking_persistence_key`), not a fixed per-run constant.
- Route-hook expansions append run-local queue tail only; existing step order is immutable for Plan 2.
- Route-hook appended step keys are deterministic: `<route_hook_key>-r<NNNN>` (1-based ordinal per route hook per run).
- Route-hook appended steps include required prompt_ref before persistence/execution.
- Expanded effective queue state is persisted in `run.json` before appended steps execute.

## Future Hook Intent
- Supports future plans for richer routing/branching by extending:
  - `route_hook_key` behavior,
  - step transition policy,
  - optional conditional edges.

## Reference Inputs
- `References/cognition/src/Cognition.Workflows`
- `References/cognition/src/Cognition.Domains`
- `References/bookforge/resources/plans/lint_repair_split_routing_plan_20260213_183000.md`

---

## Source 23: `steps/0040-define-local-persistence-artifact-and-checkpoint-layout/artifacts/local-persistence-layout.md`

# Local Persistence Layout

## Scope
- Plan: `mvp-chain-runner-local-persistence-baseline`
- Step: `0040-define-local-persistence-artifact-and-checkpoint-layout`

## Canonical Layout (Plan 2)
```text
artifacts/workflow-runs/<run-id>/
  run.json
  checkpoints/
    checkpoint-<sequence>.json
  turns/
    <turn-index>-<step-key>/
      turn.json
      raw-response.json
      normalized-response.json
      provenance.json
      validation.json
      accepted-output.json
  failures/
    failure-<turn-index>.json
```

## Required Field Categories
- Run record:
  - run identity
  - workflow identity/version
  - chain mode
  - `next_turn_index`
  - `next_checkpoint_sequence`
  - created/updated timestamps
  - current run state
- Checkpoint record:
  - checkpoint sequence
  - source turn reference
  - resume cursor metadata
  - reason codes (if checkpoint created during failure handling)
- Turn artifact set:
  - canonical turn metadata (`turn.json`)
  - raw snapshot reference/data
  - normalized output reference/data
  - provenance reference/data
  - validation result reference/data
  - objective text stored in `turn.json`/artifact content, not folder name

## Canonical Turn Metadata Contract (`turn.json`)
- `turn.json` is the authoritative turn record.
- Required fields:
  - `workflow_key`
  - `workflow_version`
  - `step_key`
  - `step_index`
  - `turn_index`
  - `attempt_index`
  - `objective`
  - `started_at_utc`
  - `completed_at_utc` (when available)
- Other artifacts reference `turn_index`/`step_key`; they do not redefine authoritative turn identity.

## Path Rules (Deterministic and Cross-Platform)
- Turn folder naming is fixed: `<turn-index>-<step-key>`.
- `step_key` allowed characters: lowercase letters, digits, hyphen.
- `step_key` regex: `^[a-z0-9][a-z0-9-]{0,63}$`.
- Objective text must not be used in path segments.
- Path generation must be deterministic across repeated runs with identical run metadata.

## Rules
- Required fields cannot be silently defaulted.
- Missing/corrupt required fields produce explicit mapping/read failures.
- Layout is local file/path-backed only for Plan 2.
- Counter rules:
  - turn reservation (`start_turn`):
    - assign `turn_index = next_turn_index`,
    - increment `next_turn_index`,
    - atomically persist `run.json`,
    - then write turn artifact set.
  - checkpoint reservation (`write_checkpoint`):
    - assign `checkpoint_sequence = next_checkpoint_sequence`,
    - increment `next_checkpoint_sequence`,
    - atomically persist `run.json`,
    - then write checkpoint artifact.
  - reserved indices are never reused; interruption may create auditable gaps.
- Minimum atomic write strategy:
  - write temp file in same directory,
  - flush/fsync temp content,
  - rename/replace target atomically.
- Failure record rule:
  - failure artifact for a failed turn attempt is `failure-<turn-index>.json`.
- Resume policy alignment:
  - cognitive runs restart from first `PLAN_STEP`,
  - conversational runs resume from last successful persisted step.

---

## Source 24: `steps/0050-define-infrastructure-mappers-and-store-contracts/artifacts/provenance-mapping-contract.md`

# Provenance Mapping Contract

## Scope
- Plan: `mvp-chain-runner-local-persistence-baseline`
- Step: `0050-define-infrastructure-mappers-and-store-contracts`

## Contract Rule
- Persisted provenance artifact is a strict superset of `Zelanthus.Prompting` provenance required fields.
- Mapping artifact -> Prompting provenance contract must be deterministic and lossless for required fields.

## Required Prompting Fields (Minimum)
- prompt identity
- prompt version
- prompt checksum/fingerprint
- provider/model identity
- timing metadata
- token/usage metadata
- reason code (failure paths)

## Relationship to Canonical Turn Metadata
- `turn.json` is authoritative for turn identity (`turn_index`, `step_key`, objective).
- Provenance mapping may reference turn identity but must not override canonical values from `turn.json`.

## Failure Behavior
- Missing required provenance fields fail deterministically with one reason code:
  - `artifact_read_failed` (materialization path)
  - `artifact_write_failed` (persistence path)
- No silent defaulting for required provenance fields.

---

## Source 25: `steps/0060-define-retry-resume-and-reason-code-policy/artifacts/reason-code-and-retry-policy.md`

# Reason Code and Retry Policy

## Scope
- Plan: `mvp-chain-runner-local-persistence-baseline`
- Step: `0060-define-retry-resume-and-reason-code-policy`

## Pinned Reason Codes
- `missing_prompt_reference`
- `missing_required_placeholder`
- `schema_validation_failed`
- `provider_protocol_error`
- `output_starvation`
- `continuity_handle_invalid`
- `cognitive_restart_required`
- `retry_budget_exhausted`
- `checkpoint_write_failed`
- `checkpoint_read_failed`
- `artifact_write_failed`
- `artifact_read_failed`
- `invalid_state_transition`

## Policy Mapping
| Scenario | Action | Required Reason Code |
|---|---|---|
| Required step `prompt_ref` missing/invalid | terminal for turn | `missing_prompt_reference` |
| Required prompt placeholder missing | terminal for turn | `missing_required_placeholder` |
| Structured output schema mismatch | retry-or-terminal by budget/policy | `schema_validation_failed` |
| Provider protocol/transport error | retry-or-terminal by budget/policy | `provider_protocol_error` |
| Output starvation after planning | restart planning unit or terminal by budget | `output_starvation` |
| Continuity handle capability unsupported | continue without continuity handle | `none (non-failure path)` |
| Continuity handle capability supported + expected handle missing/empty/malformed | continue from artifacts or restart planning | `continuity_handle_invalid` |
| Cognitive chain resume requested after interruption | restart from first `PLAN_STEP` | `cognitive_restart_required` |
| Retry budget exhausted | terminal | `retry_budget_exhausted` |
| Checkpoint cannot be written | terminal | `checkpoint_write_failed` |
| Checkpoint cannot be read/materialized | terminal or forced replan path | `checkpoint_read_failed` |
| Non-checkpoint artifact cannot be written | terminal | `artifact_write_failed` |
| Non-checkpoint artifact cannot be read/materialized | terminal | `artifact_read_failed` |
| Illegal run/turn state transition attempted | terminal | `invalid_state_transition` |
| `workflow_kind`/`chain_mode` mismatch detected for active run definition | terminal | `invalid_state_transition` |

## Invariants
- Reason code values are exact lowercase snake_case.
- Deterministic failure outcome maps to exactly one reason code.
- Retry/terminal outcomes persist reason code in provenance/failure artifacts.
- Additional failure detail is stored in diagnostics payload, not extra reason codes.
- Continuity handle is never required for correctness.
- Continuity-handle failure codes apply only when capability-supported handle is expected.
- Cognitive resume semantics restart from chain start due non-guaranteed provider-side thinking cache.
- `latest_thinking_persistence_key` advances only on successful provider response parse.

---

## Source 26: `steps/0070-plan-project-reference-graph-and-architecture-tests/artifacts/architecture-test-assertion-matrix.md`

# Architecture Test Assertion Matrix

## Scope
- Plan: `mvp-chain-runner-local-persistence-baseline`
- Step: `0070-plan-project-reference-graph-and-architecture-tests`

## Required Assertions
| Assertion | Expected Result |
|---|---|
| `StoryEngine.Domain` does not reference `StoryEngine.Infrastructure` | pass |
| `StoryEngine.Domain` does not reference `Zelanthus.API` | pass |
| `StoryEngine.Domain` does not reference provider implementations | pass |
| `StoryEngine.Application` does not reference provider implementations | pass |
| `Zelanthus.Prompting` does not reference `StoryEngine.*` or `API` | pass |
| `Zelanthus.Llm.Clients.Gemini` does not reference `StoryEngine.Domain/Application` | pass |
| `Zelanthus.API` remains composition-root-only boundary | pass |

## Gate Policy
- Architecture assertions must run in `Tests/Zelanthus.Architecture.Tests`.
- Plan 2 execution cannot proceed past early scaffolding if these assertions fail.

---

## Source 27: `steps/0080-define-runner-proof-test-suite-and-evidence-artifacts/artifacts/runner-proof-evidence-spec.md`

# Runner Proof Evidence Spec

## Scope
- Plan: `mvp-chain-runner-local-persistence-baseline`
- Step: `0080-define-runner-proof-test-suite-and-evidence-artifacts`

## Required Evidence Artifacts
- `artifacts/workflow-runs/<run-id>/run.json`
- `artifacts/workflow-runs/<run-id>/checkpoints/checkpoint-<sequence>.json`
- `artifacts/workflow-runs/<run-id>/turns/<turn-index>-<step-key>/turn.json`
- `artifacts/workflow-runs/<run-id>/turns/<turn-index>-<step-key>/provenance.json`
- `artifacts/workflow-runs/<run-id>/turns/<turn-index>-<step-key>/validation.json`
- `artifacts/workflow-runs/<run-id>/failures/failure-<turn-index>.json` (failure scenarios)

## Required Artifact Assertions
- Run artifact captures chain mode and run state transitions.
- Run artifact captures workflow metadata (`workflow_key`, `workflow_version`, `workflow_kind`, `chain_mode`, `run_state`, `current_step_index`, `last_success_step_index`, `next_turn_index`, `next_checkpoint_sequence`).
- Effective queue metadata captures per-step `prompt_ref` (`prompt_id`, `prompt_version`) for executed/queued steps.
- Run/workflow metadata evidence proves `workflow_kind` and `chain_mode` alignment for valid runs.
- Checkpoint artifact captures deterministic resume cursor metadata and checkpoint counter sequence.
- `turn.json` is canonical turn metadata (`step_key`, `step_index`, `turn_index`, objective, timestamps).
- Effective queue persistence captures deterministic route-hook appends with generated step keys.
- Provenance artifact is strict superset shape with deterministic mapping to Prompting required fields.
- Failure artifact captures pinned reason codes.
- Missing/invalid step `prompt_ref` failure artifact captures `missing_prompt_reference`.
- Cognitive resume evidence shows restart from first `PLAN_STEP` with `cognitive_restart_required`.
- Conversational resume evidence shows continuation from last successful persisted step.
- Cognitive staged-plan evidence captures per-step `thinking_persistence_key` turnover and forward propagation of the latest value.
- Cognitive continuity evidence includes capability-conditioned paths:
  - supported+expected handle missing/invalid -> `continuity_handle_invalid`,
  - unsupported capability -> non-failure path.
- Cognitive continuity failure evidence shows `latest_thinking_persistence_key` is not advanced on pre-parse failure/null handle.
- Artifact path evidence proves deterministic folder naming uses `<turn-index>-<step-key>` only.
- State machine evidence proves illegal transition path emits `invalid_state_transition`.
- State mismatch evidence proves `workflow_kind`/`chain_mode` mismatch emits deterministic terminal `invalid_state_transition`.
- Counter evidence proves deterministic reservation behavior for `next_turn_index`/`next_checkpoint_sequence` and persisted `turn_index`/`checkpoint_sequence`.
- Crash-interruption evidence proves reserved indices are not reused after partial turn/checkpoint artifact writes.
- Failure artifact naming evidence proves deterministic `failure-<turn-index>.json` anchoring.

## Determinism Rules
- Deterministic fixture path is the default execution mode.
- Live-provider tests are opt-in only and cannot be required for baseline Plan 2 acceptance.
- Artifact folder generation must be deterministic for repeated runs with identical run metadata.
- Route-hook generated step keys and effective queue persistence must be deterministic across retry/resume.
- Route-hook key inputs must satisfy pinned grammar so generated step keys remain path-safe.
- Timestamp assertions use presence/format validation (for example ISO-8601 UTC) unless deterministic clock injection is explicitly enabled.
- Cross-run deterministic assertions should use stable identifiers/checksums and not raw timestamp equality.

---

## Source 28: `steps/0080-define-runner-proof-test-suite-and-evidence-artifacts/artifacts/runner-proof-test-seed.md`

# Runner Proof Test Seed

## Project
- `Tests/Zelanthus.WorkflowContractProofs.Tests`

## Naming Pattern
- `<ContractSurface>_<Scenario>_<ExpectedOutcome>`

## Seed Tests
- `ChainRunner_CognitiveChain_StagedPlanStepsThenExecute_PersistsArtifactsAndCheckpoints`
- `ChainRunner_CognitiveChain_StagedPlanExecuteCycles_PropagatesLatestThinkingPersistenceKeyPerStep`
- `ChainRunner_CognitiveChain_CapabilitySupported_NullOrEmptyThinkingPersistenceKey_DoesNotAdvanceKeyAndEmitsContinuityHandleInvalid`
- `ChainRunner_CognitiveChain_ContinuityCapabilityUnsupported_MissingHandleIsNonFailure`
- `ChainRunner_CognitiveChain_InterruptedRun_RestartsFromPlanStepWithCognitiveRestartRequiredReasonCode`
- `ChainRunner_ConversationalChain_MultiTurnFlow_PersistsPerTurnArtifacts`
- `ChainRunner_ConversationalChain_InterruptedRun_ResumesFromLastSuccessfulStep`
- `ChainRunner_StateMachine_IllegalTransition_EmitsInvalidStateTransitionReasonCode`
- `ChainRunner_VariableLengthQueue_RouteHookAppendsTailWithoutReordering`
- `ChainRunner_VariableLengthQueue_RouteHookAppendsDeterministicGeneratedStepKeys_AndPersistsEffectiveQueue`
- `ChainRunner_VariableLengthQueue_InvalidRouteHookKey_EmitsDeterministicValidationFailure`
- `ChainRunner_WorkflowStepPromptRef_MissingOrInvalid_EmitsMissingPromptReference`
- `ChainRunner_WorkflowKindChainModeMismatch_EmitsInvalidStateTransition`
- `ChainRunner_MissingRequiredPlaceholder_EmitsReasonCodeAndFailsDeterministically`
- `ChainRunner_ProviderProtocolError_EmitsReasonCodeAndAppliesRetryPolicy`
- `ChainRunner_CheckpointWriteFailure_EmitsCheckpointWriteFailedReasonCode`
- `ChainRunner_CheckpointReadFailure_EmitsCheckpointReadFailedReasonCode`
- `ChainRunner_ArtifactWriteFailure_EmitsArtifactWriteFailedReasonCode`
- `ChainRunner_ArtifactReadFailure_EmitsArtifactReadFailedReasonCode`
- `ChainRunner_ArtifactPathDeterminism_UsesTurnIndexAndStepKeyOnly`
- `ChainRunner_FailureArtifactPathDeterminism_UsesFailureTurnIndexAnchor`
- `ChainRunner_TurnMetadata_TurnJsonIsCanonicalSourceOfTruth`
- `ChainRunner_Counters_TurnAndCheckpointIndices_IncrementDeterministically`
- `ChainRunner_Counters_CrashAfterReservation_DoesNotReuseTurnOrCheckpointIndices`
- `ChainRunner_Timestamps_PresenceAndIso8601Format_WithoutCrossRunValueEquality`
- `ChainRunner_ProvenanceArtifact_StrictSuperset_MapsLosslesslyToPromptingRequiredFields`

---

## Source 29: `steps/0100-finalize-plan-2-acceptance-gates-and-promotion-readiness/artifacts/final-promotion-checklist.md`

# Final Promotion Checklist

## Scope
- Plan: `mvp-chain-runner-local-persistence-baseline`
- Step: `0100-finalize-plan-2-acceptance-gates-and-promotion-readiness`

## Required Gates
- [ ] Scope/non-goals are explicit and still correct.
- [ ] Cross-plan dependencies are explicit and valid.
- [ ] Project dependency implementation matrix is explicit and consistent with architecture decisions.
- [ ] Runner contract explicitly supports variable-length `CognitiveChain` and `ConversationalChain` with `PLAN_STEP/EXECUTE` semantics.
- [ ] Minimal workflow abstraction and route-hook seams are explicitly defined.
- [ ] Workflow step contract includes required `prompt_ref` (`prompt_id`, `prompt_version`) for Plan 2 execution.
- [ ] Local persistence contract is explicitly workspace-local and excludes external services.
- [ ] Retry/resume policy includes pinned reason codes and deterministic behavior.
- [ ] Missing/invalid step `prompt_ref` deterministic failure (`missing_prompt_reference`) is pinned and evidenced.
- [ ] Resume policy explicitly differentiates cognitive restart-from-start vs conversational resume-from-last-success.
- [ ] Proof test seed list and evidence spec are explicit and complete.
- [ ] Acceptance evidence matrix maps all DoD points to artifact/test evidence paths.
- [ ] Artifact path contract is deterministic and uses `<turn-index>-<step-key>` only.
- [ ] Canonical turn metadata record (`turn.json`) is explicit and enforced.
- [ ] Run/turn state machine table is explicit, including illegal transition handling.
- [ ] Counter reservation rules for `next_turn_index`/`next_checkpoint_sequence` and persisted `turn_index`/`checkpoint_sequence` are explicit.
- [ ] Crash/partial-write reservation behavior is explicit and forbids index reuse.
- [ ] Minimum atomic write strategy is explicit (`temp in same directory -> flush/fsync -> atomic replace`).
- [ ] Failure artifact naming anchor is explicit (`failure-<turn-index>.json`).
- [ ] Route-hook generated step-key and persisted effective-queue rules are explicit.
- [ ] `route_hook_key` grammar is explicit and guarantees generated step-key path safety.
- [ ] `workflow_kind`/`chain_mode` alignment invariant and deterministic mismatch failure behavior are explicit.
- [ ] Provenance artifact shape is strict superset with deterministic Prompting mapping contract.
- [ ] Continuity-handle behavior is capability-conditioned (supported vs unsupported) and non-contradictory.

## Stop Conditions
- Missing dependency direction assertion coverage.
- Missing reason-code mapping for any retry/terminal scenario.
- Missing cognitive-vs-conversational resume policy differentiation.
- Any implicit reliance on hidden continuity state for correctness.
- Any planned Postgres/distributed runtime work inside Plan 2 scope.
- Any non-deterministic artifact path rule or objective-text path dependency.
- Missing canonical turn metadata authority definition (`turn.json` vs other artifacts).
- Missing explicit step-to-prompt binding contract (`prompt_ref`) for required execution paths.
- Missing deterministic reservation order for run counters (`next_turn_index`, `next_checkpoint_sequence`).
- Missing `workflow_kind`/`chain_mode` alignment rule or mismatch failure behavior.
- Missing explicit route-hook key grammar for generated step-key safety.
- Any index-reuse behavior after interruption/partial-write scenarios.
- Timestamp assertions rely on raw cross-run value equality without deterministic clock injection.

---

## Source 30: `artifacts/project-dependency-implementation-matrix.md`

# Project Dependency Implementation Matrix

## Scope
- Plan: `mvp-chain-runner-local-persistence-baseline`
- Purpose: lock dependency and implementation boundaries before Plan 2 promotion to `InProgress`.

## Matrix
| Project | Primary Responsibility | Allowed Runtime References | Forbidden Runtime References | Planned Test Coverage |
|---|---|---|---|---|
| `Source/Zelanthus.StoryEngine.Domain` | chain/run/turn invariants | `none` | `StoryEngine.Infrastructure`, `API`, provider implementations | architecture tests for no forbidden deps; domain unit tests |
| `Source/Zelanthus.StoryEngine.Application` | runner orchestration and policy | `StoryEngine.Domain`, `Prompting`, `Llm.Clients.Abstractions` | provider implementations, storage/file IO concerns | orchestration unit tests + contract-proof tests |
| `Source/Zelanthus.StoryEngine.Infrastructure` | local persistence stores and mappers | `StoryEngine.Domain`, `StoryEngine.Application`, `Prompting`, `Llm.Clients.Abstractions` | API concerns and semantic chain policy decisions | mapper round-trip tests + failure-path tests |
| `Source/Zelanthus.Llm.Clients.Gemini` | provider protocol adapter | `Llm.Clients.Abstractions` | `StoryEngine.Domain`, `StoryEngine.Application` | adapter contract tests (Plan 1 + Plan 2 reuse) |
| `Source/Zelanthus.API` | composition root | `StoryEngine.Application`, `StoryEngine.Infrastructure`, provider adapter | domain rule logic and persistence transform logic | composition smoke checks (no endpoint breadth in Plan 2) |
| `Tests/Zelanthus.Architecture.Tests` | boundary enforcement | project-under-test refs only | n/a | dependency-direction gate assertions |
| `Tests/Zelanthus.WorkflowContractProofs.Tests` | deterministic proof/evidence | contracts + fixture harness | brittle provider prose assertions | golden/failure/resume artifact evidence tests |

## Review Gate
- Any reference outside this matrix requires explicit decision update before execution.

---

## Source 31: `promotion.md`

# Promotion Trace

## Source
- Source draft path: Plans/Drafts/mvp-chain-runner-local-persistence-baseline
- Source commit SHA: cc49173737d3b767d9758f3e3706316bd5d93f05
- Promotion date: 2026-02-21

## Transformation Summary
- Copied Draft plan folder to InProgress as promotion baseline.
- Reset execution tracking surfaces for InProgress workflow semantics.
- Updated branch tracking for execution on feature/thin-clients-first-solution-structure.

## Notes
- Draft folder remains frozen baseline for planning audit.
- InProgress folder is execution-authoritative for implementation tracking.
