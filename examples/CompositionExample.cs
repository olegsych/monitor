using Microsoft.Extensions.DependencyInjection;

namespace Monitor
{
    class CompositionExample
    {
        void ConfigureServices(IServiceCollection services) =>
            services.AddMonitoring()
                .AddSingleton<ITelemetryDescriptor<Subject>, SubjectTelemetryDescriptor>()
                .AddTelemetryDescriptor<Subject, SubjectTelemetryDescriptor>();

        class Subject
        {
            public int Foo = default;
            public string? Bar = default;
            public Color Color = default;
        }

        enum Color
        {
            Red, Green, Blue
        }

        class SubjectTelemetryDescriptor: ITelemetryDescriptor<Subject>
        {
            void ITelemetryDescriptor<Subject>.Describe(ITelemetryDescription<Subject> description) {
                description.AddMeasurement(_ => _.Foo);
                description.AddProperty(_ => _.Bar);
                description.AddDimension(_ => _.Color);
            }
        }
    }
}
