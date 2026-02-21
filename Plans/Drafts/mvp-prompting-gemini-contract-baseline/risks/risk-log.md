# Risk Log

## R-001 Contract Surface Drift
- Statement: Prompting and LLM abstraction contracts drift during early implementation and invalidate adapter/harness assumptions.
- Impact: Rework across multiple projects and delayed MVP proof.
- Mitigation: Lock contract-level acceptance criteria in early steps before adapter and harness implementation.
- Status: open

## R-002 Harness Flakiness
- Statement: External provider behavior variance causes unstable MVP harness tests.
- Impact: Low-confidence acceptance evidence.
- Mitigation: Assert normalized contracts and reason codes; avoid brittle assertions against generated prose; run live provider tests only through explicit opt-in execution path.
- Status: open

## R-003 Boundary Violations
- Statement: Initial project scaffolding introduces forbidden dependencies.
- Impact: Architecture debt and doctrinal drift.
- Mitigation: Add architecture tests as an early gate, not a late cleanup task.
- Status: open

## R-004 Deferred Template Composition
- Statement: Sub-template/import use-cases may appear before a dedicated composition plan is ready.
- Impact: Ad-hoc composition behavior and contract inconsistency.
- Mitigation: Keep Plan 1 single-template rendering only; open a dedicated follow-up plan for composition/import contracts.
- Status: open
