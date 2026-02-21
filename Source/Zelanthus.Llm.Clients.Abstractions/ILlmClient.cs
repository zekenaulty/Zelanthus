namespace Zelanthus.Llm.Clients.Abstractions;

public interface ILlmClient
{
    string ProviderKey { get; }

    LlmCapabilityProfile CapabilityProfile { get; }

    Task<LlmExecutionResult> ExecuteAsync(
        ExecutionEnvelope executionEnvelope,
        CancellationToken cancellationToken = default);
}
