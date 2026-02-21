# thin-clients-first-solution-structure

## Compiled Plan Metadata

- Plan Scope: `Drafts/thin-clients-first-solution-structure`
- Compiled At (UTC): `2026-02-21T06:02:38Z`
- Source Document Count: `27`
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
12. `steps/index.md`
13. `steps/0010-map-solution-project-boundaries/step.md`
14. `steps/0020-plan-thin-clients-package-first/step.md`
15. `steps/0030-sequence-story-engine-dependencies/step.md`
16. `steps/0040-lock-prompting-ownership-policy/step.md`
17. `steps/0050-lock-infrastructure-prompt-shape-mapping/step.md`
18. `steps/0060-refine-decision-layer-responsibility-clarity/step.md`
19. `steps/0070-apply-external-review-refinements/step.md`
20. `steps/0080-lock-mvp-goals-and-first-plan-boundary/step.md`
21. `steps/0090-choose-mvp-harness-shape/step.md`
22. `steps/0100-draft-plan-1-prompting-and-gemini-contract-implementation/step.md`
23. `steps/0105-clarify-contract-proof-test-naming/step.md`
24. `steps/0110-draft-plan-2-cognitive-chain-runner-and-local-persistence/step.md`
25. `steps/0090-choose-mvp-harness-shape/artifacts/harness-shape-evaluation-matrix.md`
26. `archive-note.md`
27. `artifacts/initial-solution-shape.md`

---

## Source 1: `plan.md`

# thin-clients-first-solution-structure

## Objective
- Promote accepted thin-client brainstorm outcomes into an implementation-ready Draft package.
- Lock MVP goals and first-plan boundaries before selecting harness transport/host details.

## Scope
- In:
  - MVP goals for prompt contracts, client abstractions, Gemini adapter, and provenance/failure correctness.
  - First-plan boundary for thin implementation work (Plan 1) and explicit non-goals.
  - Harness requirements and decision criteria, without prematurely locking API-host vs test-host.
  - Draft sequencing for Plan 1 and Plan 2 handoff.
- Out:
  - Full story engine domain/application breadth.
  - Production persistence setup (Postgres) and deployment topology.
  - Frontend/chat experience concerns.
  - Prompt sub-template/import composition design and implementation (deferred follow-up scope).

## Definition of Done
- Decision `0007-mvp-goals-and-first-plan-boundary-v0.md` is accepted.
- Decision `0008-mvp-harness-shape-selection-v0.md` is accepted.
- Decision `0009-contract-proof-test-naming-policy-v0.md` is accepted.
- MVP goals are explicit, testable, and mapped to concrete acceptance evidence.
- First-plan scope, non-goals, and deliverables are explicit.
- Harness decision criteria are explicit enough to choose execution shape without redefining goals.
- Draft Plan 1 package exists and is linked for downstream implementation planning.
- Draft step sequencing is explicit for:
  - Plan 1: Prompting + abstractions + Gemini baseline.
  - Plan 2: CognitiveChain/ConversationalChain runner + local artifact/provenance persistence (minimum proof includes one `T1` -> `T2` pair, but runner is not capped at two turns).
- Thin-clients draft risk log exists and captures current high-risk uncertainties.

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

## Touchpoint Map
- Code:
  - `Zelanthus.slnx` (current baseline reference)
  - `Source/Zelanthus.API` (current host baseline reference only)
