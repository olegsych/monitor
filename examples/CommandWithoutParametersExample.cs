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
                    monitor.Finish();
                }
                catch(Exception e) {
                    monitor.Finish(e);
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
                Observation observation = monitor.Start();
                try {
                    // Business logic
                    monitor.Finish(observation);
                }
                catch(Exception e) {
                    monitor.Finish(observation, e);
                    throw;
                }
            }
        }

        public class TaskOperation: CommandWithoutParametersExample
        {
            readonly ICommandMonitor monitor;

            public TaskOperation(IMonitor monitor) =>
                this.monitor = monitor.Command(Work);

            async Task Work() {
                Observation observation = monitor.Start();
                try {
                    await Task.Yield(); // Business logic
                    monitor.Finish(observation);
                }
                catch (Exception e) {
                    monitor.Finish(observation, e);
                    throw;
                }
            }
        }

        public class TaskWithCancellationToken: CommandWithoutParametersExample
        {
            readonly ICommandMonitor monitor;

            public TaskWithCancellationToken(IMonitor monitor) =>
                this.monitor = monitor.Command(Work);

            async Task Work(CancellationToken cancellation) {
                Observation observation = monitor.Start();
                try {
                    await Task.Delay(50, cancellation); // Business logic
                    monitor.Finish(observation);
                }
                catch(Exception e) {
                    monitor.Finish(observation, e);
                    throw;
                }
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
                monitor.Finish();
                return ValueTask.CompletedTask;
            }

            async Task WorkAsync() {
                Observation observation = monitor.Start();
                try {
                    await Task.Yield(); // Business logic
                    monitor.Finish(observation);
                }
                catch(Exception e) {
                    monitor.Finish(observation, e);
                    throw;
                }
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
                monitor.Finish();
                return ValueTask.CompletedTask;
            }

            async Task WorkAsync(CancellationToken cancellation) {
                Observation observation = monitor.Start();
                try {
                    await Task.Delay(50, cancellation); // Business logic
                    monitor.Finish(observation);
                }
                catch(Exception e) {
                    monitor.Finish(observation, e);
                    throw;
                }
            }
        }
    }
}
