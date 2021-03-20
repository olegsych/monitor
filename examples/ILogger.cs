using System;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Xunit;

namespace Monitor
{
    public class ILogger
    {
        public class Subject { }

        readonly Subject subject = new Subject();
        readonly IMonitor<Subject> monitor = Substitute.For<IMonitor<Subject>>();
        readonly ILogger<Subject> logger = Substitute.For<ILogger<Subject>>();

        readonly Exception? noException = null;

        public class Observe: ILogger
        {
            [Fact]
            public void InvokesLogMethodWithGivenSubject() {
                // Example
                monitor.Observe(subject);

                // What happens
                logger.Received().Log(LogLevel.Information, Arg.Any<EventId>(), subject, noException, Arg.Any<Func<Subject, Exception, string>>());
            }

            [Fact]
            public void InvokesMethodWithGivenSubjectAndException() {
                // Example
                var exception = new Exception();
                try {
                    throw exception;
                }
                catch (Exception e) {
                    monitor.Observe(subject, e);
                }

                // What happens
                logger.Received().Log(LogLevel.Error, Arg.Any<EventId>(), subject, exception, Arg.Any<Func<Subject, Exception, string>>());
            }
        }

        public class Start: ILogger
        {
            [Fact]
            public void InvokesBeginScopeMethod() {
                // Example
                using IObservation observation = monitor.Start(subject);
                // Successful observation is finished by Dispose()

                // What happens
                object assert = logger.Received().BeginScope(subject);
                logger.Received().Log(LogLevel.Information, Arg.Any<EventId>(), subject, noException, Arg.Any<Func<Subject, Exception, string>>());
            }

            [Fact]
            public void SupportsExceptions() {
                // Example
                var exception = new Exception();
                using IObservation observation = monitor.Start(subject);
                try {
                    throw exception;
                }
                catch (Exception e) {
                    observation.Finish(e);
                }

                // What happens
                logger.Received().Log(LogLevel.Error, Arg.Any<EventId>(), subject, noException, Arg.Any<Func<Subject, Exception, string>>());
            }
        }
    }
}
