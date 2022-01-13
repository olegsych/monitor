namespace Athene.Monitor
{
    /// <summary>
    /// An example of monitoring multiple methods in the same class. While instrumenting
    /// multiple methods is a smell of breaking the Single Responsibility Principle,
    /// this is a very common scenario today.
    /// </summary>
    class MultipleMethodsExample
    {
        class Input { }

        readonly IInstrument<Input> instrument1;
        readonly IInstrument<Input> instrument2;

        public MultipleMethodsExample(IMonitor monitor) {
            instrument1 = monitor.Instrument<Input>(Method1);
            instrument2 = monitor.Instrument<Input>(Method2);
        }

        void Method1(Input input) => instrument1.Record(input);
        void Method2(Input input) => instrument2.Record(input);
    }
}
