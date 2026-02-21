namespace Zelanthus.Llm.Clients.Abstractions;

public sealed class LlmExecutionResult
{
    private LlmExecutionResult(NormalizedResponseEnvelope? response, LlmFailure? failure)
    {
        if ((response is null) == (failure is null))
        {
            throw new ArgumentException("Execution result must contain exactly one outcome.");
        }

        Response = response;
        Failure = failure;
    }

    public bool IsSuccess => Response is not null;

    public NormalizedResponseEnvelope? Response { get; }

    public LlmFailure? Failure { get; }

    public static LlmExecutionResult Success(NormalizedResponseEnvelope response)
    {
        ArgumentNullException.ThrowIfNull(response);
        return new LlmExecutionResult(response, null);
    }

    public static LlmExecutionResult Failed(LlmFailure failure)
    {
        ArgumentNullException.ThrowIfNull(failure);
        return new LlmExecutionResult(null, failure);
    }
}
