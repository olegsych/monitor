using System;
using System.Threading.Tasks;

namespace Monitor
{
    class TaskInputExample
    {
        class Input { }

        readonly IMonitor<Input> monitor;

        public TaskInputExample(IMonitorFactory monitors) =>
            monitor = monitors.Create<Input>(Work);

        async Task Work(Input input) {
            using IObservation observation = monitor.Start(input);
            try {
                await Task.Yield(); // Successful observation is finished by DisposeAsync
            }
            catch(Exception e) {
                observation.Finish(e);
                throw;
            }
        }
    }
}
