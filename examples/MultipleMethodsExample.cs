namespace Monitor
{
    /// <summary>
    /// An example of monitoring multiple methods in the same class. While needing separate monitoring for different methods is a smell of breaking the
    /// Single Responsibility Principle, this is a very common scenario today.
    /// </summary>
    class MultipleMethodsExample
    {
        class Input { }

        readonly IMonitor<Input> monitor1;
        readonly IMonitor<Input> monitor2;

        public MultipleMethodsExample(IMonitorFactory monitors) {
            monitor1 = monitors.Create<Input>(Method1);
            monitor2 = monitors.Create<Input>(Method2);
        }

        void Method1(Input input) => monitor1.Observe(input);
        void Method2(Input input) => monitor2.Observe(input);
    }
}
