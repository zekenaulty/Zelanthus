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

## Status Values
- `pending`
- `in_progress`
- `blocked`
- `completed`
