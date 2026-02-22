# Contract Surface (Initial)

This file anchors the minimum boundary contracts for the reset baseline.
Detailed contracts are defined per plan.

## Axis 1: Prompting
- Prompt identity and versioning.
- Placeholder requirements and deterministic rendering failure.
- Prompt checksum/fingerprint.

## Axis 2: LLM Client Abstractions
- Provider-agnostic request contract.
- Normalized response envelope.
- Usage/token accounting and protocol error normalization.

## Axis 3: Orchestration
- Execution mode selection: `CognitiveChain`, `ConversationalChain`, `SingleCall`.
- Transition decision based on structured outcomes.
- Deterministic state progression and explicit reason-coded failures.

## Axis 4: Dispatch
- Outcome dispatch maps to named handlers/tools by contract.
- Dispatch behavior is data-driven where possible.
- Missing mapping is deterministic failure, never silent fallback.

## Guardrails
- Keep naming unambiguous; update terminology map when introducing new axis words.
- Keep plans self-contained by folder scope.
- Keep runtime-impacting implementation on feature branches only.
