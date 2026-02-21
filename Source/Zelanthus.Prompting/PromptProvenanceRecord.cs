namespace Zelanthus.Prompting;

public sealed record PromptProvenanceRecord(
    string PromptId,
    int PromptVersion,
    string PromptChecksum);
