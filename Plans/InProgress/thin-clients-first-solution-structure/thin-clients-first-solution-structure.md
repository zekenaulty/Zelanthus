# thin-clients-first-solution-structure

## Compiled Plan Metadata

- Plan Scope: `InProgress/thin-clients-first-solution-structure`
- Compiled At (UTC): `2026-02-21T19:43:52Z`
- Source Document Count: `34`
- Projection File: `thin-clients-first-solution-structure.md`

## Contents

1. `plan.md`
2. `decisions/0001-solution-project-boundaries-v0.md`
3. `decisions/0002-thin-clients-first-sequencing-v0.md`
4. `decisions/0003-dependency-direction-rules-v0.md`
5. `decisions/0004-prompting-ownership-and-versioning-policy-v0.md`
6. `decisions/0005-infrastructure-prompt-shape-mapping-policy-v0.md`
7. `decisions/0006-call-envelope-and-enforcement-policy-v0.md`
8. `decisions/0007-mvp-goals-and-first-plan-boundary-v0.md`
9. `decisions/0008-mvp-harness-shape-selection-v0.md`
10. `decisions/0009-contract-proof-test-naming-policy-v0.md`
11. `risks/risk-log.md`
12. `validation/top-level-acceptance-evidence.md`
13. `steps/index.md`
14. `steps/0010-map-solution-project-boundaries/step.md`
15. `steps/0020-plan-thin-clients-package-first/step.md`
16. `steps/0030-sequence-story-engine-dependencies/step.md`
17. `steps/0040-lock-prompting-ownership-policy/step.md`
18. `steps/0050-lock-infrastructure-prompt-shape-mapping/step.md`
19. `steps/0060-refine-decision-layer-responsibility-clarity/step.md`
20. `steps/0070-apply-external-review-refinements/step.md`
21. `steps/0080-lock-mvp-goals-and-first-plan-boundary/step.md`
22. `steps/0090-choose-mvp-harness-shape/step.md`
23. `steps/0100-draft-plan-1-prompting-and-gemini-contract-implementation/step.md`
24. `steps/0105-clarify-contract-proof-test-naming/step.md`
25. `steps/0110-draft-plan-2-cognitive-chain-runner-and-local-persistence/step.md`
26. `steps/0120-execute-mvp-prompting-gemini-contract-baseline/step.md`
27. `steps/0130-execute-mvp-chain-runner-local-persistence-baseline/step.md`
28. `steps/0140-prepare-closeout-and-pr-handoff/step.md`
29. `steps/0090-choose-mvp-harness-shape/artifacts/harness-shape-evaluation-matrix.md`
30. `steps/0140-prepare-closeout-and-pr-handoff/validation/pr-ready-handoff-checklist.md`
31. `steps/0140-prepare-closeout-and-pr-handoff/notes/closeout-traceability-prep.md`
32. `steps/0140-prepare-closeout-and-pr-handoff/notes/completed-package-and-cleanup-sequence.md`
33. `artifacts/initial-solution-shape.md`
34. `promotion.md`

---

## Source 1: `plan.md`

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
- [x] `0120-execute-mvp-prompting-gemini-contract-baseline`
- [x] `0130-execute-mvp-chain-runner-local-persistence-baseline`
- [x] `0140-prepare-closeout-and-pr-handoff`

## Notes
- This InProgress folder is execution-authoritative for the thin-clients top-level coordination plan.
- Draft planning artifacts remain preserved in `Plans/Drafts/thin-clients-first-solution-structure`.
- Existing legacy step folders (`0010`-`0110`) remain as promoted baseline context; execution tracking in this phase uses `0120`-`0140`.

---

## Source 2: `decisions/0001-solution-project-boundaries-v0.md`

# Decision 0001: Solution Project Boundaries v0

## Status
- accepted

## Decision Summary
- Establish explicit package boundaries between prompt tooling, provider clients, story engine layers, and API composition.
- Treat `Zelanthus.Prompting` as required shared prompt contract/governance runtime.
- Keep provider-specific implementations outside story engine domain/application layers.

## Initial Boundary Set
- `Zelanthus.Prompting`
- `Zelanthus.Llm.Clients.Abstractions`
- `Zelanthus.Llm.Clients.Gemini`
- `Zelanthus.StoryEngine.Domain`
- `Zelanthus.StoryEngine.Application`
- `Zelanthus.StoryEngine.Infrastructure`
- `Zelanthus.API`

## Package Responsibilities
- `Zelanthus.Prompting`:
  - prompt identity/versioning contracts,
  - composition/rendering contracts,
  - prompt provenance contract shapes.
- `Zelanthus.Llm.Clients.Abstractions`:
  - provider-agnostic request/response contracts,
  - capability profile contracts,
  - normalized error and token accounting contracts.
- `Zelanthus.Llm.Clients.Gemini`:
  - Gemini protocol implementation against abstractions only.
- `Zelanthus.StoryEngine.Domain`:
  - core domain entities, value objects, invariants.
- `Zelanthus.StoryEngine.Application`:
  - workflow orchestration/use-cases, semantic decision logic, chain-mode selection.
- `Zelanthus.StoryEngine.Infrastructure`:
  - persistence, artifact ledger, checkpoint stores, adapter wiring for runtime contracts.
- `Zelanthus.API`:
  - composition root and external entry points.

## Boundary Invariants
- Story engine domain/application must not reference provider-specific implementation assemblies.
- Prompting runtime contracts are consumed by application/infrastructure, but prompting package does not depend on story engine or API.
- Provider adapters do not depend on story engine domain/application.
- Runtime namespaces/packages use `Zelanthus.*`; `BookForge` naming remains documentation/reference context only.

## Rationale
- Preserves provider portability and prompt-governance discipline.
- Prevents monolithic coupling and supports focused testing.
- Enables future tool/MCP/worker entry points to reuse the same contracts.

## Consequences
- More projects than current baseline, with clearer ownership and test boundaries.
- Architecture tests and review gates are needed to enforce dependency direction.
- Implementing applications integrate prompt registration/versioning/provenance through `Zelanthus.Prompting`.

---

## Source 3: `decisions/0002-thin-clients-first-sequencing-v0.md`

# Decision 0002: Thin Clients First Sequencing v0

## Status
- accepted

## Decision Summary
- Sequence implementation planning and delivery with thin client and prompt contracts first, then story engine orchestration and persistence.

## Why This Sequence
- Story engine flows depend on stable contracts for:
  - chain mode behavior,
  - provider capability handling,
  - token accounting and error normalization,
  - prompt provenance and execution metadata.
- Defining orchestration before those contracts stabilizes creates rework and ambiguous boundaries.

## Ordered Delivery Phases
1. Contract phase:
  - define `Zelanthus.Llm.Clients.Abstractions` contracts,
  - define `Zelanthus.Prompting` contracts and provenance requirements.
2. First provider phase:
  - implement `Zelanthus.Llm.Clients.Gemini` against abstractions,
  - validate capability profile wiring and normalized response behavior.
3. Story engine phase:
  - design and implement `Zelanthus.StoryEngine.Domain/Application/Infrastructure` against stabilized contracts.
4. Composition phase:
  - wire runtime composition and entry points in `Zelanthus.API`.

## Thin MVP Proving Slice
- Minimum slice to validate backbone doctrine before broad StoryEngine expansion:
  - `Zelanthus.Prompting` contract implementation (identity/version/render/checksum/provenance hooks),
  - `Zelanthus.Llm.Clients.Abstractions` + `Zelanthus.Llm.Clients.Gemini` with capability profile and normalized metadata,
  - one minimal runner proving at least one `CognitiveChain` step pair (`PLAN_STEP` + `EXECUTE`) with persisted artifacts/provenance.
- Clarification:
  - `PLAN_STEP` + `EXECUTE` is the minimum acceptance proof path, not a runner step-limit.
  - `CognitiveChain` may stage multiple `PLAN_STEP` units before one or more `EXECUTE` units and may repeat `PLAN_STEP` -> `EXECUTE` cycles.
  - `CognitiveChain` resume restarts from chain start to re-establish provider thinking continuity safely.
  - `ConversationalChain` resumes from last successful persisted step.
  - Runner design must allow multi-turn/multi-step execution for both `CognitiveChain` and `ConversationalChain`.
- This slice is the acceptance gate for promoting broad StoryEngine implementation scope.

## Entry and Exit Criteria
- Exit criteria for contract phase:
  - capability profile contract finalized,
  - prompt identity/version/provenance contract finalized.
  - canonical call envelope defined (see `0006-call-envelope-and-enforcement-policy-v0.md`).
- Exit criteria for first provider phase:
  - Gemini adapter passes contract-level tests and emits normalized metadata.
  - architecture dependency tests pass for defined boundaries.
- Entry criteria for story engine phase:
  - contracts are versioned and consumed via abstractions only.

## Non-Goals
- Building full chat/session infrastructure before core prompt/workflow contracts.
- Building multiple provider adapters before Gemini baseline is contract-verified.

## Consequences
- Early effort is spent on contract quality rather than feature breadth.
- Story engine planning can use concrete contract inputs instead of assumptions.
- Draft promotion should remain narrow:
  - Draft Plan 1: Prompting + LLM abstractions + Gemini adapter.
  - Draft Plan 2: MVP `CognitiveChain` runner + artifact/provenance persistence.

---

## Source 4: `decisions/0003-dependency-direction-rules-v0.md`

# Decision 0003: Dependency Direction Rules v0

## Status
- accepted

## Decision Summary
- Enforce one-way dependency direction.
- Separate transformation concerns by layer to avoid orchestration logic leaking into infrastructure or adapters.

## Transformation Responsibility Split
- Semantic transformation (application concern):
  - selecting prompt IDs/versions,
  - building workflow context for phase execution,
  - selecting chain mode and runtime policy.
- Protocol/provider transformation (adapter concern):
  - mapping normalized contracts to provider request shape,
  - mapping provider response/protocol errors into normalized contracts.
- Persistence transformation (infrastructure concern):
  - mapping persisted storage schema to/from canonical runtime prompt/provenance contracts.