- Docs:
  - `Plans/Drafts/thin-clients-first-solution-structure/plan.md`
  - `Plans/Drafts/thin-clients-first-solution-structure/decisions/0001-solution-project-boundaries-v0.md`
  - `Plans/Drafts/thin-clients-first-solution-structure/decisions/0002-thin-clients-first-sequencing-v0.md`
  - `Plans/Drafts/thin-clients-first-solution-structure/decisions/0003-dependency-direction-rules-v0.md`
  - `Plans/Drafts/thin-clients-first-solution-structure/decisions/0004-prompting-ownership-and-versioning-policy-v0.md`
  - `Plans/Drafts/thin-clients-first-solution-structure/decisions/0005-infrastructure-prompt-shape-mapping-policy-v0.md`
  - `Plans/Drafts/thin-clients-first-solution-structure/decisions/0006-call-envelope-and-enforcement-policy-v0.md`
  - `Plans/Drafts/thin-clients-first-solution-structure/decisions/0007-mvp-goals-and-first-plan-boundary-v0.md`
  - `Plans/Drafts/thin-clients-first-solution-structure/decisions/0008-mvp-harness-shape-selection-v0.md`
  - `Plans/Drafts/thin-clients-first-solution-structure/decisions/0009-contract-proof-test-naming-policy-v0.md`
  - `Plans/Drafts/thin-clients-first-solution-structure/artifacts/initial-solution-shape.md`
  - `Plans/Drafts/thin-clients-first-solution-structure/risks/risk-log.md`
  - `Plans/Drafts/mvp-prompting-gemini-contract-baseline/plan.md`
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
  - Draft Plan 1 is prepared; keep it in `Drafts/` until explicit promotion approval.
  - Draft Plan 2 using Plan 1 outputs and harness decision `0008` as locked dependencies.
  - Do not promote any draft to `InProgress` without explicit user approval.
- Rollback:
  - Supersede with revised draft and archive this plan folder if replaced.

## Status Tracker
- [x] `0010-map-solution-project-boundaries`
- [x] `0020-plan-thin-clients-package-first`
- [x] `0030-sequence-story-engine-dependencies`
- [x] `0040-lock-prompting-ownership-policy`
- [x] `0050-lock-infrastructure-prompt-shape-mapping`
- [x] `0060-refine-decision-layer-responsibility-clarity`
- [x] `0070-apply-external-review-refinements`
- [x] `0080-lock-mvp-goals-and-first-plan-boundary`
- [x] `0090-choose-mvp-harness-shape`
- [x] `0100-draft-plan-1-prompting-and-gemini-contract-implementation`
- [x] `0105-clarify-contract-proof-test-naming`
- [ ] `0110-draft-plan-2-cognitive-chain-runner-and-local-persistence`

## Notes
- Long-term production persistence target is Postgres, but this draft keeps persistence local and minimal to avoid out-of-order setup.
- Plan 2 runner design is chain-length-flexible:
  - `CognitiveChain` can execute multiple planned turns/steps (not only two).
  - `ConversationalChain` supports multi-turn progression using explicit persisted turn artifacts.

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
  - one minimal runner proving at least one `CognitiveChain` step pair (`T1` + `T2`) with persisted artifacts/provenance.
- Clarification:
  - `T1` + `T2` is the minimum acceptance proof path, not a runner turn-limit.
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
- Must execute a deterministic `CognitiveChain` style proof path (`T1` then `T2`) for at least one representative step.
- `T1` -> `T2` is a minimum proof path, not a hard cap on total turns.
- Plan 2 runner design must support variable-length chains for:
  - `CognitiveChain` (multi-step planning/execution with thought-aware checkpoints),
  - `ConversationalChain` (multi-turn chat-style flow with explicit turn artifacts).
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

## R-005 Two-Turn Lock-In Risk
- Statement: Treating `T1` -> `T2` as a fixed runner design (instead of minimum proof path) can block required multi-step chain workflows.
- Impact: Early rework in Plan 2 runner orchestration and persistence shape.
- Mitigation: Explicitly require chain-length-flexible runner contracts for both `CognitiveChain` and `ConversationalChain` in Plan 2 draft.
- Status: open

---

## Source 12: `steps/index.md`

# Steps Index

Use this file for dependency and status summaries.
Do not use this file as the only source of step order.
Step order must still be visible in folder names (`NNNN-semantic-step-name`).

