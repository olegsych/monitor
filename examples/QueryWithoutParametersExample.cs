using System;
using System.Threading;
using System.Threading.Tasks;
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
            readonly IQueryMonitor<Output> monitor;

            Count(IMonitor monitor) =>
                this.monitor = monitor.Query(Query);

            Output Query() {
                try {
                    var output = new Output();
                    monitor.Finish(output);
                    return output;
                }
                catch(Exception e) {
                    monitor.Finish(e);
                    throw;
                }
            }
        }

        class Duration: QueryWithoutParametersExample
        {
            readonly IQueryMonitor<Output> monitor;

            Duration(IMonitor monitor) =>
                this.monitor = monitor.Query(Query);

            Output Query() {
                Observation observation = monitor.Start();
                try {
                    var output = new Output();
                    monitor.Finish(observation, output);
                    return output;
                }
                catch(Exception e) {
                    monitor.Finish(observation, e);
                    throw;
                }
                // Dispose() records invalid observation if Finish() is not called
            }
        }

        class TaskQuery: QueryWithoutParametersExample
        {
            readonly IQueryMonitor<Output> monitor;

            TaskQuery(IMonitor monitor) =>
                this.monitor = monitor.Query(Query);

            async Task<Output> Query() {
                Observation observation = monitor.Start();
                try {
                    await Task.Yield();
                    var output = new Output();
                    monitor.Finish(observation, output);
                    return output;
                }
                catch(Exception e) {
                    monitor.Finish(observation, e);
                    throw;
                }
            }
        }

        class TaskWithCancellationToken: QueryWithoutParametersExample
        {
            readonly IQueryMonitor<Output> monitor;

            TaskWithCancellationToken(IMonitor monitor) =>
                this.monitor = monitor.Query(Query);

            async Task<Output> Query(CancellationToken cancellation) {
                Observation observation = monitor.Start();
                try {
                    await Task.Delay(50, cancellation);
                    var output = new Output();
                    monitor.Finish(observation, output);
                    return output;
                }
                catch(Exception e) {
                    monitor.Finish(observation, e);
                    throw;
                }
            }
        }

        class ValueTaskQuery: QueryWithoutParametersExample
        {
            readonly IQueryMonitor<Output> monitor;
            readonly bool completeSynchronously = fuzzy.Boolean();

            ValueTaskQuery(IMonitor monitor) =>
                this.monitor = monitor.Query(Query);

            ValueTask<Output> Query() =>
                completeSynchronously ?
                QuerySync() :
                new ValueTask<Output>(QueryAsync());

            ValueTask<Output> QuerySync() {
                var output = new Output();
                monitor.Finish(output);
                return ValueTask.FromResult(output);
            }

            async Task<Output> QueryAsync() {
                Observation observation = monitor.Start();
                try {
                    await Task.Yield();
                    var output = new Output();
                    monitor.Finish(observation, output);
                    return output;
                }
                catch(Exception e) {
                    monitor.Finish(observation, e);
                    throw;
                }
            }
        }

        class ValueTaskWithCancellationToken: QueryWithoutParametersExample
        {
            readonly IQueryMonitor<Output> monitor;
            readonly bool completeSynchronously = fuzzy.Boolean();

            ValueTaskWithCancellationToken(IMonitor monitor) =>
                this.monitor = monitor.Query(Query);

            ValueTask<Output> Query(CancellationToken cancellation) =>
                completeSynchronously ?
                QuerySync() :
                new ValueTask<Output>(QueryAsync(cancellation));

            ValueTask<Output> QuerySync() {
                var output = new Output();
                monitor.Finish(output);
                return ValueTask.FromResult(output);
            }

            async Task<Output> QueryAsync(CancellationToken cancellation) {
                Observation observation = monitor.Start();
                try {
                    await Task.Delay(50, cancellation);
                    var output = new Output();
                    monitor.Finish(observation, output);
                    return output;
                }
                catch(Exception e) {
                    monitor.Finish(observation, e);
                    throw;
                }
            }
        }
    }
}