## Allowed Dependencies (high level)
- `Zelanthus.API` -> `Zelanthus.StoryEngine.Application`, `Zelanthus.StoryEngine.Infrastructure`, `Zelanthus.Llm.Clients.Gemini`
- `Zelanthus.StoryEngine.Infrastructure` -> `Zelanthus.StoryEngine.Application`, `Zelanthus.StoryEngine.Domain`, `Zelanthus.Llm.Clients.Abstractions`, `Zelanthus.Prompting`
- `Zelanthus.StoryEngine.Application` -> `Zelanthus.StoryEngine.Domain`, `Zelanthus.Llm.Clients.Abstractions`, `Zelanthus.Prompting`
- `Zelanthus.Llm.Clients.Gemini` -> `Zelanthus.Llm.Clients.Abstractions`
- `Zelanthus.Prompting` -> no dependencies on story engine/API/provider implementation projects
- `Zelanthus.StoryEngine.Domain` -> no infrastructure/provider dependencies

## Forbidden Dependencies and Anti-Patterns
- Story engine domain/application referencing provider-specific adapters.
- Prompting package referencing story engine or API projects.
- Provider adapters referencing story engine domain/application.
- Infrastructure persistence shapes used directly as runtime prompt contracts without explicit mapping.
- Infrastructure owning semantic prompt assembly decisions that belong to application workflows.

## Enforcement Guidance
- Add architecture tests for dependency direction in the thin MVP slice (not deferred).
- Add review checklist items for transformation ownership:
  - "Is semantic transform in application?"
  - "Is provider protocol transform in adapter?"
  - "Is storage transform in infrastructure?"
- Minimum architecture assertions:
  - domain has no infrastructure/provider adapter dependencies,
  - prompting has no story engine/API dependencies,
  - provider implementation projects have no story engine domain/application dependencies.

## Rationale
- Keeps abstractions stable and reusable.
- Prevents boundary drift and accidental coupling.
- Reduces refactor risk when adding providers or changing storage schemas.

---

## Source 5: `decisions/0004-prompting-ownership-and-versioning-policy-v0.md`

# Decision 0004: Prompting Ownership and Versioning Policy v0

## Status
- accepted

## Decision Summary
- `Zelanthus.Prompting` is the required runtime contract layer for prompt management and versioning.
- Implementing applications own prompt content packs and prompt selection policy, but not ad-hoc prompt mechanics.
- Workflow-relevant model calls must emit prompt and execution provenance using prompting contracts.

## Ownership Model
- `Zelanthus.Prompting` owns:
  - prompt identity/version contract,
  - composition/render contract,
  - provenance contract shapes.
- Implementing applications (API/worker/CLI/tool/MCP host) own:
  - which prompt pack/version is deployed,
  - which prompt version is selected for a workflow step,
  - environment-specific configuration for prompt loading.

## Required Policy
- Prompt identity and versioning:
  - stable semantic prompt IDs,
  - explicit version values,
  - no in-place edits to released versions.
- Runtime usage:
  - prompt templates are loaded through prompting contracts, not inline orchestration strings.
  - required placeholders must be resolved before execution.
- Provenance:
  - rendered prompt hash/checksum and prompt identity/version are mandatory.
  - provider/model metadata and normalized response metadata are mandatory.
  - provenance emission is required for every workflow call that can affect persisted artifacts.
- Persistence compatibility:
  - if infrastructure persists non-1:1 schemas, mapping to/from prompting contracts is mandatory and explicit.

## Call Envelope Integration
- Prompting outputs are consumed through the canonical call envelope defined in `0006-call-envelope-and-enforcement-policy-v0.md`.
- Prompting does not invoke provider protocols directly; provider invocation is through client abstractions.

## Allowed Exceptions
- Unit tests may use inline prompt fixtures when prompt lifecycle behavior is not under test.
- Short-lived spikes may use temporary inline prompts only outside production paths and only with explicit temporary marker notes.

## Rationale
- Prevents prompt drift between applications and environments.
- Keeps replay/debug surfaces deterministic.
- Supports multi-provider evolution without changing application workflow contracts.

## Consequences
- Startup/composition for implementing apps must include prompt pack/registry wiring.
- Draft and InProgress plans must include prompt versioning/provenance acceptance criteria.

---

## Source 6: `decisions/0005-infrastructure-prompt-shape-mapping-policy-v0.md`

# Decision 0005: Infrastructure Prompt Shape Mapping Policy v0

## Status
- accepted

## Decision Summary
- `Zelanthus.StoryEngine.Infrastructure` may persist prompt and provenance data in storage schemas that are not 1:1 with runtime prompting contracts.
- Infrastructure must implement deterministic mapping to/from canonical runtime contract shapes.

## Context
- Persistence models often optimize for indexing, querying, retention, and storage costs.
- Runtime contract models optimize for execution correctness and workflow validation.
- Forcing identical models in both layers creates unnecessary coupling.

## Required Mapping Guarantees
- Deterministic mapping:
  - same input record and mapper version produce same runtime contract output.
- Round-trip guarantees for required fields:
  - prompt semantic ID,
  - prompt version,
  - rendered prompt hash/checksum,
  - provider/model identity,
  - chain mode and turn metadata,
  - normalized response metadata required by validators.
- Explicit failure semantics:
  - mapping failures are surfaced as explicit errors,
  - required fields cannot be silently dropped or defaulted without reason code.

## Mapping Boundary Rules
- Infrastructure owns storage<->runtime mapping only.
- Application owns semantic assembly decisions (prompt choice, context assembly, chain policy).
- Provider adapters own runtime<->protocol mapping.

## Validation and Test Expectations
- Contract materialization tests:
  - persisted record -> runtime contract passes schema checks.
- Round-trip tests:
  - runtime contract -> storage model -> runtime contract preserves required fields.
- Error-path tests:
  - missing required stored fields produce explicit mapping failures.

## Rationale
- Enables storage flexibility without sacrificing runtime contract integrity.
- Preserves clear layer responsibilities and reduces accidental coupling.

## Consequences
- Infrastructure implementation plans must include mapper contracts and mapping test suites.
- Review gates must reject persistence changes that bypass mapping boundaries.

---

## Source 7: `decisions/0006-call-envelope-and-enforcement-policy-v0.md`

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

---

## Source 8: `decisions/0007-mvp-goals-and-first-plan-boundary-v0.md`

# Decision 0007: MVP Goals and First Plan Boundary v0

## Status
- accepted

## Decision Summary
- Lock MVP goals before selecting harness host/transport details.
- Keep first implementation plan narrow: contracts and Gemini baseline first.
- Defer Postgres and broader story engine expansion until MVP proof goals are met.

## MVP Goals
1. Prompt contract correctness:
  - `Zelanthus.Prompting` provides deterministic prompt identity/version/render/checksum behavior.
2. Client contract correctness:
  - `Zelanthus.Llm.Clients.Abstractions` defines normalized request/response/capability/error and `TokenAccounting` contracts.
3. Gemini baseline correctness:
  - `Zelanthus.Llm.Clients.Gemini` satisfies abstraction contracts and emits required normalized metadata.
4. Provenance and failure correctness:
  - workflow-relevant calls emit required provenance fields and reason-coded failures.
5. Boundary correctness:
  - architecture tests enforce dependency-direction rules during MVP, not later.

## First Plan Boundary (Draft Plan 1)
- In scope:
  - `Zelanthus.Prompting` baseline implementation.
  - `Zelanthus.Llm.Clients.Abstractions` baseline contracts.
  - `Zelanthus.Llm.Clients.Gemini` baseline adapter.
  - Architecture test gate project for dependency boundaries.
  - Minimal execution harness path capable of proving one golden-path call.
- Out of scope:
  - broad story engine domain modeling,
  - multi-provider implementations,
  - production persistence and Postgres setup,
  - frontend/chat feature development.

## Harness Requirement Contract (host-shape neutral)
- Must execute a deterministic `CognitiveChain` style proof path (`PLAN_STEP` then `EXECUTE`) for at least one representative step.
- `PLAN_STEP` -> `EXECUTE` is a minimum proof path, not a hard cap on total chain steps.
- Plan 2 runner design must support variable-length chains for:
  - `CognitiveChain` (multi-step planning/execution with thought-aware checkpoints and optional staged `PLAN_STEP` groups),
  - `ConversationalChain` (multi-turn chat-style flow with explicit turn artifacts).
- `CognitiveChain` resume policy must restart from chain start when execution is interrupted and provider thinking cache durability is unknown.
- `ConversationalChain` resume policy may continue from the last successful persisted step.
- Must run from one repeatable command path (for example `dotnet test` or one explicit host command).
- Must capture raw response snapshot reference plus normalized metadata.
- Must persist provenance and validation/failure outcome artifacts locally.
- Local persistence in MVP means workspace-local file/path-backed storage for artifacts/checkpoints/provenance/failures only.
- Local persistence in MVP excludes external database/cache/queue/service-hosted storage.
- Must support clear failure reason code reporting for MVP baseline codes.

## Candidate Harness Shapes (decision deferred to step `0090`)
- Option A: dedicated MVP integration test project under `Tests/` (recommended starting point).
- Option B: minimal API-hosted endpoint plus integration tests (future-only option, not authorized for Round-1 Plan 1 scope).

## Rationale
- Goal-first planning prevents host-shape discussions from redefining MVP success criteria mid-draft.
- Narrow Plan 1 scope reduces rework and keeps doctrine validation fast.
- Deferring Postgres aligns with current priority: prove runtime contracts first.

## Consequences
- Draft steps must choose harness shape against these requirements, not by preference alone.
- Plan 2 draft depends on Plan 1 contract outputs and acceptance evidence.

---

## Source 9: `decisions/0008-mvp-harness-shape-selection-v0.md`

# Decision 0008: MVP Harness Shape Selection v0

## Status
- accepted

## Decision Summary
- Select a dedicated contract-proof test project under `Tests/` as the default Plan 1 proving surface.
- Defer API-hosted harness execution to a follow-up only if test-host criteria fail.

## Selected Shape
- Primary shape:
  - `Tests/Zelanthus.WorkflowContractProofs.Tests` (integration-style test project).
- Command entry point:
  - `dotnet test` for deterministic execution and CI friendliness.

## Naming Intent
- This test project is long-lived and reusable for one-off MVP/contract proof phases.
- Name encodes function, not milestone:
  - `WorkflowContractProofs` communicates purpose better than generic labels like `MvpHarness`.
- New test project naming rule for this scope:
  - `Zelanthus.<semantic-purpose>.Tests`
  - avoid generic names with low semantic intent (`MvpHarness`, `TempTests`, `MiscTests`).

## Why This Shape
- Lowest setup overhead while contracts are still stabilizing.
- Keeps harness focused on prompt/client/provenance contracts without API transport noise.
- Improves repeatability and failure isolation for early MVP iterations.
- Aligns with current non-goal: no production persistence/deployment setup in Plan 1.

