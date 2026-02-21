namespace Zelanthus.StoryEngine.Application;

public static class RunnerReasonCodes
{
    public const string MissingPromptReference = "missing_prompt_reference";
    public const string MissingRequiredPlaceholder = "missing_required_placeholder";
    public const string SchemaValidationFailed = "schema_validation_failed";
    public const string ProviderProtocolError = "provider_protocol_error";
    public const string OutputStarvation = "output_starvation";
    public const string ContinuityHandleInvalid = "continuity_handle_invalid";
    public const string CognitiveRestartRequired = "cognitive_restart_required";
    public const string RetryBudgetExhausted = "retry_budget_exhausted";
    public const string CheckpointWriteFailed = "checkpoint_write_failed";
    public const string CheckpointReadFailed = "checkpoint_read_failed";
    public const string ArtifactWriteFailed = "artifact_write_failed";
    public const string ArtifactReadFailed = "artifact_read_failed";
    public const string InvalidStateTransition = "invalid_state_transition";
}
