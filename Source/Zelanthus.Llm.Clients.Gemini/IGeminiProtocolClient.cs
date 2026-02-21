using Zelanthus.Llm.Clients.Abstractions;

namespace Zelanthus.Llm.Clients.Gemini;

public interface IGeminiProtocolClient
{
    Task<GeminiProtocolResult> ExecuteAsync(
        ExecutionEnvelope executionEnvelope,
        CancellationToken cancellationToken = default);
}