## Acceptance Requirements (from Decision 0007)
- Harness runs a deterministic MVP proof path with explicit objectives and validations.
- Harness persists local artifact and provenance evidence.
- Harness captures raw snapshot references and normalized metadata.
- Harness emits reason-coded failures for baseline failure scenarios.

## API-Host Deferral Rule
- API-host harness remains a valid later evolution path.
- Promote to API-host only if one or more criteria are unmet in test-host shape:
  - required execution lifecycle hooks cannot be modeled in tests,
  - dependency wiring fidelity cannot be proven without API composition root,
  - operational diagnostics require API middleware context.

## Consequences
- Plan 1 drafting will assume a test-host MVP harness project in `Tests/`.
- Plan 1 DoD must include harness evidence artifacts produced from test runs.
- Plan 2 can reuse harness contracts while introducing runner orchestration and persistence evolution.
- First proof tests in this project must also use semantic names that express contract intent.

---

## Source 10: `decisions/0009-contract-proof-test-naming-policy-v0.md`

# Decision 0009: Contract-Proof Test Naming Policy v0

## Status
- accepted

## Decision Summary
- Adopt semantic naming rules for contract-proof test projects and first proof tests.
- Treat these tests as long-lived contract proof assets, not temporary MVP leftovers.

## Project Naming Rule
- Pattern:
  - `Zelanthus.<semantic-purpose>.Tests`
- Selected project name for Plan 1 proof scope:
  - `Zelanthus.WorkflowContractProofs.Tests`
- Avoid:
  - `MvpHarness`, `TempTests`, `MiscTests`, or sequence-only names.

## Test Naming Rule
- Test names must encode:
  - contract surface,
  - scenario,
  - expected outcome.
- Preferred style:
  - `<ContractSurface>_<Scenario>_<ExpectedOutcome>`

## Initial Seed Test Names (Plan 1)
- `PromptRendering_TwoRequiredPlaceholders_OneMissing_ReturnsDeterministicRenderFailure`
- `PromptRendering_TwoRequiredPlaceholders_OneMissing_ReportsMissingPlaceholdersInLexicalOrder`
- `PromptRendering_AllRequiredPlaceholdersProvided_ReturnsRenderedPromptWithStableChecksum`
- `GeminiAdapter_ValidProviderResponse_ProducesNormalizedResponseEnvelope`
- `GeminiAdapter_ProviderProtocolError_EmitsProviderProtocolErrorReasonCode`
- `GeminiAdapter_ResponseWithoutUsage_MapsTokenAccountingToUnknown`
- `ProvenanceRecord_GoldenPathExecution_ContainsRequiredMvpFields`

## Consequences
- Draft Plan 1 references are updated to `Zelanthus.WorkflowContractProofs.Tests`.
- InProgress implementation reviews should reject low-semantic test naming.

---

## Source 11: `risks/risk-log.md`

# Risk Log

## R-001 Harness Shape Rework Risk
- Statement: Selecting an MVP harness host shape too early (API-first vs test-host) introduces avoidable rework.
- Impact: Draft churn and unstable acceptance pathways.
- Mitigation: Lock goal/acceptance contract first, then choose host shape with explicit criteria in step `0090`.
- Status: open

## R-002 Provider Variance Risk
- Statement: Gemini behavior variance (rate limits, token profile differences, response variability) may destabilize deterministic MVP checks.
- Impact: Flaky acceptance evidence and delayed draft completion.
- Mitigation: Use normalized response contract assertions and reason-coded failure expectations.
- Status: open

## R-003 Persistence Migration Risk
- Statement: Local MVP persistence could diverge from future Postgres schema expectations.
- Impact: Added mapping work when production persistence starts.
- Mitigation: Keep persistence contract-first and record explicit mapping constraints in Plan 2 draft.
- Status: open

## R-004 Boundary Drift Risk
- Statement: New projects may violate dependency-direction rules during rapid scaffolding.
- Impact: Early coupling and architecture debt.
- Mitigation: Add architecture tests as an MVP gate in Plan 1 work.
- Status: open

## R-005 Single-Pair Lock-In Risk
- Statement: Treating `PLAN_STEP` -> `EXECUTE` as a fixed one-pair runner design (instead of minimum proof path) can block required multi-step chain workflows.
- Impact: Early rework in Plan 2 runner orchestration and persistence shape.
- Mitigation: Explicitly require chain-length-flexible runner contracts for both `CognitiveChain` and `ConversationalChain` in Plan 2 draft.
- Status: open

---

## Source 12: `validation/top-level-acceptance-evidence.md`

# Top-Level Acceptance Evidence

## Summary
- Top-level thin-clients execution coordination plan reached execution-complete status for steps `0120`, `0130`, and `0140`.
- Plan 1 and Plan 2 validation evidence has been captured and linked for PR handoff.

## Evidence Links
- Plan 1 final acceptance checklist:
  - `Plans/InProgress/mvp-prompting-gemini-contract-baseline/steps/0070-validate-golden-path-and-reason-coded-failure-path/validation/final-acceptance-checklist.md`
- Plan 2 final promotion checklist:
  - `Plans/InProgress/mvp-chain-runner-local-persistence-baseline/steps/0100-finalize-plan-2-acceptance-gates-and-promotion-readiness/validation/final-promotion-checklist.md`
- Plan 2 acceptance matrix:
  - `Plans/InProgress/mvp-chain-runner-local-persistence-baseline/validation/plan-2-acceptance-evidence-matrix.md`
- Top-level closeout traceability prep:
  - `Plans/InProgress/thin-clients-first-solution-structure/steps/0140-prepare-closeout-and-pr-handoff/notes/closeout-traceability-prep.md`
- Top-level PR-ready checklist:
  - `Plans/InProgress/thin-clients-first-solution-structure/steps/0140-prepare-closeout-and-pr-handoff/validation/pr-ready-handoff-checklist.md`

## Branch and Review Context
- Execution branch: `feature/thin-clients-first-solution-structure`
- Base branch: `main`
- PR ownership: user-managed manual PR flow

---

## Source 13: `steps/index.md`

# Steps Index

Use this file for dependency and status summaries.
Do not use this file as the only source of step order.
Step order must still be visible in folder names (`NNNN-semantic-step-name`).

## Step Map
| Order | Step Folder | Status | Depends On | Notes |
|---|---|---|---|---|
| 0120 | `0120-execute-mvp-prompting-gemini-contract-baseline` | `completed` | `none` | Execute Plan 1 on feature branch and maintain evidence/step notes in its InProgress folder |
| 0130 | `0130-execute-mvp-chain-runner-local-persistence-baseline` | `completed` | `0120-execute-mvp-prompting-gemini-contract-baseline` | Execute Plan 2 after Plan 1 contracts are implemented and validated |
| 0140 | `0140-prepare-closeout-and-pr-handoff` | `completed` | `0120-execute-mvp-prompting-gemini-contract-baseline`, `0130-execute-mvp-chain-runner-local-persistence-baseline` | Prepare Completed package, cleanup commit plan, and explicit PR-ready handoff for user merge |

## Baseline Note
- Legacy promoted baseline step folders (`0010`-`0110`) are retained for audit context only.
- InProgress execution tracking for this phase uses `0120`-`0140`.

## Status Values
- `pending`
- `in_progress`
- `blocked`
- `completed`

---

## Source 14: `steps/0010-map-solution-project-boundaries/step.md`

# Step: 0010-map-solution-project-boundaries

## Goal
- Define a clear initial solution/package shape that separates thin clients from story engine concerns.

## Context
- We need boundary clarity before drafting implementation plans to avoid monolithic project drift.

## Commands Executed
- `Get-Content -Raw "Zelanthus.slnx"`
- `Get-Content -Raw "Plans/Brainstorms/backbone-doctrine-prompt-first-multi-provider/plan.md"`

## Files Changed
- `Plans/InProgress/thin-clients-first-solution-structure/plan.md`
- `Plans/InProgress/thin-clients-first-solution-structure/steps/index.md`
- `Plans/InProgress/thin-clients-first-solution-structure/decisions/0001-solution-project-boundaries-v0.md`
- `Plans/InProgress/thin-clients-first-solution-structure/artifacts/initial-solution-shape.md`

## Tests / Results
- `not-run` -> docs-only brainstorm step

## Issues
- none

## Decision
- Keep thin LLM clients/prompts outside story engine and enforce layered dependency direction.

## Completion
- `pending`

## Next Actions
- Capture thin-clients-first sequencing and dependency rules in dedicated decisions.

---

## Source 15: `steps/0020-plan-thin-clients-package-first/step.md`

# Step: 0020-plan-thin-clients-package-first

## Goal
- Define implementation planning order where thin clients are planned and built before story engine workflows.

## Context
- Prompt-first, multi-provider-ready architecture depends on stable client and capability contracts before story engine orchestration.

## Commands Executed
- `Get-Content -Raw "Plans/Brainstorms/backbone-doctrine-prompt-first-multi-provider/decisions/0002-chain-continuity-strategy-v0.md"`
- `Get-Content -Raw "Plans/Brainstorms/backbone-doctrine-prompt-first-multi-provider/decisions/0003-prompt-governance-and-provenance-v0.md"`

## Files Changed
- `Plans/InProgress/thin-clients-first-solution-structure/steps/0020-plan-thin-clients-package-first/step.md`
- `Plans/InProgress/thin-clients-first-solution-structure/decisions/0002-thin-clients-first-sequencing-v0.md`

## Tests / Results
- `not-run` -> docs-only brainstorm step

## Issues
- none

## Decision
- Sequence planning as: thin client abstractions + Gemini adapter + prompting contracts first, story engine planning second.

## Completion
- `pending`

## Next Actions
- Capture allowed dependency direction to preserve package boundaries during implementation.

---

## Source 16: `steps/0030-sequence-story-engine-dependencies/step.md`

# Step: 0030-sequence-story-engine-dependencies

## Goal
- Define dependency rules that keep story engine implementation cleanly layered after thin-client contract planning.

## Context
- Without explicit dependency direction, API/story engine/provider code tends to collapse into coupled implementations.

## Commands Executed
- `Get-Content -Raw "Plans/InProgress/thin-clients-first-solution-structure/artifacts/initial-solution-shape.md"`
- `Get-Content -Raw "Plans/README.md"`

