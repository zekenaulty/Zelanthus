namespace Zelanthus.Prompting;

public sealed class PromptRenderResult
{
    private PromptRenderResult(RenderedPrompt? renderedPrompt, RenderFailure? renderFailure)
    {
        if ((renderedPrompt is null) == (renderFailure is null))
        {
            throw new ArgumentException("Render result must contain exactly one outcome.");
        }

        RenderedPrompt = renderedPrompt;
        RenderFailure = renderFailure;
    }

    public bool IsSuccess => RenderedPrompt is not null;

    public RenderedPrompt? RenderedPrompt { get; }

    public RenderFailure? RenderFailure { get; }

    public static PromptRenderResult Success(RenderedPrompt renderedPrompt)
    {
        ArgumentNullException.ThrowIfNull(renderedPrompt);
        return new PromptRenderResult(renderedPrompt, null);
    }

    public static PromptRenderResult Failure(RenderFailure renderFailure)
    {
        ArgumentNullException.ThrowIfNull(renderFailure);
        return new PromptRenderResult(null, renderFailure);
    }
}
