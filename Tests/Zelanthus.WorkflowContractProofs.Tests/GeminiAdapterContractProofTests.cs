namespace Zelanthus.WorkflowContractProofs.Tests;

public sealed class GeminiAdapterContractProofTests
{
    [Fact]
    public async Task GeminiAdapter_ValidProviderResponse_ProducesNormalizedResponseEnvelope()
    {
        var protocolClient = new StubGeminiProtocolClient(
            _ => GeminiProtocolResult.Success(
                new GeminiProtocolResponse(
                    ContentText: "Generated chapter plan",
                    Usage: new GeminiUsage(120, 340, 460, 45),
                    ModelId: "gemini-2.5-pro",
                    ThoughtSignature: "tsig-1234",
                    RawSnapshotRef: "snapshots/gemini-response-001.json")));

        var llmClient = new GeminiLlmClient(protocolClient);
        var executionResult = await llmClient.ExecuteAsync(CreateExecutionEnvelope());

        Assert.True(executionResult.IsSuccess);
        var response = Assert.IsType<NormalizedResponseEnvelope>(executionResult.Response);
        Assert.Equal("Generated chapter plan", response.NormalizedResponse.ContentText);
        Assert.Equal("gemini", response.ProviderMetadata.ProviderKey);
        Assert.Equal("gemini-2.5-pro", response.ProviderMetadata.ModelId);
        Assert.Equal("tsig-1234", response.ContinuityHandle);
        Assert.Equal("snapshots/gemini-response-001.json", response.RawSnapshotRef);
        Assert.Equal(120, response.TokenAccounting.PromptTokens.Value);
        Assert.False(response.TokenAccounting.PromptTokens.IsUnknown);

        ContractProofArtifactWriter.Write(
            "gemini-normalized-response.json",
            new
            {
                response.ProviderMetadata.ProviderKey,
                response.ProviderMetadata.ModelId,
                response.NormalizedResponse.ContentText,
                response.ContinuityHandle,
                response.RawSnapshotRef,
                PromptTokens = response.TokenAccounting.PromptTokens.ToString(),
                OutputTokens = response.TokenAccounting.OutputTokens.ToString(),
                TotalTokens = response.TokenAccounting.TotalTokens.ToString(),
                ThoughtTokens = response.TokenAccounting.ThoughtTokens.ToString(),
            });
    }

    [Fact]
    public async Task GeminiAdapter_ProviderProtocolError_EmitsProviderProtocolErrorReasonCode()
    {
        var protocolClient = new StubGeminiProtocolClient(
            _ => GeminiProtocolResult.Failure(
                new GeminiProtocolError(
                    Message: "Malformed provider payload",
                    Code: "malformed_payload",
                    RawSnapshotRef: "snapshots/gemini-error-001.json",
                    Diagnostics: new Dictionary<string, string>(StringComparer.Ordinal)
                    {
                        ["status_code"] = "400",
                    })));

        var llmClient = new GeminiLlmClient(protocolClient);
        var executionResult = await llmClient.ExecuteAsync(CreateExecutionEnvelope());

        Assert.False(executionResult.IsSuccess);
        var failure = Assert.IsType<LlmFailure>(executionResult.Failure);
        Assert.Equal(LlmReasonCodes.ProviderProtocolError, failure.ReasonCode);
        Assert.Equal("snapshots/gemini-error-001.json", failure.RawSnapshotRef);

        ContractProofArtifactWriter.Write(
            "gemini-provider-protocol-error.json",
            new
            {
                failure.ReasonCode,
                failure.Message,
                failure.RawSnapshotRef,
                failure.Diagnostics,
            });
    }

