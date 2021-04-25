using System;
using System.Threading;
using System.Threading.Tasks;
using Fuzzy;

namespace Monitor
{
    public class CommandWithoutParametersExample: Example
    {
        public class Count: CommandWithoutParametersExample
        {
            readonly ICommandMonitor monitor;

            public Count(IMonitor monitor) =>
                this.monitor = monitor.Command(Work);

            void Work() {
                try {
                    // Business logic
                    monitor.Observe();
                }
                catch(Exception e) {
                    monitor.Observe(e);
                    throw;
                }
            }
        }

        public class Duration: CommandWithoutParametersExample
        {
            readonly ICommandMonitor monitor;

            public Duration(IMonitor monitor) =>
                this.monitor = monitor.Command(Work);

            void Work() {
                using IObservation observation = monitor.Start();
                try {
                    // Business logic
                }
                catch(Exception e) {
                    observation.Finish(e);
                    throw;
                }
                // Dispose() finishes successful observation
            }
        }

        public class TaskOperation: CommandWithoutParametersExample
        {
            readonly ICommandMonitor monitor;

            public TaskOperation(IMonitor monitor) =>
                this.monitor = monitor.Command(Work);

            async Task Work() {
                using IObservation observation = monitor.Start();
                try {
                    await Task.Yield(); // Business logic
                }
                catch (Exception e) {
                    monitor.Observe(e);
                    throw;
                }
                // Dispose() finishes successful observation
            }
        }

        public class TaskWithCancellationToken: CommandWithoutParametersExample
        {
            readonly ICommandMonitor monitor;

            public TaskWithCancellationToken(IMonitor monitor) =>
                this.monitor = monitor.Command(Work);

            async Task Work(CancellationToken cancellation) {
                using IObservation observation = monitor.Start();
                try {
                    await Task.Delay(50, cancellation); // Business logic
                }
                catch(Exception e) {
                    monitor.Observe(e);
                    throw;
                }
                // Dispose() finishes successful observation
            }
        }

        public class ValueTaskOperation: CommandWithoutParametersExample
        {
            readonly ICommandMonitor monitor;
            readonly bool completeSynchronously = fuzzy.Boolean();

            public ValueTaskOperation(IMonitor monitor) =>
                this.monitor = monitor.Command(Work);

            ValueTask Work() =>
                completeSynchronously
                ? WorkSync()
                : new ValueTask(WorkAsync());

            ValueTask WorkSync() {
                // Business logic
                monitor.Observe();
                return ValueTask.CompletedTask;
            }

            async Task WorkAsync() {
                using IObservation observation = monitor.Start();
                try {
                    await Task.Yield(); // Business logic
                }
                catch(Exception e) {
                    observation.Finish(e);
                    throw;
                }
                // Dispose() finishes successful observation
            }
        }

        public class ValueTaskWithCancellationToken: CommandWithoutParametersExample
        {
            readonly ICommandMonitor monitor;
            readonly bool completeSynchronously = fuzzy.Boolean();

            public ValueTaskWithCancellationToken(IMonitor monitor) =>
                this.monitor = monitor.Command(Work);

            ValueTask Work(CancellationToken cancellation) =>
                completeSynchronously
                ? WorkSync()
                : new ValueTask(WorkAsync(cancellation));

            ValueTask WorkSync() {
                // Business logic
                monitor.Observe();
                return ValueTask.CompletedTask;
            }

            async Task WorkAsync(CancellationToken cancellation) {
                using IObservation observation = monitor.Start();
                try {
                    await Task.Delay(50, cancellation); // Business logic
                }
                catch(Exception e) {
                    observation.Finish(e);
                    throw;
                }
                // Dispose() finishes successful observation
            }
        }
    }
}
