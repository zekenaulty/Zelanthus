using System.Text.Json;

namespace Zelanthus.WorkflowContractProofs.Tests;

internal static class ContractProofArtifactWriter
{
    private static readonly JsonSerializerOptions SerializerOptions = new()
    {
        WriteIndented = true,
    };

    private static readonly object ArtifactWriteLock = new();

    public static string Write(string fileName, object payload)
    {
        var outputDirectory = Path.Combine(
            GetRepositoryRoot(),
            "artifacts",
            "workflow-contract-proofs");

        Directory.CreateDirectory(outputDirectory);

        var outputPath = Path.Combine(outputDirectory, fileName);
        var serializedPayload = JsonSerializer.Serialize(payload, SerializerOptions);

        lock (ArtifactWriteLock)
        {
            File.WriteAllText(outputPath, serializedPayload);
        }

        return outputPath;
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
