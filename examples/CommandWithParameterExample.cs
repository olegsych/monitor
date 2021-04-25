using System;
using System.Threading;
using System.Threading.Tasks;
using Fuzzy;

namespace Monitor
{
    class CommandWithParametersExample: Example
    {
        class Input { }

        class Count: CommandWithParametersExample
        {
            readonly IMonitor<Input> monitor;

            Count(IMonitorFactory factory) =>
                monitor = factory.Create<Input>(Work);

            void Work(Input input) {
                try {
                    // Business logic
                    monitor.Observe(input);
                }
                catch(Exception e) {
                    monitor.Observe(input, e);
                    throw;
                }
            }
        }

        class Duration: CommandWithParametersExample
        {
            readonly IMonitor<Input> monitor;

            Duration(IMonitorFactory factory) =>
                monitor = factory.Create<Input>(Work);

            void Work(Input input) {
                using IObservation observation = monitor.Start(input);
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

        class TaskCommand: CommandWithParametersExample
        {
            readonly IMonitor<Input> monitor;

            TaskCommand(IMonitorFactory factory) =>
                monitor = factory.Create<Input>(Work);

            async Task Work(Input input) {
                using IObservation observation = monitor.Start(input);
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

        class TaskWithCancellationToken: CommandWithParametersExample
        {
            readonly IMonitor<Input> monitor;

            TaskWithCancellationToken(IMonitorFactory factory) =>
                monitor = factory.Create<Input>(Work);

            async Task Work(Input input, CancellationToken cancellation) {
                using IObservation observation = monitor.Start(input);
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

        class ValueTaskCommand: CommandWithParametersExample
        {
            readonly IMonitor<Input> monitor;
            readonly bool completeSynchronously = fuzzy.Boolean();

            ValueTaskCommand(IMonitorFactory factory) =>
                monitor = factory.Create<Input>(Work);

            ValueTask Work(Input input) =>
                completeSynchronously
                ? WorkSync(input)
                : new ValueTask(WorkAsync(input));

            ValueTask WorkSync(Input input) {
                // Business logic
                monitor.Observe(input);
                return ValueTask.CompletedTask;
            }

            async Task WorkAsync(Input input) {
                using IObservation observation = monitor.Start(input);
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

        class ValueTaskWithCancellationToken: CommandWithParametersExample
        {
            readonly IMonitor<Input> monitor;
            readonly bool completeSynchronously = fuzzy.Boolean();

            ValueTaskWithCancellationToken(IMonitorFactory factory) =>
                monitor = factory.Create<Input>(Work);

            ValueTask Work(Input input, CancellationToken cancellation) =>
                completeSynchronously
                ? WorkSync(input)
                : new ValueTask(WorkAsync(input, cancellation));

            ValueTask WorkSync(Input input) {
                // Business logic
                monitor.Observe(input);
                return ValueTask.CompletedTask;
            }

            async Task WorkAsync(Input input, CancellationToken cancellation) {
                using IObservation observation = monitor.Start(input);
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
