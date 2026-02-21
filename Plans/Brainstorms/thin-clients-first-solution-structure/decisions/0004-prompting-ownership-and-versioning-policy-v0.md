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
  - required tokens must be resolved before execution.
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
