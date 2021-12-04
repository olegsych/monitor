using System;
using System.Threading;
using System.Threading.Tasks;

namespace Monitor
{
    public static class IMonitorExtensions
    {
        #region Instrument

        public static IInstrument Instrument(this IMonitor factory, Action action) =>
            factory.Instrument(action.Method);

        public static IInstrument Instrument(this IMonitor factory, Func<Task> command) =>
            throw new NotImplementedException();

        public static IInstrument Instrument(this IMonitor factory, Func<ValueTask> command) =>
            throw new NotImplementedException();

        public static IInstrument Instrument(this IMonitor factory, Func<CancellationToken, Task> command) =>
            throw new NotImplementedException();

        public static IInstrument Instrument(this IMonitor factory, Func<CancellationToken, ValueTask> command) =>
            throw new NotImplementedException();

        #endregion

        #region Instrument<TInput>

        public static IInstrument<T> Instrument<T>(this IMonitor factory, Action<T> command) =>
            throw new NotImplementedException();

        public static IInstrument<T> Instrument<T>(this IMonitor factory, Func<T, Task> command) =>
            throw new NotImplementedException();

        public static IInstrument<T> Instrument<T>(this IMonitor factory, Func<T, CancellationToken, Task> command) =>
            throw new NotImplementedException();

        public static IInstrument<T> Instrument<T>(this IMonitor factory, Func<T, ValueTask> command) =>
            throw new NotImplementedException();

        public static IInstrument<T> Instrument<T>(this IMonitor factory, Func<T, CancellationToken, ValueTask> command) =>
            throw new NotImplementedException();

        #endregion

        #region Query<TOutput>

        public static IInstrument<T> Instrument<T>(this IMonitor factory, Func<T> command) =>
            throw new NotImplementedException();

        public static IInstrument<T> Instrument<T>(this IMonitor factory, Func<Task<T>> command) =>
            throw new NotImplementedException();

        public static IInstrument<T> Instrument<T>(this IMonitor factory, Func<CancellationToken, Task<T>> command) =>
            throw new NotImplementedException();

        public static IInstrument<T> Instrument<T>(this IMonitor factory, Func<ValueTask<T>> command) =>
            throw new NotImplementedException();

        public static IInstrument<T> Instrument<T>(this IMonitor factory, Func<CancellationToken, ValueTask<T>> command) =>
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
