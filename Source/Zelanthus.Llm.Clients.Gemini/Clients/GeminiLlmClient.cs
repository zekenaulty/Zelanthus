namespace Zelanthus.Llm.Clients.Gemini.Clients;

public sealed class GeminiLlmClient : ILlmClient
{
    private const string UnknownModelId = "unknown";

    private readonly IGeminiProtocolClient _protocolClient;

    public GeminiLlmClient(
        IGeminiProtocolClient protocolClient,
        string providerKey = "gemini")
    {
        _protocolClient = protocolClient ?? throw new ArgumentNullException(nameof(protocolClient));

        if (string.IsNullOrWhiteSpace(providerKey))
        {
            throw new ArgumentException("Provider key is required.", nameof(providerKey));
        }

        ProviderKey = providerKey;
    }

    public string ProviderKey { get; }

    public LlmCapabilityProfile CapabilityProfile { get; } = new(
        SupportsContinuityHandle: true,
        SupportsThinking: true,
        SupportsStructuredOutput: true,
        SupportsJsonMode: true);

    public async Task<LlmExecutionResult> ExecuteAsync(
        ExecutionEnvelope executionEnvelope,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(executionEnvelope);

        var protocolResult = await _protocolClient.ExecuteAsync(executionEnvelope, cancellationToken).ConfigureAwait(false);
        if (protocolResult.Error is not null)
        {
            return LlmExecutionResult.Failed(CreateFailure(protocolResult.Error));
        }

        var response = protocolResult.Response
            ?? throw new InvalidOperationException("Protocol result did not include a response.");

        var metadata = new ProviderMetadata(
            ProviderKey,
            response.ModelId ?? UnknownModelId,
            response.Metadata);

        var normalizedResponse = new NormalizedResponse(response.ContentText ?? string.Empty);
        if (!TryMapTokenAccounting(
            response.Usage,
            out var tokenAccounting,
            out var invalidUsageField,
            out var invalidUsageValue))
        {
            return LlmExecutionResult.Failed(
                CreateInvalidUsageFailure(
                    response.ModelId ?? UnknownModelId,
                    response.RawSnapshotRef,
                    invalidUsageField!,
                    invalidUsageValue));
        }

        var envelope = new NormalizedResponseEnvelope(
            normalizedResponse,
            tokenAccounting,
            metadata,
            response.RawSnapshotRef,
            response.ThoughtSignature);

        return LlmExecutionResult.Success(envelope);
    }

    private LlmFailure CreateFailure(GeminiProtocolError protocolError)
    {
        var metadata = new ProviderMetadata(
            ProviderKey,
            UnknownModelId,
            protocolError.Code is null
                ? null
                : new Dictionary<string, string>(StringComparer.Ordinal)
                {
                    ["provider_error_code"] = protocolError.Code,
                });

        return new LlmFailure(
            LlmReasonCodes.ProviderProtocolError,
            protocolError.Message,
            metadata,
            protocolError.RawSnapshotRef,
            protocolError.Diagnostics);
    }

    private LlmFailure CreateInvalidUsageFailure(
        string modelId,
        string? rawSnapshotRef,
        string invalidUsageField,
        int invalidUsageValue)
    {
        var diagnostics = new Dictionary<string, string>(StringComparer.Ordinal)
        {
            ["provider_error_code"] = "invalid_usage_payload",
            ["invalid_usage_field"] = invalidUsageField,
            ["invalid_usage_value"] = invalidUsageValue.ToString(),
        };

        var metadata = new ProviderMetadata(ProviderKey, modelId, diagnostics);
        return new LlmFailure(
            LlmReasonCodes.ProviderProtocolError,
            "Provider usage payload contained a negative token count.",
            metadata,
            rawSnapshotRef,
            diagnostics);
    }

    private static bool TryMapTokenAccounting(
        GeminiUsage? usage,
        out TokenAccounting tokenAccounting,
        out string? invalidUsageField,
        out int invalidUsageValue)
    {
        invalidUsageField = null;
        invalidUsageValue = default;

        if (usage is null)
        {
            tokenAccounting = TokenAccounting.Unknown();
            return true;
        }

        if (!TryMapTokenValue(usage.PromptTokens, out var promptTokens, out invalidUsageValue))
        {
            invalidUsageField = "prompt_tokens";
            tokenAccounting = TokenAccounting.Unknown();
            return false;
        }

        if (!TryMapTokenValue(usage.OutputTokens, out var outputTokens, out invalidUsageValue))
        {
            invalidUsageField = "output_tokens";
            tokenAccounting = TokenAccounting.Unknown();
            return false;
        }

        if (!TryMapTokenValue(usage.TotalTokens, out var totalTokens, out invalidUsageValue))
        {
            invalidUsageField = "total_tokens";
            tokenAccounting = TokenAccounting.Unknown();
            return false;
        }

        if (!TryMapTokenValue(usage.ThoughtTokens, out var thoughtTokens, out invalidUsageValue))
        {
            invalidUsageField = "thought_tokens";
            tokenAccounting = TokenAccounting.Unknown();
            return false;
        }

        tokenAccounting = new TokenAccounting(promptTokens, outputTokens, totalTokens, thoughtTokens);
        return true;
    }

    private static bool TryMapTokenValue(int? value, out TokenUsageValue tokenUsageValue, out int invalidUsageValue)
    {
        invalidUsageValue = default;
        if (value is null)
        {
            tokenUsageValue = TokenUsageValue.Unknown();
            return true;
        }

        if (value.Value < 0)
        {
            tokenUsageValue = default;
            invalidUsageValue = value.Value;
            return false;
        }

        tokenUsageValue = TokenUsageValue.Known(value.Value);
        return true;
    }
}
