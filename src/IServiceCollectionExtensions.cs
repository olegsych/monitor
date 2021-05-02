using System;
using Microsoft.Extensions.DependencyInjection;

namespace Monitor
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddMonitoring(this IServiceCollection services) =>
            throw new NotImplementedException();

        public static IServiceCollection AddTelemetryDescriptor<TSubject, TDescriptor>(this IServiceCollection services)
            where TSubject: class
            where TDescriptor : class, ITelemetryDescriptor<TSubject> =>
            services.AddSingleton<ITelemetryDescriptor<TSubject>, TDescriptor>();
    }
}
