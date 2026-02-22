using System.Text.Json;

namespace Zelanthus.WorkflowContractProofs.Tests;

public sealed class ChainRunnerContractProofTests
{
    [Fact]
    public async Task ChainRunner_CognitiveChain_StagedPlanStepsThenExecute_PersistsArtifactsAndCheckpoints()
    {
        var workflow = CreateWorkflowDefinition(
            "story-engine-cognitive",
            WorkflowKind.CognitiveChain,
            [
                CreateWorkflowStep("0010-plan-step", StepKind.PlanStep),
                CreateWorkflowStep("0020-plan-step", StepKind.PlanStep),
                CreateWorkflowStep("0030-execute-step", StepKind.Execute),
            ]);

        var runId = Guid.NewGuid();
        var runCursor = CreateRunCursor(runId, workflow);
        var runPaths = CreateWorkflowRunPaths();
        var runStore = new LocalFileWorkflowRunStore(runPaths);

        var stepExecutor = new ArtifactPersistingStepExecutor(
            runStore,
            resultFactory: context => WorkflowStepExecutionResult.Succeeded($"tsig-{context.StepIndex + 1:0000}"));
        var runner = new WorkflowRunner(stepExecutor);

        var result = await runner.RunAsync(new WorkflowExecutionRequest(workflow, runCursor));
        await PersistRunRecordAsync(runStore, result.WorkflowRunCursor, result.EffectiveStepKeys);

        Assert.True(result.IsSuccess);
        Assert.True(File.Exists(runPaths.GetRunRecordPath(runId)));
        Assert.True(File.Exists(runPaths.GetCheckpointPath(runId, 0)));
        Assert.True(File.Exists(Path.Combine(runPaths.GetTurnDirectory(runId, 0, "0010-plan-step"), "turn.json")));
        Assert.True(File.Exists(Path.Combine(runPaths.GetTurnDirectory(runId, 1, "0020-plan-step"), "turn.json")));
        Assert.True(File.Exists(Path.Combine(runPaths.GetTurnDirectory(runId, 2, "0030-execute-step"), "turn.json")));
    }

    [Fact]
    public async Task ChainRunner_CognitiveChain_StagedPlanExecuteCycles_PropagatesLatestThinkingPersistenceKeyPerStep()
    {
        var workflow = CreateWorkflowDefinition(
            "story-engine-cognitive-propagation",
            WorkflowKind.CognitiveChain,
            [
                CreateWorkflowStep("0010-plan-step", StepKind.PlanStep),
                CreateWorkflowStep("0020-execute-step", StepKind.Execute),
                CreateWorkflowStep("0030-plan-step", StepKind.PlanStep),
                CreateWorkflowStep("0040-execute-step", StepKind.Execute),
            ]);

        var runCursor = CreateRunCursor(Guid.NewGuid(), workflow);
        var stepExecutor = new ArtifactPersistingStepExecutor(
            new LocalFileWorkflowRunStore(CreateWorkflowRunPaths()),
            resultFactory: context => WorkflowStepExecutionResult.Succeeded($"tsig-{context.StepIndex + 1:0000}"));
        var runner = new WorkflowRunner(stepExecutor);

        var result = await runner.RunAsync(new WorkflowExecutionRequest(workflow, runCursor));

        Assert.True(result.IsSuccess);
        Assert.Equal("tsig-0004", result.WorkflowRunCursor.LatestThinkingPersistenceKey);
    }

    [Fact]
    public async Task ChainRunner_CognitiveChain_CapabilitySupported_NullOrEmptyThinkingPersistenceKey_DoesNotAdvanceKeyAndEmitsContinuityHandleInvalid()
    {
        var workflow = CreateWorkflowDefinition(
            "continuity-supported",
            WorkflowKind.CognitiveChain,
            [CreateWorkflowStep("0010-plan-step", StepKind.PlanStep)]);

        var runCursor = CreateRunCursor(
            Guid.NewGuid(),
            workflow,
            WorkflowKind.CognitiveChain,
            latestThinkingPersistenceKey: "existing-thought-key");

        var stepExecutor = new ArtifactPersistingStepExecutor(
            new LocalFileWorkflowRunStore(CreateWorkflowRunPaths()),
            resultFactory: _ => WorkflowStepExecutionResult.TerminalFailure(RunnerReasonCodes.ContinuityHandleInvalid));
        var runner = new WorkflowRunner(stepExecutor);

        var result = await runner.RunAsync(new WorkflowExecutionRequest(workflow, runCursor));

        Assert.False(result.IsSuccess);
        Assert.Equal(RunnerReasonCodes.ContinuityHandleInvalid, result.ReasonCode);
        Assert.Equal("existing-thought-key", result.WorkflowRunCursor.LatestThinkingPersistenceKey);
    }

