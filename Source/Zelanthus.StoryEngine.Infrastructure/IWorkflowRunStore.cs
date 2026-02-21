namespace Zelanthus.StoryEngine.Infrastructure;

public interface IWorkflowRunStore
{
    Task SaveRunRecordAsync(WorkflowRunRecord runRecord, CancellationToken cancellationToken = default);

    Task<WorkflowRunRecord?> ReadRunRecordAsync(Guid runId, CancellationToken cancellationToken = default);

    Task SaveTurnRecordAsync(Guid runId, WorkflowTurnRecord turnRecord, CancellationToken cancellationToken = default);

    Task SaveCheckpointAsync(Guid runId, int checkpointSequence, object checkpointPayload, CancellationToken cancellationToken = default);

    Task SaveFailureRecordAsync(Guid runId, WorkflowFailureRecord failureRecord, CancellationToken cancellationToken = default);
}
