using System.Text.Json;

namespace Zelanthus.StoryEngine.Infrastructure.Storage.Local;

public sealed class LocalFileWorkflowRunStore : IWorkflowRunStore
{
    private static readonly JsonSerializerOptions SerializerOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        WriteIndented = true,
    };

    private readonly WorkflowRunPaths _workflowRunPaths;

    public LocalFileWorkflowRunStore(WorkflowRunPaths workflowRunPaths)
    {
        _workflowRunPaths = workflowRunPaths ?? throw new ArgumentNullException(nameof(workflowRunPaths));
    }

    public Task SaveRunRecordAsync(WorkflowRunRecord runRecord, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(runRecord);
        var path = _workflowRunPaths.GetRunRecordPath(runRecord.RunId);
        return WriteJsonAtomicAsync(path, runRecord, cancellationToken, LocalPersistenceReasonCodes.ArtifactWriteFailed);
    }

    public async Task<WorkflowRunRecord?> ReadRunRecordAsync(Guid runId, CancellationToken cancellationToken = default)
    {
        var path = _workflowRunPaths.GetRunRecordPath(runId);
        if (!File.Exists(path))
        {
            return null;
        }

        try
        {
            await using var stream = new FileStream(
                path,
                FileMode.Open,
                FileAccess.Read,
                FileShare.ReadWrite | FileShare.Delete);
            var record = await JsonSerializer.DeserializeAsync<WorkflowRunRecord>(stream, SerializerOptions, cancellationToken).ConfigureAwait(false);
            if (record is null)
            {
                throw new JsonException("Run record payload deserialized to null.");
            }

            return record;
        }
        catch (OperationCanceledException)
        {
            throw;
        }
        catch (Exception exception)
        {
            throw new InvalidOperationException(
                $"{LocalPersistenceReasonCodes.ArtifactReadFailed}: failed to read run record '{path}'.",
                exception);
        }
    }

    public Task SaveTurnRecordAsync(Guid runId, WorkflowTurnRecord turnRecord, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(turnRecord);
        var turnDirectory = _workflowRunPaths.GetTurnDirectory(runId, turnRecord.TurnIndex, turnRecord.StepKey);
        var path = Path.Combine(turnDirectory, "turn.json");
        return WriteJsonAtomicAsync(path, turnRecord, cancellationToken, LocalPersistenceReasonCodes.ArtifactWriteFailed);
    }

    public Task SaveCheckpointAsync(Guid runId, int checkpointSequence, object checkpointPayload, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(checkpointPayload);
        var path = _workflowRunPaths.GetCheckpointPath(runId, checkpointSequence);
        return WriteJsonAtomicAsync(path, checkpointPayload, cancellationToken, LocalPersistenceReasonCodes.CheckpointWriteFailed);
    }

    public Task SaveFailureRecordAsync(Guid runId, WorkflowFailureRecord failureRecord, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(failureRecord);
        var path = _workflowRunPaths.GetFailurePath(runId, failureRecord.TurnIndex);
        return WriteJsonAtomicAsync(path, failureRecord, cancellationToken, LocalPersistenceReasonCodes.ArtifactWriteFailed);
    }

    private static async Task WriteJsonAtomicAsync<T>(
        string targetPath,
        T payload,
        CancellationToken cancellationToken,
        string writeFailureReasonCode)
    {
        try
        {
            var targetDirectory = Path.GetDirectoryName(targetPath)
                ?? throw new InvalidOperationException("Target path must include a directory.");

            Directory.CreateDirectory(targetDirectory);

            var temporaryPath = Path.Combine(
                targetDirectory,
                $".{Path.GetFileName(targetPath)}.{Guid.NewGuid():N}.tmp");

            await using (var stream = new FileStream(
                temporaryPath,
                FileMode.CreateNew,
                FileAccess.Write,
                FileShare.None,
                bufferSize: 16 * 1024,
                FileOptions.WriteThrough))
            {
                await JsonSerializer.SerializeAsync(stream, payload, SerializerOptions, cancellationToken).ConfigureAwait(false);
                await stream.FlushAsync(cancellationToken).ConfigureAwait(false);
            }

            ReplaceOrMoveAtomically(temporaryPath, targetPath);
        }
        catch (OperationCanceledException)
        {
            throw;
        }
        catch (Exception exception)
        {
            throw new InvalidOperationException(
                $"{writeFailureReasonCode}: failed to write '{targetPath}'.",
                exception);
        }
    }

    private static void ReplaceOrMoveAtomically(string temporaryPath, string targetPath)
    {
        try
        {
            File.Replace(temporaryPath, targetPath, destinationBackupFileName: null, ignoreMetadataErrors: true);
            return;
        }
        catch (FileNotFoundException)
        {
            MoveWithCreateRaceHandling(temporaryPath, targetPath);
            return;
        }
        catch (IOException)
        {
            OverwriteExistingTargetFromTemporaryFile(temporaryPath, targetPath);
            return;
        }
    }

    private static void MoveWithCreateRaceHandling(string temporaryPath, string targetPath)
    {
        try
        {
            File.Move(temporaryPath, targetPath);
        }
        catch (IOException)
        {
            // Another writer may have created the destination between replace and move.
            File.Replace(temporaryPath, targetPath, destinationBackupFileName: null, ignoreMetadataErrors: true);
        }
    }

    private static void OverwriteExistingTargetFromTemporaryFile(string temporaryPath, string targetPath)
    {
        try
        {
            using (var sourceStream = new FileStream(
                temporaryPath,
                FileMode.Open,
                FileAccess.Read,
                FileShare.Read))
            using (var destinationStream = new FileStream(
                targetPath,
                FileMode.Open,
                FileAccess.Write,
                FileShare.ReadWrite | FileShare.Delete,
                bufferSize: 16 * 1024,
                FileOptions.WriteThrough))
            {
                destinationStream.SetLength(0);
                sourceStream.CopyTo(destinationStream);
                destinationStream.Flush(flushToDisk: true);
            }

            File.Delete(temporaryPath);
        }
        catch (FileNotFoundException)
        {
            MoveWithCreateRaceHandling(temporaryPath, targetPath);
        }
    }
}