## Step Map
| Order | Step Folder | Status | Depends On | Notes |
|---|---|---|---|---|
| 0010 | `0010-map-solution-project-boundaries` | `completed` | `none` | Defined initial package boundaries for API, story engine, prompting, and clients |
| 0020 | `0020-plan-thin-clients-package-first` | `completed` | `0010-map-solution-project-boundaries` | Defined thin clients as first implementation planning target |
| 0030 | `0030-sequence-story-engine-dependencies` | `completed` | `0010-map-solution-project-boundaries` | Defined allowed dependency direction and implementation order |
| 0040 | `0040-lock-prompting-ownership-policy` | `completed` | `0010-map-solution-project-boundaries` | Locked requirement that implementing apps use `Zelanthus.Prompting` for prompt management/versioning/provenance |
| 0050 | `0050-lock-infrastructure-prompt-shape-mapping` | `completed` | `0030-sequence-story-engine-dependencies`, `0040-lock-prompting-ownership-policy` | Locked policy that Infrastructure storage models may differ but must map/transform to prompting contracts |
| 0060 | `0060-refine-decision-layer-responsibility-clarity` | `completed` | `0010-map-solution-project-boundaries`, `0030-sequence-story-engine-dependencies`, `0040-lock-prompting-ownership-policy`, `0050-lock-infrastructure-prompt-shape-mapping` | Expanded all solution-structure decisions with detailed boundaries and explicit transform ownership by layer |
| 0070 | `0070-apply-external-review-refinements` | `completed` | `0060-refine-decision-layer-responsibility-clarity` | Applied external review refinements for call envelope, MVP proving slice, architecture test gate, and naming alignment |
| 0080 | `0080-lock-mvp-goals-and-first-plan-boundary` | `completed` | `0070-apply-external-review-refinements` | Promoted plan to Draft and locked MVP goals, first-plan boundaries, and risk baseline |
| 0090 | `0090-choose-mvp-harness-shape` | `completed` | `0080-lock-mvp-goals-and-first-plan-boundary` | Selected test-host baseline (`Tests/Zelanthus.WorkflowContractProofs.Tests`); API-host deferred by criteria |
| 0100 | `0100-draft-plan-1-prompting-and-gemini-contract-implementation` | `completed` | `0090-choose-mvp-harness-shape` | Created `mvp-prompting-gemini-contract-baseline` draft plan package with explicit scope, DoD, and step map |
| 0105 | `0105-clarify-contract-proof-test-naming` | `completed` | `0100-draft-plan-1-prompting-and-gemini-contract-implementation` | Locked semantic naming policy for contract-proof test project and seed test names |
| 0110 | `0110-draft-plan-2-cognitive-chain-runner-and-local-persistence` | `pending` | `0100-draft-plan-1-prompting-and-gemini-contract-implementation`, `0105-clarify-contract-proof-test-naming` | Produce implementation-ready Draft Plan 2 for chain-length-flexible Cognitive/Conversational runner behavior and workspace-local persistence |

## Status Values
- `pending`
- `in_progress`
- `blocked`
- `completed`

---

## Source 13: `steps/0010-map-solution-project-boundaries/step.md`

# Step: 0010-map-solution-project-boundaries

## Goal
- Define a clear initial solution/package shape that separates thin clients from story engine concerns.

## Context
- We need boundary clarity before drafting implementation plans to avoid monolithic project drift.

## Commands Executed
- `Get-Content -Raw "Zelanthus.slnx"`
- `Get-Content -Raw "Plans/Brainstorms/backbone-doctrine-prompt-first-multi-provider/plan.md"`

## Files Changed
- `Plans/Drafts/thin-clients-first-solution-structure/plan.md`
- `Plans/Drafts/thin-clients-first-solution-structure/steps/index.md`
- `Plans/Drafts/thin-clients-first-solution-structure/decisions/0001-solution-project-boundaries-v0.md`
- `Plans/Drafts/thin-clients-first-solution-structure/artifacts/initial-solution-shape.md`

## Tests / Results
- `not-run` -> docs-only brainstorm step

## Issues
- none

## Decision
- Keep thin LLM clients/prompts outside story engine and enforce layered dependency direction.

## Completion
- `completed`

## Next Actions
- Capture thin-clients-first sequencing and dependency rules in dedicated decisions.

---

## Source 14: `steps/0020-plan-thin-clients-package-first/step.md`

# Step: 0020-plan-thin-clients-package-first

## Goal
- Define implementation planning order where thin clients are planned and built before story engine workflows.

## Context
- Prompt-first, multi-provider-ready architecture depends on stable client and capability contracts before story engine orchestration.

