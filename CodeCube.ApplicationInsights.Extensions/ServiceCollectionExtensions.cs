using Microsoft.Extensions.DependencyInjection;
using OpenTelemetry.Trace;
using System;

namespace CodeCube.ApplicationInsights.Extensions
{
    public static class ServiceCollectionExtensions
    {
        [Obsolete("Use AddCloudRoleNameTelemetryProcessor instead.")]
        public static IServiceCollection AddCloudRoleNameTelemetryInitializer(this IServiceCollection serviceCollection, string cloudRoleName)
            => AddCloudRoleNameTelemetryProcessor(serviceCollection, cloudRoleName);

        [Obsolete("Use AddCloudRoleNameTelemetryProcessor(Action<CloudRoleNameTelemetryOptions>) instead.")]
        public static IServiceCollection AddCloudRoleNameTelemetryInitializer(this IServiceCollection serviceCollection, Action<CloudRoleNameTelemetryOptions> options)
            => AddCloudRoleNameTelemetryProcessor(serviceCollection, options);

        public static IServiceCollection AddCloudRoleNameTelemetryProcessor(this IServiceCollection serviceCollection, string cloudRoleName)
        {
            if (string.IsNullOrWhiteSpace(cloudRoleName)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(cloudRoleName));

            return RegisterProcessor(serviceCollection, new CloudRoleNameTelemetryOptions { ServiceName = cloudRoleName });
        }

        public static IServiceCollection AddCloudRoleNameTelemetryProcessor(this IServiceCollection serviceCollection, Action<CloudRoleNameTelemetryOptions> configure)
        {
            ArgumentNullException.ThrowIfNull(configure);

            var options = new CloudRoleNameTelemetryOptions { ServiceName = string.Empty };
            configure(options);

            return RegisterProcessor(serviceCollection, options);
        }

        private static IServiceCollection RegisterProcessor(IServiceCollection serviceCollection, CloudRoleNameTelemetryOptions options)
            => serviceCollection.ConfigureOpenTelemetryTracerProvider((_, tracerBuilder) =>
                tracerBuilder.AddProcessor(new CloudRoleNameTelemetryProcessor(options)));
    }
}