    [Fact]
    public async Task ChainRunner_CognitiveChain_ContinuityCapabilityUnsupported_MissingHandleIsNonFailure()
    {
        var workflow = CreateWorkflowDefinition(
            "continuity-unsupported",
            WorkflowKind.CognitiveChain,
            [CreateWorkflowStep("0010-plan-step", StepKind.PlanStep)]);

        var runCursor = CreateRunCursor(Guid.NewGuid(), workflow);
        var stepExecutor = new ArtifactPersistingStepExecutor(
            new LocalFileWorkflowRunStore(CreateWorkflowRunPaths()),
            resultFactory: _ => WorkflowStepExecutionResult.Succeeded(latestThinkingPersistenceKey: null));
        var runner = new WorkflowRunner(stepExecutor);

        var result = await runner.RunAsync(new WorkflowExecutionRequest(workflow, runCursor));

        Assert.True(result.IsSuccess);
        Assert.Null(result.ReasonCode);
    }

    [Fact]
    public async Task ChainRunner_CognitiveChain_InterruptedRun_RestartsFromPlanStepWithCognitiveRestartRequiredReasonCode()
    {
        var workflow = CreateWorkflowDefinition(
            "cognitive-restart",
            WorkflowKind.CognitiveChain,
            [
                CreateWorkflowStep("0010-plan-step", StepKind.PlanStep),
                CreateWorkflowStep("0020-plan-step", StepKind.PlanStep),
                CreateWorkflowStep("0030-execute-step", StepKind.Execute),
            ]);

        var runCursor = new WorkflowRunCursor(
            Guid.NewGuid(),
            workflow.WorkflowKey,
            workflow.WorkflowVersion,
            WorkflowKind.CognitiveChain,
            RunState.Created,
            currentStepIndex: 2,
            lastSuccessStepIndex: 1,
            nextTurnIndex: 5,
            nextCheckpointSequence: 8,
            latestThinkingPersistenceKey: "stale-thought-key");

        var stepExecutor = new ArtifactPersistingStepExecutor(
            new LocalFileWorkflowRunStore(CreateWorkflowRunPaths()),
            resultFactory: _ => WorkflowStepExecutionResult.Succeeded("renewed-key"));
        var runner = new WorkflowRunner(stepExecutor);

        var result = await runner.RunAsync(new WorkflowExecutionRequest(workflow, runCursor));

        Assert.True(result.IsSuccess);
        Assert.Equal(RunnerReasonCodes.CognitiveRestartRequired, result.PolicyReasonCode);
        Assert.Equal(0, stepExecutor.ExecutedStepIndices[0]);
    }

    [Fact]
    public async Task ChainRunner_ConversationalChain_InterruptedRun_ResumesFromLastSuccessfulStep()
    {
        var workflow = CreateWorkflowDefinition(
            "conversation-resume",
            WorkflowKind.ConversationalChain,
            [
                CreateWorkflowStep("0010-conversation-step", StepKind.ConversationStep),
                CreateWorkflowStep("0020-conversation-step", StepKind.ConversationStep),
                CreateWorkflowStep("0030-conversation-step", StepKind.ConversationStep),
            ]);

        var runCursor = new WorkflowRunCursor(
            Guid.NewGuid(),
            workflow.WorkflowKey,
            workflow.WorkflowVersion,
            WorkflowKind.ConversationalChain,
            RunState.Created,
            currentStepIndex: 1,
            lastSuccessStepIndex: 0,
            nextTurnIndex: 2,
            nextCheckpointSequence: 2,
            latestThinkingPersistenceKey: null);

        var stepExecutor = new ArtifactPersistingStepExecutor(
            new LocalFileWorkflowRunStore(CreateWorkflowRunPaths()),
            resultFactory: _ => WorkflowStepExecutionResult.Succeeded());
        var runner = new WorkflowRunner(stepExecutor);

        var result = await runner.RunAsync(new WorkflowExecutionRequest(
            workflow,
            runCursor,
            workflow.Steps));

        Assert.True(result.IsSuccess);
        Assert.Null(result.PolicyReasonCode);
        Assert.Equal(1, stepExecutor.ExecutedStepIndices[0]);
    }