## Commands Executed
- `Get-Content -Raw "Plans/Brainstorms/backbone-doctrine-prompt-first-multi-provider/decisions/0002-chain-continuity-strategy-v0.md"`
- `Get-Content -Raw "Plans/Brainstorms/backbone-doctrine-prompt-first-multi-provider/decisions/0003-prompt-governance-and-provenance-v0.md"`

## Files Changed
- `Plans/Drafts/thin-clients-first-solution-structure/steps/0020-plan-thin-clients-package-first/step.md`
- `Plans/Drafts/thin-clients-first-solution-structure/decisions/0002-thin-clients-first-sequencing-v0.md`

## Tests / Results
- `not-run` -> docs-only brainstorm step

## Issues
- none

## Decision
- Sequence planning as: thin client abstractions + Gemini adapter + prompting contracts first, story engine planning second.

## Completion
- `completed`

## Next Actions
- Capture allowed dependency direction to preserve package boundaries during implementation.

---

## Source 15: `steps/0030-sequence-story-engine-dependencies/step.md`

# Step: 0030-sequence-story-engine-dependencies

## Goal
- Define dependency rules that keep story engine implementation cleanly layered after thin-client contract planning.

## Context
- Without explicit dependency direction, API/story engine/provider code tends to collapse into coupled implementations.

## Commands Executed
- `Get-Content -Raw "Plans/Drafts/thin-clients-first-solution-structure/artifacts/initial-solution-shape.md"`
- `Get-Content -Raw "Plans/README.md"`

## Files Changed
- `Plans/Drafts/thin-clients-first-solution-structure/steps/0030-sequence-story-engine-dependencies/step.md`
- `Plans/Drafts/thin-clients-first-solution-structure/decisions/0003-dependency-direction-rules-v0.md`

## Tests / Results
- `not-run` -> docs-only brainstorm step

## Issues
- none

## Decision
- Enforce one-way dependency direction from API to application and from application to contracts, with provider implementations hidden behind abstraction boundaries.

## Completion
- `completed`

## Next Actions
- Promote this brainstorm into a Draft implementation plan for the thin clients package.

---

## Source 16: `steps/0040-lock-prompting-ownership-policy/step.md`

# Step: 0040-lock-prompting-ownership-policy

## Goal
- Explicitly lock whether implementing applications must manage and version prompts through `Zelanthus.Prompting`.

## Context
- This policy is critical to avoid prompt/version drift and provenance inconsistency across implementations.

## Commands Executed
- `Get-Content -Raw "Plans/Drafts/thin-clients-first-solution-structure/plan.md"`
- `Get-Content -Raw "Plans/Drafts/thin-clients-first-solution-structure/decisions/0001-solution-project-boundaries-v0.md"`
- `Get-Content -Raw "Plans/Drafts/thin-clients-first-solution-structure/decisions/0002-thin-clients-first-sequencing-v0.md"`

## Files Changed
- `Plans/Drafts/thin-clients-first-solution-structure/plan.md`
- `Plans/Drafts/thin-clients-first-solution-structure/steps/index.md`
- `Plans/Drafts/thin-clients-first-solution-structure/steps/0040-lock-prompting-ownership-policy/step.md`
- `Plans/Drafts/thin-clients-first-solution-structure/decisions/0001-solution-project-boundaries-v0.md`
- `Plans/Drafts/thin-clients-first-solution-structure/decisions/0004-prompting-ownership-and-versioning-policy-v0.md`
- `Plans/Drafts/thin-clients-first-solution-structure/artifacts/initial-solution-shape.md`

## Tests / Results
- `not-run` -> docs-only brainstorm step

## Issues
- none

## Decision
- Implementing applications are required to manage/version prompts and record prompt provenance through `Zelanthus.Prompting` shapes/tools.

## Completion
- `completed`

## Next Actions
- Carry this policy as a non-negotiable requirement into the Draft thin-clients implementation plan.

---

## Source 17: `steps/0050-lock-infrastructure-prompt-shape-mapping/step.md`

# Step: 0050-lock-infrastructure-prompt-shape-mapping

## Goal
- Explicitly lock how `Zelanthus.StoryEngine.Infrastructure` storage relates to `Zelanthus.Prompting` contract shapes.

