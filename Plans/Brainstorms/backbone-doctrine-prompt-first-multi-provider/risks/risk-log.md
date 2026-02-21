# Risk Log

## R-001 Hidden Continuity Coupling
- Statement: Provider continuity handles become an implicit dependency for correctness.
- Impact: Resume failures, non-deterministic behavior across retries/model changes.
- Mitigation: Explicit plan artifacts are authoritative; continuity is optional optimization.
- Status: open

## R-002 Premature Abstraction
- Statement: Multi-provider abstraction becomes over-engineered before concrete second provider requirements exist.
- Impact: Slower delivery, unclear boundaries.
- Mitigation: Define minimal capability profile now; implement Gemini adapter first.
- Status: open

## R-003 Chat-First Drift
- Statement: Architecture drifts into conversation state management before prompt/runtime contracts are stable.
- Impact: Complexity growth and lower execution reliability.
- Mitigation: Keep prompt-first doctrine and artifact provenance as stage gate for Draft plans.
- Status: open
