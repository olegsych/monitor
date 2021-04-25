namespace Monitor
{
    /// <summary>
    /// An example of monitoring multiple methods in the same class. While needing separate monitoring for different methods is a smell of breaking the
    /// Single Responsibility Principle, this is a very common scenario today.
    /// </summary>
    class MultipleMethodsExample
    {
        class Input { }

        readonly ICommandMonitor<Input> monitor1;
        readonly ICommandMonitor<Input> monitor2;

        public MultipleMethodsExample(IMonitor monitor) {
            monitor1 = monitor.Command<Input>(Method1);
            monitor2 = monitor.Command<Input>(Method2);
        }

        void Method1(Input input) => monitor1.Observe(input);
        void Method2(Input input) => monitor2.Observe(input);
    }
}
