# Step: 0030-define-application-orchestration-flow-and-chain-router

## Goal
- Define application-layer runner orchestration flow and chain router behavior for cognitive and conversational execution.

## Context
- Application layer owns semantic transform, chain-mode choice, and retry/resume policy entry points.
- Provider adapter and infrastructure should not absorb orchestration semantics.

## Git Branch
- `feature/thin-clients-first-solution-structure`

## Commits
- `none`

## Commands Executed
- `dotnet build "Zelanthus.slnx"`
- `dotnet test "Tests/Zelanthus.Architecture.Tests/Zelanthus.Architecture.Tests.csproj"`

## Files Changed
- `Source/Zelanthus.StoryEngine.Application/RunnerReasonCodes.cs`
- `Source/Zelanthus.StoryEngine.Application/WorkflowExecutionRequest.cs`
- `Source/Zelanthus.StoryEngine.Application/WorkflowExecutionResult.cs`
- `Source/Zelanthus.StoryEngine.Application/WorkflowStepExecutionContext.cs`
- `Source/Zelanthus.StoryEngine.Application/WorkflowStepExecutionResult.cs`
- `Source/Zelanthus.StoryEngine.Application/IWorkflowStepExecutor.cs`
- `Source/Zelanthus.StoryEngine.Application/IWorkflowRunner.cs`
- `Source/Zelanthus.StoryEngine.Application/WorkflowRunner.cs`
- `Source/Zelanthus.StoryEngine.Application/Class1.cs` (deleted)

## Outputs
- `Plans/InProgress/mvp-chain-runner-local-persistence-baseline/plan.md`
  - application orchestration ownership and runner execution contract are explicit.
- `Plans/InProgress/mvp-chain-runner-local-persistence-baseline/decisions/0003-project-reference-graph-and-boundary-enforcement-v0.md`
  - orchestration boundary ownership is aligned with project references.
- `Plans/InProgress/mvp-chain-runner-local-persistence-baseline/decisions/0005-minimal-workflow-abstraction-v0.md`
  - workflow/step abstraction is integrated into orchestration flow planning.

## Tests / Results
- `dotnet build "Zelanthus.slnx"` -> `passed` (0 warnings, 0 errors)
- `dotnet test "Tests/Zelanthus.Architecture.Tests/Zelanthus.Architecture.Tests.csproj"` -> `passed` (7 tests)

## Acceptance Evidence
- Orchestration flow explicitly distinguishes:
  - chain-mode selection,
  - workflow step objective assembly (`PLAN_STEP`, `EXECUTE`, `CONVERSATION_STEP`),
  - step-level `prompt_ref` resolution (`prompt_id`, `prompt_version`),
  - `ILlmClient` invocation path,
  - post-call validation and persistence dispatch.
- Flow supports variable step counts and does not encode a fixed single-pair loop.
- Chain routing contract includes explicit upgrade/downgrade conditions between single-turn and chain execution.
- Flow supports staged `PLAN_STEP` sequences followed by one or more `EXECUTE` steps in cognitive mode.
- Variable-length expansion mechanism is explicit: route hooks append steps to run-local queue tail only.
- Route-hook appended step identity is deterministic (`<route_hook_key>-r<NNNN>`) and persisted in effective queue state before appended-step execution.
- Missing/invalid step `prompt_ref` is deterministic terminal failure (`missing_prompt_reference`) before provider invocation.
- `workflow_kind`/`chain_mode` mismatch handling is deterministic and explicitly reason-coded.

## Issues
- none

## Decision
- accepted: application orchestration contracts and runner baseline are implemented with chain-mode alignment checks, cognitive restart behavior, and queue-tail route-hook extension support.

## Completion
- `completed`

## Next Actions
- Execute step `0040-define-local-persistence-artifact-and-checkpoint-layout`.



