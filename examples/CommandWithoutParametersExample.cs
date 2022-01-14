using System;
using System.Threading;
using System.Threading.Tasks;
using Fuzzy;

namespace Athene.Monitor
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
                    instrument.Record();
                }
                catch(Exception e) {
                    instrument.Record(e);
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
                Observation observation = instrument.Start();
                try {
                    // Business logic
                    instrument.Record(observation);
                }
                catch(Exception e) {
                    instrument.Record(observation, e);
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
                Observation observation = instrument.Start();
                try {
                    await Task.Yield(); // Business logic
                    instrument.Record(observation);
                }
                catch (Exception e) {
                    instrument.Record(observation, e);
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
                Observation observation = instrument.Start();
                try {
                    await Task.Delay(50, cancellation); // Business logic
                    instrument.Record(observation);
                }
                catch(Exception e) {
                    instrument.Record(observation, e);
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
                instrument.Record();
                return ValueTask.CompletedTask;
            }

            async Task WorkAsync() {
                Observation observation = instrument.Start();
                try {
                    await Task.Yield(); // Business logic
                    instrument.Record(observation);
                }
                catch(Exception e) {
                    instrument.Record(observation, e);
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
                instrument.Record();
                return ValueTask.CompletedTask;
            }

            async Task WorkAsync(CancellationToken cancellation) {
                Observation observation = instrument.Start();
                try {
                    await Task.Delay(50, cancellation); // Business logic
                    instrument.Record(observation);
                }
                catch(Exception e) {
                    instrument.Record(observation, e);
                    throw;
                }
            }
        }
    }
}