    [Fact]
    public async Task ChainRunner_ConversationalChain_InterruptedRun_WithoutRehydratedSteps_EmitsInvalidStateTransition()
    {
        var workflow = CreateWorkflowDefinition(
            "conversation-resume-missing-rehydration",
            WorkflowKind.ConversationalChain,
            [
                CreateWorkflowStep("0010-conversation-step", StepKind.ConversationStep),
                CreateWorkflowStep("0020-conversation-step", StepKind.ConversationStep),
                CreateWorkflowStep("0030-conversation-step", StepKind.ConversationStep),
            ]);

        var runCursor = new WorkflowRunCursor(
            Guid.NewGuid(),
            workflow.WorkflowKey,
            workflow.WorkflowVersion,
            WorkflowKind.ConversationalChain,
            RunState.Created,
            currentStepIndex: 1,
            lastSuccessStepIndex: 0,
            nextTurnIndex: 2,
            nextCheckpointSequence: 2,
            latestThinkingPersistenceKey: null);

        var stepExecutor = new ArtifactPersistingStepExecutor(
            new LocalFileWorkflowRunStore(CreateWorkflowRunPaths()),
            resultFactory: _ => WorkflowStepExecutionResult.Succeeded());
        var runner = new WorkflowRunner(stepExecutor);

        var result = await runner.RunAsync(new WorkflowExecutionRequest(workflow, runCursor));

        Assert.False(result.IsSuccess);
        Assert.Equal(RunnerReasonCodes.InvalidStateTransition, result.ReasonCode);
        Assert.Equal(0, stepExecutor.Invocations);
    }

    [Fact]
    public async Task ChainRunner_ConversationalChain_RehydratedSteps_ExecuteAppendedTail()
    {
        var workflow = CreateWorkflowDefinition(
            "conversation-resume-rehydrated-tail",
            WorkflowKind.ConversationalChain,
            [
                CreateWorkflowStep("0010-conversation-step", StepKind.ConversationStep),
                CreateWorkflowStep("0020-conversation-step", StepKind.ConversationStep),
            ]);

        var runCursor = new WorkflowRunCursor(
            Guid.NewGuid(),
            workflow.WorkflowKey,
            workflow.WorkflowVersion,
            WorkflowKind.ConversationalChain,
            RunState.Created,
            currentStepIndex: 1,
            lastSuccessStepIndex: 0,
            nextTurnIndex: 1,
            nextCheckpointSequence: 1,
            latestThinkingPersistenceKey: null);

        var appendedStep = CreateWorkflowStep("0100-conversation-step", StepKind.ConversationStep);
        var effectiveSteps = workflow.Steps.Concat([appendedStep]).ToArray();

        var stepExecutor = new ArtifactPersistingStepExecutor(
            new LocalFileWorkflowRunStore(CreateWorkflowRunPaths()),
            resultFactory: _ => WorkflowStepExecutionResult.Succeeded());
        var runner = new WorkflowRunner(stepExecutor);

        var result = await runner.RunAsync(new WorkflowExecutionRequest(
            workflow,
            runCursor,
            effectiveSteps));

        Assert.True(result.IsSuccess);
        Assert.Equal(
            ["0020-conversation-step", "0100-conversation-step"],
            stepExecutor.ExecutedStepKeys);
        Assert.Equal(
            ["0010-conversation-step", "0020-conversation-step", "0100-conversation-step"],
            result.EffectiveStepKeys);
    }

    [Fact]
    public void ChainRunner_StateMachine_IllegalTransition_EmitsInvalidStateTransitionReasonCode()
    {
        var exception = Assert.Throws<InvalidOperationException>(
            () => RunStateTransitionRules.EnsureValidTransition(RunState.Created, RunState.Succeeded));

        Assert.Contains(RunnerReasonCodes.InvalidStateTransition, exception.Message, StringComparison.Ordinal);
    }

    [Fact]
    public async Task ChainRunner_VariableLengthQueue_RouteHookAppendsTailWithoutReordering()
    {
        var workflow = CreateWorkflowDefinition(
            "route-tail",
            WorkflowKind.CognitiveChain,
            [
                CreateWorkflowStep("0010-plan-step", StepKind.PlanStep, routeHookKey: "repair-route"),
                CreateWorkflowStep("0020-execute-step", StepKind.Execute),
            ]);

        var runCursor = CreateRunCursor(Guid.NewGuid(), workflow);
        var stepExecutor = new ArtifactPersistingStepExecutor(
            new LocalFileWorkflowRunStore(CreateWorkflowRunPaths()),
            resultFactory: context =>
            {
                if (context.StepIndex == 0)
                {
                    return WorkflowStepExecutionResult.Succeeded(
                        appendedSteps: [CreateWorkflowStep("repair-route-r0001", StepKind.Execute)]);
                }

                return WorkflowStepExecutionResult.Succeeded();
            });
        var runner = new WorkflowRunner(stepExecutor);

        var result = await runner.RunAsync(new WorkflowExecutionRequest(workflow, runCursor));

        Assert.True(result.IsSuccess);
        Assert.Equal(
            ["0010-plan-step", "0020-execute-step", "repair-route-r0001"],
            stepExecutor.ExecutedStepKeys);
        Assert.Contains("repair-route-r0001", result.EffectiveStepKeys);
    }

