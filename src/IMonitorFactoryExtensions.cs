﻿using System;
using System.Threading;
using System.Threading.Tasks;

namespace Monitor
{
    public static class IMonitorFactoryExtensions
    {
        #region Command

        public static ICommandMonitor Create(this IMonitorFactory factory, Action command) =>
            throw new NotImplementedException();

        public static ICommandMonitor Create(this IMonitorFactory factory, Func<Task> command) =>
            throw new NotImplementedException();

        public static ICommandMonitor Create(this IMonitorFactory factory, Func<ValueTask> command) =>
            throw new NotImplementedException();

        public static ICommandMonitor Create(this IMonitorFactory factory, Func<CancellationToken, Task> command) =>
            throw new NotImplementedException();

        public static ICommandMonitor Create(this IMonitorFactory factory, Func<CancellationToken, ValueTask> command) =>
            throw new NotImplementedException();

        #endregion

        #region Command<TInput>

        public static ICommandMonitor<TInput> Create<TInput>(this IMonitorFactory factory, Action<TInput> command) =>
            throw new NotImplementedException();

        public static ICommandMonitor<TInput> Create<TInput>(this IMonitorFactory factory, Func<TInput, Task> command) =>
            throw new NotImplementedException();

        public static ICommandMonitor<TInput> Create<TInput>(this IMonitorFactory factory, Func<TInput, CancellationToken, Task> command) =>
            throw new NotImplementedException();

        public static ICommandMonitor<TInput> Create<TInput>(this IMonitorFactory factory, Func<TInput, ValueTask> command) =>
            throw new NotImplementedException();

        public static ICommandMonitor<TInput> Create<TInput>(this IMonitorFactory factory, Func<TInput, CancellationToken, ValueTask> command) =>
            throw new NotImplementedException();

        #endregion

        #region Query<TOutput>

        public static IQueryMonitor<TOutput> Create<TOutput>(this IMonitorFactory factory, Func<TOutput> command) =>
            throw new NotImplementedException();

        public static IQueryMonitor<TOutput> Create<TOutput>(this IMonitorFactory factory, Func<Task<TOutput>> command) =>
            throw new NotImplementedException();

        public static IQueryMonitor<TOutput> Create<TOutput>(this IMonitorFactory factory, Func<CancellationToken, Task<TOutput>> command) =>
            throw new NotImplementedException();

        public static IQueryMonitor<TOutput> Create<TOutput>(this IMonitorFactory factory, Func<ValueTask<TOutput>> command) =>
            throw new NotImplementedException();

        public static IQueryMonitor<TOutput> Create<TOutput>(this IMonitorFactory factory, Func<CancellationToken, ValueTask<TOutput>> command) =>
            throw new NotImplementedException();

        #endregion

        #region Query<TInput, TOutput>

        public static IQueryMonitor<TInput, TOutput> Create<TInput, TOutput>(this IMonitorFactory factory, Func<TInput, TOutput> query) =>
            throw new NotImplementedException();

        public static IQueryMonitor<TInput, TOutput> Create<TInput, TOutput>(this IMonitorFactory factory, Func<TInput, Task<TOutput>> query) =>
            throw new NotImplementedException();

        public static IQueryMonitor<TInput, TOutput> Create<TInput, TOutput>(this IMonitorFactory factory, Func<TInput, CancellationToken, Task<TOutput>> query) =>
            throw new NotImplementedException();

        public static IQueryMonitor<TInput, TOutput> Create<TInput, TOutput>(this IMonitorFactory factory, Func<TInput, ValueTask<TOutput>> query) =>
            throw new NotImplementedException();

        public static IQueryMonitor<TInput, TOutput> Create<TInput, TOutput>(this IMonitorFactory factory, Func<TInput, CancellationToken, ValueTask<TOutput>> query) =>
            throw new NotImplementedException();

        #endregion
    }
}
