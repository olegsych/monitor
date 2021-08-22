using System;
using System.Linq.Expressions;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Xunit;

namespace Monitor
{
    public class ILogger
    {
        public class Subject { }

        readonly Subject subject = new Subject();
        readonly ICommandMonitor<Subject> monitor = Substitute.For<ICommandMonitor<Subject>>();
        readonly ILogger<Subject> logger = Substitute.For<ILogger<Subject>>();

        readonly Exception? noException = null;
        readonly Expression<Predicate<Func<Subject, Exception, string>>> expectedFormatter = _ => true;

        public class Observe: ILogger
        {
            [Fact]
            public void InvokesLogMethodWithGivenSubject() {
                // Example
                monitor.Finish(subject);

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
                    monitor.Finish(subject, e);
                }

                // What happens
                logger.Received().Log(LogLevel.Error, Arg.Any<EventId>(), subject, exception, Arg.Is(expectedFormatter));
            }
        }

        public class Start: ILogger
        {
            [Fact]
            public void InvokesBeginScopeMethod() {
                // Example
                Observation<Subject> observation = monitor.Start(subject);
                object assert = logger.Received().BeginScope(subject);

                monitor.Finish(observation);
                logger.Received().Log(LogLevel.Information, Arg.Any<EventId>(), subject, noException, Arg.Is(expectedFormatter));
            }

            [Fact]
            public void SupportsExceptions() {
                // Example
                var exception = new Exception();
                Observation<Subject> observation = monitor.Start(subject);
                try {
                    throw exception;
                }
                catch (Exception e) {
                    monitor.Finish(observation, e);
                }

                // What happens
                logger.Received().Log(LogLevel.Error, Arg.Any<EventId>(), subject, noException, Arg.Is(expectedFormatter));
            }
        }
    }
}