    [Fact]
    public void ChainRunner_VariableLengthQueue_InvalidRouteHookKey_EmitsDeterministicValidationFailure()
    {
        var exception = Assert.Throws<ArgumentException>(
            () => CreateWorkflowStep("0010-plan-step", StepKind.PlanStep, routeHookKey: "Invalid_Hook"));

        Assert.Contains("Route hook key", exception.Message, StringComparison.Ordinal);
    }

    [Fact]
    public async Task ChainRunner_WorkflowStepPromptRef_MissingOrInvalid_EmitsMissingPromptReference()
    {
        var workflow = CreateWorkflowDefinition(
            "missing-prompt-ref",
            WorkflowKind.CognitiveChain,
            [
                new WorkflowStepDefinition(
                    "0010-plan-step",
                    StepKind.PlanStep,
                    promptReference: null,
                    inputContractReference: "input.contract",
                    outputContractReference: "output.contract"),
            ]);

        var stepExecutor = new ArtifactPersistingStepExecutor(
            new LocalFileWorkflowRunStore(CreateWorkflowRunPaths()),
            resultFactory: _ => WorkflowStepExecutionResult.Succeeded());
        var runner = new WorkflowRunner(stepExecutor);

        var result = await runner.RunAsync(new WorkflowExecutionRequest(
            workflow,
            CreateRunCursor(Guid.NewGuid(), workflow)));

        Assert.False(result.IsSuccess);
        Assert.Equal(RunnerReasonCodes.MissingPromptReference, result.ReasonCode);
        Assert.Equal(0, stepExecutor.Invocations);
    }

    [Fact]
    public async Task ChainRunner_WorkflowKindChainModeMismatch_EmitsInvalidStateTransition()
    {
        var workflow = CreateWorkflowDefinition(
            "mismatch-case",
            WorkflowKind.CognitiveChain,
            [CreateWorkflowStep("0010-plan-step", StepKind.PlanStep)]);

        var runner = new WorkflowRunner(
            new ArtifactPersistingStepExecutor(
                new LocalFileWorkflowRunStore(CreateWorkflowRunPaths()),
                resultFactory: _ => WorkflowStepExecutionResult.Succeeded()));

        var result = await runner.RunAsync(new WorkflowExecutionRequest(
            workflow,
            CreateRunCursor(Guid.NewGuid(), workflow, workflowKind: WorkflowKind.ConversationalChain)));

        Assert.False(result.IsSuccess);
        Assert.Equal(RunnerReasonCodes.InvalidStateTransition, result.ReasonCode);
    }

    [Fact]
    public async Task ChainRunner_ConversationalChain_OutOfRangeResumeIndex_EmitsInvalidStateTransition()
    {
        var workflow = CreateWorkflowDefinition(
            "out-of-range-resume",
            WorkflowKind.ConversationalChain,
            [CreateWorkflowStep("0010-conversation-step", StepKind.ConversationStep)]);

        var runCursor = new WorkflowRunCursor(
            Guid.NewGuid(),
            workflow.WorkflowKey,
            workflow.WorkflowVersion,
            workflow.WorkflowKind,
            RunState.Created,
            currentStepIndex: 2,
            lastSuccessStepIndex: 0,
            nextTurnIndex: 0,
            nextCheckpointSequence: 0,
            latestThinkingPersistenceKey: null);

        var stepExecutor = new ArtifactPersistingStepExecutor(
            new LocalFileWorkflowRunStore(CreateWorkflowRunPaths()),
            resultFactory: _ => WorkflowStepExecutionResult.Succeeded());
        var runner = new WorkflowRunner(stepExecutor);

        var result = await runner.RunAsync(new WorkflowExecutionRequest(workflow, runCursor));

        Assert.False(result.IsSuccess);
        Assert.Equal(RunnerReasonCodes.InvalidStateTransition, result.ReasonCode);
        Assert.Equal(RunState.FailedTerminal, result.WorkflowRunCursor.RunState);
        Assert.Equal(0, stepExecutor.Invocations);
    }