## Context
- Story engine storage models may need database-optimized structures that are not 1:1 with prompting contracts.
- We still need deterministic transform/mapping to valid prompting shapes for execution and provenance workflows.

## Commands Executed
- `Get-Content -Raw "Plans/Drafts/thin-clients-first-solution-structure/plan.md"`
- `Get-Content -Raw "Plans/Drafts/thin-clients-first-solution-structure/decisions/0003-dependency-direction-rules-v0.md"`
- `Get-Content -Raw "Plans/Drafts/thin-clients-first-solution-structure/decisions/0004-prompting-ownership-and-versioning-policy-v0.md"`

## Files Changed
- `Plans/Drafts/thin-clients-first-solution-structure/plan.md`
- `Plans/Drafts/thin-clients-first-solution-structure/steps/index.md`
- `Plans/Drafts/thin-clients-first-solution-structure/steps/0050-lock-infrastructure-prompt-shape-mapping/step.md`
- `Plans/Drafts/thin-clients-first-solution-structure/decisions/0003-dependency-direction-rules-v0.md`
- `Plans/Drafts/thin-clients-first-solution-structure/decisions/0004-prompting-ownership-and-versioning-policy-v0.md`
- `Plans/Drafts/thin-clients-first-solution-structure/decisions/0005-infrastructure-prompt-shape-mapping-policy-v0.md`
- `Plans/Drafts/thin-clients-first-solution-structure/artifacts/initial-solution-shape.md`

## Tests / Results
- `not-run` -> docs-only brainstorm step

## Issues
- none

## Decision
- `Zelanthus.StoryEngine.Infrastructure` may persist prompt-related data in non-1:1 storage models, but must provide deterministic mapping/transform to/from `Zelanthus.Prompting` contract shapes used by runtime workflows.

## Completion
- `completed`

## Next Actions
- Carry this policy as a non-negotiable acceptance criterion in Draft plans for Infrastructure persistence.

---

## Source 18: `steps/0060-refine-decision-layer-responsibility-clarity/step.md`

# Step: 0060-refine-decision-layer-responsibility-clarity

## Goal
- Refine the full solution-structure decision set with deeper detail and explicit layer responsibility contracts.

## Context
- We identified ambiguity risk around where transformation responsibilities belong.
- Decision docs need enough specificity to prevent boundary drift during implementation.

## Commands Executed
- `Get-Content -Raw "Plans/Drafts/thin-clients-first-solution-structure/decisions/0001-solution-project-boundaries-v0.md"`
- `Get-Content -Raw "Plans/Drafts/thin-clients-first-solution-structure/decisions/0002-thin-clients-first-sequencing-v0.md"`
- `Get-Content -Raw "Plans/Drafts/thin-clients-first-solution-structure/decisions/0003-dependency-direction-rules-v0.md"`
- `Get-Content -Raw "Plans/Drafts/thin-clients-first-solution-structure/decisions/0004-prompting-ownership-and-versioning-policy-v0.md"`
- `Get-Content -Raw "Plans/Drafts/thin-clients-first-solution-structure/decisions/0005-infrastructure-prompt-shape-mapping-policy-v0.md"`

## Files Changed
- `Plans/Drafts/thin-clients-first-solution-structure/plan.md`
- `Plans/Drafts/thin-clients-first-solution-structure/steps/index.md`
- `Plans/Drafts/thin-clients-first-solution-structure/steps/0060-refine-decision-layer-responsibility-clarity/step.md`
- `Plans/Drafts/thin-clients-first-solution-structure/decisions/0001-solution-project-boundaries-v0.md`
- `Plans/Drafts/thin-clients-first-solution-structure/decisions/0002-thin-clients-first-sequencing-v0.md`
- `Plans/Drafts/thin-clients-first-solution-structure/decisions/0003-dependency-direction-rules-v0.md`
- `Plans/Drafts/thin-clients-first-solution-structure/decisions/0004-prompting-ownership-and-versioning-policy-v0.md`
- `Plans/Drafts/thin-clients-first-solution-structure/decisions/0005-infrastructure-prompt-shape-mapping-policy-v0.md`

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
- `completed`

## Next Actions
- Carry this refined decision set into Draft implementation plans as explicit acceptance criteria.

