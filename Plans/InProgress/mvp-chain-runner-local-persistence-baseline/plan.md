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
  - Domain scope policy for Plan 2:
    - `Source/Zelanthus.StoryEngine.Domain` is in scope for minimal runner value objects/invariants only.
    - If the Domain project already exists, extend it in place; do not create an alternate Domain project.
    - Domain scope excludes persistence, provider protocol mapping, and API composition concerns.
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
  - Broad domain-model expansion beyond runner invariants required for Plan 2.
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
  - file naming convention is single-sourced and consistent across Decision `0002`, local persistence layout artifact, and runner proof evidence spec.
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
    checkpoint-<checkpoint-sequence>.json
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
  - Verify checkpoint/failure filename conventions are single-sourced and consistent before implementing persistence layout:
    - `checkpoints/checkpoint-<checkpoint-sequence>.json`
    - `failures/failure-<turn-index>.json`
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
  - Do not implement local persistence layout code until filename conventions are confirmed consistent across Decision `0002`, step `0040` layout artifact, and step `0080` evidence spec.
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


