using System;
using System.Threading;
using System.Threading.Tasks;
using Fuzzy;

namespace Athene.Monitor
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
                    instrument.Record(output);
                    return output;
                }
                catch(Exception e) {
                    instrument.Record(e);
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
                Observation observation = instrument.Start();
                try {
                    var output = new Output();
                    instrument.Record(observation, output);
                    return output;
                }
                catch(Exception e) {
                    instrument.Record(observation, e);
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
                Observation observation = instrument.Start();
                try {
                    await Task.Yield();
                    var output = new Output();
                    instrument.Record(observation, output);
                    return output;
                }
                catch(Exception e) {
                    instrument.Record(observation, e);
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
                Observation observation = instrument.Start();
                try {
                    await Task.Delay(50, cancellation);
                    var output = new Output();
                    instrument.Record(observation, output);
                    return output;
                }
                catch(Exception e) {
                    instrument.Record(observation, e);
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
                instrument.Record(output);
                return ValueTask.FromResult(output);
            }

            async Task<Output> QueryAsync() {
                Observation observation = instrument.Start();
                try {
                    await Task.Yield();
                    var output = new Output();
                    instrument.Record(observation, output);
                    return output;
                }
                catch(Exception e) {
                    instrument.Record(observation, e);
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
                instrument.Record(output);
                return ValueTask.FromResult(output);
            }

            async Task<Output> QueryAsync(CancellationToken cancellation) {
                Observation observation = instrument.Start();
                try {
                    await Task.Delay(50, cancellation);
                    var output = new Output();
                    instrument.Record(observation, output);
                    return output;
                }
                catch(Exception e) {
                    instrument.Record(observation, e);
                    throw;
                }
            }
        }
    }
}
