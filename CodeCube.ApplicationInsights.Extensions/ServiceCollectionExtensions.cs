using System;
using CodeCube.ApplicationInsights.Extensions.TelemetryInitializers;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.Extensions.DependencyInjection;

namespace CodeCube.ApplicationInsights.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCloudRoleNameTelemetryInitializer(this IServiceCollection serviceCollection, string cloudRoleName)
        {
            if (string.IsNullOrWhiteSpace(cloudRoleName)) throw new ArgumentNullException(nameof(cloudRoleName));

            return serviceCollection.AddSingleton<ITelemetryInitializer>(_ => new CloudRoleNameTelemetryInitializer(new CloudRoleNameTelemetryOptions { RoleName = cloudRoleName }));
        }

        public static IServiceCollection AddCloudRoleNameTelemetryInitializer(this IServiceCollection serviceCollection, Action<CloudRoleNameTelemetryOptions> options)
        {
            if (options == null) throw new ArgumentNullException(nameof(options));

            var cloudRoleNameTelemetryOptions = new CloudRoleNameTelemetryOptions();
            options?.Invoke(cloudRoleNameTelemetryOptions);

            return serviceCollection.AddSingleton<ITelemetryInitializer>(_ => new CloudRoleNameTelemetryInitializer(cloudRoleNameTelemetryOptions));
        }
    }
}
