# Decision 0003: Prompt Governance and Provenance v0

## Status
- accepted

## Decision Summary
- Prompt governance is a runtime contract, not a documentation convention.
- Provider integrations are capability-driven through shared abstractions.
- Provenance capture is mandatory for every workflow-relevant model call.

## Context
- Prompt drift and missing provenance are major failure sources in iterative workflow systems.
- Multi-provider support requires normalized capabilities and deterministic metadata, not provider-specific branches in workflow orchestration.

## Prompt Governance Requirements
- Identity and versioning:
  - Prompts use stable semantic IDs and explicit versions.
  - Released versions are immutable; edits require a new version.
- Composition:
  - Composition of prompt blocks/fragments must be deterministic.
  - Composition determinism is verified by checksum consistency.
- Rendering:
  - Required placeholders/tokens must be enforced with hard failures on missing values.
  - Token allowlist is explicit and validated.
- Change control:
  - Prompt updates must leave a traceable change surface (ID, version, checksum delta, reason).

## Provider Capability Contract (minimum)
- Required normalized capability profile fields:
  - `supports_thinking_controls`
  - `supports_continuity_handle`
  - `supports_structured_output`
  - `supports_tool_calls`
  - `supports_json_mode`
- Runtime behavior must branch on capability contract values, not provider-name conditionals in application orchestration code.

## Provenance Contract (minimum)
- Prompt provenance:
  - prompt semantic ID,
  - prompt version,
  - rendered prompt hash/checksum,
  - prompt source locator (pack/template key).
- Execution provenance:
  - provider ID,
  - model ID,
  - chain mode and turn index,
  - request/response timing and token usage (including thought tokens when available).
- Validation provenance:
  - validation outcome,
  - reason codes for retries/failures,
  - normalized output snapshot,
  - raw response snapshot.

## Quality Gates
- A workflow step cannot be considered complete if required provenance fields are missing.
- A prompt-render execution cannot proceed when required token values are unresolved.
- A provider integration is incomplete without a declared capability profile.

## Consequences
- Prompt assets and provider contracts can evolve without rewriting story engine orchestration.
- Gemini-first rollout remains compatible with later providers.
- Replay and debugging quality improves because output artifacts are tied to verifiable prompt and execution metadata.
- MVP artifact taxonomy/reason-code baseline/structured-output canonical rules are defined in `0004-mvp-artifact-and-failure-contract-v0.md`.
