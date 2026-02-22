# Steps Index

Use this file for dependency and status summaries.
Do not use this file as the only source of step order.
Step order must still be visible in folder names (`NNNN-semantic-step-name`).

## Step Map
| Order | Step Folder | Status | Depends On | Notes |
|---|---|---|---|---|
| 0010 | `0010-lock-plan-2-scope-and-contract-dependencies` | `completed` | `none` | Lock Plan 2 scope, cross-plan dependencies, and dependency implementation matrix |
| 0020 | `0020-define-chain-runner-domain-and-state-model` | `completed` | `0010-lock-plan-2-scope-and-contract-dependencies` | Define run/turn state contracts and domain-level invariants for chain execution |
| 0025 | `0025-define-minimal-workflow-abstraction-hooks` | `completed` | `0010-lock-plan-2-scope-and-contract-dependencies`, `0020-define-chain-runner-domain-and-state-model` | Define minimal workflow/step abstraction and route-hook seams for future expansion |
| 0030 | `0030-define-application-orchestration-flow-and-chain-router` | `completed` | `0010-lock-plan-2-scope-and-contract-dependencies`, `0020-define-chain-runner-domain-and-state-model`, `0025-define-minimal-workflow-abstraction-hooks` | Define chain routing, step sequencing, and orchestration policy contracts |
| 0040 | `0040-define-local-persistence-artifact-and-checkpoint-layout` | `completed` | `0010-lock-plan-2-scope-and-contract-dependencies`, `0020-define-chain-runner-domain-and-state-model` | Define local file/path-backed run, turn, checkpoint, and failure artifact layout |
| 0050 | `0050-define-infrastructure-mappers-and-store-contracts` | `completed` | `0020-define-chain-runner-domain-and-state-model`, `0040-define-local-persistence-artifact-and-checkpoint-layout` | Define storage mapper and store interfaces with deterministic mapping guarantees |
| 0060 | `0060-define-retry-resume-and-reason-code-policy` | `completed` | `0030-define-application-orchestration-flow-and-chain-router`, `0040-define-local-persistence-artifact-and-checkpoint-layout`, `0050-define-infrastructure-mappers-and-store-contracts` | Define retry/resume semantics, pinned reason codes, and continuity fallback policy |
| 0070 | `0070-plan-project-reference-graph-and-architecture-tests` | `completed` | `0010-lock-plan-2-scope-and-contract-dependencies`, `0030-define-application-orchestration-flow-and-chain-router`, `0050-define-infrastructure-mappers-and-store-contracts` | Define project reference updates and architecture test assertions as early implementation gate |
| 0080 | `0080-define-runner-proof-test-suite-and-evidence-artifacts` | `completed` | `0030-define-application-orchestration-flow-and-chain-router`, `0040-define-local-persistence-artifact-and-checkpoint-layout`, `0060-define-retry-resume-and-reason-code-policy`, `0070-plan-project-reference-graph-and-architecture-tests` | Define deterministic proof test set and required evidence artifacts for runner acceptance |
| 0090 | `0090-define-api-composition-boundaries-and-host-integration` | `completed` | `0030-define-application-orchestration-flow-and-chain-router`, `0070-plan-project-reference-graph-and-architecture-tests` | Define API composition responsibilities and explicit non-goals for endpoint scope |
| 0100 | `0100-finalize-plan-2-acceptance-gates-and-promotion-readiness` | `completed` | `0020-define-chain-runner-domain-and-state-model`, `0025-define-minimal-workflow-abstraction-hooks`, `0030-define-application-orchestration-flow-and-chain-router`, `0040-define-local-persistence-artifact-and-checkpoint-layout`, `0050-define-infrastructure-mappers-and-store-contracts`, `0060-define-retry-resume-and-reason-code-policy`, `0070-plan-project-reference-graph-and-architecture-tests`, `0080-define-runner-proof-test-suite-and-evidence-artifacts`, `0090-define-api-composition-boundaries-and-host-integration` | Finalize Plan 2 acceptance matrix and promotion checklist |

## Status Values
- `pending`
- `in_progress`
- `blocked`
- `completed`

