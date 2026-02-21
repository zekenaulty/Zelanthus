# Project Dependency Implementation Matrix

## Scope
- Plan: `mvp-chain-runner-local-persistence-baseline`
- Purpose: lock dependency and implementation boundaries before Plan 2 promotion to `InProgress`.

## Matrix
| Project | Primary Responsibility | Allowed Runtime References | Forbidden Runtime References | Planned Test Coverage |
|---|---|---|---|---|
| `Source/Zelanthus.StoryEngine.Domain` | chain/run/turn invariants | `none` | `StoryEngine.Infrastructure`, `API`, provider implementations | architecture tests for no forbidden deps; domain unit tests |
| `Source/Zelanthus.StoryEngine.Application` | runner orchestration and policy | `StoryEngine.Domain`, `Prompting`, `Llm.Clients.Abstractions` | provider implementations, storage/file IO concerns | orchestration unit tests + contract-proof tests |
| `Source/Zelanthus.StoryEngine.Infrastructure` | local persistence stores and mappers | `StoryEngine.Domain`, `StoryEngine.Application`, `Prompting`, `Llm.Clients.Abstractions` | API concerns and semantic chain policy decisions | mapper round-trip tests + failure-path tests |
| `Source/Zelanthus.Llm.Clients.Gemini` | provider protocol adapter | `Llm.Clients.Abstractions` | `StoryEngine.Domain`, `StoryEngine.Application` | adapter contract tests (Plan 1 + Plan 2 reuse) |
| `Source/Zelanthus.API` | composition root | `StoryEngine.Application`, `StoryEngine.Infrastructure`, provider adapter | domain rule logic and persistence transform logic | composition smoke checks (no endpoint breadth in Plan 2) |
| `Tests/Zelanthus.Architecture.Tests` | boundary enforcement | project-under-test refs only | n/a | dependency-direction gate assertions |
| `Tests/Zelanthus.WorkflowContractProofs.Tests` | deterministic proof/evidence | contracts + fixture harness | brittle provider prose assertions | golden/failure/resume artifact evidence tests |

## Review Gate
- Any reference outside this matrix requires explicit decision update before execution.
