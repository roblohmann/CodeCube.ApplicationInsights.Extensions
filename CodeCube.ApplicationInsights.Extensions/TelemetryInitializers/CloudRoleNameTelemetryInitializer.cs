using System;
using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.Extensibility;

namespace CodeCube.ApplicationInsights.Extensions.TelemetryInitializers
{
    public sealed class CloudRoleNameTelemetryInitializer : ITelemetryInitializer
    {
        private readonly string _cloudRoleName;

        public CloudRoleNameTelemetryInitializer(CloudRoleNameTelemetryOptions options)
        {
            if (options == null) throw new ArgumentNullException(nameof(options));

            _cloudRoleName = options.RoleName;
        }

        public void Initialize(ITelemetry telemetry)
        {
            telemetry.Context.Cloud.RoleName = _cloudRoleName;
        }
    }
}
