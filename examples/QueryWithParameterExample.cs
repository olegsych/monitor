using System;
using System.Threading;
using System.Threading.Tasks;
using Fuzzy;

namespace Athene.Monitor
{
    /// <summary>
    /// Examples of monitoring a query that takes an input and returns an output.
    /// </summary>
    class QueryWithParameterExample: Example
    {
        class Input { }
        class Output { }

        class Count: QueryWithParameterExample
        {
            readonly IInstrument<Input, Output> instrument;

            Count(IMonitor monitor) =>
                instrument = monitor.Instrument<Input, Output>(Query);

            Output Query(Input input) {
                try {
                    var output = new Output();
                    instrument.Record(input, output);
                    return output;
                }
                catch(Exception e) {
                    instrument.Record(e, input);
                    throw;
                }
            }
        }

        class Duration: QueryWithParameterExample
        {
            readonly IInstrument<Input, Output> instrument;

            Duration(IMonitor monitor) =>
                instrument = monitor.Instrument<Input, Output>(Query);

            Output Query(Input input) {
                Measurement measurement = instrument.Start();
                try {
                    var output = new Output(); // Business logic
                    instrument.Record(measurement, input, output);
                    return output;
                }
                catch(Exception e) {
                    instrument.Record(measurement, e, input);
                    throw;
                }
            }
        }

        class TaskQuery: QueryWithParameterExample
        {
            readonly IInstrument<Input, Output> instrument;

            TaskQuery(IMonitor monitor) =>
                instrument = monitor.Instrument<Input, Output>(Query);

            async Task<Output> Query(Input input) {
                Measurement measurement = instrument.Start();
                try {
                    await Task.Yield();
                    var output = new Output();
                    instrument.Record(measurement, input, output);
                    return output;
                }
                catch(Exception e) {
                    instrument.Record(measurement, e, input);
                    throw;
                }
            }
        }

        class TaskWithCancellationToken: QueryWithParameterExample
        {
            readonly IInstrument<Input, Output> instrument;

            TaskWithCancellationToken(IMonitor monitor) =>
                instrument = monitor.Instrument<Input, Output>(Query);

            async Task<Output> Query(Input input, CancellationToken cancellation) {
                Measurement measurement = instrument.Start();
                try {
                    await Task.Delay(50, cancellation);
                    var output = new Output();
                    instrument.Record(measurement, input, output);
                    return output;
                }
                catch(Exception e) {
                    instrument.Record(measurement, e, input);
                    throw;
                }
            }
        }

        class ValueTaskQuery: QueryWithParameterExample
        {
            readonly IInstrument<Input, Output> instrument;
            readonly bool completeSynchronously = fuzzy.Boolean();

            ValueTaskQuery(IMonitor monitor) =>
                instrument = monitor.Instrument<Input, Output>(Query);

            ValueTask<Output> Query(Input input) =>
                completeSynchronously ?
                QuerySync(input) :
                new ValueTask<Output>(QueryAsync(input));

            ValueTask<Output> QuerySync(Input input) {
                var output = new Output();
                instrument.Record(input, output);
                return ValueTask.FromResult(output);
            }

            async Task<Output> QueryAsync(Input input) {
                Measurement measurement = instrument.Start();
                try {
                    await Task.Yield();
                    var output = new Output();
                    instrument.Record(measurement, input, output);
                    return output;
                }
                catch(Exception e) {
                    instrument.Record(measurement, e, input);
                    throw;
                }
            }
        }

        class ValueTaskWithCancellationToken: QueryWithParameterExample
        {
            readonly IInstrument<Input, Output> instrument;
            readonly bool completeSynchronously = fuzzy.Boolean();

            ValueTaskWithCancellationToken(IMonitor monitor) =>
                instrument = monitor.Instrument<Input, Output>(Query);

            ValueTask<Output> Query(Input input, CancellationToken cancellation) =>
                completeSynchronously ?
                QuerySync(input) :
                new ValueTask<Output>(QueryAsync(input, cancellation));

            ValueTask<Output> QuerySync(Input input) {
                var output = new Output();
                instrument.Record(input, output);
                return ValueTask.FromResult(output);
            }

            async Task<Output> QueryAsync(Input input, CancellationToken cancellation) {
                Measurement measurement = instrument.Start();
                try {
                    await Task.Delay(50, cancellation);
                    var output = new Output();
                    instrument.Record(measurement, input, output);
                    return output;
                }
                catch(Exception e) {
                    instrument.Record(measurement, e, input);
                    throw;
                }
            }
        }
    }
}
