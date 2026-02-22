# Cognition Anti-Pattern Guardrails

## Guardrails
- Do not introduce generalized orchestration-engine abstractions in this baseline.
- Do not allow implicit transition behavior that is not persisted as explicit decision artifacts.
- Do not mix naming axes (topology, execution kind, LLM mode, phase role).
- Do not collapse node identity and node occurrence identity.

## Acceptance Reminder
- Transition behavior must be data-edit driven.
- Implementation code changes should not be required for flow edits in this scope.
