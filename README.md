[![Build](https://img.shields.io/appveyor/ci/olegsych/monitor/master)](https://ci.appveyor.com/project/olegsych/monitor/branch/master)
[![Tests](https://img.shields.io/appveyor/tests/olegsych/monitor/master)](https://ci.appveyor.com/project/olegsych/monitor/branch/master/tests)
[![Nuget](https://img.shields.io/nuget/v/monitor.svg)](https://www.nuget.org/packages/monitor)

Monitor will provide an application-focused .NET instrumentation API.

## Don't we already have too many instrumentation APIs?

Yes! .NET already has a [number](https://docs.microsoft.com/en-us/dotnet/core/diagnostics/) of instrumentation APIs.
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

The `DiagnosticSource` and `ILogger` APIs are abstract and can be used with the dynamic test isolation tools to test
the application code. The `ILogger` in particular has served as an inspiration for this design. However, these APIs still
have low-level instrumentation details, they are focused primarily on logging and lack direct support for metrics and
distributed traces.

## How would Monitor be any better?

### API focused on the application

`Monitor` provides an API focused on the application types and methods. The `IMonitor` interface will be injected in the
application code, typically by the dependency injection container. It allows the application code to create an `IInstrument`
for instrumenting a particular method. The example below shows how an observation of method duration, its input and possible
can be recorded.

```C#
enum ApplicationEnum
{
    Item1,
    Item2,
    Item3,
}

class ApplicationInput
{
    public string PropertyFoo { get; set; }
    public int MetricBar { get; set; }
    public ApplicationEnum DimensionBaz { get; set; }
}

class ApplicationComponent
{
    readonly IInstrument<ApplicationInput> instrument;

    ApplicationComponent(IMonitor monitor) =>
        instrument = monitor.Instrument<ApplicationInput>(Work);

    void Work(ApplicationInput input) {
        Observation observation = instrument.Start();
        try {
            // Business logic
            instrument.Record(observation, input);
        }
        catch(Exception e) {
            instrument.Record(observation, e, input);
            throw;
        }
    }
}
```

### Inferred instrumentation defaults

Using .NET type information available through this API, `Monitor` can already generate a lot of useful telemetry. For example:
- Emit logs with application type, method, duration, input and exception details.
    - Properties of the input object, such as `PropertyFoo`, can be logged as structured data for optimized query access.
- Emit metrics
    - `ApplicationComponent.Work.Duration` tracking duration of the `Work` method call.
    - `ApplicationComponent.Work.Count` can be tracked instead of `ApplicationComponent.Work.Duration` if duration of the
      method is not measured.
    - `ApplicationInput.MetricBar` based on the numeric property of the input object.
- Emit metric dimensions using low-cardinality values appropriate for metric aggregation:
    - `ExceptionType` dimension based on the recorded exception types.
    - `DimensionBaz` dimension based on the `ApplicationEnum` values.

### Customizing telemetry with attributes

.NET type information is sufficient to emit logs in most cases. However, metrics and dimensions may need additional metadata.

```C#
class ApplicationInput
{
    public int MetricBar { get; set; }
    [TelemetryDimension] public string StringProperty { get; set; }
    [TelemetryIgnore] public int IntProperty { get; set; }
}
```

Metric aggregation requires memory to store timeseries (aggregated measurements for each unique combination of metric name
and dimension values). Only low cardinality values, such as enums, can be automatically identified as dimensions. String
values, for example, are assumed to have high cardinality and aren't automatically chosen to serve as dimensions. Application
code can use attributes, like `TelemetryDimension` designate a property as a dimension.

When a particular property should not be automatically tracked by Monitor as a metric, metric dimension or a log property,
application code can use the `TelemetryIgnore` attribute.

### Customizing telemetry with code

When telemetry needs to be extracted from types defined outside of application code, where telemetry attributes cannot be used,
an `ITelemetryDescriptor` can be implemented and registered with Monitor instead. Telemetry descriptors are also automatically
generated by Monitor when it infers properties, metrics and dimensions from the .NET type information and attributes.

```C#

class ApplicationInputDescriptor: ITelemetryDescriptor<ApplicationInput>
{
    public void Describe(ITelemetryDescription<ApplicationInput> description) {
        // For aggregated metrics
        description.AddMetric(_ => _.MetricBar);
        description.AddDimension(_ => _.StringProperty);

        // For Logs
        description.AddProperty(_ => _.StringProperty);
        description.AddProperty(_ => _.MetricBar);
    }
}
```

### Vendor-neutral and high-performace

The telemetry metadata available to Monitor is used to generate `IInstrument` implementations directly invoking
vendor-specific APIs for optimal performance.
