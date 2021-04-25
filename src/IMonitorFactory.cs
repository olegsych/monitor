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
        IMonitor Create(Command command);
        IMonitor<TInput> Create<TInput>(Command<TInput> actor);

        IMonitor<TInput, TOutput> Create<TInput, TOutput>(Func<TInput, TOutput> actor);
        IMonitor<TInput, TOutput> Create<TInput, TOutput>(Func<TInput, Task<TOutput>> actor);
        IMonitor<TInput, TOutput> Create<TInput, TOutput>(Func<TInput, ValueTask<TOutput>> actor);
    }
}
