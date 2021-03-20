using System;
using System.Threading.Tasks;

namespace Monitor
{
    /// <summary>
    /// A basic example of monitoring an actor that takes an input, processes it synchronously and returns an output.
    /// </summary>
    class OutputExample
    {
        class Input { }
        class Output { }

        readonly IMonitor<Input, Output> monitor;

        public OutputExample(IMonitorFactory monitors) =>
            monitor = monitors.Create<Input, Output>(Work);

        Output Work(Input input) {
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
        }
    }
}