    [Fact]
    public async Task ChainRunner_WorkflowIdentityMismatch_EmitsInvalidStateTransition()
    {
        var workflow = CreateWorkflowDefinition(
            "workflow-identity-match",
            WorkflowKind.CognitiveChain,
            [CreateWorkflowStep("0010-plan-step", StepKind.PlanStep)]);

        var stepExecutor = new ArtifactPersistingStepExecutor(
            new LocalFileWorkflowRunStore(CreateWorkflowRunPaths()),
            resultFactory: _ => WorkflowStepExecutionResult.Succeeded());
        var runner = new WorkflowRunner(stepExecutor);

        var mismatchedCursor = new WorkflowRunCursor(
            Guid.NewGuid(),
            workflowKey: "workflow-identity-mismatch",
            workflowVersion: workflow.WorkflowVersion + 1,
            workflowKind: WorkflowKind.CognitiveChain,
            runState: RunState.Created,
            currentStepIndex: 0,
            lastSuccessStepIndex: -1,
            nextTurnIndex: 0,
            nextCheckpointSequence: 0,
            latestThinkingPersistenceKey: null);

        var result = await runner.RunAsync(new WorkflowExecutionRequest(workflow, mismatchedCursor));

        Assert.False(result.IsSuccess);
        Assert.Equal(RunnerReasonCodes.InvalidStateTransition, result.ReasonCode);
        Assert.Equal(0, stepExecutor.Invocations);
    }

    [Fact]
    public async Task ChainRunner_CancellationDuringStepExecution_TransitionsRunStateToCancelled()
    {
        var workflow = CreateWorkflowDefinition(
            "step-cancellation",
            WorkflowKind.CognitiveChain,
            [CreateWorkflowStep("0010-plan-step", StepKind.PlanStep)]);

        var runCursor = CreateRunCursor(Guid.NewGuid(), workflow);
        var stepExecutor = new CanceledStepExecutor();
        var runner = new WorkflowRunner(stepExecutor);

        using var cancellation = new CancellationTokenSource();
        await cancellation.CancelAsync();

        await Assert.ThrowsAnyAsync<OperationCanceledException>(
            () => runner.RunAsync(new WorkflowExecutionRequest(workflow, runCursor), cancellation.Token));

        Assert.Equal(RunState.Cancelled, runCursor.RunState);
        Assert.Equal(1, stepExecutor.Invocations);
    }

    [Fact]
    public async Task ChainRunner_NonRunnableCursorState_EmitsInvalidStateTransitionWithoutThrowing()
    {
        var workflow = CreateWorkflowDefinition(
            "non-runnable-cursor-state",
            WorkflowKind.CognitiveChain,
            [CreateWorkflowStep("0010-plan-step", StepKind.PlanStep)]);

        var runCursor = new WorkflowRunCursor(
            Guid.NewGuid(),
            workflow.WorkflowKey,
            workflow.WorkflowVersion,
            workflow.WorkflowKind,
            RunState.Succeeded,
            currentStepIndex: 0,
            lastSuccessStepIndex: 0,
            nextTurnIndex: 1,
            nextCheckpointSequence: 1,
            latestThinkingPersistenceKey: null);

        var stepExecutor = new ArtifactPersistingStepExecutor(
            new LocalFileWorkflowRunStore(CreateWorkflowRunPaths()),
            resultFactory: _ => WorkflowStepExecutionResult.Succeeded());
        var runner = new WorkflowRunner(stepExecutor);

        var result = await runner.RunAsync(new WorkflowExecutionRequest(workflow, runCursor));

        Assert.False(result.IsSuccess);
        Assert.Equal(RunnerReasonCodes.InvalidStateTransition, result.ReasonCode);
        Assert.Equal(RunState.Succeeded, result.WorkflowRunCursor.RunState);
        Assert.Equal(0, stepExecutor.Invocations);
    }

    [Theory]
    [InlineData("missing_required_placeholder")]
    [InlineData("checkpoint_write_failed")]
    [InlineData("checkpoint_read_failed")]
    [InlineData("artifact_write_failed")]
    [InlineData("artifact_read_failed")]
    public async Task ChainRunner_ReasonCodedTerminalFailures_AreDeterministic(string reasonCode)
    {
        var workflow = CreateWorkflowDefinition(
            $"failure-{reasonCode}",
            WorkflowKind.CognitiveChain,
            [CreateWorkflowStep("0010-plan-step", StepKind.PlanStep)]);

        var runner = new WorkflowRunner(
            new ArtifactPersistingStepExecutor(
                new LocalFileWorkflowRunStore(CreateWorkflowRunPaths()),
                resultFactory: _ => WorkflowStepExecutionResult.TerminalFailure(reasonCode)));

        var result = await runner.RunAsync(new WorkflowExecutionRequest(
            workflow,
            CreateRunCursor(Guid.NewGuid(), workflow)));

        Assert.False(result.IsSuccess);
        Assert.False(result.IsRetryableFailure);
        Assert.Equal(reasonCode, result.ReasonCode);
    }