    [Fact]
    public async Task GeminiAdapter_ResponseWithoutUsage_MapsTokenAccountingToUnknown()
    {
        var protocolClient = new StubGeminiProtocolClient(
            _ => GeminiProtocolResult.Success(
                new GeminiProtocolResponse(
                    ContentText: "Generated output without usage",
                    Usage: null,
                    ModelId: "gemini-2.5-pro")));

        var llmClient = new GeminiLlmClient(protocolClient);
        var executionResult = await llmClient.ExecuteAsync(CreateExecutionEnvelope());

        Assert.True(executionResult.IsSuccess);
        var response = Assert.IsType<NormalizedResponseEnvelope>(executionResult.Response);
        Assert.True(response.TokenAccounting.PromptTokens.IsUnknown);
        Assert.True(response.TokenAccounting.OutputTokens.IsUnknown);
        Assert.True(response.TokenAccounting.TotalTokens.IsUnknown);
        Assert.True(response.TokenAccounting.ThoughtTokens.IsUnknown);
        Assert.Equal("unknown", response.TokenAccounting.PromptTokens.ToString());

        ContractProofArtifactWriter.Write(
            "gemini-token-accounting-unknown.json",
            new
            {
                PromptTokens = response.TokenAccounting.PromptTokens.ToString(),
                OutputTokens = response.TokenAccounting.OutputTokens.ToString(),
                TotalTokens = response.TokenAccounting.TotalTokens.ToString(),
                ThoughtTokens = response.TokenAccounting.ThoughtTokens.ToString(),
            });
    }

    [Fact]
    public async Task GeminiAdapter_NegativeUsage_EmitsProviderProtocolErrorReasonCode()
    {
        var protocolClient = new StubGeminiProtocolClient(
            _ => GeminiProtocolResult.Success(
                new GeminiProtocolResponse(
                    ContentText: "Generated output with invalid usage telemetry",
                    Usage: new GeminiUsage(-1, 2, 3, 4),
                    ModelId: "gemini-2.5-pro",
                    RawSnapshotRef: "snapshots/gemini-negative-usage.json")));

        var llmClient = new GeminiLlmClient(protocolClient);
        var executionResult = await llmClient.ExecuteAsync(CreateExecutionEnvelope());

        Assert.False(executionResult.IsSuccess);
        var failure = Assert.IsType<LlmFailure>(executionResult.Failure);
        Assert.Equal(LlmReasonCodes.ProviderProtocolError, failure.ReasonCode);
        Assert.Equal("snapshots/gemini-negative-usage.json", failure.RawSnapshotRef);
        Assert.NotNull(failure.Diagnostics);
        Assert.Equal("prompt_tokens", failure.Diagnostics!["invalid_usage_field"]);
        Assert.Equal("-1", failure.Diagnostics["invalid_usage_value"]);
        Assert.Equal("invalid_usage_payload", failure.Diagnostics["provider_error_code"]);
    }

    private static ExecutionEnvelope CreateExecutionEnvelope()
    {
        return new ExecutionEnvelope(
            new RenderedPromptPayload(
                PromptId: "story.chapter.plan",
                PromptVersion: 1,
                RenderedText: "Render this prompt",
                Checksum: "checksum",
                RequiredPlaceholders: ["objective", "tone"]),
            new Zelanthus.Llm.Clients.Abstractions.Contracts.ExecutionContext(
                ChainMode.CognitiveChain,
                TurnIndex: 1,
                WorkflowKey: "mvp.workflow",
                StepKey: "0010-plan-step",
                CorrelationId: "test-correlation-id"));
    }

    private sealed class StubGeminiProtocolClient : IGeminiProtocolClient
    {
        private readonly Func<ExecutionEnvelope, GeminiProtocolResult> _responseFactory;

        public StubGeminiProtocolClient(Func<ExecutionEnvelope, GeminiProtocolResult> responseFactory)
        {
            _responseFactory = responseFactory;
        }

        public Task<GeminiProtocolResult> ExecuteAsync(
            ExecutionEnvelope executionEnvelope,
            CancellationToken cancellationToken = default)
        {
            return Task.FromResult(_responseFactory(executionEnvelope));
        }
    }
}
