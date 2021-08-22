using System;
using System.Reflection;
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
            public void Work() => monitor.Finish();
        }

        readonly SUT sut;
        readonly IMonitor factory = Substitute.For<IMonitor>();
        readonly ICommandMonitor monitor = Substitute.For<ICommandMonitor>();

        public TestExample() {
            _ = factory.Command(Arg.Any<MethodBase>()).Returns(monitor);
            sut = new SUT(factory);
        }

        [Fact]
        public void ConstructorCreatesCommandMonitor() {
            _ = factory.Received().Command(((Action)sut.Work).Method);
            _ = factory.Received(1).Command(Arg.Any<MethodBase>());
        }

        [Fact]
        public void WorkObservesCommand() {
            sut.Work();
            monitor.Received().Record(default(Observation), default(Exception));
        }
    }
}
