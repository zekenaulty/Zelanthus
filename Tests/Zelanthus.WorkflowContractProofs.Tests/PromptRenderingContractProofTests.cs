namespace Zelanthus.WorkflowContractProofs.Tests;

public sealed class PromptRenderingContractProofTests
{
    private readonly IPromptRenderer _renderer = new PromptRenderer();

    [Fact]
    public void PromptRendering_TwoRequiredPlaceholders_OneMissing_ReturnsDeterministicRenderFailure()
    {
        var template = new PromptTemplateDefinition(
            promptId: "story.chapter.plan",
            promptVersion: 1,
            templateText: "Objective: {{objective}}\nTone: {{tone}}",
            requiredPlaceholders: ["objective", "tone"]);

        var result = _renderer.Render(
            template,
            new Dictionary<string, string?>(StringComparer.Ordinal)
            {
                ["objective"] = "Plan chapter outline",
            });

        Assert.False(result.IsSuccess);
        var failure = Assert.IsType<RenderFailure>(result.RenderFailure);
        Assert.Equal(PromptReasonCodes.MissingRequiredPlaceholder, failure.ReasonCode);
        Assert.Equal(["tone"], failure.MissingPlaceholders);

        ContractProofArtifactWriter.Write(
            "prompt-render-failure-missing-placeholder.json",
            new
            {
                failure.PromptId,
                failure.PromptVersion,
                failure.ReasonCode,
                MissingPlaceholders = failure.MissingPlaceholders,
            });
    }

    [Fact]
    public void PromptRendering_TwoRequiredPlaceholders_OneMissing_ReportsMissingPlaceholdersInLexicalOrder()
    {
        var template = new PromptTemplateDefinition(
            promptId: "story.chapter.plan",
            promptVersion: 1,
            templateText: "Tone: {{tone}}",
            requiredPlaceholders: ["tone", "tone"]);

        var result = _renderer.Render(
            template,
            new Dictionary<string, string?>(StringComparer.Ordinal)
            {
                ["objective"] = "ignored-extra-key",
            });

        Assert.False(result.IsSuccess);
        var failure = Assert.IsType<RenderFailure>(result.RenderFailure);
        Assert.Equal(["tone"], failure.MissingPlaceholders);
    }

    [Fact]
    public void PromptRendering_AllRequiredPlaceholdersProvided_ReturnsRenderedPromptWithStableChecksum()
    {
        var template = new PromptTemplateDefinition(
            promptId: "story.chapter.plan",
            promptVersion: 1,
            templateText: "Objective: {{objective}}\r\nTone: {{tone}}",
            requiredPlaceholders: ["objective", "tone"]);

        var placeholderValues = new Dictionary<string, string?>(StringComparer.Ordinal)
        {
            ["objective"] = "Plan chapter outline",
            ["tone"] = "analytical",
            ["ignored_extra"] = "this-should-not-fail",
        };

        var firstResult = _renderer.Render(template, placeholderValues);
        var secondResult = _renderer.Render(template, placeholderValues);

        Assert.True(firstResult.IsSuccess);
        Assert.True(secondResult.IsSuccess);

        var firstPrompt = Assert.IsType<RenderedPrompt>(firstResult.RenderedPrompt);
        var secondPrompt = Assert.IsType<RenderedPrompt>(secondResult.RenderedPrompt);

        Assert.Equal(firstPrompt.Checksum, secondPrompt.Checksum);
        Assert.Equal("Objective: Plan chapter outline\nTone: analytical", firstPrompt.RenderedText);
        Assert.Equal(64, firstPrompt.Checksum.Length);

        ContractProofArtifactWriter.Write(
            "prompt-render-success.json",
            new
            {
                firstPrompt.PromptId,
                firstPrompt.PromptVersion,
                firstPrompt.RequiredPlaceholders,
                firstPrompt.RenderedText,
                firstPrompt.Checksum,
                ChecksumAlgorithm = "SHA-256",
            });

        ContractProofArtifactWriter.Write(
            "prompt-render-checksum-stability.json",
            new
            {
                FirstChecksum = firstPrompt.Checksum,
                SecondChecksum = secondPrompt.Checksum,
                ChecksumsMatch = string.Equals(firstPrompt.Checksum, secondPrompt.Checksum, StringComparison.Ordinal),
                ChecksumAlgorithm = "SHA-256",
            });
    }

    [Fact]
    public void PromptRendering_MultipleMissingPlaceholders_AreReportedInLexicalOrder()
    {
        var template = new PromptTemplateDefinition(
            promptId: "story.chapter.plan",
            promptVersion: 1,
            templateText: "{{zeta}} {{alpha}} {{alpha}}",
            requiredPlaceholders: ["zeta", "alpha", "alpha"]);

        var result = _renderer.Render(
            template,
            new Dictionary<string, string?>(StringComparer.Ordinal));

        Assert.False(result.IsSuccess);
        var failure = Assert.IsType<RenderFailure>(result.RenderFailure);
        Assert.Equal(["alpha", "zeta"], failure.MissingPlaceholders);
    }
}
