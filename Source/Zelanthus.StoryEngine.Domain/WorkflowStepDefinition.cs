namespace Zelanthus.StoryEngine.Domain;

public sealed record WorkflowStepDefinition
{
    public WorkflowStepDefinition(
        string stepKey,
        StepKind stepKind,
        PromptReference? promptReference,
        string inputContractReference,
        string outputContractReference,
        string? routeHookKey = null)
    {
        StepKey = WorkflowKeyValidator.ValidateStepKey(stepKey);
        StepKind = stepKind;
        PromptReference = promptReference;
        InputContractReference = string.IsNullOrWhiteSpace(inputContractReference)
            ? throw new ArgumentException("Input contract reference is required.", nameof(inputContractReference))
            : inputContractReference;
        OutputContractReference = string.IsNullOrWhiteSpace(outputContractReference)
            ? throw new ArgumentException("Output contract reference is required.", nameof(outputContractReference))
            : outputContractReference;
        RouteHookKey = WorkflowKeyValidator.ValidateRouteHookKey(routeHookKey);
    }

    public string StepKey { get; }

    public StepKind StepKind { get; }

    public PromptReference? PromptReference { get; }

    public string InputContractReference { get; }

    public string OutputContractReference { get; }

    public string? RouteHookKey { get; }
}
