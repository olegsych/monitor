using System;
using System.Threading;
using System.Threading.Tasks;
using Fuzzy;

namespace Athene.Monitor
{
    class CommandWithParametersExample: Example
    {
        class Input { }

        class Count: CommandWithParametersExample
        {
            readonly IInstrument<Input> instrument;

            Count(IMonitor monitor) =>
                instrument = monitor.Instrument<Input>(Work);

            void Work(Input input) {
                try {
                    // Business logic
                    instrument.Record(input);
                }
                catch(Exception e) {
                    instrument.Record(e, input);
                    throw;
                }
            }
        }

        class Duration: CommandWithParametersExample
        {
            readonly IInstrument<Input> instrument;

            Duration(IMonitor monitor) =>
                instrument = monitor.Instrument<Input>(Work);

            void Work(Input input) {
                Observation observation = instrument.Start();
                try {
                    // Business logic
                    instrument.Record(observation, input);
                }
                catch(Exception e) {
                    instrument.Record(observation, e, input);
                    throw;
                }
            }
        }

        class TaskCommand: CommandWithParametersExample
        {
            readonly IInstrument<Input> instrument;

            TaskCommand(IMonitor monitor) =>
                instrument = monitor.Instrument<Input>(Work);

            async Task Work(Input input) {
                Observation observation = instrument.Start();
                try {
                    await Task.Yield(); // Business logic
                    instrument.Record(observation, input);
                }
                catch(Exception e) {
                    instrument.Record(observation, e);
                    throw;
                }
            }
        }

        class TaskWithCancellationToken: CommandWithParametersExample
        {
            readonly IInstrument<Input> instrument;

            TaskWithCancellationToken(IMonitor monitor) =>
                instrument = monitor.Instrument<Input>(Work);

            async Task Work(Input input, CancellationToken cancellation) {
                Observation observation = instrument.Start();
                try {
                    await Task.Delay(50, cancellation); // Business logic
                    instrument.Record(observation, input);
                }
                catch(Exception e) {
                    instrument.Record(observation, e);
                    throw;
                }
            }
        }

        class ValueTaskCommand: CommandWithParametersExample
        {
            readonly IInstrument<Input> instrument;
            readonly bool completeSynchronously = fuzzy.Boolean();

            ValueTaskCommand(IMonitor monitor) =>
                instrument = monitor.Instrument<Input>(Work);

            ValueTask Work(Input input) =>
                completeSynchronously
                ? WorkSync(input)
                : new ValueTask(WorkAsync(input));

            ValueTask WorkSync(Input input) {
                // Business logic
                instrument.Record(input);
                return ValueTask.CompletedTask;
            }

            async Task WorkAsync(Input input) {
                Observation observation = instrument.Start();
                try {
                    await Task.Yield(); // Business logic
                    instrument.Record(observation, input);
                }
                catch(Exception e) {
                    instrument.Record(observation, e);
                    throw;
                }
            }
        }

        class ValueTaskWithCancellationToken: CommandWithParametersExample
        {
            readonly IInstrument<Input> instrument;
            readonly bool completeSynchronously = fuzzy.Boolean();

            ValueTaskWithCancellationToken(IMonitor monitor) =>
                instrument = monitor.Instrument<Input>(Work);

            ValueTask Work(Input input, CancellationToken cancellation) =>
                completeSynchronously
                ? WorkSync(input)
                : new ValueTask(WorkAsync(input, cancellation));

            ValueTask WorkSync(Input input) {
                // Business logic
                instrument.Record(input);
                return ValueTask.CompletedTask;
            }

            async Task WorkAsync(Input input, CancellationToken cancellation) {
                Observation observation = instrument.Start();
                try {
                    await Task.Delay(50, cancellation); // Business logic
                    instrument.Record(observation, input);
                }
                catch(Exception e) {
                    instrument.Record(observation, e);
                    throw;
                }
            }
        }
    }
}
