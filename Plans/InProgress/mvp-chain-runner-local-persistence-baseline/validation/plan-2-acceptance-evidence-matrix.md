# Plan 2 Acceptance Evidence Matrix

## Scope
- Plan: `mvp-chain-runner-local-persistence-baseline`
- Purpose: map acceptance points to required evidence before promotion to `InProgress`.

## Execution Evidence Summary
- `dotnet test "Tests/Zelanthus.WorkflowContractProofs.Tests/Zelanthus.WorkflowContractProofs.Tests.csproj" --filter "Category!=LiveGemini"` -> passed (28 tests)
- `dotnet test "Tests/Zelanthus.Architecture.Tests/Zelanthus.Architecture.Tests.csproj"` -> passed (7 tests)
- `dotnet test "Zelanthus.slnx" --filter "Category!=LiveGemini"` -> passed (35 tests)

## Acceptance Map
| Acceptance Point | Required Evidence Type | Planned Location |
|---|---|---|
| Variable-length `CognitiveChain` support with `PLAN_STEP/EXECUTE` semantics | proof test + run artifact set | `steps/0080-define-runner-proof-test-suite-and-evidence-artifacts/artifacts/` |
| Variable-length `ConversationalChain` support | proof test + run artifact set | `steps/0080-define-runner-proof-test-suite-and-evidence-artifacts/artifacts/` |
| Deterministic local checkpoint persistence | mapper/store tests + checkpoint artifacts | `steps/0040-define-local-persistence-artifact-and-checkpoint-layout/artifacts/` |
| Cognitive resume restarts from first `PLAN_STEP` | failure/resume proof tests + reason-coded artifacts | `steps/0060-define-retry-resume-and-reason-code-policy/artifacts/` |
| Conversational resume continues from last successful step | failure/resume proof tests + checkpoint artifacts | `steps/0080-define-runner-proof-test-suite-and-evidence-artifacts/artifacts/` |
| Cognitive staged flow persists and forwards latest `thinking_persistence_key` (`thoughtSignature`) each step | proof test + run metadata artifacts | `steps/0080-define-runner-proof-test-suite-and-evidence-artifacts/artifacts/` |
| Capability-supported null/empty cognitive continuity handle is deterministic failure (`continuity_handle_invalid`) | failure-path proof test + reason-coded artifact | `steps/0060-define-retry-resume-and-reason-code-policy/artifacts/` |
| Continuity capability unsupported path does not emit failure code | capability-profile proof test + run/failure artifact absence check | `steps/0080-define-runner-proof-test-suite-and-evidence-artifacts/artifacts/` |
| Pinned reason-code emission | proof tests + provenance/failure artifacts | `steps/0060-define-retry-resume-and-reason-code-policy/artifacts/` |
| Deterministic failure maps to exactly one reason code | proof tests + failure diagnostics artifact | `steps/0060-define-retry-resume-and-reason-code-policy/artifacts/` |
| Required step `prompt_ref` missing/invalid emits deterministic terminal failure (`missing_prompt_reference`) | proof test + reason-coded failure artifact | `steps/0060-define-retry-resume-and-reason-code-policy/artifacts/` |
| Illegal run/turn transition emits `invalid_state_transition` | state-machine proof test + failure artifact | `steps/0020-define-chain-runner-domain-and-state-model/artifacts/` |
| Artifact paths are deterministic and use `<turn-index>-<step-key>` only | proof test + run artifact path evidence | `steps/0040-define-local-persistence-artifact-and-checkpoint-layout/artifacts/` |
| Canonical turn metadata is authored in `turn.json` | artifact contract + proof test | `steps/0040-define-local-persistence-artifact-and-checkpoint-layout/artifacts/` |
| Turn/checkpoint counters reserve deterministically and never reuse indices after interruption | counter proof tests + run/checkpoint artifacts | `steps/0020-define-chain-runner-domain-and-state-model/artifacts/` |
| Failure artifacts are deterministically anchored by `turn_index` (`failure-<turn-index>.json`) | failure-path proof tests + artifact path evidence | `steps/0040-define-local-persistence-artifact-and-checkpoint-layout/artifacts/` |
| Route-hook appended steps use deterministic generated keys and persisted effective queue state | route-hook proof tests + run metadata evidence | `steps/0025-define-minimal-workflow-abstraction-hooks/artifacts/` |
| `route_hook_key` grammar is pinned and generated step keys remain path-safe | contract artifact + route-hook proof tests | `steps/0025-define-minimal-workflow-abstraction-hooks/artifacts/` |
| `workflow_kind` and `chain_mode` alignment is enforced with deterministic mismatch failure | state/runner proof test + failure artifact | `steps/0020-define-chain-runner-domain-and-state-model/artifacts/` |
| Timestamp assertions are presence/format based by default | evidence-spec rule + proof test behavior note | `steps/0080-define-runner-proof-test-suite-and-evidence-artifacts/artifacts/` |
| Provenance artifact is strict superset with deterministic Prompting mapping | mapper/proof tests + mapping artifact evidence | `steps/0050-define-infrastructure-mappers-and-store-contracts/artifacts/` |
| Minimal workflow abstraction hooks exist (`WorkflowDefinition`/`WorkflowStepDefinition` with required `prompt_ref`/route hooks) | decision + contract artifact | `steps/0025-define-minimal-workflow-abstraction-hooks/artifacts/` |
| Project dependency boundary integrity | architecture assertions matrix + test pass evidence | `steps/0070-plan-project-reference-graph-and-architecture-tests/artifacts/` |
| Final promotion readiness | checklist showing DoD coverage | `steps/0100-finalize-plan-2-acceptance-gates-and-promotion-readiness/artifacts/` |

## Promotion Gate Rule
- Plan 2 cannot move to `InProgress` until each acceptance point has explicit mapped evidence path and acceptance assertion.