## Files Changed
- `Plans/InProgress/thin-clients-first-solution-structure/steps/0030-sequence-story-engine-dependencies/step.md`
- `Plans/InProgress/thin-clients-first-solution-structure/decisions/0003-dependency-direction-rules-v0.md`

## Tests / Results
- `not-run` -> docs-only brainstorm step

## Issues
- none

## Decision
- Enforce one-way dependency direction from API to application and from application to contracts, with provider implementations hidden behind abstraction boundaries.

## Completion
- `pending`

## Next Actions
- Promote this brainstorm into a Draft implementation plan for the thin clients package.

---

## Source 17: `steps/0040-lock-prompting-ownership-policy/step.md`

# Step: 0040-lock-prompting-ownership-policy

## Goal
- Explicitly lock whether implementing applications must manage and version prompts through `Zelanthus.Prompting`.

## Context
- This policy is critical to avoid prompt/version drift and provenance inconsistency across implementations.

## Commands Executed
- `Get-Content -Raw "Plans/InProgress/thin-clients-first-solution-structure/plan.md"`
- `Get-Content -Raw "Plans/InProgress/thin-clients-first-solution-structure/decisions/0001-solution-project-boundaries-v0.md"`
- `Get-Content -Raw "Plans/InProgress/thin-clients-first-solution-structure/decisions/0002-thin-clients-first-sequencing-v0.md"`

## Files Changed
- `Plans/InProgress/thin-clients-first-solution-structure/plan.md`
- `Plans/InProgress/thin-clients-first-solution-structure/steps/index.md`
- `Plans/InProgress/thin-clients-first-solution-structure/steps/0040-lock-prompting-ownership-policy/step.md`
- `Plans/InProgress/thin-clients-first-solution-structure/decisions/0001-solution-project-boundaries-v0.md`
- `Plans/InProgress/thin-clients-first-solution-structure/decisions/0004-prompting-ownership-and-versioning-policy-v0.md`
- `Plans/InProgress/thin-clients-first-solution-structure/artifacts/initial-solution-shape.md`

## Tests / Results
- `not-run` -> docs-only brainstorm step

## Issues
- none

## Decision
- Implementing applications are required to manage/version prompts and record prompt provenance through `Zelanthus.Prompting` shapes/tools.

## Completion
- `pending`

## Next Actions
- Carry this policy as a non-negotiable requirement into the Draft thin-clients implementation plan.

---

## Source 18: `steps/0050-lock-infrastructure-prompt-shape-mapping/step.md`

# Step: 0050-lock-infrastructure-prompt-shape-mapping

## Goal
- Explicitly lock how `Zelanthus.StoryEngine.Infrastructure` storage relates to `Zelanthus.Prompting` contract shapes.

## Context
- Story engine storage models may need database-optimized structures that are not 1:1 with prompting contracts.
- We still need deterministic transform/mapping to valid prompting shapes for execution and provenance workflows.

## Commands Executed
- `Get-Content -Raw "Plans/InProgress/thin-clients-first-solution-structure/plan.md"`
- `Get-Content -Raw "Plans/InProgress/thin-clients-first-solution-structure/decisions/0003-dependency-direction-rules-v0.md"`
- `Get-Content -Raw "Plans/InProgress/thin-clients-first-solution-structure/decisions/0004-prompting-ownership-and-versioning-policy-v0.md"`

## Files Changed
- `Plans/InProgress/thin-clients-first-solution-structure/plan.md`
- `Plans/InProgress/thin-clients-first-solution-structure/steps/index.md`
- `Plans/InProgress/thin-clients-first-solution-structure/steps/0050-lock-infrastructure-prompt-shape-mapping/step.md`
- `Plans/InProgress/thin-clients-first-solution-structure/decisions/0003-dependency-direction-rules-v0.md`
- `Plans/InProgress/thin-clients-first-solution-structure/decisions/0004-prompting-ownership-and-versioning-policy-v0.md`
- `Plans/InProgress/thin-clients-first-solution-structure/decisions/0005-infrastructure-prompt-shape-mapping-policy-v0.md`
- `Plans/InProgress/thin-clients-first-solution-structure/artifacts/initial-solution-shape.md`

## Tests / Results
- `not-run` -> docs-only brainstorm step

## Issues
- none

## Decision
- `Zelanthus.StoryEngine.Infrastructure` may persist prompt-related data in non-1:1 storage models, but must provide deterministic mapping/transform to/from `Zelanthus.Prompting` contract shapes used by runtime workflows.

## Completion
- `pending`

## Next Actions
- Carry this policy as a non-negotiable acceptance criterion in Draft plans for Infrastructure persistence.

---

## Source 19: `steps/0060-refine-decision-layer-responsibility-clarity/step.md`

# Step: 0060-refine-decision-layer-responsibility-clarity

## Goal
- Refine the full solution-structure decision set with deeper detail and explicit layer responsibility contracts.

## Context
- We identified ambiguity risk around where transformation responsibilities belong.
- Decision docs need enough specificity to prevent boundary drift during implementation.

## Commands Executed
- `Get-Content -Raw "Plans/InProgress/thin-clients-first-solution-structure/decisions/0001-solution-project-boundaries-v0.md"`
- `Get-Content -Raw "Plans/InProgress/thin-clients-first-solution-structure/decisions/0002-thin-clients-first-sequencing-v0.md"`
- `Get-Content -Raw "Plans/InProgress/thin-clients-first-solution-structure/decisions/0003-dependency-direction-rules-v0.md"`
- `Get-Content -Raw "Plans/InProgress/thin-clients-first-solution-structure/decisions/0004-prompting-ownership-and-versioning-policy-v0.md"`
- `Get-Content -Raw "Plans/InProgress/thin-clients-first-solution-structure/decisions/0005-infrastructure-prompt-shape-mapping-policy-v0.md"`

## Files Changed
- `Plans/InProgress/thin-clients-first-solution-structure/plan.md`
- `Plans/InProgress/thin-clients-first-solution-structure/steps/index.md`
- `Plans/InProgress/thin-clients-first-solution-structure/steps/0060-refine-decision-layer-responsibility-clarity/step.md`
- `Plans/InProgress/thin-clients-first-solution-structure/decisions/0001-solution-project-boundaries-v0.md`
- `Plans/InProgress/thin-clients-first-solution-structure/decisions/0002-thin-clients-first-sequencing-v0.md`
- `Plans/InProgress/thin-clients-first-solution-structure/decisions/0003-dependency-direction-rules-v0.md`
- `Plans/InProgress/thin-clients-first-solution-structure/decisions/0004-prompting-ownership-and-versioning-policy-v0.md`
- `Plans/InProgress/thin-clients-first-solution-structure/decisions/0005-infrastructure-prompt-shape-mapping-policy-v0.md`

## Tests / Results
- `not-run` -> docs-only brainstorm step

## Issues
- none

## Decision
- Publish a detailed decision set that clearly separates:
  - semantic transformation ownership (application),
  - provider protocol transformation ownership (adapter),
  - persistence transformation ownership (infrastructure).

## Completion
- `pending`

## Next Actions
- Carry this refined decision set into Draft implementation plans as explicit acceptance criteria.

---

## Source 20: `steps/0070-apply-external-review-refinements/step.md`

# Step: 0070-apply-external-review-refinements

## Goal
- Apply external-review refinements to strengthen decision clarity before Draft promotion.

## Context
- External review requested concrete guidance on:
  - call envelope ownership between Prompting and LLM abstractions,
  - early architecture test enforcement,
  - naming alignment policy,
  - thin MVP proving slice.

## Commands Executed
- `Get-Content -Raw "Plans/InProgress/thin-clients-first-solution-structure/decisions/0001-solution-project-boundaries-v0.md"`
- `Get-Content -Raw "Plans/InProgress/thin-clients-first-solution-structure/decisions/0002-thin-clients-first-sequencing-v0.md"`
- `Get-Content -Raw "Plans/InProgress/thin-clients-first-solution-structure/decisions/0003-dependency-direction-rules-v0.md"`
- `Get-Content -Raw "Plans/InProgress/thin-clients-first-solution-structure/decisions/0004-prompting-ownership-and-versioning-policy-v0.md"`
- `Get-Content -Raw "Plans/InProgress/thin-clients-first-solution-structure/decisions/0005-infrastructure-prompt-shape-mapping-policy-v0.md"`
 - `Get-Content -Raw "Plans/Brainstorms/backbone-doctrine-prompt-first-multi-provider/decisions/0004-mvp-artifact-and-failure-contract-v0.md"`

## Files Changed
- `Plans/README.md`
- `Plans/Templates/README.md`
- `Plans/Templates/plan-folder/plan.md`
- `Plans/Brainstorms/backbone-doctrine-prompt-first-multi-provider/plan.md`
- `Plans/Brainstorms/backbone-doctrine-prompt-first-multi-provider/steps/index.md`
- `Plans/Brainstorms/backbone-doctrine-prompt-first-multi-provider/decisions/0003-prompt-governance-and-provenance-v0.md`
- `Plans/Brainstorms/backbone-doctrine-prompt-first-multi-provider/decisions/0004-mvp-artifact-and-failure-contract-v0.md`
- `Plans/Brainstorms/backbone-doctrine-prompt-first-multi-provider/steps/0050-define-mvp-artifact-and-failure-contract/step.md`
- `Plans/InProgress/thin-clients-first-solution-structure/plan.md`
- `Plans/InProgress/thin-clients-first-solution-structure/steps/index.md`
- `Plans/InProgress/thin-clients-first-solution-structure/steps/0070-apply-external-review-refinements/step.md`
- `Plans/InProgress/thin-clients-first-solution-structure/decisions/0001-solution-project-boundaries-v0.md`
- `Plans/InProgress/thin-clients-first-solution-structure/decisions/0002-thin-clients-first-sequencing-v0.md`
- `Plans/InProgress/thin-clients-first-solution-structure/decisions/0003-dependency-direction-rules-v0.md`
- `Plans/InProgress/thin-clients-first-solution-structure/decisions/0004-prompting-ownership-and-versioning-policy-v0.md`
- `Plans/InProgress/thin-clients-first-solution-structure/decisions/0005-infrastructure-prompt-shape-mapping-policy-v0.md`
- `Plans/InProgress/thin-clients-first-solution-structure/decisions/0006-call-envelope-and-enforcement-policy-v0.md`
- `Plans/InProgress/thin-clients-first-solution-structure/artifacts/initial-solution-shape.md`

## Tests / Results
- `not-run` -> docs-only brainstorm step

## Issues
- none

