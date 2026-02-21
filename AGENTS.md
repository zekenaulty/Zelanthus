# Zelanthus Agent Guidance

Planning authority for this repository:
- `Plans/README.md`

Required behavior:
- Follow folder-first scope isolation for all planning artifacts.
- Keep each plan self-contained in its own plan folder.
- Keep step artifacts inside their owning step folder.

Path casing and token conventions:
- Treat path casing as canonical even on Windows; match existing folder case exactly.
- Repository root folder casing standard:
  - `Plans/`, `Docs/`, `Help/`, `References/`, `Source/`, `Tests/` (when present).
- Planning stage folder names are case-sensitive by convention and must be referenced exactly:
  - `Plans/Brainstorms`, `Plans/Drafts`, `Plans/InProgress`, `Plans/Completed`, `Plans/Archived/CompletedHistory`.
- Use `<plan-folder-name>` as the standard placeholder token in planning path examples.

.NET version baseline:
- Workspace baseline is `.NET 10` (`net10.0`).
- New projects must target `net10.0` unless the user explicitly approves a different target.
- Do not introduce mixed target frameworks without an explicit planning decision.
- If a package or tool requires a target framework change, stop and ask before applying it.
- Test project names must be semantic and intent-rich (for example `Zelanthus.WorkflowContractProofs.Tests`).
- Avoid low-semantic generic names like `MvpHarness`, `TempTests`, or `MiscTests`.

Git and execution non-negotiables:
- Planning work (`Plans/Brainstorms`, `Plans/Drafts`, doctrine/readme updates) defaults to `main`/trunk.
- Do not promote `Drafts -> InProgress` unless the user explicitly says to promote.
- If promotion intent is unclear, ask before moving any plan to `Plans/InProgress/...`.
- After explicit promotion, execute on a feature branch (`feature/<plan-folder-name>` by default).
- Runtime-impacting work (`Source/`, `Tests/`, config, containers, migrations, build changes) must not be committed directly to `main`/trunk.
- Keep commits atomic and semantic; update step notes with full commit SHAs, commands, files changed, and test results.
- If a new dependency is discovered during feature execution, capture the smallest scoped note in the current plan folder and return to implementation.
- Treat template changes under `Plans/Templates/` as constitution-level: change only through an explicit planning decision and dedicated scoped step.

Starter templates:
- `Plans/Templates/`
