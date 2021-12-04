using System;
using System.Threading;
using System.Threading.Tasks;
using Chronology;
using Fuzzy;

namespace Monitor
{
    /// <summary>
    /// Examples of monitoring a query that takes an input and returns an output.
    /// </summary>
    class QueryWithoutParametersExample: Example
    {
        class Output { }

        public class Count: QueryWithoutParametersExample
        {
            readonly IInstrument<Output> instrument;

            Count(IMonitor monitor) =>
                instrument = monitor.Instrument(Query);

            Output Query() {
                try {
                    var output = new Output();
                    instrument.Measure(output);
                    return output;
                }
                catch(Exception e) {
                    instrument.Measure(e);
                    throw;
                }
            }
        }

        class Duration: QueryWithoutParametersExample
        {
            readonly IInstrument<Output> instrument;

            Duration(IMonitor monitor) =>
                instrument = monitor.Instrument(Query);

            Output Query() {
                HighResolutionTimestamp start = instrument.Start();
                try {
                    var output = new Output();
                    instrument.Measure(start, output);
                    return output;
                }
                catch(Exception e) {
                    instrument.Measure(start, e);
                    throw;
                }
            }
        }

        class TaskQuery: QueryWithoutParametersExample
        {
            readonly IInstrument<Output> instrument;

            TaskQuery(IMonitor monitor) =>
                instrument = monitor.Instrument(Query);

            async Task<Output> Query() {
                HighResolutionTimestamp start = instrument.Start();
                try {
                    await Task.Yield();
                    var output = new Output();
                    instrument.Measure(start, output);
                    return output;
                }
                catch(Exception e) {
                    instrument.Measure(start, e);
                    throw;
                }
            }
        }

        class TaskWithCancellationToken: QueryWithoutParametersExample
        {
            readonly IInstrument<Output> instrument;

            TaskWithCancellationToken(IMonitor monitor) =>
                instrument = monitor.Instrument(Query);

            async Task<Output> Query(CancellationToken cancellation) {
                HighResolutionTimestamp start = instrument.Start();
                try {
                    await Task.Delay(50, cancellation);
                    var output = new Output();
                    instrument.Measure(start, output);
                    return output;
                }
                catch(Exception e) {
                    instrument.Measure(start, e);
                    throw;
                }
            }
        }

        class ValueTaskQuery: QueryWithoutParametersExample
        {
            readonly IInstrument<Output> instrument;
            readonly bool completeSynchronously = fuzzy.Boolean();

            ValueTaskQuery(IMonitor monitor) =>
                instrument = monitor.Instrument(Query);

            ValueTask<Output> Query() =>
                completeSynchronously ?
                QuerySync() :
                new ValueTask<Output>(QueryAsync());

            ValueTask<Output> QuerySync() {
                var output = new Output();
                instrument.Measure(output);
                return ValueTask.FromResult(output);
            }

            async Task<Output> QueryAsync() {
                HighResolutionTimestamp start = instrument.Start();
                try {
                    await Task.Yield();
                    var output = new Output();
                    instrument.Measure(start, output);
                    return output;
                }
                catch(Exception e) {
                    instrument.Measure(start, e);
                    throw;
                }
            }
        }

        class ValueTaskWithCancellationToken: QueryWithoutParametersExample
        {
            readonly IInstrument<Output> instrument;
            readonly bool completeSynchronously = fuzzy.Boolean();

            ValueTaskWithCancellationToken(IMonitor monitor) =>
                instrument = monitor.Instrument(Query);

            ValueTask<Output> Query(CancellationToken cancellation) =>
                completeSynchronously ?
                QuerySync() :
                new ValueTask<Output>(QueryAsync(cancellation));

            ValueTask<Output> QuerySync() {
                var output = new Output();
                instrument.Measure(output);
                return ValueTask.FromResult(output);
            }

            async Task<Output> QueryAsync(CancellationToken cancellation) {
                HighResolutionTimestamp start = instrument.Start();
                try {
                    await Task.Delay(50, cancellation);
                    var output = new Output();
                    instrument.Measure(start, output);
                    return output;
                }
                catch(Exception e) {
                    instrument.Measure(start, e);
                    throw;
                }
            }
        }
    }
}
