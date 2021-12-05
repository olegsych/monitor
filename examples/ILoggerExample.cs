using System;
using System.Linq.Expressions;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Xunit;

namespace Monitor
{
    public class ILoggerExample
    {
        public class Subject { }

        readonly Subject subject = new Subject();
        readonly IInstrument<Subject> instrument = Substitute.For<IInstrument<Subject>>();
        readonly ILogger logger = Substitute.For<ILogger>();

        readonly Exception? noException = null;
        readonly Expression<Predicate<Func<Subject, Exception, string>>> expectedFormatter = _ => true;

        public class Observe: ILoggerExample
        {
            [Fact]
            public void InvokesLogMethodWithGivenSubject() {
                // Example
                instrument.Record(subject);

                // What happens
                logger.Received().Log(LogLevel.Information, Arg.Any<EventId>(), subject, noException, Arg.Is(expectedFormatter));
            }

            [Fact]
            public void InvokesMethodWithGivenSubjectAndException() {
                // Example
                var exception = new Exception();
                try {
                    throw exception;
                }
                catch (Exception e) {
                    instrument.Record(e, subject);
                }

                // What happens
                logger.Received().Log(LogLevel.Error, Arg.Any<EventId>(), subject, exception, Arg.Is(expectedFormatter));
            }
        }

        public class Start: ILoggerExample
        {
            [Fact]
            public void InvokesBeginScopeMethod() {
                // Example
                Measurement measurement = instrument.Start();
                object assert = logger.Received().BeginScope(measurement);

                instrument.Record(measurement, subject);
                logger.Received().Log(LogLevel.Information, Arg.Any<EventId>(), subject, noException, Arg.Is(expectedFormatter));
            }

            [Fact]
            public void SupportsExceptions() {
                // Example
                var exception = new Exception();
                Measurement measurement = instrument.Start();
                try {
                    throw exception;
                }
                catch (Exception e) {
                    instrument.Record(measurement, e);
                }

                // What happens
                logger.Received().Log(LogLevel.Error, Arg.Any<EventId>(), subject, noException, Arg.Is(expectedFormatter));
            }
        }
    }
}
