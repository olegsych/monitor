using System;
using System.Threading.Tasks;

namespace Monitor
{
    class TaskOutputExample
    {
        class Input { }
        class Output { }

        readonly IMonitor<Input, Output> monitor;

        public TaskOutputExample(IMonitorFactory monitors) =>
            monitor = monitors.Create<Input, Output>(Work);

        async Task<Output> Work(Input input) {
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