---

## Source 19: `steps/0070-apply-external-review-refinements/step.md`

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
- `Get-Content -Raw "Plans/Drafts/thin-clients-first-solution-structure/decisions/0001-solution-project-boundaries-v0.md"`
- `Get-Content -Raw "Plans/Drafts/thin-clients-first-solution-structure/decisions/0002-thin-clients-first-sequencing-v0.md"`
- `Get-Content -Raw "Plans/Drafts/thin-clients-first-solution-structure/decisions/0003-dependency-direction-rules-v0.md"`
- `Get-Content -Raw "Plans/Drafts/thin-clients-first-solution-structure/decisions/0004-prompting-ownership-and-versioning-policy-v0.md"`
- `Get-Content -Raw "Plans/Drafts/thin-clients-first-solution-structure/decisions/0005-infrastructure-prompt-shape-mapping-policy-v0.md"`
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
- `Plans/Drafts/thin-clients-first-solution-structure/plan.md`
- `Plans/Drafts/thin-clients-first-solution-structure/steps/index.md`
- `Plans/Drafts/thin-clients-first-solution-structure/steps/0070-apply-external-review-refinements/step.md`
- `Plans/Drafts/thin-clients-first-solution-structure/decisions/0001-solution-project-boundaries-v0.md`
- `Plans/Drafts/thin-clients-first-solution-structure/decisions/0002-thin-clients-first-sequencing-v0.md`
- `Plans/Drafts/thin-clients-first-solution-structure/decisions/0003-dependency-direction-rules-v0.md`
- `Plans/Drafts/thin-clients-first-solution-structure/decisions/0004-prompting-ownership-and-versioning-policy-v0.md`
- `Plans/Drafts/thin-clients-first-solution-structure/decisions/0005-infrastructure-prompt-shape-mapping-policy-v0.md`
- `Plans/Drafts/thin-clients-first-solution-structure/decisions/0006-call-envelope-and-enforcement-policy-v0.md`
- `Plans/Drafts/thin-clients-first-solution-structure/artifacts/initial-solution-shape.md`

## Tests / Results
- `not-run` -> docs-only brainstorm step

## Issues
- none

## Decision
- Lock a concrete application call envelope and enforce boundary integrity via early architecture tests and naming policy.

## Completion
- `completed`

## Next Actions
- Use this refined decision set to create two Draft plans:
  - thin contracts/adapter implementation plan,
  - cognitive chain runner/artifact persistence plan.

---

## Source 20: `steps/0080-lock-mvp-goals-and-first-plan-boundary/step.md`

# Step: 0080-lock-mvp-goals-and-first-plan-boundary

## Goal
- Promote thin-clients planning from Brainstorms to Drafts and lock explicit MVP goals before harness-shape selection.

## Context
- Harness discussion is active, but goals must be fixed first so host-shape choices do not redefine success criteria.

## Git Branch
- `main`

## Commits
- `none`

## Commands Executed
- `Move-Item -Path Plans/Brainstorms/thin-clients-first-solution-structure -Destination Plans/Drafts/thin-clients-first-solution-structure`
- `Get-Content "Plans/Drafts/thin-clients-first-solution-structure/plan.md"`
- `Get-Content "Plans/Drafts/thin-clients-first-solution-structure/steps/index.md"`

## Files Changed
- `Plans/Drafts/thin-clients-first-solution-structure/plan.md`
- `Plans/Drafts/thin-clients-first-solution-structure/steps/index.md`
- `Plans/Drafts/thin-clients-first-solution-structure/decisions/0007-mvp-goals-and-first-plan-boundary-v0.md`
- `Plans/Drafts/thin-clients-first-solution-structure/risks/risk-log.md`
- `Plans/Drafts/thin-clients-first-solution-structure/steps/0080-lock-mvp-goals-and-first-plan-boundary/step.md`

## Tests / Results
- `not-run` -> docs-only draft refinement step

## Issues
- none

## Decision
- Keep Plan 1 narrow and goal-driven; postpone harness host choice until criteria-driven step `0090`.

## Completion
- `completed`

## Next Actions
- Choose MVP harness shape using locked criteria in `0090-choose-mvp-harness-shape`.

