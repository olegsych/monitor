using System;

namespace Monitor
{
    /// <summary>
    /// An example of how to implement a monitoring decorator when separation of processing and monitoring logic is desirable.
    /// </summary>
    class DecoratorExample
    {
        class Input { }

        interface IWork
        {
            void Work(Input input);
        }

        class Decorator: IWork
        {
            readonly IWork worker;
            readonly ICommandMonitor<Input> monitor;

            public Decorator(IWork worker, IMonitor monitor) {
                this.worker = worker;
                this.monitor = monitor.Command<Input>(worker.Work);
            }

            public void Work(Input input) {
                Observation<Input> observation = monitor.Start(input);
                try {
                    worker.Work(input);
                    monitor.Finish(observation);
                }
                catch (Exception e){
                    monitor.Finish(observation, e);
                }
            }
        }
    }
}
