# Step: 0070-plan-project-reference-graph-and-architecture-tests

## Goal
- Define project reference updates and architecture-test assertions that enforce Plan 2 dependency boundaries.

## Context
- Plan 2 introduces StoryEngine projects and cross-project references that can drift without an explicit gate.

## Git Branch
- `main`

## Commits
- `none`

## Commands Executed
- `none`

## Files Changed
- `none`

## Outputs
- `Plans/Drafts/mvp-chain-runner-local-persistence-baseline/decisions/0003-project-reference-graph-and-boundary-enforcement-v0.md`
  - allowed/forbidden references and enforcement policy are explicit.
- `Plans/Drafts/mvp-chain-runner-local-persistence-baseline/steps/0070-plan-project-reference-graph-and-architecture-tests/artifacts/architecture-test-assertion-matrix.md`
  - required architecture assertions for Domain/Application/Infrastructure/API/provider boundaries.
- `Plans/Drafts/mvp-chain-runner-local-persistence-baseline/artifacts/project-dependency-implementation-matrix.md`
  - implementation boundary matrix is aligned with architecture assertions.

## Tests / Results
- `not-run` -> draft artifact complete

## Acceptance Evidence
- Architecture assertion matrix covers:
  - domain has no infra/provider/API dependencies,
  - application has no provider implementation dependency,
  - prompting has no story engine/API dependencies,
  - provider implementation has no story engine domain/application dependencies.
- Step output maps each assertion to planned test project coverage.

## Issues
- none

## Decision
- accepted

## Completion
- `completed`

## Next Actions
- Define runner proof tests and evidence artifact requirements.