---

## Source 21: `steps/0090-choose-mvp-harness-shape/step.md`

# Step: 0090-choose-mvp-harness-shape

## Goal
- Choose MVP harness shape (test-host vs API-host) against locked acceptance criteria.

## Context
- Harness shape must be selected by contract fit and execution reliability, not convenience.

## Git Branch
- `main`

## Commits
- `none`

## Commands Executed
- `Get-Content "Plans/Drafts/thin-clients-first-solution-structure/decisions/0007-mvp-goals-and-first-plan-boundary-v0.md"`
- `Get-Content "Plans/Drafts/thin-clients-first-solution-structure/decisions/0006-call-envelope-and-enforcement-policy-v0.md"`
- `Get-Content "Plans/README.md"`

## Files Changed
- `Plans/Drafts/thin-clients-first-solution-structure/steps/0090-choose-mvp-harness-shape/step.md`
- `Plans/Drafts/thin-clients-first-solution-structure/steps/0090-choose-mvp-harness-shape/artifacts/harness-shape-evaluation-matrix.md`
- `Plans/Drafts/thin-clients-first-solution-structure/decisions/0008-mvp-harness-shape-selection-v0.md`
- `Plans/Drafts/thin-clients-first-solution-structure/steps/index.md`
- `Plans/Drafts/thin-clients-first-solution-structure/plan.md`

## Tests / Results
- `not-run` -> docs-only draft step

## Issues
- none

## Decision
- Select a dedicated test-host contract-proof project (`Tests/Zelanthus.WorkflowContractProofs.Tests`) for Plan 1.
- Defer API-host harness execution unless test-host criteria fail.

## Completion
- `completed`

## Next Actions
- Draft Plan 1 implementation details using the selected test-host harness baseline.

---

## Source 22: `steps/0100-draft-plan-1-prompting-and-gemini-contract-implementation/step.md`

# Step: 0100-draft-plan-1-prompting-and-gemini-contract-implementation

## Goal
- Produce implementation-ready Draft Plan 1 for Prompting, client abstractions, Gemini adapter, and architecture gate.

## Context
- Plan 1 must validate backbone contracts before broader story engine work.

## Git Branch
- `main`

## Commits
- `none`

## Commands Executed
- `Copy-Item -Path "Plans/Templates/plan-folder" -Destination "Plans/Drafts/mvp-prompting-gemini-contract-baseline" -Recurse`
- `Get-Content "Plans/Drafts/mvp-prompting-gemini-contract-baseline/plan.md"`
- `Get-Content "Plans/Drafts/mvp-prompting-gemini-contract-baseline/steps/index.md"`

## Files Changed
- `Plans/Drafts/thin-clients-first-solution-structure/steps/0100-draft-plan-1-prompting-and-gemini-contract-implementation/step.md`
- `Plans/Drafts/thin-clients-first-solution-structure/steps/index.md`
- `Plans/Drafts/thin-clients-first-solution-structure/plan.md`
- `Plans/Drafts/mvp-prompting-gemini-contract-baseline/plan.md`
- `Plans/Drafts/mvp-prompting-gemini-contract-baseline/steps/index.md`
- `Plans/Drafts/mvp-prompting-gemini-contract-baseline/risks/risk-log.md`
- `Plans/Drafts/mvp-prompting-gemini-contract-baseline/steps/0010-scaffold-projects-and-references/step.md`
- `Plans/Drafts/mvp-prompting-gemini-contract-baseline/steps/0020-define-prompting-contracts-and-rendering-rules/step.md`
- `Plans/Drafts/mvp-prompting-gemini-contract-baseline/steps/0030-define-llm-client-abstractions-and-capability-profile/step.md`
- `Plans/Drafts/mvp-prompting-gemini-contract-baseline/steps/0040-implement-gemini-adapter-normalization-path/step.md`
- `Plans/Drafts/mvp-prompting-gemini-contract-baseline/steps/0050-add-architecture-boundary-tests/step.md`
- `Plans/Drafts/mvp-prompting-gemini-contract-baseline/steps/0060-implement-contract-proof-tests-and-local-artifact-persistence/step.md`
- `Plans/Drafts/mvp-prompting-gemini-contract-baseline/steps/0070-validate-golden-path-and-reason-coded-failure-path/step.md`

