using System;
using System.Threading;
using System.Threading.Tasks;
using Fuzzy;

namespace Monitor
{
    /// <summary>
    /// Examples of monitoring a query that takes an input and returns an output.
    /// </summary>
    class QueryWithParameterExample: Example
    {
        class Input { }
        class Output { }

        public class Count: QueryWithParameterExample
        {
            readonly IQueryMonitor<Input, Output> monitor;

            Count(IMonitorFactory monitors) =>
                monitor = monitors.Create<Input, Output>(Query);

            Output Query(Input input) {
                try {
                    var output = new Output();
                    monitor.Observe(input, output);
                    return output;
                }
                catch(Exception e) {
                    monitor.Observe(input, e);
                    throw;
                }
            }
        }

        class Duration: QueryWithParameterExample
        {
            readonly IQueryMonitor<Input, Output> monitor;

            Duration(IMonitorFactory monitors) =>
                monitor = monitors.Create<Input, Output>(Query);

            Output Query(Input input) {
                using IObservation<Output> observation = monitor.Start(input);
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

        class TaskQuery: QueryWithParameterExample
        {
            readonly IQueryMonitor<Input, Output> monitor;

            TaskQuery(IMonitorFactory monitors) =>
                monitor = monitors.Create<Input, Output>(Query);

            async Task<Output> Query(Input input) {
                using IObservation<Output> observation = monitor.Start(input);
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

        class TaskWithCancellationToken: QueryWithParameterExample
        {
            readonly IQueryMonitor<Input, Output> monitor;

            TaskWithCancellationToken(IMonitorFactory monitors) =>
                monitor = monitors.Create<Input, Output>(Query);

            async Task<Output> Query(Input input, CancellationToken cancellation) {
                using IObservation<Output> observation = monitor.Start(input);
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

        class ValueTaskQuery: QueryWithParameterExample
        {
            readonly IQueryMonitor<Input, Output> monitor;
            readonly bool completeSynchronously = fuzzy.Boolean();

            ValueTaskQuery(IMonitorFactory monitors) =>
                monitor = monitors.Create<Input, Output>(Query);

            ValueTask<Output> Query(Input input) =>
                completeSynchronously ?
                QuerySync(input) :
                new ValueTask<Output>(QueryAsync(input));

            ValueTask<Output> QuerySync(Input input) {
                var output = new Output();
                monitor.Observe(input, output);
                return ValueTask.FromResult(output);
            }

            async Task<Output> QueryAsync(Input input) {
                using IObservation<Output> observation = monitor.Start(input);
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

        class ValueTaskWithCancellationToken: QueryWithParameterExample
        {
            readonly IQueryMonitor<Input, Output> monitor;
            readonly bool completeSynchronously = fuzzy.Boolean();

            ValueTaskWithCancellationToken(IMonitorFactory monitors) =>
                monitor = monitors.Create<Input, Output>(Query);

            ValueTask<Output> Query(Input input, CancellationToken cancellation) =>
                completeSynchronously ?
                QuerySync(input) :
                new ValueTask<Output>(QueryAsync(input, cancellation));

            ValueTask<Output> QuerySync(Input input) {
                var output = new Output();
                monitor.Observe(input, output);
                return ValueTask.FromResult(output);
            }

            async Task<Output> QueryAsync(Input input, CancellationToken cancellation) {
                using IObservation<Output> observation = monitor.Start(input);
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
