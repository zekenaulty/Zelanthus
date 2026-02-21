# Steps Index

Use this file for dependency and status summaries.
Do not use this file as the only source of step order.
Step order must still be visible in folder names (`NNNN-semantic-step-name`).

## Step Map
| Order | Step Folder | Status | Depends On | Notes |
|---|---|---|---|---|
| 0120 | `0120-execute-mvp-prompting-gemini-contract-baseline` | `completed` | `none` | Execute Plan 1 on feature branch and maintain evidence/step notes in its InProgress folder |
| 0130 | `0130-execute-mvp-chain-runner-local-persistence-baseline` | `in_progress` | `0120-execute-mvp-prompting-gemini-contract-baseline` | Execute Plan 2 after Plan 1 contracts are implemented and validated |
| 0140 | `0140-prepare-closeout-and-pr-handoff` | `pending` | `0120-execute-mvp-prompting-gemini-contract-baseline`, `0130-execute-mvp-chain-runner-local-persistence-baseline` | Prepare Completed package, cleanup commit plan, and explicit PR-ready handoff for user merge |

## Baseline Note
- Legacy promoted baseline step folders (`0010`-`0110`) are retained for audit context only.
- InProgress execution tracking for this phase uses `0120`-`0140`.

## Status Values
- `pending`
- `in_progress`
- `blocked`
- `completed`
