using System;
using System.Threading;
using System.Threading.Tasks;
using Chronology;
using Fuzzy;

namespace Monitor
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
                    instrument.Measure(input);
                }
                catch(Exception e) {
                    instrument.Measure(e, input);
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
                HighResolutionTimestamp start = instrument.Start();
                try {
                    // Business logic
                    instrument.Measure(start, input);
                }
                catch(Exception e) {
                    instrument.Measure(start, e, input);
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
                HighResolutionTimestamp start = instrument.Start();
                try {
                    await Task.Yield(); // Business logic
                    instrument.Measure(start, input);
                }
                catch(Exception e) {
                    instrument.Measure(start, e);
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
                HighResolutionTimestamp start = instrument.Start();
                try {
                    await Task.Delay(50, cancellation); // Business logic
                    instrument.Measure(start, input);
                }
                catch(Exception e) {
                    instrument.Measure(start, e);
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
                instrument.Measure(input);
                return ValueTask.CompletedTask;
            }

            async Task WorkAsync(Input input) {
                HighResolutionTimestamp start = instrument.Start();
                try {
                    await Task.Yield(); // Business logic
                    instrument.Measure(start, input);
                }
                catch(Exception e) {
                    instrument.Measure(start, e);
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
                instrument.Measure(input);
                return ValueTask.CompletedTask;
            }

            async Task WorkAsync(Input input, CancellationToken cancellation) {
                HighResolutionTimestamp start = instrument.Start();
                try {
                    await Task.Delay(50, cancellation); // Business logic
                    instrument.Measure(start, input);
                }
                catch(Exception e) {
                    instrument.Measure(start, e);
                    throw;
                }
            }
        }
    }
}
