﻿using System;

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
        IMonitor<TInput> Create<TInput>(Command<TInput> command);
        IMonitor<TInput, TOutput> Create<TInput, TOutput>(Query<TInput, TOutput> query);
    }
}