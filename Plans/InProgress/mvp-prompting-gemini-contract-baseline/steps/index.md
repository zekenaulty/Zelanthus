# Steps Index

Use this file for dependency and status summaries.
Do not use this file as the only source of step order.
Step order must still be visible in folder names (`NNNN-semantic-step-name`).

## Step Map
| Order | Step Folder | Status | Depends On | Notes |
|---|---|---|---|---|
| 0010 | `0010-scaffold-projects-and-references` | `completed` | `none` | Create initial Source/Tests projects and wire solution references |
| 0020 | `0020-define-prompting-contracts-and-rendering-rules` | `pending` | `0010-scaffold-projects-and-references`, `0050-add-architecture-boundary-tests` | Define prompt identity/version/render/checksum and required-placeholder enforcement behavior |
| 0030 | `0030-define-llm-client-abstractions-and-capability-profile` | `pending` | `0010-scaffold-projects-and-references`, `0050-add-architecture-boundary-tests` | Define execution envelope, normalized response, capabilities, and reason-coded errors |
| 0040 | `0040-implement-gemini-adapter-normalization-path` | `pending` | `0030-define-llm-client-abstractions-and-capability-profile`, `0050-add-architecture-boundary-tests` | Map Gemini protocol to abstraction contracts and normalized metadata |
| 0050 | `0050-add-architecture-boundary-tests` | `pending` | `0010-scaffold-projects-and-references` | Add dependency-direction tests as an early enforcement gate; must complete before 0020/0030/0040 |
| 0060 | `0060-implement-contract-proof-tests-and-local-artifact-persistence` | `pending` | `0020-define-prompting-contracts-and-rendering-rules`, `0030-define-llm-client-abstractions-and-capability-profile`, `0040-implement-gemini-adapter-normalization-path` | Build contract-proof tests and artifact/provenance persistence proof path |
| 0070 | `0070-validate-golden-path-and-reason-coded-failure-path` | `pending` | `0050-add-architecture-boundary-tests`, `0060-implement-contract-proof-tests-and-local-artifact-persistence` | Validate success and failure evidence against Plan 1 acceptance criteria |

## Status Values
- `pending`
- `in_progress`
- `blocked`
- `completed`
