﻿using System;

namespace Monitor
{
    public static class IQueryMonitorExtensions
    {
        #region IQueryMonitor<TInput, TOutput>

        public static void Finish<TInput, TOutput>(this IQueryMonitor<TInput, TOutput> monitor, TInput input, TOutput output) =>
            // monitor.Record(new Observation<TInput> { Input = input }, output, null);
            throw new NotImplementedException();

        public static void Finish<TInput, TOutput>(this IQueryMonitor<TInput, TOutput> monitor, TInput input, Exception exception) =>
            // monitor.Record(new Observation<TInput> { Input = input }, default, exception);
            throw new NotImplementedException();

        public static void Finish<TInput, TOutput>(this IQueryMonitor<TInput, TOutput> monitor, Observation<TInput> observation, TOutput output) =>
            // monitor.Record(observation, output, null);
            throw new NotImplementedException();

        public static void Finish<TInput, TOutput>(this IQueryMonitor<TInput, TOutput> monitor, Observation<TInput> observation, Exception exception) =>
            // monitor.Record(observation, default, exception);
            throw new NotImplementedException();

        #endregion
    }
}
