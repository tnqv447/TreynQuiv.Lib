using System.Collections.Concurrent;

namespace TreynQuiv.Lib.Common.Components;

public abstract class TaskQueue
{
    private readonly ConcurrentQueue<Func<CancellationToken, Task>> _taskQueue = new();
    private readonly SemaphoreSlim _signal = new(0);
    public async Task<Func<CancellationToken, Task>> DequeueAsync(CancellationToken cancellationToken)
    {
        await _signal.WaitAsync(cancellationToken);
        _taskQueue.TryDequeue(out var workItem);

        return workItem ?? default!;
    }

    public void EnqueueAsync(Func<CancellationToken, Task> workItem)
    {
        ArgumentNullException.ThrowIfNull(workItem);
        _taskQueue.Enqueue(workItem);
        _signal.Release();
    }
}

public abstract class TaskQueue<T>
{
    private readonly ConcurrentQueue<Func<CancellationToken, Task<T>>> _taskQueue = new();
    private readonly SemaphoreSlim _signal = new(0);
    public async Task<Func<CancellationToken, Task<T>>> DequeueAsync(CancellationToken cancellationToken)
    {
        await _signal.WaitAsync(cancellationToken);
        _taskQueue.TryDequeue(out var workItem);

        return workItem ?? default!;
    }

    public void EnqueueAsync(Func<CancellationToken, Task<T>> workItem)
    {
        ArgumentNullException.ThrowIfNull(workItem);
        _taskQueue.Enqueue(workItem);
        _signal.Release();
    }
}
