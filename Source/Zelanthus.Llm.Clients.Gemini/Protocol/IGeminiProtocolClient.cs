namespace Zelanthus.Llm.Clients.Gemini.Protocol;

public interface IGeminiProtocolClient
{
    Task<GeminiProtocolResult> ExecuteAsync(
        ExecutionEnvelope executionEnvelope,
        CancellationToken cancellationToken = default);
}
