using System;
using Athene.Monitor;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddMonitor(this IServiceCollection services) =>
            throw new NotImplementedException();

        public static IServiceCollection AddTelemetryDescriptor<TSubject, TDescriptor>(this IServiceCollection services)
            where TSubject: class
            where TDescriptor : class, ITelemetryDescriptor<TSubject> =>
            services.AddSingleton<ITelemetryDescriptor<TSubject>, TDescriptor>();
    }
}
