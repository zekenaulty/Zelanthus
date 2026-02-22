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

    [Fact]
    public void ChainModeEnum_IsOwnedByLlmClientAbstractionsOnly()
    {
        var abstractionsAssembly = Assembly.Load("Zelanthus.Llm.Clients.Abstractions");
        var domainAssembly = Assembly.Load("Zelanthus.StoryEngine.Domain");

        Assert.Contains(
            abstractionsAssembly.GetTypes(),
            type => type.IsEnum && string.Equals(type.Name, "ChainMode", StringComparison.Ordinal));
        Assert.DoesNotContain(
            domainAssembly.GetTypes(),
            type => type.IsEnum && string.Equals(type.Name, "ChainMode", StringComparison.Ordinal));
    }

    [Fact]
    public void SourceProjects_DoNotKeepFeatureClassesInProjectRoot()
    {
        var repositoryRoot = GetRepositoryRoot();
        var sourceRoot = Path.Combine(repositoryRoot, "Source");
        var allowedRootFileNames = new HashSet<string>(StringComparer.Ordinal)
        {
            "Program.cs",
            "GlobalUsings.cs",
        };

        var projectDirectories = Directory
            .GetFiles(sourceRoot, "*.csproj", SearchOption.AllDirectories)
            .Select(Path.GetDirectoryName)
            .OfType<string>()
            .Distinct(StringComparer.Ordinal)
            .OrderBy(path => path, StringComparer.Ordinal)
            .ToArray();

        foreach (var projectDirectory in projectDirectories)
        {
            var rootCsFiles = Directory
                .GetFiles(projectDirectory, "*.cs", SearchOption.TopDirectoryOnly)
                .Where(path => !allowedRootFileNames.Contains(Path.GetFileName(path)))
                .Select(Path.GetFileName)
                .OrderBy(name => name, StringComparer.Ordinal)
                .ToArray();

            Assert.True(
                rootCsFiles.Length == 0,
                $"Project '{Path.GetFileName(projectDirectory)}' has root-level .cs files that must be folderized: {string.Join(", ", rootCsFiles)}");
        }
    }

    [Fact]
    public void StoryEngineApplication_ContractsUseSecondTierNamespaces()
    {
        var applicationAssembly = Assembly.Load("Zelanthus.StoryEngine.Application");
        var types = applicationAssembly.GetTypes();

        Assert.Contains(types, type => type.FullName == "Zelanthus.StoryEngine.Application.Contracts.RunExecution.IWorkflowRunner");
        Assert.Contains(types, type => type.FullName == "Zelanthus.StoryEngine.Application.Contracts.RunExecution.WorkflowExecutionRequest");
        Assert.Contains(types, type => type.FullName == "Zelanthus.StoryEngine.Application.Contracts.RunExecution.WorkflowExecutionResult");
        Assert.Contains(types, type => type.FullName == "Zelanthus.StoryEngine.Application.Contracts.StepExecution.IWorkflowStepExecutor");
        Assert.Contains(types, type => type.FullName == "Zelanthus.StoryEngine.Application.Contracts.StepExecution.WorkflowStepExecutionContext");
        Assert.Contains(types, type => type.FullName == "Zelanthus.StoryEngine.Application.Contracts.StepExecution.WorkflowStepExecutionResult");

        Assert.DoesNotContain(
            types,
            type => string.Equals(type.Namespace, "Zelanthus.StoryEngine.Application.Contracts", StringComparison.Ordinal));
    }

    [Fact]
    public void StoryEngineInfrastructure_StorageIsIsolatedByTierAndBackend()
    {
        var infrastructureAssembly = Assembly.Load("Zelanthus.StoryEngine.Infrastructure");
        var types = infrastructureAssembly.GetTypes();

        Assert.Contains(types, type => type.FullName == "Zelanthus.StoryEngine.Infrastructure.Storage.Abstractions.IWorkflowRunStore");
        Assert.Contains(types, type => type.FullName == "Zelanthus.StoryEngine.Infrastructure.Storage.Records.WorkflowRunRecord");
        Assert.Contains(types, type => type.FullName == "Zelanthus.StoryEngine.Infrastructure.Storage.Local.LocalFileWorkflowRunStore");
        Assert.Contains(types, type => type.FullName == "Zelanthus.StoryEngine.Infrastructure.Storage.Local.WorkflowRunPaths");

        Assert.DoesNotContain(
            types,
            type => string.Equals(type.Namespace, "Zelanthus.StoryEngine.Infrastructure.Storage", StringComparison.Ordinal));
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
