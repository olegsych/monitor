[![Build](https://img.shields.io/appveyor/ci/olegsych/monitor/master)](https://ci.appveyor.com/project/olegsych/monitor/branch/master)
[![Tests](https://img.shields.io/appveyor/tests/olegsych/monitor/master)](https://ci.appveyor.com/project/olegsych/monitor/branch/master/tests)
[![Nuget](https://img.shields.io/nuget/v/monitor.svg)](https://www.nuget.org/packages/monitor)

Monitor is an application-focused .NET instrumentation API.

## Don't we already have too many instrumentation APIs?

Yes! .NET already has a number of [instrumentation APIs](https://docs.microsoft.com/en-us/dotnet/core/diagnostics/).
Almost every major new version of the platform introduced new APIs, here are just a few examples.
- .NET 1: [Trace](https://docs.microsoft.com/en-us/dotnet/api/system.diagnostics.trace).
- .NET 2: [TraceSource](https://docs.microsoft.com/en-us/dotnet/api/system.diagnostics.tracesource)
- .NET 4: [EventSource](https://docs.microsoft.com/en-us/dotnet/api/system.diagnostics.tracing.eventsource)
- .NET Core 3: [DiagnosticSource](https://docs.microsoft.com/en-us/dotnet/api/system.diagnostics.diagnosticsource)
- .NET 5: [ActivitySource](https://docs.microsoft.com/en-us/dotnet/api/system.diagnostics.activitysource)
- .NET 6: [Metrics](https://docs.microsoft.com/en-us/dotnet/api/system.diagnostics.metrics)
As the evolution of the platform accelerated in recent years, any reasonably successful application or service had witnessed
emergence of at least one, often many new instrumentation APIs. And while we can hope that this area of .NET is now stable,
backward compatibility requirements could mean that yet another API will need to be introduced in the future, once a
shortcoming is discovered in the most recently released ones. Re-instrumenting a mature application with a new API is an
expensive and risky proposition, so many are still using the legacy APIs.

## What's wrong with the most recent APIs we have today?

Let's pretend for a minute that the historical trend of new APIs stops now. Why can't we treat `DiagnosticSource`,
`ActivitySource` and `Metrics` as stable dependencies, build new applications against them and plan to eventually migrate
the existing application from older APIs?

### They focus on how to instrument instead of what to instrument

The existing APIs focus on the low-level details of instrumentation and encourage leaking of concepts such as activity,
instrumentation source, listener, counter, etc into the application code. Even if these are stable dependencies
from the platform standpoint, the application's instrumentation needs continue changing throughout its lifetime. Application
code often starts to with logging to help developers unfamiliar with the problem space. As the application matures, and
especially the scale of a service grows, its logs need to become more selective and be complemented with aggregated metrics
and distributed traces.

Discovery of new instrumentation requirements by application developers is unavoidable and unpredictable. Developers simply
don't know what they don't know in the beginning. And because of the focus on the low-level instrumentation details, the
existing .NET APIs encourage addition of new instrumentation dependencies and further coupling of the instrumentation with
the business logic.

An instrumentation API focused on the application concepts, its entities, values and services, rather
than logs, metrics and traces, would encourage a better, more flexible and losely-coupled application design that allows
changing instrumentation logic without having to change and re-test its business logic.

### Difficult testability

The `ActivitySource` and `Metrics` APIs are sealed, making direct API-level testing with the dynamic test isolation tools
like [Moq](https://github.com/moq/moq4) and [NSubstitute](https://github.com/nsubstitute/NSubstitute) impossible and requiring
the test fixture to understand implementation details of the respective instrumentation API. When faced with this difficulty
most application developers would choose to a) keep the instrumentation logic mixed with the business logic and b) leave it
untested. Separating instrumentation logic into separate application services and testing against the sealed dependencies
require not just additional time, but a level of engineering maturity that many teams don't posess when they first start
building a new application.

The `DiagnosticSource` and `ILogger` APIs are abstract and can be easily used with the dynamic test isolation tools to test
the application code. The `ILogger` in particular has served as an inspiration for this design. However, these APIs are
focused primarily on logging and lack direct support for metrics and distributed traces.
