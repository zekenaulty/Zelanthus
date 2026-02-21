using Zelanthus.Llm.Clients.Abstractions;

namespace Zelanthus.Llm.Clients.Gemini;

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
        var tokenAccounting = MapTokenAccounting(response.Usage);
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

    private static TokenAccounting MapTokenAccounting(GeminiUsage? usage)
    {
        if (usage is null)
        {
            return TokenAccounting.Unknown();
        }

        return new TokenAccounting(
            MapTokenValue(usage.PromptTokens),
            MapTokenValue(usage.OutputTokens),
            MapTokenValue(usage.TotalTokens),
            MapTokenValue(usage.ThoughtTokens));
    }

    private static TokenUsageValue MapTokenValue(int? value)
    {
        return value is null
            ? TokenUsageValue.Unknown()
            : TokenUsageValue.Known(value.Value);
    }
}
