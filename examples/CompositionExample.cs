using Microsoft.Extensions.DependencyInjection;

namespace Monitor
{
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
                description.AddMeasurement(_ => _.Foo);
                description.AddProperty(_ => _.Bar);
                description.AddDimension(_ => _.Color);
            }
        }
    }
}