## Decision
- Lock a concrete application call envelope and enforce boundary integrity via early architecture tests and naming policy.

## Completion
- `pending`

## Next Actions
- Use this refined decision set to create two Draft plans:
  - thin contracts/adapter implementation plan,
  - cognitive chain runner/artifact persistence plan.

---

## Source 21: `steps/0080-lock-mvp-goals-and-first-plan-boundary/step.md`

# Step: 0080-lock-mvp-goals-and-first-plan-boundary

## Goal
- Promote thin-clients planning from Brainstorms to Drafts and lock explicit MVP goals before harness-shape selection.

## Context
- Harness discussion is active, but goals must be fixed first so host-shape choices do not redefine success criteria.

## Git Branch
- `feature/thin-clients-first-solution-structure`

## Commits
- `none`

## Commands Executed
- `Move-Item -Path Plans/Brainstorms/thin-clients-first-solution-structure -Destination Plans/InProgress/thin-clients-first-solution-structure`
- `Get-Content "Plans/InProgress/thin-clients-first-solution-structure/plan.md"`
- `Get-Content "Plans/InProgress/thin-clients-first-solution-structure/steps/index.md"`

## Files Changed
- `Plans/InProgress/thin-clients-first-solution-structure/plan.md`
- `Plans/InProgress/thin-clients-first-solution-structure/steps/index.md`
- `Plans/InProgress/thin-clients-first-solution-structure/decisions/0007-mvp-goals-and-first-plan-boundary-v0.md`
- `Plans/InProgress/thin-clients-first-solution-structure/risks/risk-log.md`
- `Plans/InProgress/thin-clients-first-solution-structure/steps/0080-lock-mvp-goals-and-first-plan-boundary/step.md`

## Tests / Results
- `not-run` -> docs-only draft refinement step

## Issues
- none

## Decision
- Keep Plan 1 narrow and goal-driven; postpone harness host choice until criteria-driven step `0090`.

## Completion
- `pending`

## Next Actions
- Choose MVP harness shape using locked criteria in `0090-choose-mvp-harness-shape`.

---

## Source 22: `steps/0090-choose-mvp-harness-shape/step.md`

# Step: 0090-choose-mvp-harness-shape

## Goal
- Choose MVP harness shape (test-host vs API-host) against locked acceptance criteria.

## Context
- Harness shape must be selected by contract fit and execution reliability, not convenience.

## Git Branch
- `feature/thin-clients-first-solution-structure`

## Commits
- `none`

## Commands Executed
- `Get-Content "Plans/InProgress/thin-clients-first-solution-structure/decisions/0007-mvp-goals-and-first-plan-boundary-v0.md"`
- `Get-Content "Plans/InProgress/thin-clients-first-solution-structure/decisions/0006-call-envelope-and-enforcement-policy-v0.md"`
- `Get-Content "Plans/README.md"`

## Files Changed
- `Plans/InProgress/thin-clients-first-solution-structure/steps/0090-choose-mvp-harness-shape/step.md`
- `Plans/InProgress/thin-clients-first-solution-structure/steps/0090-choose-mvp-harness-shape/artifacts/harness-shape-evaluation-matrix.md`
- `Plans/InProgress/thin-clients-first-solution-structure/decisions/0008-mvp-harness-shape-selection-v0.md`
- `Plans/InProgress/thin-clients-first-solution-structure/steps/index.md`
- `Plans/InProgress/thin-clients-first-solution-structure/plan.md`

## Tests / Results
- `not-run` -> docs-only draft step

## Issues
- none

## Decision
- Select a dedicated test-host contract-proof project (`Tests/Zelanthus.WorkflowContractProofs.Tests`) for Plan 1.
- Defer API-host harness execution unless test-host criteria fail.

## Completion
- `pending`

## Next Actions
- Draft Plan 1 implementation details using the selected test-host harness baseline.

---

## Source 23: `steps/0100-draft-plan-1-prompting-and-gemini-contract-implementation/step.md`

# Step: 0100-draft-plan-1-prompting-and-gemini-contract-implementation

## Goal
- Produce implementation-ready Draft Plan 1 for Prompting, client abstractions, Gemini adapter, and architecture gate.

## Context
- Plan 1 must validate backbone contracts before broader story engine work.

## Git Branch
- `feature/thin-clients-first-solution-structure`

## Commits
- `none`

## Commands Executed
- `Copy-Item -Path "Plans/Templates/plan-folder" -Destination "Plans/InProgress/mvp-prompting-gemini-contract-baseline" -Recurse`
- `Get-Content "Plans/InProgress/mvp-prompting-gemini-contract-baseline/plan.md"`
- `Get-Content "Plans/InProgress/mvp-prompting-gemini-contract-baseline/steps/index.md"`

## Files Changed
- `Plans/InProgress/thin-clients-first-solution-structure/steps/0100-draft-plan-1-prompting-and-gemini-contract-implementation/step.md`
- `Plans/InProgress/thin-clients-first-solution-structure/steps/index.md`
- `Plans/InProgress/thin-clients-first-solution-structure/plan.md`
- `Plans/InProgress/mvp-prompting-gemini-contract-baseline/plan.md`
- `Plans/InProgress/mvp-prompting-gemini-contract-baseline/steps/index.md`
- `Plans/InProgress/mvp-prompting-gemini-contract-baseline/risks/risk-log.md`
- `Plans/InProgress/mvp-prompting-gemini-contract-baseline/steps/0010-scaffold-projects-and-references/step.md`
- `Plans/InProgress/mvp-prompting-gemini-contract-baseline/steps/0020-define-prompting-contracts-and-rendering-rules/step.md`
- `Plans/InProgress/mvp-prompting-gemini-contract-baseline/steps/0030-define-llm-client-abstractions-and-capability-profile/step.md`
- `Plans/InProgress/mvp-prompting-gemini-contract-baseline/steps/0040-implement-gemini-adapter-normalization-path/step.md`
- `Plans/InProgress/mvp-prompting-gemini-contract-baseline/steps/0050-add-architecture-boundary-tests/step.md`
- `Plans/InProgress/mvp-prompting-gemini-contract-baseline/steps/0060-implement-contract-proof-tests-and-local-artifact-persistence/step.md`
- `Plans/InProgress/mvp-prompting-gemini-contract-baseline/steps/0070-validate-golden-path-and-reason-coded-failure-path/step.md`

## Tests / Results
- `not-run` -> docs-only draft step

## Issues
- none

## Decision
- Use a dedicated Plan 1 draft folder (`mvp-prompting-gemini-contract-baseline`) so implementation details are isolated and promotion-ready.

## Completion
- `pending`

## Next Actions
- Draft Plan 2 with explicit dependencies on Plan 1 contracts and harness outputs.

---

## Source 24: `steps/0105-clarify-contract-proof-test-naming/step.md`

# Step: 0105-clarify-contract-proof-test-naming

## Goal
- Remove ambiguous harness naming and lock a semantic naming policy for long-lived contract-proof tests.

## Context
- `MvpHarness` is unclear over time and creates drift risk for future one-off proof tests.

## Git Branch
- `feature/thin-clients-first-solution-structure`

## Commits
- `none`

## Commands Executed
- `Move-Item "Plans/InProgress/mvp-prompting-gemini-contract-baseline/steps/0060-implement-mvp-harness-tests-and-local-artifact-persistence" "Plans/InProgress/mvp-prompting-gemini-contract-baseline/steps/0060-implement-contract-proof-tests-and-local-artifact-persistence"`
- `Get-Content "Plans/InProgress/thin-clients-first-solution-structure/decisions/0008-mvp-harness-shape-selection-v0.md"`
- `Get-Content "Plans/InProgress/mvp-prompting-gemini-contract-baseline/plan.md"`

## Files Changed
- `Plans/InProgress/thin-clients-first-solution-structure/steps/0105-clarify-contract-proof-test-naming/step.md`
- `Plans/InProgress/thin-clients-first-solution-structure/steps/index.md`
- `Plans/InProgress/thin-clients-first-solution-structure/plan.md`
- `Plans/InProgress/thin-clients-first-solution-structure/decisions/0008-mvp-harness-shape-selection-v0.md`
- `Plans/InProgress/thin-clients-first-solution-structure/decisions/0009-contract-proof-test-naming-policy-v0.md`
- `Plans/InProgress/thin-clients-first-solution-structure/steps/0090-choose-mvp-harness-shape/artifacts/harness-shape-evaluation-matrix.md`
- `Plans/InProgress/mvp-prompting-gemini-contract-baseline/plan.md`
- `Plans/InProgress/mvp-prompting-gemini-contract-baseline/steps/index.md`
- `Plans/InProgress/mvp-prompting-gemini-contract-baseline/steps/0060-implement-contract-proof-tests-and-local-artifact-persistence/step.md`
- `AGENTS.md`
- `Plans/README.md`

## Tests / Results
- `not-run` -> docs-only draft refinement step

## Issues
- none

## Decision
- Standardize on long-lived semantic project naming:
  - `Tests/Zelanthus.WorkflowContractProofs.Tests`
- Seed test names must encode contract surface, scenario, and expected outcome.

## Completion
- `pending`

## Next Actions
- Continue `0110` draft with naming policy treated as a dependency.

---

## Source 25: `steps/0110-draft-plan-2-cognitive-chain-runner-and-local-persistence/step.md`

# Step: 0110-draft-plan-2-cognitive-chain-runner-and-local-persistence

## Goal
- Produce implementation-ready Draft Plan 2 for chain-length-flexible `CognitiveChain`/`ConversationalChain` runner behavior and local artifact/provenance persistence.

## Context
- Plan 2 depends on Plan 1 contract outputs and should avoid premature production persistence setup.
- Plan 2 must not hard-lock runner flow to one pair; `PLAN_STEP` -> `EXECUTE` is minimum proof only.

## Git Branch
- `feature/thin-clients-first-solution-structure`

## Commits
- `none`

## Commands Executed
- `New-Item -ItemType Directory -Path Plans/InProgress/mvp-chain-runner-local-persistence-baseline`
- `python Plans/compile-plan.py Plans/InProgress/mvp-chain-runner-local-persistence-baseline`
- `python Plans/compile-plan.py Plans/InProgress/thin-clients-first-solution-structure`
- `python Plans/compile-plan.py Plans/Brainstorms/backbone-doctrine-prompt-first-multi-provider`
- `rg -n "PLAN_STEP|EXECUTE|single-pair" Plans -g "*.md"`

