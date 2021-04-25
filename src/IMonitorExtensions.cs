using System;
using System.Threading;
using System.Threading.Tasks;

namespace Monitor
{
    public static class IMonitorExtensions
    {
        #region Command

        public static ICommandMonitor Command(this IMonitor factory, Action command) =>
            factory.Command(command.Method);

        public static ICommandMonitor Command(this IMonitor factory, Func<Task> command) =>
            throw new NotImplementedException();

        public static ICommandMonitor Command(this IMonitor factory, Func<ValueTask> command) =>
            throw new NotImplementedException();

        public static ICommandMonitor Command(this IMonitor factory, Func<CancellationToken, Task> command) =>
            throw new NotImplementedException();

        public static ICommandMonitor Command(this IMonitor factory, Func<CancellationToken, ValueTask> command) =>
            throw new NotImplementedException();

        #endregion

        #region Command<TInput>

        public static ICommandMonitor<TInput> Command<TInput>(this IMonitor factory, Action<TInput> command) =>
            throw new NotImplementedException();

        public static ICommandMonitor<TInput> Command<TInput>(this IMonitor factory, Func<TInput, Task> command) =>
            throw new NotImplementedException();

        public static ICommandMonitor<TInput> Command<TInput>(this IMonitor factory, Func<TInput, CancellationToken, Task> command) =>
            throw new NotImplementedException();

        public static ICommandMonitor<TInput> Command<TInput>(this IMonitor factory, Func<TInput, ValueTask> command) =>
            throw new NotImplementedException();

        public static ICommandMonitor<TInput> Command<TInput>(this IMonitor factory, Func<TInput, CancellationToken, ValueTask> command) =>
            throw new NotImplementedException();

        #endregion

        #region Query<TOutput>

        public static IQueryMonitor<TOutput> Query<TOutput>(this IMonitor factory, Func<TOutput> command) =>
            throw new NotImplementedException();

        public static IQueryMonitor<TOutput> Query<TOutput>(this IMonitor factory, Func<Task<TOutput>> command) =>
            throw new NotImplementedException();

        public static IQueryMonitor<TOutput> Query<TOutput>(this IMonitor factory, Func<CancellationToken, Task<TOutput>> command) =>
            throw new NotImplementedException();

        public static IQueryMonitor<TOutput> Query<TOutput>(this IMonitor factory, Func<ValueTask<TOutput>> command) =>
            throw new NotImplementedException();

        public static IQueryMonitor<TOutput> Query<TOutput>(this IMonitor factory, Func<CancellationToken, ValueTask<TOutput>> command) =>
            throw new NotImplementedException();

        #endregion

        #region Query<TInput, TOutput>

        public static IQueryMonitor<TInput, TOutput> Query<TInput, TOutput>(this IMonitor factory, Func<TInput, TOutput> query) =>
            throw new NotImplementedException();

        public static IQueryMonitor<TInput, TOutput> Query<TInput, TOutput>(this IMonitor factory, Func<TInput, Task<TOutput>> query) =>
            throw new NotImplementedException();

        public static IQueryMonitor<TInput, TOutput> Query<TInput, TOutput>(this IMonitor factory, Func<TInput, CancellationToken, Task<TOutput>> query) =>
            throw new NotImplementedException();

        public static IQueryMonitor<TInput, TOutput> Query<TInput, TOutput>(this IMonitor factory, Func<TInput, ValueTask<TOutput>> query) =>
            throw new NotImplementedException();

        public static IQueryMonitor<TInput, TOutput> Query<TInput, TOutput>(this IMonitor factory, Func<TInput, CancellationToken, ValueTask<TOutput>> query) =>
            throw new NotImplementedException();

        #endregion
    }
}
