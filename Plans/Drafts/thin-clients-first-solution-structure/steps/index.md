# Steps Index

Use this file for dependency and status summaries.
Do not use this file as the only source of step order.
Step order must still be visible in folder names (`NNNN-semantic-step-name`).

## Step Map
| Order | Step Folder | Status | Depends On | Notes |
|---|---|---|---|---|
| 0010 | `0010-map-solution-project-boundaries` | `completed` | `none` | Defined initial package boundaries for API, story engine, prompting, and clients |
| 0020 | `0020-plan-thin-clients-package-first` | `completed` | `0010-map-solution-project-boundaries` | Defined thin clients as first implementation planning target |
| 0030 | `0030-sequence-story-engine-dependencies` | `completed` | `0010-map-solution-project-boundaries` | Defined allowed dependency direction and implementation order |
| 0040 | `0040-lock-prompting-ownership-policy` | `completed` | `0010-map-solution-project-boundaries` | Locked requirement that implementing apps use `Zelanthus.Prompting` for prompt management/versioning/provenance |
| 0050 | `0050-lock-infrastructure-prompt-shape-mapping` | `completed` | `0030-sequence-story-engine-dependencies`, `0040-lock-prompting-ownership-policy` | Locked policy that Infrastructure storage models may differ but must map/transform to prompting contracts |
| 0060 | `0060-refine-decision-layer-responsibility-clarity` | `completed` | `0010-map-solution-project-boundaries`, `0030-sequence-story-engine-dependencies`, `0040-lock-prompting-ownership-policy`, `0050-lock-infrastructure-prompt-shape-mapping` | Expanded all solution-structure decisions with detailed boundaries and explicit transform ownership by layer |
| 0070 | `0070-apply-external-review-refinements` | `completed` | `0060-refine-decision-layer-responsibility-clarity` | Applied external review refinements for call envelope, MVP proving slice, architecture test gate, and naming alignment |
| 0080 | `0080-lock-mvp-goals-and-first-plan-boundary` | `completed` | `0070-apply-external-review-refinements` | Promoted plan to Draft and locked MVP goals, first-plan boundaries, and risk baseline |
| 0090 | `0090-choose-mvp-harness-shape` | `completed` | `0080-lock-mvp-goals-and-first-plan-boundary` | Selected test-host baseline (`Tests/Zelanthus.WorkflowContractProofs.Tests`); API-host deferred by criteria |
| 0100 | `0100-draft-plan-1-prompting-and-gemini-contract-implementation` | `completed` | `0090-choose-mvp-harness-shape` | Created `mvp-prompting-gemini-contract-baseline` draft plan package with explicit scope, DoD, and step map |
| 0105 | `0105-clarify-contract-proof-test-naming` | `completed` | `0100-draft-plan-1-prompting-and-gemini-contract-implementation` | Locked semantic naming policy for contract-proof test project and seed test names |
| 0110 | `0110-draft-plan-2-cognitive-chain-runner-and-local-persistence` | `completed` | `0100-draft-plan-1-prompting-and-gemini-contract-implementation`, `0105-clarify-contract-proof-test-naming` | Created `mvp-chain-runner-local-persistence-baseline` draft package with dependency matrix, decisions, risk log, and acceptance evidence artifacts |

## Status Values
- `pending`
- `in_progress`
- `blocked`
- `completed`
