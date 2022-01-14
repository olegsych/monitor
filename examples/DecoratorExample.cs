using System;

namespace Athene.Monitor
{
    /// <summary>
    /// An example of how to implement a monitoring decorator when separation
    /// of processing and monitoring logic is desirable.
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
            readonly IInstrument<Input> instrument;

            public Decorator(IWork worker, IMonitor monitor) {
                this.worker = worker;
                instrument = monitor.Instrument<Input>(worker.Work);
            }

            public void Work(Input input) {
                Observation observation = instrument.Start();
                try {
                    worker.Work(input);
                    instrument.Record(observation, input);
                }
                catch (Exception e){
                    instrument.Record(observation, e, input);
                }
            }
        }
    }
}
