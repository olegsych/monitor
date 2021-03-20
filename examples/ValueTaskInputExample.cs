using System;
using System.Threading.Tasks;

namespace Monitor
{
    class ValueTaskInputExample
    {
        class Input { }

        readonly IMonitor<Input> monitor;

        public ValueTaskInputExample(IMonitorFactory monitors) =>
            monitor = monitors.Create<Input>(Process);

        ValueTask Process(Input input) =>
            Processed(input)
            ? ValueTask.CompletedTask
            : new ValueTask(ProcessAsync(input));

        bool Processed(Input input) {
            try {
                bool result = true;
                monitor.Observe(input);
                return result;
            }
            catch(Exception e) {
                monitor.Observe(input, e);
                throw;
            }
        }

        async Task ProcessAsync(Input input) {
            using IObservation observation = monitor.Start(input);
            try {
                await Task.Yield();
                // Successful observation is finished by Dispose
            }
            catch(Exception e) {
                observation.Finish(e);
                throw;
            }
        }
    }
}