## Files Changed
- `Plans/InProgress/mvp-chain-runner-local-persistence-baseline/plan.md`
- `Plans/InProgress/mvp-chain-runner-local-persistence-baseline/steps/index.md`
- `Plans/InProgress/mvp-chain-runner-local-persistence-baseline/decisions/0001-chain-runner-execution-model-v0.md`
- `Plans/InProgress/mvp-chain-runner-local-persistence-baseline/decisions/0002-local-persistence-contract-and-layout-v0.md`
- `Plans/InProgress/mvp-chain-runner-local-persistence-baseline/decisions/0003-project-reference-graph-and-boundary-enforcement-v0.md`
- `Plans/InProgress/mvp-chain-runner-local-persistence-baseline/decisions/0004-retry-resume-and-reason-code-policy-v0.md`
- `Plans/InProgress/mvp-chain-runner-local-persistence-baseline/risks/risk-log.md`
- `Plans/InProgress/mvp-chain-runner-local-persistence-baseline/artifacts/project-dependency-implementation-matrix.md`
- `Plans/InProgress/mvp-chain-runner-local-persistence-baseline/validation/plan-2-acceptance-evidence-matrix.md`
- `Plans/InProgress/mvp-chain-runner-local-persistence-baseline/steps/0040-define-local-persistence-artifact-and-checkpoint-layout/artifacts/local-persistence-layout.md`
- `Plans/InProgress/mvp-chain-runner-local-persistence-baseline/steps/0060-define-retry-resume-and-reason-code-policy/artifacts/reason-code-and-retry-policy.md`
- `Plans/InProgress/mvp-chain-runner-local-persistence-baseline/steps/0070-plan-project-reference-graph-and-architecture-tests/artifacts/architecture-test-assertion-matrix.md`
- `Plans/InProgress/mvp-chain-runner-local-persistence-baseline/steps/0080-define-runner-proof-test-suite-and-evidence-artifacts/artifacts/runner-proof-test-seed.md`
- `Plans/InProgress/mvp-chain-runner-local-persistence-baseline/steps/0080-define-runner-proof-test-suite-and-evidence-artifacts/artifacts/runner-proof-evidence-spec.md`
- `Plans/InProgress/mvp-chain-runner-local-persistence-baseline/steps/0100-finalize-plan-2-acceptance-gates-and-promotion-readiness/artifacts/final-promotion-checklist.md`
- `Plans/InProgress/mvp-chain-runner-local-persistence-baseline/mvp-chain-runner-local-persistence-baseline.md`
- `Plans/InProgress/thin-clients-first-solution-structure/plan.md`
- `Plans/InProgress/thin-clients-first-solution-structure/decisions/0002-thin-clients-first-sequencing-v0.md`
- `Plans/InProgress/thin-clients-first-solution-structure/decisions/0007-mvp-goals-and-first-plan-boundary-v0.md`
- `Plans/InProgress/thin-clients-first-solution-structure/risks/risk-log.md`
- `Plans/Brainstorms/backbone-doctrine-prompt-first-multi-provider/decisions/0002-chain-continuity-strategy-v0.md`
- `Plans/InProgress/thin-clients-first-solution-structure/thin-clients-first-solution-structure.md`
- `Plans/Brainstorms/backbone-doctrine-prompt-first-multi-provider/backbone-doctrine-prompt-first-multi-provider.md`

## Tests / Results
- `python Plans/compile-plan.py Plans/InProgress/mvp-chain-runner-local-persistence-baseline` -> `pass`

## Issues
- none

## Decision
- Draft Plan 2 package created with explicit dependency implementation details, chain-mode flexibility contract, and local persistence boundaries.

## Completion
- `pending`

## Next Actions
- Review Draft Plan 2 and decide whether further refinement is needed before promotion consideration.

---

## Source 26: `steps/0120-execute-mvp-prompting-gemini-contract-baseline/step.md`

# Step: 0120-execute-mvp-prompting-gemini-contract-baseline

## Goal
- Execute Plan 1 (`mvp-prompting-gemini-contract-baseline`) and maintain implementation evidence in its InProgress folder.

## Context
- Plan 1 contracts are the required upstream baseline for Plan 2 execution.

## Git Branch
- `feature/thin-clients-first-solution-structure`

## Commits
- `a334401ddc2675bb6c004229750000bbd8e6a31f`
- `5e1e3a941954e24dece3f3a340f8cf12e2acc548`
- `37a32635a968954bdf2d775977e940ee766278f0`
- `f8454a38ae534c78691921c81d0f30fd4ae94ab7`

## Commands Executed
- `dotnet sln "Zelanthus.slnx" add "Source/Zelanthus.Prompting/Zelanthus.Prompting.csproj" "Source/Zelanthus.Llm.Clients.Abstractions/Zelanthus.Llm.Clients.Abstractions.csproj" "Source/Zelanthus.Llm.Clients.Gemini/Zelanthus.Llm.Clients.Gemini.csproj" "Tests/Zelanthus.Architecture.Tests/Zelanthus.Architecture.Tests.csproj" "Tests/Zelanthus.WorkflowContractProofs.Tests/Zelanthus.WorkflowContractProofs.Tests.csproj"`
- `dotnet add "Source/Zelanthus.Llm.Clients.Gemini/Zelanthus.Llm.Clients.Gemini.csproj" reference "Source/Zelanthus.Llm.Clients.Abstractions/Zelanthus.Llm.Clients.Abstractions.csproj"`
- `dotnet add "Tests/Zelanthus.WorkflowContractProofs.Tests/Zelanthus.WorkflowContractProofs.Tests.csproj" reference "Source/Zelanthus.Prompting/Zelanthus.Prompting.csproj" "Source/Zelanthus.Llm.Clients.Abstractions/Zelanthus.Llm.Clients.Abstractions.csproj" "Source/Zelanthus.Llm.Clients.Gemini/Zelanthus.Llm.Clients.Gemini.csproj"`
- `dotnet add "Tests/Zelanthus.Architecture.Tests/Zelanthus.Architecture.Tests.csproj" reference "Source/Zelanthus.API/Zelanthus.API.csproj" "Source/Zelanthus.Prompting/Zelanthus.Prompting.csproj" "Source/Zelanthus.Llm.Clients.Abstractions/Zelanthus.Llm.Clients.Abstractions.csproj" "Source/Zelanthus.Llm.Clients.Gemini/Zelanthus.Llm.Clients.Gemini.csproj"`
- `dotnet build "Zelanthus.slnx"`
- `dotnet test "Tests/Zelanthus.Architecture.Tests/Zelanthus.Architecture.Tests.csproj"`
- `dotnet test "Tests/Zelanthus.WorkflowContractProofs.Tests/Zelanthus.WorkflowContractProofs.Tests.csproj" --filter "Category!=LiveGemini"`
- `dotnet test "Zelanthus.slnx" --filter "Category!=LiveGemini"`

## Files Changed
- `Zelanthus.slnx`
- `Source/Zelanthus.Llm.Clients.Gemini/Zelanthus.Llm.Clients.Gemini.csproj`
- `Tests/Zelanthus.Architecture.Tests/Zelanthus.Architecture.Tests.csproj`
- `Tests/Zelanthus.Architecture.Tests/DependencyDirectionTests.cs`
- `Tests/Zelanthus.WorkflowContractProofs.Tests/Zelanthus.WorkflowContractProofs.Tests.csproj`
- `Tests/Zelanthus.WorkflowContractProofs.Tests/ContractProofArtifactWriter.cs`
- `Tests/Zelanthus.WorkflowContractProofs.Tests/GeminiAdapterContractProofTests.cs`
- `Tests/Zelanthus.WorkflowContractProofs.Tests/PromptRenderingContractProofTests.cs`
- `Tests/Zelanthus.WorkflowContractProofs.Tests/TestAssembly.cs`
- `Source/Zelanthus.Prompting/*`
- `Source/Zelanthus.Llm.Clients.Abstractions/*`
- `Source/Zelanthus.Llm.Clients.Gemini/*`
- `Plans/InProgress/mvp-prompting-gemini-contract-baseline/plan.md`
- `Plans/InProgress/mvp-prompting-gemini-contract-baseline/steps/index.md`
- `Plans/InProgress/mvp-prompting-gemini-contract-baseline/steps/0010-scaffold-projects-and-references/step.md`
- `Plans/InProgress/mvp-prompting-gemini-contract-baseline/steps/0020-define-prompting-contracts-and-rendering-rules/step.md`
- `Plans/InProgress/mvp-prompting-gemini-contract-baseline/steps/0030-define-llm-client-abstractions-and-capability-profile/step.md`
- `Plans/InProgress/mvp-prompting-gemini-contract-baseline/steps/0040-implement-gemini-adapter-normalization-path/step.md`
- `Plans/InProgress/mvp-prompting-gemini-contract-baseline/steps/0050-add-architecture-boundary-tests/step.md`
- `Plans/InProgress/mvp-prompting-gemini-contract-baseline/steps/0060-implement-contract-proof-tests-and-local-artifact-persistence/step.md`
- `Plans/InProgress/mvp-prompting-gemini-contract-baseline/steps/0070-validate-golden-path-and-reason-coded-failure-path/step.md`
- `Plans/InProgress/mvp-prompting-gemini-contract-baseline/steps/0070-validate-golden-path-and-reason-coded-failure-path/validation/final-acceptance-checklist.md`

## Tests / Results
- `dotnet build "Zelanthus.slnx"` -> `passed` (0 warnings, 0 errors)
- `dotnet test "Tests/Zelanthus.Architecture.Tests/Zelanthus.Architecture.Tests.csproj"` -> `passed` (4 tests)
- `dotnet test "Tests/Zelanthus.WorkflowContractProofs.Tests/Zelanthus.WorkflowContractProofs.Tests.csproj" --filter "Category!=LiveGemini"` -> `passed` (7 tests)
- `dotnet test "Zelanthus.slnx" --filter "Category!=LiveGemini"` -> `passed` (11 tests)

## Issues
- none

## Decision
- accepted: Plan 1 execution is complete with all step gates, proof tests, and acceptance evidence validated.

## Completion
- `completed`

## Next Actions
- Execute `Plans/InProgress/mvp-chain-runner-local-persistence-baseline` step sequence under top-level step `0130`.

---

## Source 27: `steps/0130-execute-mvp-chain-runner-local-persistence-baseline/step.md`

# Step: 0130-execute-mvp-chain-runner-local-persistence-baseline

