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
