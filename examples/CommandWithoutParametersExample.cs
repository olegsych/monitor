using System;
using System.Threading;
using System.Threading.Tasks;
using Chronology;
using Fuzzy;

namespace Monitor
{
    class CommandWithoutParametersExample: Example
    {
        class Count: CommandWithoutParametersExample
        {
            readonly IInstrument instrument;

            public Count(IMonitor monitor) =>
                instrument = monitor.Instrument(Work);

            void Work() {
                try {
                    // Business logic
                    instrument.Measure();
                }
                catch(Exception e) {
                    instrument.Measure(e);
                    throw;
                }
            }
        }

        class Duration: CommandWithoutParametersExample
        {
            readonly IInstrument instrument;

            public Duration(IMonitor monitor) =>
                instrument = monitor.Instrument(Work);

            void Work() {
                HighResolutionTimestamp start = instrument.Start();
                try {
                    // Business logic
                    instrument.Measure(start);
                }
                catch(Exception e) {
                    instrument.Measure(start, e);
                    throw;
                }
            }
        }

        class TaskOperation: CommandWithoutParametersExample
        {
            readonly IInstrument instrument;

            public TaskOperation(IMonitor monitor) =>
                instrument = monitor.Instrument(Work);

            async Task Work() {
                HighResolutionTimestamp start = instrument.Start();
                try {
                    await Task.Yield(); // Business logic
                    instrument.Measure(start);
                }
                catch (Exception e) {
                    instrument.Measure(start, e);
                    throw;
                }
            }
        }

        public class TaskWithCancellationToken: CommandWithoutParametersExample
        {
            readonly IInstrument instrument;

            public TaskWithCancellationToken(IMonitor monitor) =>
                instrument = monitor.Instrument(Work);

            async Task Work(CancellationToken cancellation) {
                HighResolutionTimestamp start = instrument.Start();
                try {
                    await Task.Delay(50, cancellation); // Business logic
                    instrument.Measure(start);
                }
                catch(Exception e) {
                    instrument.Measure(start, e);
                    throw;
                }
            }
        }

        public class ValueTaskOperation: CommandWithoutParametersExample
        {
            readonly IInstrument instrument;
            readonly bool completeSynchronously = fuzzy.Boolean();

            public ValueTaskOperation(IMonitor monitor) =>
                instrument = monitor.Instrument(Work);

            ValueTask Work() =>
                completeSynchronously
                ? WorkSync()
                : new ValueTask(WorkAsync());

            ValueTask WorkSync() {
                // Business logic
                instrument.Measure();
                return ValueTask.CompletedTask;
            }

            async Task WorkAsync() {
                HighResolutionTimestamp start = instrument.Start();
                try {
                    await Task.Yield(); // Business logic
                    instrument.Measure(start);
                }
                catch(Exception e) {
                    instrument.Measure(start, e);
                    throw;
                }
            }
        }

        public class ValueTaskWithCancellationToken: CommandWithoutParametersExample
        {
            readonly IInstrument instrument;
            readonly bool completeSynchronously = fuzzy.Boolean();

            public ValueTaskWithCancellationToken(IMonitor monitor) =>
                instrument = monitor.Instrument(Work);

            ValueTask Work(CancellationToken cancellation) =>
                completeSynchronously
                ? WorkSync()
                : new ValueTask(WorkAsync(cancellation));

            ValueTask WorkSync() {
                // Business logic
                instrument.Measure();
                return ValueTask.CompletedTask;
            }

            async Task WorkAsync(CancellationToken cancellation) {
                HighResolutionTimestamp start = instrument.Start();
                try {
                    await Task.Delay(50, cancellation); // Business logic
                    instrument.Measure(start);
                }
                catch(Exception e) {
                    instrument.Measure(start, e);
                    throw;
                }
            }
        }
    }
}