## Goal
- Execute Plan 2 (`mvp-chain-runner-local-persistence-baseline`) after Plan 1 completion gates are satisfied.

## Context
- Plan 2 depends on Plan 1 prompting/client contract outputs and must reuse the same feature branch traceability chain.

## Git Branch
- `feature/thin-clients-first-solution-structure`

## Commits
- `952c43f705d35adf53fe53bcd7f6c645cfb6f367`
- `d1a05f72f904cd5d2081272d3627ec0f3a105620`
- `e32c94d7f5df2c8a13c420f1645f9f4f8d2e6683`

## Commands Executed
- `dotnet new classlib -n Zelanthus.StoryEngine.Domain -f net10.0 -o "Source/Zelanthus.StoryEngine.Domain"`
- `dotnet new classlib -n Zelanthus.StoryEngine.Application -f net10.0 -o "Source/Zelanthus.StoryEngine.Application"`
- `dotnet new classlib -n Zelanthus.StoryEngine.Infrastructure -f net10.0 -o "Source/Zelanthus.StoryEngine.Infrastructure"`
- `dotnet sln "Zelanthus.slnx" add "Source/Zelanthus.StoryEngine.Domain/Zelanthus.StoryEngine.Domain.csproj" "Source/Zelanthus.StoryEngine.Application/Zelanthus.StoryEngine.Application.csproj" "Source/Zelanthus.StoryEngine.Infrastructure/Zelanthus.StoryEngine.Infrastructure.csproj"`
- `dotnet add "Source/Zelanthus.StoryEngine.Application/Zelanthus.StoryEngine.Application.csproj" reference "Source/Zelanthus.StoryEngine.Domain/Zelanthus.StoryEngine.Domain.csproj" "Source/Zelanthus.Prompting/Zelanthus.Prompting.csproj" "Source/Zelanthus.Llm.Clients.Abstractions/Zelanthus.Llm.Clients.Abstractions.csproj"`
- `dotnet add "Source/Zelanthus.StoryEngine.Infrastructure/Zelanthus.StoryEngine.Infrastructure.csproj" reference "Source/Zelanthus.StoryEngine.Domain/Zelanthus.StoryEngine.Domain.csproj" "Source/Zelanthus.StoryEngine.Application/Zelanthus.StoryEngine.Application.csproj" "Source/Zelanthus.Prompting/Zelanthus.Prompting.csproj" "Source/Zelanthus.Llm.Clients.Abstractions/Zelanthus.Llm.Clients.Abstractions.csproj"`
- `dotnet add "Source/Zelanthus.API/Zelanthus.API.csproj" reference "Source/Zelanthus.StoryEngine.Application/Zelanthus.StoryEngine.Application.csproj" "Source/Zelanthus.StoryEngine.Infrastructure/Zelanthus.StoryEngine.Infrastructure.csproj" "Source/Zelanthus.Llm.Clients.Gemini/Zelanthus.Llm.Clients.Gemini.csproj"`
- `dotnet add "Tests/Zelanthus.Architecture.Tests/Zelanthus.Architecture.Tests.csproj" reference "Source/Zelanthus.StoryEngine.Domain/Zelanthus.StoryEngine.Domain.csproj" "Source/Zelanthus.StoryEngine.Application/Zelanthus.StoryEngine.Application.csproj" "Source/Zelanthus.StoryEngine.Infrastructure/Zelanthus.StoryEngine.Infrastructure.csproj"`
- `dotnet build "Zelanthus.slnx"`
- `dotnet test "Tests/Zelanthus.Architecture.Tests/Zelanthus.Architecture.Tests.csproj"`
- `dotnet test "Zelanthus.slnx" --filter "Category!=LiveGemini"`
- `dotnet build "Zelanthus.slnx"`
- `dotnet test "Tests/Zelanthus.WorkflowContractProofs.Tests/Zelanthus.WorkflowContractProofs.Tests.csproj" --filter "Category!=LiveGemini"`
- `dotnet test "Tests/Zelanthus.Architecture.Tests/Zelanthus.Architecture.Tests.csproj"`
- `dotnet test "Zelanthus.slnx" --filter "Category!=LiveGemini"`

## Files Changed
- `Zelanthus.slnx`
- `Source/Zelanthus.API/Zelanthus.API.csproj`
- `Source/Zelanthus.StoryEngine.Domain/*`
- `Source/Zelanthus.StoryEngine.Application/*`
- `Source/Zelanthus.StoryEngine.Infrastructure/*`
- `Tests/Zelanthus.Architecture.Tests/Zelanthus.Architecture.Tests.csproj`
- `Tests/Zelanthus.Architecture.Tests/DependencyDirectionTests.cs`
- `Plans/InProgress/mvp-chain-runner-local-persistence-baseline/plan.md`
- `Plans/InProgress/mvp-chain-runner-local-persistence-baseline/steps/index.md`
- `Plans/InProgress/mvp-chain-runner-local-persistence-baseline/steps/0010-lock-plan-2-scope-and-contract-dependencies/step.md`
- `Plans/InProgress/mvp-chain-runner-local-persistence-baseline/steps/0020-define-chain-runner-domain-and-state-model/step.md`
- `Plans/InProgress/mvp-chain-runner-local-persistence-baseline/steps/0025-define-minimal-workflow-abstraction-hooks/step.md`
- `Plans/InProgress/mvp-chain-runner-local-persistence-baseline/steps/0030-define-application-orchestration-flow-and-chain-router/step.md`
- `Plans/InProgress/mvp-chain-runner-local-persistence-baseline/steps/0040-define-local-persistence-artifact-and-checkpoint-layout/step.md`
- `Plans/InProgress/mvp-chain-runner-local-persistence-baseline/steps/0050-define-infrastructure-mappers-and-store-contracts/step.md`
- `Plans/InProgress/mvp-chain-runner-local-persistence-baseline/steps/0060-define-retry-resume-and-reason-code-policy/step.md`
- `Plans/InProgress/mvp-chain-runner-local-persistence-baseline/steps/0070-plan-project-reference-graph-and-architecture-tests/step.md`
- `Plans/InProgress/mvp-chain-runner-local-persistence-baseline/steps/0080-define-runner-proof-test-suite-and-evidence-artifacts/step.md`
- `Plans/InProgress/mvp-chain-runner-local-persistence-baseline/steps/0090-define-api-composition-boundaries-and-host-integration/step.md`
- `Plans/InProgress/mvp-chain-runner-local-persistence-baseline/steps/0100-finalize-plan-2-acceptance-gates-and-promotion-readiness/step.md`
- `Plans/InProgress/mvp-chain-runner-local-persistence-baseline/steps/0100-finalize-plan-2-acceptance-gates-and-promotion-readiness/validation/final-promotion-checklist.md`
- `Plans/InProgress/mvp-chain-runner-local-persistence-baseline/validation/plan-2-acceptance-evidence-matrix.md`
- `Tests/Zelanthus.WorkflowContractProofs.Tests/Zelanthus.WorkflowContractProofs.Tests.csproj`
- `Tests/Zelanthus.WorkflowContractProofs.Tests/ChainRunnerContractProofTests.cs`

## Tests / Results
- `dotnet build "Zelanthus.slnx"` -> `passed` (0 warnings, 0 errors)
- `dotnet test "Tests/Zelanthus.Architecture.Tests/Zelanthus.Architecture.Tests.csproj"` -> `passed` (7 tests)
- `dotnet test "Zelanthus.slnx" --filter "Category!=LiveGemini"` -> `passed` (14 tests)
- `dotnet test "Tests/Zelanthus.WorkflowContractProofs.Tests/Zelanthus.WorkflowContractProofs.Tests.csproj" --filter "Category!=LiveGemini"` -> `passed` (28 tests)
- `dotnet test "Zelanthus.slnx" --filter "Category!=LiveGemini"` -> `passed` (35 tests)

## Issues
- none

## Decision
- accepted: Plan 2 execution is complete with runner contracts, local persistence contracts, architecture boundary gates, reason-code policies, proof-suite coverage, and finalized promotion checklist artifacts.

## Completion
- `completed`

## Next Actions
- Proceed to top-level step `0140-prepare-closeout-and-pr-handoff`.

---

## Source 28: `steps/0140-prepare-closeout-and-pr-handoff/step.md`

# Step: 0140-prepare-closeout-and-pr-handoff

## Goal
- Prepare completion package, cleanup plan, and explicit PR-ready handoff for user-owned PR creation/merge.

## Context
- Completion requires deterministic packaging (`Completed`), separate cleanup commit planning, and clear user handoff.

## Git Branch
- `feature/thin-clients-first-solution-structure`

## Commits
- `a334401ddc2675bb6c004229750000bbd8e6a31f`
- `5e1e3a941954e24dece3f3a340f8cf12e2acc548`
- `37a32635cc871a343af589e8f6af21b9ab56934a`
- `f8454a34cea9b3e00e18fae294a06294ee1cedeb`
- `11ae188c0d76e0e6b6f7d5677cad120d57388013`
- `952c43f8937ac0b96a2277d52afc3b1e1af3a38f`
- `d1a05f7c732512a01f39326adf2869af66271198`
- `e32c94daa65943bf2d9cbfdb787ef113708b334d`
- `e97278a565b66adf81c32fc58e0509f2218f5902`

## Commands Executed
- `git merge-base main HEAD`
- `git log --reverse --pretty=format:"%H|%s" main..HEAD`
- `git log --reverse --pretty=format:"===%H|%s" --name-only main..HEAD`

## Files Changed
- `Plans/InProgress/thin-clients-first-solution-structure/steps/0140-prepare-closeout-and-pr-handoff/step.md`
- `Plans/InProgress/thin-clients-first-solution-structure/steps/0140-prepare-closeout-and-pr-handoff/notes/closeout-traceability-prep.md`
- `Plans/InProgress/thin-clients-first-solution-structure/steps/0140-prepare-closeout-and-pr-handoff/validation/pr-ready-handoff-checklist.md`
- `Plans/InProgress/thin-clients-first-solution-structure/steps/0140-prepare-closeout-and-pr-handoff/notes/completed-package-and-cleanup-sequence.md`
- `Plans/InProgress/thin-clients-first-solution-structure/validation/top-level-acceptance-evidence.md`
- `Plans/InProgress/thin-clients-first-solution-structure/steps/index.md`
- `Plans/InProgress/thin-clients-first-solution-structure/plan.md`

## Tests / Results
- `not-run` -> docs/planning-only closeout preparation step

## Issues
- none

