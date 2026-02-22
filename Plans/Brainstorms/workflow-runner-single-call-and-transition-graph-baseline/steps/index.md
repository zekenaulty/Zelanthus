# Steps Index

Use this file for dependency and status summaries.
Do not use this file as the only source of step order.
Step order must still be visible in folder names (`NNNN-semantic-step-name`).

## Step Map
| Order | Step Folder | Status | Depends On | Notes |
|---|---|---|---|---|
| 0010 | `0010-inventory-current-runner-and-proof-surfaces` | `pending` | `none` | Inventory current Zelanthus contracts and proof coverage gaps. |
| 0020 | `0020-extract-bookforge-phase-and-transition-patterns` | `pending` | `0010` | Extract reusable phase/transition patterns from BookForge planning evidence. |
| 0030 | `0030-map-cognition-workflow-concepts-and-anti-patterns` | `pending` | `0010` | Map reusable seams and avoid over-complex patterns seen in Cognition. |
| 0040 | `0040-define-single-call-and-transition-graph-contracts` | `pending` | `0020`, `0030` | Draft deterministic single-call + transition-graph contract shape and constraints. |
| 0050 | `0050-capture-carry-forward-backlog-and-promotion-gates` | `pending` | `0040` | Lock backlog carry-forward and Draft promotion gate criteria. |

## Status Values
- `pending`
- `in_progress`
- `blocked`
- `completed`
