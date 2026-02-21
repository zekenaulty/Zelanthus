namespace Zelanthus.Llm.Clients.Gemini.Protocol;

public sealed class GeminiProtocolResult
{
    private GeminiProtocolResult(GeminiProtocolResponse? response, GeminiProtocolError? error)
    {
        if ((response is null) == (error is null))
        {
            throw new ArgumentException("Protocol result must contain either response or error.");
        }

        Response = response;
        Error = error;
    }

    public GeminiProtocolResponse? Response { get; }

    public GeminiProtocolError? Error { get; }

    public static GeminiProtocolResult Success(GeminiProtocolResponse response)
    {
        ArgumentNullException.ThrowIfNull(response);
        return new GeminiProtocolResult(response, null);
    }

    public static GeminiProtocolResult Failure(GeminiProtocolError error)
    {
        ArgumentNullException.ThrowIfNull(error);
        return new GeminiProtocolResult(null, error);
    }
}

public sealed record GeminiProtocolResponse(
    string ContentText,
    GeminiUsage? Usage = null,
    string? ModelId = null,
    string? ThoughtSignature = null,
    string? RawSnapshotRef = null,
    IReadOnlyDictionary<string, string>? Metadata = null);

public sealed record GeminiUsage(
    int? PromptTokens,
    int? OutputTokens,
    int? TotalTokens,
    int? ThoughtTokens);

public sealed record GeminiProtocolError(
    string Message,
    string? Code = null,
    string? RawSnapshotRef = null,
    IReadOnlyDictionary<string, string>? Diagnostics = null);
