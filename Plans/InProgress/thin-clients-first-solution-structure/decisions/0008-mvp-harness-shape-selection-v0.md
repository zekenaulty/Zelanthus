# Decision 0008: MVP Harness Shape Selection v0

## Status
- accepted

## Decision Summary
- Select a dedicated contract-proof test project under `Tests/` as the default Plan 1 proving surface.
- Defer API-hosted harness execution to a follow-up only if test-host criteria fail.

## Selected Shape
- Primary shape:
  - `Tests/Zelanthus.WorkflowContractProofs.Tests` (integration-style test project).
- Command entry point:
  - `dotnet test` for deterministic execution and CI friendliness.

## Naming Intent
- This test project is long-lived and reusable for one-off MVP/contract proof phases.
- Name encodes function, not milestone:
  - `WorkflowContractProofs` communicates purpose better than generic labels like `MvpHarness`.
- New test project naming rule for this scope:
  - `Zelanthus.<semantic-purpose>.Tests`
  - avoid generic names with low semantic intent (`MvpHarness`, `TempTests`, `MiscTests`).

## Why This Shape
- Lowest setup overhead while contracts are still stabilizing.
- Keeps harness focused on prompt/client/provenance contracts without API transport noise.
- Improves repeatability and failure isolation for early MVP iterations.
- Aligns with current non-goal: no production persistence/deployment setup in Plan 1.

## Acceptance Requirements (from Decision 0007)
- Harness runs a deterministic MVP proof path with explicit objectives and validations.
- Harness persists local artifact and provenance evidence.
- Harness captures raw snapshot references and normalized metadata.
- Harness emits reason-coded failures for baseline failure scenarios.

## API-Host Deferral Rule
- API-host harness remains a valid later evolution path.
- Promote to API-host only if one or more criteria are unmet in test-host shape:
  - required execution lifecycle hooks cannot be modeled in tests,
  - dependency wiring fidelity cannot be proven without API composition root,
  - operational diagnostics require API middleware context.

## Consequences
- Plan 1 drafting will assume a test-host MVP harness project in `Tests/`.
- Plan 1 DoD must include harness evidence artifacts produced from test runs.
- Plan 2 can reuse harness contracts while introducing runner orchestration and persistence evolution.
- First proof tests in this project must also use semantic names that express contract intent.