## Tests / Results
- `not-run` -> docs-only draft step

## Issues
- none

## Decision
- Use a dedicated Plan 1 draft folder (`mvp-prompting-gemini-contract-baseline`) so implementation details are isolated and promotion-ready.

## Completion
- `completed`

## Next Actions
- Draft Plan 2 with explicit dependencies on Plan 1 contracts and harness outputs.

---

## Source 23: `steps/0105-clarify-contract-proof-test-naming/step.md`

# Step: 0105-clarify-contract-proof-test-naming

## Goal
- Remove ambiguous harness naming and lock a semantic naming policy for long-lived contract-proof tests.

## Context
- `MvpHarness` is unclear over time and creates drift risk for future one-off proof tests.

## Git Branch
- `main`

## Commits
- `none`

## Commands Executed
- `Move-Item "Plans/Drafts/mvp-prompting-gemini-contract-baseline/steps/0060-implement-mvp-harness-tests-and-local-artifact-persistence" "Plans/Drafts/mvp-prompting-gemini-contract-baseline/steps/0060-implement-contract-proof-tests-and-local-artifact-persistence"`
- `Get-Content "Plans/Drafts/thin-clients-first-solution-structure/decisions/0008-mvp-harness-shape-selection-v0.md"`
- `Get-Content "Plans/Drafts/mvp-prompting-gemini-contract-baseline/plan.md"`

## Files Changed
- `Plans/Drafts/thin-clients-first-solution-structure/steps/0105-clarify-contract-proof-test-naming/step.md`
- `Plans/Drafts/thin-clients-first-solution-structure/steps/index.md`
- `Plans/Drafts/thin-clients-first-solution-structure/plan.md`
- `Plans/Drafts/thin-clients-first-solution-structure/decisions/0008-mvp-harness-shape-selection-v0.md`
- `Plans/Drafts/thin-clients-first-solution-structure/decisions/0009-contract-proof-test-naming-policy-v0.md`
- `Plans/Drafts/thin-clients-first-solution-structure/steps/0090-choose-mvp-harness-shape/artifacts/harness-shape-evaluation-matrix.md`
- `Plans/Drafts/mvp-prompting-gemini-contract-baseline/plan.md`
- `Plans/Drafts/mvp-prompting-gemini-contract-baseline/steps/index.md`
- `Plans/Drafts/mvp-prompting-gemini-contract-baseline/steps/0060-implement-contract-proof-tests-and-local-artifact-persistence/step.md`
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
- `completed`

## Next Actions
- Continue `0110` draft with naming policy treated as a dependency.

---

## Source 24: `steps/0110-draft-plan-2-cognitive-chain-runner-and-local-persistence/step.md`

# Step: 0110-draft-plan-2-cognitive-chain-runner-and-local-persistence

## Goal
- Produce implementation-ready Draft Plan 2 for chain-length-flexible `CognitiveChain`/`ConversationalChain` runner behavior and local artifact/provenance persistence.

## Context
- Plan 2 depends on Plan 1 contract outputs and should avoid premature production persistence setup.
- Plan 2 must not hard-lock runner flow to two turns; `T1` -> `T2` is minimum proof only.

## Git Branch
- `main`

## Commits
- `none`

## Commands Executed
- `none`

## Files Changed
- `none`

## Tests / Results
- `not-run` -> pending draft step

## Issues
- none

## Decision
- pending

## Completion
- `pending`

## Next Actions
- Define runner acceptance criteria for variable-length `CognitiveChain` and `ConversationalChain` flows.
- Define local persistence boundaries:
  - workspace-local file/path-backed storage for checkpoints/artifacts/provenance/failures,
  - deterministic resume/replay from persisted artifacts,
  - no external database/cache/queue/service storage in Plan 2.
- Define migration constraints toward future Postgres.

---

## Source 25: `steps/0090-choose-mvp-harness-shape/artifacts/harness-shape-evaluation-matrix.md`

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

## Source 26: `archive-note.md`

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

---

## Source 27: `artifacts/initial-solution-shape.md`

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
