# Steps Index

Use this file for dependency and status summaries.
Do not use this file as the only source of step order.
Step order must still be visible in folder names (`NNNN-semantic-step-name`).

## Step Map
| Order | Step Folder | Status | Depends On | Notes |
|---|---|---|---|---|
| 0010 | `0010-inventory-legacy-usage-and-rename-impact` | `pending` | `none` | Inventory all legacy term usage and classify rename/adapter impact. |
| 0020 | `0020-define-code-rename-and-adapter-strategy` | `pending` | `0010` | Lock exact type/field rename strategy and deprecation approach. |
| 0030 | `0030-define-persistence-compatibility-and-migration-path` | `pending` | `0010`, `0020` | Lock legacy artifact compatibility policy and migration behavior. |
| 0040 | `0040-define-proof-updates-and-audit-closeout` | `pending` | `0020`, `0030` | Define proof updates, acceptance evidence, and closeout audit data. |

## Status Values
- `pending`
- `in_progress`
- `blocked`
- `completed`
