namespace Zelanthus.Llm.Clients.Abstractions.Contracts;

public interface ILlmClient
{
    string ProviderKey { get; }

    LlmCapabilityProfile CapabilityProfile { get; }

    Task<LlmExecutionResult> ExecuteAsync(
        ExecutionEnvelope executionEnvelope,
        CancellationToken cancellationToken = default);
}