## Decision
- accepted: closeout traceability, PR-ready checklist, and closure sequencing are prepared and execution evidence is fully mapped.

## Completion
- `completed`

## Next Actions
- Signal PR-ready handoff to user with closure prep artifacts and wait for user-owned PR creation/merge.

---

## Source 29: `steps/0090-choose-mvp-harness-shape/artifacts/harness-shape-evaluation-matrix.md`

# Harness Shape Evaluation Matrix

## Scope
- Plan: `thin-clients-first-solution-structure`
- Step: `0090-choose-mvp-harness-shape`

## Criteria
- C1: setup effort and execution speed
- C2: deterministic repeatability
- C3: contract-proving fidelity for Plan 1 scope
- C4: failure diagnostics and evidence capture
- C5: alignment to current non-goals (no early deployment/runtime sprawl)

## Options
| Option | C1 | C2 | C3 | C4 | C5 | Outcome |
|---|---|---|---|---|---|---|
| Test-host (`Tests/Zelanthus.WorkflowContractProofs.Tests`) | strong | strong | strong | strong | strong | selected |
| API-host (`Source/Zelanthus.API` endpoint + integration tests) | moderate | moderate | strong | moderate | weak | deferred |

## Decision Notes
- Test-host is sufficient for Plan 1 contract proving and avoids API transport overhead.
- API-host remains a follow-up path if test-host cannot prove required lifecycle fidelity.

---

## Source 30: `steps/0140-prepare-closeout-and-pr-handoff/validation/pr-ready-handoff-checklist.md`

# PR-Ready Handoff Checklist

## Pre-PR Readiness (Must Be True Before User PR Creation)
- [x] Plan 1 execution complete and evidence captured.
- [x] Plan 2 execution complete and evidence captured.
- [x] Top-level execution coordination steps `0120` and `0130` marked `completed`.
- [x] Closeout traceability prep artifact created:
  - `Plans/InProgress/thin-clients-first-solution-structure/steps/0140-prepare-closeout-and-pr-handoff/notes/closeout-traceability-prep.md`
- [x] Completion and cleanup sequence documented:
  - `Plans/InProgress/thin-clients-first-solution-structure/steps/0140-prepare-closeout-and-pr-handoff/notes/completed-package-and-cleanup-sequence.md`
- [x] Branch state clean and reviewable (`feature/thin-clients-first-solution-structure`).
- [x] PR ownership remains user-owned manual flow (`agent prepares, user opens/merges PR`).

## Closure Actions Planned (Executed As Separate Closure Commits)
- [ ] Commit A: create `Plans/Completed/<plan-slug>/` package(s) with `draft-baseline/`, `implementation/`, `closeout/`.
- [ ] Commit B: cleanup active stage folders (`Plans/Drafts/<plan-slug>/`, `Plans/InProgress/<plan-slug>/`) per `Plans/README.md`.
- [ ] Populate merge, completed-package, and cleanup SHAs in closure traceability docs.

## Owner Notes
- User creates and merges the PR manually.
- Agent should run closure transition commits only when explicitly requested.

---

## Source 31: `steps/0140-prepare-closeout-and-pr-handoff/notes/closeout-traceability-prep.md`

# Closeout Traceability Prep

## Intent
- Capture the execution-to-evidence chain needed for final `InProgress -> Completed` closure.
- Prepare deterministic SHA mapping and PR handoff context without mutating stage folders yet.

## Baseline and Branch References
- Source draft path: `Plans/Drafts/thin-clients-first-solution-structure`
- Source draft commit SHA (frozen baseline): `cc49173737d3b767d9758f3e3706316bd5d93f05`
- Promotion commit SHA (`Drafts -> InProgress`): `e38ed07d2c51bd640499b13131c9d8e96abbedab`
- Feature branch base commit SHA: `e38ed07d2c51bd640499b13131c9d8e96abbedab`
- Active branch: `feature/thin-clients-first-solution-structure`

## Execution Commit Set (`main..HEAD`)
- `17a7dfae74477ffa2c2e5dddd4003989bf02525b` -> planning refinement gate for Plan 2 persistence/name consistency.
- `a334401ddc2675bb6c004229750000bbd8e6a31f` -> Plan 1 scaffolding and baseline references.
- `5e1e3a941954e24dece3f3a340f8cf12e2acc548` -> Plan 1 architecture boundary tests.
- `37a32635cc871a343af589e8f6af21b9ab56934a` -> Plan 1 prompting/abstractions/gemini contracts.
- `f8454a34cea9b3e00e18fae294a06294ee1cedeb` -> Plan 1 contract proof tests and acceptance validation.
- `11ae188c0d76e0e6b6f7d5677cad120d57388013` -> Plan 1 step SHA/evidence documentation update.
- `952c43f8937ac0b96a2277d52afc3b1e1af3a38f` -> Plan 2 StoryEngine scaffolding and boundary gates.
- `d1a05f7c732512a01f39326adf2869af66271198` -> Plan 2 initial step SHA/evidence documentation update.
- `e32c94daa65943bf2d9cbfdb787ef113708b334d` -> Plan 2 reason policy/proof suite implementation.
- `e97278a565b66adf81c32fc58e0509f2218f5902` -> Plan 2 final step SHA/evidence documentation update.

## Traceability Map
- Top-level step `0120-execute-mvp-prompting-gemini-contract-baseline`
  - Execution evidence:
    - `Plans/InProgress/mvp-prompting-gemini-contract-baseline/steps/0070-validate-golden-path-and-reason-coded-failure-path/validation/final-acceptance-checklist.md`
    - `Plans/InProgress/thin-clients-first-solution-structure/steps/0120-execute-mvp-prompting-gemini-contract-baseline/step.md`
- Top-level step `0130-execute-mvp-chain-runner-local-persistence-baseline`
  - Execution evidence:
    - `Plans/InProgress/mvp-chain-runner-local-persistence-baseline/steps/0100-finalize-plan-2-acceptance-gates-and-promotion-readiness/validation/final-promotion-checklist.md`
    - `Plans/InProgress/mvp-chain-runner-local-persistence-baseline/validation/plan-2-acceptance-evidence-matrix.md`
    - `Plans/InProgress/thin-clients-first-solution-structure/steps/0130-execute-mvp-chain-runner-local-persistence-baseline/step.md`
- Top-level step `0140-prepare-closeout-and-pr-handoff`
  - Closeout prep artifacts:
    - `Plans/InProgress/thin-clients-first-solution-structure/steps/0140-prepare-closeout-and-pr-handoff/notes/closeout-traceability-prep.md`
    - `Plans/InProgress/thin-clients-first-solution-structure/steps/0140-prepare-closeout-and-pr-handoff/validation/pr-ready-handoff-checklist.md`
    - `Plans/InProgress/thin-clients-first-solution-structure/steps/0140-prepare-closeout-and-pr-handoff/notes/completed-package-and-cleanup-sequence.md`

## Required Closure SHA Fields (Prepared)
- Merge commit SHA (`feature -> main`): `pending-user-pr-merge`
- Completed-package commit SHA: `pending-completed-package-commit`
- Cleanup commit SHA: `pending-cleanup-commit`

## Completion Packaging Targets (Prepared)
- `Plans/Completed/thin-clients-first-solution-structure/`
- `Plans/Completed/mvp-prompting-gemini-contract-baseline/`
- `Plans/Completed/mvp-chain-runner-local-persistence-baseline/`

## Notes
- This artifact is preparation evidence for PR-ready handoff.
- Final closure SHAs are populated when `Completed` packaging and cleanup commits are executed.

---

## Source 32: `steps/0140-prepare-closeout-and-pr-handoff/notes/completed-package-and-cleanup-sequence.md`

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

---

## Source 33: `artifacts/initial-solution-shape.md`

# Initial Solution Shape (Brainstorm v0)

## Current Baseline
- Existing solution entry: `Zelanthus.API`

## Target Logical Packages
- `Zelanthus.Prompting`
  - Template registry/composition/rendering/fingerprinting/provenance helpers.
  - Required integration boundary for implementing applications to manage and version prompts.
- `Zelanthus.Llm.Clients.Abstractions`
  - Provider-agnostic request/response contracts, capability profile, normalized errors, token usage contracts.
- `Zelanthus.Llm.Clients.Gemini`
  - Gemini implementation of abstractions (first provider target).
- `Zelanthus.StoryEngine.Domain`
  - Core story domain entities, value objects, invariants.
- `Zelanthus.StoryEngine.Application`
  - Workflow/phase orchestration contracts and use-cases.
- `Zelanthus.StoryEngine.Infrastructure`
  - Persistence/artifact stores and runtime adapters that consume abstractions.
  - May use persistence schemas that differ from `Zelanthus.Prompting` contract shapes.
  - Must own deterministic mapping/transform to and from prompting/runtime contract shapes.
- `Zelanthus.API`
  - Composition root and external API surface.

## Why This Shape
- Keeps provider-specific logic outside story engine.
- Keeps prompt system reusable and testable as a first-class boundary.
- Supports prompt-first workflows without requiring chat subsystem coupling.

## Prompt Ownership Rule
- Implementing applications do not own ad-hoc prompt mechanics.
- Implementing applications own prompt content packs, but they must manage and execute those prompts through `Zelanthus.Prompting` contracts/tooling.

## Infrastructure Mapping Rule
- Storage representation and runtime prompt representation are allowed to differ.
- Required prompt/provenance fields must be mappable without silent data loss.

## Canonical Call Envelope Rule
- Application builds `RenderedPrompt` + `ExecutionContext`.
- Application invokes `ILlmClient` with a single canonical `ExecutionEnvelope`.
- Adapter returns normalized response metadata and raw snapshot references.
- Application composes final `ProvenanceRecord`; Infrastructure persists it.

## Naming Alignment Rule
- Runtime projects and namespaces use `Zelanthus.*`.
- `BookForge` is preserved as reference context in documentation only.

---

## Source 34: `promotion.md`

# Promotion Trace

## Source
- Source draft path: Plans/Drafts/thin-clients-first-solution-structure
- Source commit SHA: cc49173737d3b767d9758f3e3706316bd5d93f05
- Promotion date: 2026-02-21

## Transformation Summary
- Copied Draft plan folder to InProgress as promotion baseline.
- Reset execution tracking surfaces for InProgress workflow semantics.
- Updated branch tracking for execution on feature/thin-clients-first-solution-structure.

## Notes
- Draft folder remains frozen baseline for planning audit.
- InProgress folder is execution-authoritative for implementation tracking.
