using System;
using System.Linq.Expressions;
using Chronology;
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
                instrument.Measure(subject);

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
                    instrument.Measure(e, subject);
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
                HighResolutionTimestamp start = instrument.Start();
                object assert = logger.Received().BeginScope(start);

                instrument.Measure(start, subject);
                logger.Received().Log(LogLevel.Information, Arg.Any<EventId>(), subject, noException, Arg.Is(expectedFormatter));
            }

            [Fact]
            public void SupportsExceptions() {
                // Example
                var exception = new Exception();
                HighResolutionTimestamp start = instrument.Start();
                try {
                    throw exception;
                }
                catch (Exception e) {
                    instrument.Measure(start, e);
                }

                // What happens
                logger.Received().Log(LogLevel.Error, Arg.Any<EventId>(), subject, noException, Arg.Is(expectedFormatter));
            }
        }
    }
}
