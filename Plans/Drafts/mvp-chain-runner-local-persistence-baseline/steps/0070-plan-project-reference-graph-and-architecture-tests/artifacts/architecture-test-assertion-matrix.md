# Architecture Test Assertion Matrix

## Scope
- Plan: `mvp-chain-runner-local-persistence-baseline`
- Step: `0070-plan-project-reference-graph-and-architecture-tests`

## Required Assertions
| Assertion | Expected Result |
|---|---|
| `StoryEngine.Domain` does not reference `StoryEngine.Infrastructure` | pass |
| `StoryEngine.Domain` does not reference `Zelanthus.API` | pass |
| `StoryEngine.Domain` does not reference provider implementations | pass |
| `StoryEngine.Application` does not reference provider implementations | pass |
| `Zelanthus.Prompting` does not reference `StoryEngine.*` or `API` | pass |
| `Zelanthus.Llm.Clients.Gemini` does not reference `StoryEngine.Domain/Application` | pass |
| `Zelanthus.API` remains composition-root-only boundary | pass |

## Gate Policy
- Architecture assertions must run in `Tests/Zelanthus.Architecture.Tests`.
- Plan 2 execution cannot proceed past early scaffolding if these assertions fail.
