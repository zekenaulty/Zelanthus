using System.Reflection;
using System.Xml.Linq;

namespace Zelanthus.Architecture.Tests;

public sealed class DependencyDirectionTests
{
    private static readonly string[] StoryEngineOrApiReferences =
    [
        "Zelanthus.API",
        "Zelanthus.StoryEngine.Domain",
        "Zelanthus.StoryEngine.Application",
        "Zelanthus.StoryEngine.Infrastructure",
    ];

    private static readonly string[] ProviderReferences =
    [
        "Zelanthus.Llm.Clients.Gemini",
    ];

    [Fact]
    public void Prompting_DoesNotReferenceApiStoryEngineOrProviderImplementationProjects()
    {
        var references = GetZelanthusReferences("Zelanthus.Prompting");

        AssertNoReferences(
            references,
            StoryEngineOrApiReferences.Concat(["Zelanthus.Llm.Clients.Gemini"]));
    }

    [Fact]
    public void LlmClientAbstractions_DoesNotReferencePromptingProviderImplementationApiOrStoryEngine()
    {
        var references = GetZelanthusReferences("Zelanthus.Llm.Clients.Abstractions");

        AssertNoReferences(
            references,
            StoryEngineOrApiReferences.Concat(
                [
                    "Zelanthus.Prompting",
                    "Zelanthus.Llm.Clients.Gemini",
                ]));
    }

    [Fact]
    public void GeminiAdapter_DoesNotReferencePromptingApiOrStoryEngineProjects()
    {
        var references = GetZelanthusReferences("Zelanthus.Llm.Clients.Gemini");

        AssertNoReferences(
            references,
            StoryEngineOrApiReferences.Concat(["Zelanthus.Prompting"]));
    }

    [Fact]
    public void StoryEngineDomain_DoesNotReferenceInfrastructureProviderApiOrPromptingProjects()
    {
        var references = GetZelanthusReferences("Zelanthus.StoryEngine.Domain");

        AssertNoReferences(
            references,
            StoryEngineOrApiReferences
                .Where(reference => !string.Equals(reference, "Zelanthus.StoryEngine.Domain", StringComparison.Ordinal))
                .Concat(
                    [
                        "Zelanthus.Prompting",
                        "Zelanthus.Llm.Clients.Abstractions",
                        "Zelanthus.Llm.Clients.Gemini",
                    ]));
    }

    [Fact]
    public void StoryEngineApplication_DoesNotReferenceInfrastructureProviderOrApiProjects()
    {
        var references = GetZelanthusReferences("Zelanthus.StoryEngine.Application");

        AssertNoReferences(
            references,
            ProviderReferences.Concat(
                [
                    "Zelanthus.API",
                    "Zelanthus.StoryEngine.Infrastructure",
                ]));
    }

    [Fact]
    public void StoryEngineInfrastructure_DoesNotReferenceApiOrProviderImplementationProjects()
    {
        var references = GetZelanthusReferences("Zelanthus.StoryEngine.Infrastructure");

        AssertNoReferences(
            references,
            ProviderReferences.Concat(["Zelanthus.API"]));
    }

    [Fact]
    public void GeminiProject_ReferencesLlmClientAbstractionsProject()
    {
        var repositoryRoot = GetRepositoryRoot();
        var geminiProjectPath = Path.Combine(
            repositoryRoot,
            "Source",
            "Zelanthus.Llm.Clients.Gemini",
            "Zelanthus.Llm.Clients.Gemini.csproj");
        var project = XDocument.Load(geminiProjectPath);

        var hasAbstractionsReference = project
            .Descendants("ProjectReference")
            .Select(element => element.Attribute("Include")?.Value)
            .OfType<string>()
            .Any(path => path.EndsWith("Zelanthus.Llm.Clients.Abstractions.csproj", StringComparison.OrdinalIgnoreCase));

        Assert.True(hasAbstractionsReference);
    }

    private static HashSet<string> GetZelanthusReferences(string assemblyName)
    {
        var assembly = Assembly.Load(assemblyName);

        return assembly
            .GetReferencedAssemblies()
            .Select(reference => reference.Name)
            .OfType<string>()
            .Where(name => name.StartsWith("Zelanthus.", StringComparison.Ordinal))
            .ToHashSet(StringComparer.Ordinal);
    }

    private static void AssertNoReferences(IEnumerable<string> references, IEnumerable<string> forbiddenReferences)
    {
        foreach (var forbiddenReference in forbiddenReferences)
        {
            Assert.DoesNotContain(forbiddenReference, references);
        }
    }

    private static string GetRepositoryRoot()
    {
        var directory = new DirectoryInfo(AppContext.BaseDirectory);

        while (directory is not null)
        {
            var solutionPath = Path.Combine(directory.FullName, "Zelanthus.slnx");
            if (File.Exists(solutionPath))
            {
                return directory.FullName;
            }

            directory = directory.Parent;
        }

        throw new InvalidOperationException("Could not locate repository root containing Zelanthus.slnx.");
    }
}
