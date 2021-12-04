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
            readonly IInstrument instrument;
            public SUT(IMonitor monitor) => instrument = monitor.Instrument(Work);
            public void Work() => instrument.Measure();
        }

        readonly SUT sut;
        readonly IMonitor factory = Substitute.For<IMonitor>();
        readonly IInstrument instrument = Substitute.For<IInstrument>();

        public TestExample() {
            _ = factory.Instrument(Arg.Any<MethodBase>()).Returns(instrument);
            sut = new SUT(factory);
        }

        [Fact]
        public void ConstructorCreatesCommandMonitor() {
            _ = factory.Received().Instrument(((Action)sut.Work).Method);
            _ = factory.Received(1).Instrument(Arg.Any<MethodBase>());
        }

        [Fact]
        public void WorkObservesCommand() {
            sut.Work();
            instrument.Received().Measure();
        }
    }
}
