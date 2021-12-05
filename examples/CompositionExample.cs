using Microsoft.Extensions.DependencyInjection;

namespace Monitor
{
    /// <summary>
    /// Shows how an application developer can register <see cref="Monitor"/>
    /// and their own monitoring services for dependency injection.
    /// </summary>
    class CompositionExample
    {
        void ConfigureServices(IServiceCollection services) => services
            .AddMonitor()
            .AddTelemetryDescriptor<Subject, SubjectTelemetryDescriptor>();

        class Subject
        {
            public int Foo;
            public string? Bar;
            public Color Color;
        }

        enum Color
        {
            Red, Green, Blue
        }

        class SubjectTelemetryDescriptor: ITelemetryDescriptor<Subject>
        {
            public void Describe(ITelemetryDescription<Subject> description) {
                description.AddMetric(_ => _.Foo);
                description.AddProperty(_ => _.Bar);
                description.AddDimension(_ => _.Color);
            }
        }
    }
}
