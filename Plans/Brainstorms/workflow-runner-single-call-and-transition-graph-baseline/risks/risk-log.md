# Risk Log

## R-001 Scope Creep Beyond Minimal TransitionGraph
- Risk: TransitionGraph work expands into full orchestration-engine complexity (scheduler, parallelization, graph optimization).
- Impact: High
- Likelihood: Medium
- Mitigation: Keep non-goals explicit; reject scheduler/DAG/parallelization scope in this brainstorm.

## R-002 Naming Drift Reintroduces Ambiguity
- Risk: Legacy terms are used as authoritative semantics in new docs, causing contract drift.
- Impact: High
- Likelihood: Medium
- Mitigation: Enforce terminology supersession map and explicit acceptance checklist checks.

## R-003 Transition Non-Determinism on Resume
- Risk: Transition decisions are re-evaluated or inferred during resume, creating divergent behavior.
- Impact: High
- Likelihood: Medium
- Mitigation: Persist transition-decision artifacts before next-node execution; disallow re-decision on resume.

## R-004 Proof Drift vs Runtime Process
- Risk: Proof contracts exist without matching runtime process hardening.
- Impact: High
- Likelihood: High
- Mitigation: Maintain explicit carry-forward backlog entries and require Draft mapping before implementation.

## R-005 Overfitting to Reference Systems
- Risk: Importing BookForge/Cognition complexity that is not needed for this baseline.
- Impact: Medium
- Likelihood: Medium
- Mitigation: Accept only patterns with direct contract value and clear minimal implementation path.
