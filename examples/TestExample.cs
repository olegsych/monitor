using System;
using System.Reflection;
using NSubstitute;
using Xunit;

namespace Athene.Monitor
{
    /// <summary>
    /// Shows how an application developer can test a class instrumented with <see cref="Monitor"/>.
    /// </summary>
    public class TestExample
    {
        class ApplicationCode
        {
            readonly IInstrument instrument;
            public ApplicationCode(IMonitor monitor) => instrument = monitor.Instrument(Work);
            public void Work() => instrument.Record();
        }

        readonly ApplicationCode sut;
        readonly IMonitor monitor = Substitute.For<IMonitor>();
        readonly IInstrument instrument = Substitute.For<IInstrument>();

        public TestExample() {
            _ = monitor.Instrument(Arg.Any<MethodBase>()).Returns(instrument);
            sut = new ApplicationCode(monitor);
        }

        [Fact(Skip = "Not implemented yet")]
        public void ConstructorCreatesInstrument() {
            _ = monitor.Received().Instrument(((Action)sut.Work).Method);
            _ = monitor.Received(1).Instrument(Arg.Any<MethodBase>());
        }

        [Fact(Skip = "Not implemented yet")]
        public void WorkRecordsExpectedObservation() {
            sut.Work();
            instrument.Received().Record();
        }
    }
}
