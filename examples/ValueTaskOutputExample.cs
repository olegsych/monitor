using System;
using System.Threading.Tasks;

namespace Monitor
{
    class ValueTaskOutputExample
    {
        class Input { }
        class Output { }

        readonly IMonitor<Input, Output> monitor;

        public ValueTaskOutputExample(IMonitorFactory monitors) =>
            monitor = monitors.Create<Input, Output>(Query);

        ValueTask<Output> Query(Input input) =>
            TryGetFromCache(input, out Output output)
            ? ValueTask.FromResult(output)
            : new ValueTask<Output>(QueryAsync(input));

        bool TryGetFromCache(Input input, out Output output) {
            try {
                output = new Output();
                monitor.Observe(input, output);
                return true;
            }
            catch(Exception e) {
                monitor.Observe(input, e);
                throw;
            }
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
}
