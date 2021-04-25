using NSubstitute;
using Xunit;

namespace Monitor
{
    public class TestExample
    {
        class SUT
        {
            readonly ICommandMonitor monitor;
            public SUT(IMonitor monitor) => this.monitor = monitor.Command(Work);
            public void Work() => monitor.Observe();
        }

        readonly SUT sut;
        readonly IMonitor monitor = Substitute.For<IMonitor>();
        readonly ICommandMonitor command = Substitute.For<ICommandMonitor>();

        public TestExample() {
            _ = monitor.Command(Arg.Any<Command>()).Returns(command);
            sut = new SUT(monitor);
        }

        [Fact]
        public void ConstructorCreatesCommandMonitor() {
            _ = monitor.Received().Command(sut.Work);
            _ = monitor.Received(1).Command(Arg.Any<Command>());
        }

        [Fact]
        public void WorkObservesCommand() {
            sut.Work();
            command.Received().Observe();
        }
    }
}
