using System;
using System.Threading;
using System.Threading.Tasks;

namespace Monitor
{
    /// <summary>
    /// Creates monitors for actors of given <see cref="Type"/>.
    /// </summary>
    /// <remarks>
    /// <see cref="Type"> may be too restrictive. 
    /// </remarks>
    public interface IMonitorFactory
    {
        IMonitor Create(Action actor);
        IMonitor Create(Func<Task> actor);
        IMonitor Create(Func<ValueTask> actor);
        IMonitor Create(Func<CancellationToken, Task> actor);
        IMonitor Create(Func<CancellationToken, ValueTask> actor);

        IMonitor<TInput> Create<TInput>(Action<TInput> actor);
        IMonitor<TInput> Create<TInput>(Func<TInput, Task> actor);
        IMonitor<TInput> Create<TInput>(Func<TInput, ValueTask> actor);

        IMonitor<TInput, TOutput> Create<TInput, TOutput>(Func<TInput, TOutput> actor);
        IMonitor<TInput, TOutput> Create<TInput, TOutput>(Func<TInput, Task<TOutput>> actor);
        IMonitor<TInput, TOutput> Create<TInput, TOutput>(Func<TInput, ValueTask<TOutput>> actor);
    }
}
