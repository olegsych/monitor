using System;

namespace Monitor
{
    public static class ICommandMonitorExtensions
    {
        #region ICommandMonitor<TInput>

        public static void Finish<T>(this ICommandMonitor<T> monitor, T input) =>
            // monitor.Record(new Observation<T> { Input = input }, null);
            throw new NotImplementedException();

        public static void Finish<T>(this ICommandMonitor<T> monitor, T input, Exception exception) =>
            // monitor.Record(new Observation<T> { Input = input }, exception);
            throw new NotImplementedException();

        public static void Finish<T>(this ICommandMonitor<T> monitor, Observation<T> observation) =>
            // monitor.Record(observation, null);
            throw new NotImplementedException();

        public static void Finish<T>(this ICommandMonitor<T> monitor, Observation<T> observation, Exception exception) =>
            // monitor.Record(observation, exception);
            throw new NotImplementedException();

        #endregion
    }
}
