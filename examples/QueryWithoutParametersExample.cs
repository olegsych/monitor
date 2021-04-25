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
                this.monitor = monitor.Create(Query);

            Output Query() {
                try {
                    var output = new Output();
                    monitor.Observe(output);
                    return output;
                }
                catch(Exception e) {
                    monitor.Observe(e);
                    throw;
                }
            }
        }

        class Duration: QueryWithoutParametersExample
        {
            readonly IQueryMonitor<Output> monitor;

            Duration(IMonitor monitor) =>
                this.monitor = monitor.Create(Query);

            Output Query() {
                using IObservation<Output> observation = monitor.Start();
                try {
                    var output = new Output();
                    observation.Finish(output);
                    return output;
                }
                catch(Exception e) {
                    observation.Finish(e);
                    throw;
                }
                // Dispose() records invalid observation if Finish() is not called
            }
        }

        class TaskQuery: QueryWithoutParametersExample
        {
            readonly IQueryMonitor<Output> monitor;

            TaskQuery(IMonitor monitor) =>
                this.monitor = monitor.Create(Query);

            async Task<Output> Query() {
                using IObservation<Output> observation = monitor.Start();
                try {
                    await Task.Yield();
                    var output = new Output();
                    observation.Finish(output);
                    return output;
                }
                catch(Exception e) {
                    observation.Finish(e);
                    throw;
                }
            }
        }

        class TaskWithCancellationToken: QueryWithoutParametersExample
        {
            readonly IQueryMonitor<Output> monitor;

            TaskWithCancellationToken(IMonitor monitor) =>
                this.monitor = monitor.Create(Query);

            async Task<Output> Query(CancellationToken cancellation) {
                using IObservation<Output> observation = monitor.Start();
                try {
                    await Task.Delay(50, cancellation);
                    var output = new Output();
                    observation.Finish(output);
                    return output;
                }
                catch(Exception e) {
                    observation.Finish(e);
                    throw;
                }
            }
        }

        class ValueTaskQuery: QueryWithoutParametersExample
        {
            readonly IQueryMonitor<Output> monitor;
            readonly bool completeSynchronously = fuzzy.Boolean();

            ValueTaskQuery(IMonitor monitor) =>
                this.monitor = monitor.Create(Query);

            ValueTask<Output> Query() =>
                completeSynchronously ?
                QuerySync() :
                new ValueTask<Output>(QueryAsync());

            ValueTask<Output> QuerySync() {
                var output = new Output();
                monitor.Observe(output);
                return ValueTask.FromResult(output);
            }

            async Task<Output> QueryAsync() {
                using IObservation<Output> observation = monitor.Start();
                try {
                    await Task.Yield();
                    var output = new Output();
                    observation.Finish(output);
                    return output;
                }
                catch(Exception e) {
                    observation.Finish(e);
                    throw;
                }
            }
        }

        class ValueTaskWithCancellationToken: QueryWithoutParametersExample
        {
            readonly IQueryMonitor<Output> monitor;
            readonly bool completeSynchronously = fuzzy.Boolean();

            ValueTaskWithCancellationToken(IMonitor monitor) =>
                this.monitor = monitor.Create(Query);

            ValueTask<Output> Query(CancellationToken cancellation) =>
                completeSynchronously ?
                QuerySync() :
                new ValueTask<Output>(QueryAsync(cancellation));

            ValueTask<Output> QuerySync() {
                var output = new Output();
                monitor.Observe(output);
                return ValueTask.FromResult(output);
            }

            async Task<Output> QueryAsync(CancellationToken cancellation) {
                using IObservation<Output> observation = monitor.Start();
                try {
                    await Task.Delay(50, cancellation);
                    var output = new Output();
                    observation.Finish(output);
                    return output;
                }
                catch(Exception e) {
                    observation.Finish(e);
                    throw;
                }
            }
        }
    }
}
