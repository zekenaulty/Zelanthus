# Proof vs Runtime Gap Notes

## Confirmed Proof Coverage
- Cognitive resume restart policy is proof-covered.
- Conversational resume with effective-step rehydration is proof-covered.
- Variable-length execution behavior is proof-covered.
- Deterministic local pathing and atomic persistence behavior is proof-covered.

## Runtime Doctrine Gaps
- Transition-rule evaluation contract is not yet fully hardened in runtime implementation policy.
- Node identity (`node_key`) vs occurrence identity (`node_instance_key`) needs complete runtime contract hardening.
- Naming axis split (topology vs execution kind vs LLM mode) must be enforced in implementation.
- Carry-forward backlog gating needs explicit enforcement in draft promotion checklist.

## Planning Implication
- Next draft must promote explicit contracts for transition decisions and node identity semantics before implementation changes are accepted.
