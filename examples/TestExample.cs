using System;
using System.Reflection;
using NSubstitute;
using Xunit;

namespace Monitor
{
    /// <summary>
    /// Shows how an application developer can test a class instrumented with <see cref="Monitor"/>.
    /// </summary>
    public class TestExample
    {
        class SystemUnderTest
        {
            readonly IInstrument instrument;
            public SystemUnderTest(IMonitor monitor) => instrument = monitor.Instrument(Work);
            public void Work() => instrument.Record();
        }

        readonly SystemUnderTest sut;
        readonly IMonitor factory = Substitute.For<IMonitor>();
        readonly IInstrument instrument = Substitute.For<IInstrument>();

        public TestExample() {
            _ = factory.Instrument(Arg.Any<MethodBase>()).Returns(instrument);
            sut = new SystemUnderTest(factory);
        }

        [Fact]
        public void ConstructorCreatesInstrument() {
            _ = factory.Received().Instrument(((Action)sut.Work).Method);
            _ = factory.Received(1).Instrument(Arg.Any<MethodBase>());
        }

        [Fact]
        public void WorkRecordsExpectedMeasurement() {
            sut.Work();
            instrument.Received().Record();
        }
    }
}
