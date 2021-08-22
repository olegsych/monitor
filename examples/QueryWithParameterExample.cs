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

            Count(IMonitor monitor) =>
                this.monitor = monitor.Query<Input, Output>(Query);

            Output Query(Input input) {
                try {
                    var output = new Output();
                    monitor.Finish(input, output);
                    return output;
                }
                catch(Exception e) {
                    monitor.Finish(input, e);
                    throw;
                }
            }
        }

        class Duration: QueryWithParameterExample
        {
            readonly IQueryMonitor<Input, Output> monitor;

            Duration(IMonitor monitor) =>
                this.monitor = monitor.Query<Input, Output>(Query);

            Output Query(Input input) {
                Observation<Input> observation = monitor.Start(input);
                try {
                    var output = new Output(); // Business logic
                    monitor.Finish(observation, output);
                    return output;
                }
                catch(Exception e) {
                    monitor.Finish(observation, e);
                    throw;
                }
            }
        }

        class TaskQuery: QueryWithParameterExample
        {
            readonly IQueryMonitor<Input, Output> monitor;

            TaskQuery(IMonitor monitor) =>
                this.monitor = monitor.Query<Input, Output>(Query);

            async Task<Output> Query(Input input) {
                Observation<Input> observation = monitor.Start(input);
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

        class TaskWithCancellationToken: QueryWithParameterExample
        {
            readonly IQueryMonitor<Input, Output> monitor;

            TaskWithCancellationToken(IMonitor monitor) =>
                this.monitor = monitor.Query<Input, Output>(Query);

            async Task<Output> Query(Input input, CancellationToken cancellation) {
                Observation<Input> observation = monitor.Start(input);
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

        class ValueTaskQuery: QueryWithParameterExample
        {
            readonly IQueryMonitor<Input, Output> monitor;
            readonly bool completeSynchronously = fuzzy.Boolean();

            ValueTaskQuery(IMonitor monitor) =>
                this.monitor = monitor.Query<Input, Output>(Query);

            ValueTask<Output> Query(Input input) =>
                completeSynchronously ?
                QuerySync(input) :
                new ValueTask<Output>(QueryAsync(input));

            ValueTask<Output> QuerySync(Input input) {
                var output = new Output();
                monitor.Finish(input, output);
                return ValueTask.FromResult(output);
            }

            async Task<Output> QueryAsync(Input input) {
                Observation<Input> observation = monitor.Start(input);
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

        class ValueTaskWithCancellationToken: QueryWithParameterExample
        {
            readonly IQueryMonitor<Input, Output> monitor;
            readonly bool completeSynchronously = fuzzy.Boolean();

            ValueTaskWithCancellationToken(IMonitor monitor) =>
                this.monitor = monitor.Query<Input, Output>(Query);

            ValueTask<Output> Query(Input input, CancellationToken cancellation) =>
                completeSynchronously ?
                QuerySync(input) :
                new ValueTask<Output>(QueryAsync(input, cancellation));

            ValueTask<Output> QuerySync(Input input) {
                var output = new Output();
                monitor.Finish(input, output);
                return ValueTask.FromResult(output);
            }

            async Task<Output> QueryAsync(Input input, CancellationToken cancellation) {
                Observation<Input> observation = monitor.Start(input);
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
