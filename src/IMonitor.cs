using System;

namespace Monitor
{
    /// <summary>
    /// Creates monitors for actors of given <see cref="Type"/>.
    /// </summary>
    /// <remarks>
    /// <see cref="Type"> may be too restrictive. 
    /// </remarks>
    public interface IMonitor
    {
        ICommandMonitor Command(Command command);
        ICommandMonitor<TInput> Command<TInput>(Command<TInput> command);
        IQueryMonitor<TOutput> Query<TOutput>(Query<TOutput> query);
        IQueryMonitor<TInput, TOutput> Query<TInput, TOutput>(Query<TInput, TOutput> query);
    }
}
