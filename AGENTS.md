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
- For Entity Framework persistence, tracked entities must be `class` types with identity semantics (not `record` types).
- Use `record` types for value-style contracts (DTOs, snapshots, value objects) where value equality is desired.
- Test project names must be semantic and intent-rich (for example `Zelanthus.WorkflowContractProofs.Tests`).
- Avoid low-semantic generic names like `MvpHarness`, `TempTests`, or `MiscTests`.

Source layout and namespace isolation:
- Do not dump production `.cs` files in a project root.
- Project-root `.cs` allowlist is only:
  - `Program.cs` (host bootstrap when needed)
  - `GlobalUsings.cs`
- All other source files must live in semantic subfolders (for example `Contracts/`, `Models/`, `Policies/`, `Storage/`, `Workflows/`).
- Namespaces must follow folder scope and semantic meaning (for example `Zelanthus.StoryEngine.Application.Contracts` for files under `Contracts/`).
- One-tier isolation is minimum only. If a folder starts mixing responsibilities, split into second-tier folders and namespaces immediately.
- High-churn boundaries (especially `Application/Contracts` and `Infrastructure/Storage`) must use second-tier namespaces by concern.
- Storage implementations must be isolated by backend type (for example `Storage/Local`, `Storage/Postgres`) with shared abstractions/records in their own namespaces.
- If a file name needs scope tokens to stay understandable, split by folder and narrow namespace instead.
- Use canonical orchestration terms from `Plans/README.md`: `WorkflowTopology`, `WorkflowNodeExecutionKind`, and `LlmTaskMode`.

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
