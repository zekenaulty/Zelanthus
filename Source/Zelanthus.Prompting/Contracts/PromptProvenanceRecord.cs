namespace Zelanthus.Prompting.Contracts;

public sealed record PromptProvenanceRecord(
    string PromptId,
    int PromptVersion,
    string PromptChecksum);
