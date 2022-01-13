using System;
using System.Threading;
using System.Threading.Tasks;

namespace Monitor
{
    public static class IMonitorExtensions
    {
        #region Instrument

        public static IInstrument Instrument(this IMonitor monitor, Action method) =>
            throw new NotImplementedException();

        public static IInstrument Instrument(this IMonitor monitor, Func<Task> method) =>
            throw new NotImplementedException();

        public static IInstrument Instrument(this IMonitor monitor, Func<ValueTask> method) =>
            throw new NotImplementedException();

        public static IInstrument Instrument(this IMonitor monitor, Func<CancellationToken, Task> method) =>
            throw new NotImplementedException();

        public static IInstrument Instrument(this IMonitor monitor, Func<CancellationToken, ValueTask> method) =>
            throw new NotImplementedException();

        #endregion

        #region Instrument<TInput>

        public static IInstrument<T> Instrument<T>(this IMonitor monitor, Action<T> method) =>
            throw new NotImplementedException();

        public static IInstrument<T> Instrument<T>(this IMonitor monitor, Func<T, Task> method) =>
            throw new NotImplementedException();

        public static IInstrument<T> Instrument<T>(this IMonitor monitor, Func<T, CancellationToken, Task> method) =>
            throw new NotImplementedException();

        public static IInstrument<T> Instrument<T>(this IMonitor monitor, Func<T, ValueTask> method) =>
            throw new NotImplementedException();

        public static IInstrument<T> Instrument<T>(this IMonitor monitor, Func<T, CancellationToken, ValueTask> method) =>
            throw new NotImplementedException();

        #endregion

        #region Instrument<TOutput>

        public static IInstrument<T> Instrument<T>(this IMonitor monitor, Func<T> method) =>
            throw new NotImplementedException();

        public static IInstrument<T> Instrument<T>(this IMonitor monitor, Func<Task<T>> method) =>
            throw new NotImplementedException();

        public static IInstrument<T> Instrument<T>(this IMonitor monitor, Func<CancellationToken, Task<T>> method) =>
            throw new NotImplementedException();

        public static IInstrument<T> Instrument<T>(this IMonitor monitor, Func<ValueTask<T>> method) =>
            throw new NotImplementedException();

        public static IInstrument<T> Instrument<T>(this IMonitor monitor, Func<CancellationToken, ValueTask<T>> method) =>
            throw new NotImplementedException();

        #endregion

        #region Instrument<TInput, TOutput>

        public static IInstrument<TInput, TOutput> Instrument<TInput, TOutput>(this IMonitor monitor, Func<TInput, TOutput> method) =>
            throw new NotImplementedException();

        public static IInstrument<TInput, TOutput> Instrument<TInput, TOutput>(this IMonitor monitor, Func<TInput, Task<TOutput>> method) =>
            throw new NotImplementedException();

        public static IInstrument<TInput, TOutput> Instrument<TInput, TOutput>(this IMonitor monitor, Func<TInput, CancellationToken, Task<TOutput>> method) =>
            throw new NotImplementedException();

        public static IInstrument<TInput, TOutput> Instrument<TInput, TOutput>(this IMonitor monitor, Func<TInput, ValueTask<TOutput>> method) =>
            throw new NotImplementedException();

        public static IInstrument<TInput, TOutput> Instrument<TInput, TOutput>(this IMonitor monitor, Func<TInput, CancellationToken, ValueTask<TOutput>> method) =>
            throw new NotImplementedException();

        #endregion
    }
}
