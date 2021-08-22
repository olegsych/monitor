using System;

namespace Monitor
{
    public static class ICommandMonitorExtensions
    {
        public static void Finish(this ICommandMonitor monitor) =>
            // monitor.Record(new Observation(), null);
            throw new NotImplementedException();

        public static void Finish(this ICommandMonitor monitor, Exception exception) =>
            // monitor.Record(new Observation(), exception);
            throw new NotImplementedException();

        public static void Finish(this ICommandMonitor monitor, Observation observation) =>
            // monitor.Record(observation, null);
            throw new NotImplementedException();

        public static void Finish(this ICommandMonitor monitor, Observation observation, Exception exception) =>
            // monitor.Record(observation, exception);
            throw new NotImplementedException();
    }
}
