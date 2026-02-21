# Steps Index

Use this file for dependency and status summaries.
Do not use this file as the only source of step order.
Step order must still be visible in folder names (`NNNN-semantic-step-name`).

## Step Map
| Order | Step Folder | Status | Depends On | Notes |
|---|---|---|---|---|
| 0010 | `0010-define-backbone-doctrine-boundaries` | `completed` | `none` | Captured prompt-first doctrine and semantic/deterministic split |
| 0020 | `0020-define-chain-modes-and-continuity-contract` | `completed` | `0010-define-backbone-doctrine-boundaries` | Captured `CognitiveChain` vs `ConversationalChain` and resume contract |
| 0030 | `0030-define-provider-capability-and-prompt-governance` | `completed` | `0010-define-backbone-doctrine-boundaries` | Captured provider capability profile and prompt provenance requirements |
| 0040 | `0040-refine-decision-contract-clarity` | `completed` | `0010-define-backbone-doctrine-boundaries`, `0020-define-chain-modes-and-continuity-contract`, `0030-define-provider-capability-and-prompt-governance` | Expanded all doctrine decisions with explicit contracts, scope boundaries, and layer ownership |
| 0050 | `0050-define-mvp-artifact-and-failure-contract` | `completed` | `0040-refine-decision-contract-clarity` | Defined MVP artifact taxonomy, reason-code baseline, and canonical structured-output rule |

## Status Values
- `pending`
- `in_progress`
- `blocked`
- `completed`