    [Fact]
    public async Task ChainRunner_ProviderProtocolError_EmitsReasonCodeAndAppliesRetryPolicy()
    {
        var workflow = CreateWorkflowDefinition(
            "provider-protocol-failure",
            WorkflowKind.CognitiveChain,
            [CreateWorkflowStep("0010-plan-step", StepKind.PlanStep)]);

        var runner = new WorkflowRunner(
            new ArtifactPersistingStepExecutor(
                new LocalFileWorkflowRunStore(CreateWorkflowRunPaths()),
                resultFactory: _ => WorkflowStepExecutionResult.RetryableFailure(RunnerReasonCodes.ProviderProtocolError)));

        var result = await runner.RunAsync(new WorkflowExecutionRequest(
            workflow,
            CreateRunCursor(Guid.NewGuid(), workflow)));

        Assert.False(result.IsSuccess);
        Assert.True(result.IsRetryableFailure);
        Assert.Equal(RunnerReasonCodes.ProviderProtocolError, result.ReasonCode);
        Assert.Equal(RunState.WaitingRetry, result.WorkflowRunCursor.RunState);
    }

    [Fact]
    public void ChainRunner_ArtifactPathDeterminism_UsesTurnIndexAndStepKeyOnly()
    {
        var runId = Guid.NewGuid();
        var runPaths = CreateWorkflowRunPaths();

        Assert.Contains(
            $"{Path.DirectorySeparatorChar}turns{Path.DirectorySeparatorChar}7-0010-plan-step",
            runPaths.GetTurnDirectory(runId, 7, "0010-plan-step"),
            StringComparison.Ordinal);
        Assert.EndsWith(
            $"{Path.DirectorySeparatorChar}failures{Path.DirectorySeparatorChar}failure-5.json",
            runPaths.GetFailurePath(runId, 5),
            StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public async Task ChainRunner_TurnMetadata_TurnJsonIsCanonicalSourceOfTruth()
    {
        var runId = Guid.NewGuid();
        var runPaths = CreateWorkflowRunPaths();
        var runStore = new LocalFileWorkflowRunStore(runPaths);

        await runStore.SaveTurnRecordAsync(
            runId,
            new WorkflowTurnRecord(
                TurnIndex: 4,
                StepKey: "0010-plan-step",
                StepIndex: 0,
                Objective: "Build chapter plan",
                CreatedUtc: DateTimeOffset.UtcNow,
                UpdatedUtc: DateTimeOffset.UtcNow));

        var turnPath = Path.Combine(runPaths.GetTurnDirectory(runId, 4, "0010-plan-step"), "turn.json");
        using var json = JsonDocument.Parse(File.ReadAllText(turnPath));

        Assert.Equal(4, json.RootElement.GetProperty("turnIndex").GetInt32());
        Assert.Equal("0010-plan-step", json.RootElement.GetProperty("stepKey").GetString());
        Assert.Equal("Build chapter plan", json.RootElement.GetProperty("objective").GetString());
    }

    [Fact]
    public void ChainRunner_Counters_CrashAfterReservation_DoesNotReuseTurnOrCheckpointIndices()
    {
        var workflow = CreateWorkflowDefinition(
            "counter-crash-behavior",
            WorkflowKind.CognitiveChain,
            [CreateWorkflowStep("0010-plan-step", StepKind.PlanStep)]);

        var runCursor = new WorkflowRunCursor(
            Guid.NewGuid(),
            workflow.WorkflowKey,
            workflow.WorkflowVersion,
            WorkflowKind.CognitiveChain,
            RunState.Created,
            currentStepIndex: 0,
            lastSuccessStepIndex: -1,
            nextTurnIndex: 10,
            nextCheckpointSequence: 15,
            latestThinkingPersistenceKey: null);

        Assert.Equal(10, runCursor.ReserveTurnIndex());
        Assert.Equal(15, runCursor.ReserveCheckpointSequence());
        Assert.Equal(11, runCursor.ReserveTurnIndex());
        Assert.Equal(16, runCursor.ReserveCheckpointSequence());
    }

    [Fact]
    public void ChainRunner_ProvenanceArtifact_StrictSuperset_MapsLosslesslyToPromptingRequiredFields()
    {
        var mapped = PromptingProvenanceMapper.MapToPrompting(
            new ProvenanceArtifactRecord(
                PromptId: "story.chapter.plan",
                PromptVersion: 1,
                PromptChecksum: "abc123",
                ProviderKey: "gemini",
                ModelId: "gemini-2.5-pro",
                ReasonCode: null,
                Diagnostics: null));

        Assert.Equal("story.chapter.plan", mapped.PromptId);
        Assert.Equal(1, mapped.PromptVersion);
        Assert.Equal("abc123", mapped.PromptChecksum);

        var exception = Assert.Throws<InvalidOperationException>(
            () => PromptingProvenanceMapper.MapToPrompting(
                new ProvenanceArtifactRecord(
                    PromptId: string.Empty,
                    PromptVersion: 0,
                    PromptChecksum: string.Empty,
                    ProviderKey: "gemini",
                    ModelId: "gemini-2.5-pro",
                    ReasonCode: null,
                    Diagnostics: null)));

        Assert.Contains(LocalPersistenceReasonCodes.ArtifactReadFailed, exception.Message, StringComparison.Ordinal);
    }

    [Fact]
    public async Task LocalFileWorkflowRunStore_SaveRunRecord_CancelledToken_ThrowsOperationCanceledException()
    {
        var runStore = new LocalFileWorkflowRunStore(CreateWorkflowRunPaths());
        var runId = Guid.NewGuid();
        var runRecord = new WorkflowRunRecord(
            runId,
            WorkflowKey: "workflow.cancellation.check",
            WorkflowVersion: 1,
            WorkflowKind: WorkflowKind.CognitiveChain.ToString(),
            RunState: RunState.Created.ToString(),
            CurrentStepIndex: 0,
            LastSuccessStepIndex: -1,
            NextTurnIndex: 0,
            NextCheckpointSequence: 0,
            LatestThinkingPersistenceKey: null,
            EffectiveStepKeys: ["0010-plan-step"],
            UpdatedUtc: DateTimeOffset.UtcNow);

        using var cancellation = new CancellationTokenSource();
        await cancellation.CancelAsync();

        await Assert.ThrowsAnyAsync<OperationCanceledException>(
            () => runStore.SaveRunRecordAsync(runRecord, cancellation.Token));
    }

    [Fact]
    public async Task LocalFileWorkflowRunStore_SaveRunRecord_WithSharedReadHandle_AllowsOverwrite()
    {
        var runPaths = CreateWorkflowRunPaths();
        var runStore = new LocalFileWorkflowRunStore(runPaths);
        var runId = Guid.NewGuid();

        var firstRecord = new WorkflowRunRecord(
            runId,
            WorkflowKey: "workflow.shared-read-overwrite",
            WorkflowVersion: 1,
            WorkflowKind: WorkflowKind.CognitiveChain.ToString(),
            RunState: RunState.Created.ToString(),
            CurrentStepIndex: 0,
            LastSuccessStepIndex: -1,
            NextTurnIndex: 0,
            NextCheckpointSequence: 0,
            LatestThinkingPersistenceKey: null,
            EffectiveStepKeys: ["0010-plan-step"],
            UpdatedUtc: DateTimeOffset.UtcNow);

        await runStore.SaveRunRecordAsync(firstRecord);

        var runRecordPath = runPaths.GetRunRecordPath(runId);
        using var sharedReader = new FileStream(
            runRecordPath,
            FileMode.Open,
            FileAccess.Read,
            FileShare.ReadWrite | FileShare.Delete);

        var secondRecord = firstRecord with
        {
            NextCheckpointSequence = 1,
            UpdatedUtc = DateTimeOffset.UtcNow,
        };

        await runStore.SaveRunRecordAsync(secondRecord);
        var reloadedRecord = await runStore.ReadRunRecordAsync(runId);

        Assert.NotNull(reloadedRecord);
        Assert.Equal(1, reloadedRecord!.NextCheckpointSequence);
    }

    private static WorkflowDefinition CreateWorkflowDefinition(
        string workflowKey,
        WorkflowKind workflowKind,
        IReadOnlyList<WorkflowStepDefinition> steps)
    {
        return new WorkflowDefinition(workflowKey, workflowKind, workflowVersion: 1, steps);
    }

    private static WorkflowStepDefinition CreateWorkflowStep(
        string stepKey,
        StepKind stepKind,
        string? routeHookKey = null)
    {
        return new WorkflowStepDefinition(
            stepKey,
            stepKind,
            new PromptReference("story.chapter.plan", 1),
            inputContractReference: "input.contract",
            outputContractReference: "output.contract",
            routeHookKey: routeHookKey);
    }

    private static WorkflowRunCursor CreateRunCursor(
        Guid runId,
        WorkflowDefinition workflowDefinition,
        WorkflowKind? workflowKind = null,
        string? latestThinkingPersistenceKey = null)
    {
        return new WorkflowRunCursor(
            runId,
            workflowDefinition.WorkflowKey,
            workflowDefinition.WorkflowVersion,
            workflowKind ?? workflowDefinition.WorkflowKind,
            RunState.Created,
            currentStepIndex: 0,
            lastSuccessStepIndex: -1,
            nextTurnIndex: 0,
            nextCheckpointSequence: 0,
            latestThinkingPersistenceKey: latestThinkingPersistenceKey);
    }

    private static WorkflowRunPaths CreateWorkflowRunPaths()
    {
        return new WorkflowRunPaths(GetRepositoryRoot());
    }

    private static async Task PersistRunRecordAsync(
        IWorkflowRunStore runStore,
        WorkflowRunCursor runCursor,
        IReadOnlyList<string>? effectiveStepKeys = null)
    {
        await runStore.SaveRunRecordAsync(
            new WorkflowRunRecord(
                runCursor.RunId,
                runCursor.WorkflowKey,
                runCursor.WorkflowVersion,
                runCursor.WorkflowKind.ToString(),
                runCursor.RunState.ToString(),
                runCursor.CurrentStepIndex,
                runCursor.LastSuccessStepIndex,
                runCursor.NextTurnIndex,
                runCursor.NextCheckpointSequence,
                runCursor.LatestThinkingPersistenceKey,
                effectiveStepKeys,
                DateTimeOffset.UtcNow));
    }

    private static string GetRepositoryRoot()
    {
        var directory = new DirectoryInfo(AppContext.BaseDirectory);
        while (directory is not null)
        {
            if (File.Exists(Path.Combine(directory.FullName, "Zelanthus.slnx")))
            {
                return directory.FullName;
            }

            directory = directory.Parent;
        }

        throw new InvalidOperationException("Could not locate repository root containing Zelanthus.slnx.");
    }

    private sealed class ArtifactPersistingStepExecutor : IWorkflowStepExecutor
    {
        private readonly IWorkflowRunStore _runStore;
        private readonly Func<WorkflowStepExecutionContext, WorkflowStepExecutionResult> _resultFactory;

        public ArtifactPersistingStepExecutor(
            IWorkflowRunStore runStore,
            Func<WorkflowStepExecutionContext, WorkflowStepExecutionResult> resultFactory)
        {
            _runStore = runStore;
            _resultFactory = resultFactory;
        }

        public int Invocations { get; private set; }

        public List<int> ExecutedStepIndices { get; } = [];

        public List<string> ExecutedStepKeys { get; } = [];

        public async Task<WorkflowStepExecutionResult> ExecuteAsync(
            WorkflowStepExecutionContext executionContext,
            CancellationToken cancellationToken = default)
        {
            Invocations++;
            ExecutedStepIndices.Add(executionContext.StepIndex);
            ExecutedStepKeys.Add(executionContext.WorkflowStepDefinition.StepKey);

            var turnIndex = executionContext.WorkflowRunCursor.ReserveTurnIndex();
            var checkpointSequence = executionContext.WorkflowRunCursor.ReserveCheckpointSequence();
            var now = DateTimeOffset.UtcNow;

            await _runStore.SaveTurnRecordAsync(
                executionContext.WorkflowRunCursor.RunId,
                new WorkflowTurnRecord(
                    turnIndex,
                    executionContext.WorkflowStepDefinition.StepKey,
                    executionContext.StepIndex,
                    $"Execute {executionContext.WorkflowStepDefinition.StepKey}",
                    now,
                    now),
                cancellationToken);

            await _runStore.SaveCheckpointAsync(
                executionContext.WorkflowRunCursor.RunId,
                checkpointSequence,
                new
                {
                    checkpointSequence,
                    stepIndex = executionContext.StepIndex,
                    stepKey = executionContext.WorkflowStepDefinition.StepKey,
                },
                cancellationToken);

            var result = _resultFactory(executionContext);
            if (!result.IsSuccess)
            {
                await _runStore.SaveFailureRecordAsync(
                    executionContext.WorkflowRunCursor.RunId,
                    new WorkflowFailureRecord(
                        turnIndex,
                        result.ReasonCode!,
                        Diagnostics: null,
                        CreatedUtc: DateTimeOffset.UtcNow),
                    cancellationToken);
            }

            return result;
        }
    }

    private sealed class CanceledStepExecutor : IWorkflowStepExecutor
    {
        public int Invocations { get; private set; }

        public Task<WorkflowStepExecutionResult> ExecuteAsync(
            WorkflowStepExecutionContext executionContext,
            CancellationToken cancellationToken = default)
        {
            Invocations++;
            var canceledToken = cancellationToken.IsCancellationRequested
                ? cancellationToken
                : new CancellationToken(canceled: true);
            return Task.FromCanceled<WorkflowStepExecutionResult>(canceledToken);
        }
    }
}
