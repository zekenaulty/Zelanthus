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
