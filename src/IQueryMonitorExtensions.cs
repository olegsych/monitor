using System;

namespace Monitor
{
    public static class IQueryMonitorExtensions
    {
        #region IQueryMonitor<TOutput>

        public static void Finish<TOutput>(this IQueryMonitor<TOutput> monitor, TOutput output) =>
            // monitor.Record(new Observation(), output, null);
            throw new NotImplementedException();

        public static void Finish<TOutput>(this IQueryMonitor<TOutput> monitor, Exception exception) =>
            // monitor.Record(new Observation(), default, exception);
            throw new NotImplementedException();

        public static void Finish<TOutput>(this IQueryMonitor<TOutput> monitor, Observation observation, TOutput output) =>
            // monitor.Record(observation, output, null);
            throw new NotImplementedException();

        public static void Finish<TOutput>(this IQueryMonitor<TOutput> monitor, Observation observation, Exception exception) =>
            // monitor.Record(observation, default, exception);
            throw new NotImplementedException();

        #endregion

        //#region ICommandMonitor<TInput>

        //public static void Finish<T>(this ICommandMonitor<T> monitor, T input) =>
        //    // monitor.Record(new Observation<T> { Input = input }, null);
        //    throw new NotImplementedException();

        //public static void Finish<T>(this ICommandMonitor<T> monitor, T input, Exception exception) =>
        //    // monitor.Record(new Observation<T> { Input = input }, exception);
        //    throw new NotImplementedException();

        //public static void Finish<T>(this ICommandMonitor<T> monitor, Observation<T> observation) =>
        //    // monitor.Record(observation, null);
        //    throw new NotImplementedException();

        //public static void Finish<T>(this ICommandMonitor<T> monitor, Observation<T> observation, Exception exception) =>
        //    // monitor.Record(observation, exception);
        //    throw new NotImplementedException();

        //#endregion
    }
}
