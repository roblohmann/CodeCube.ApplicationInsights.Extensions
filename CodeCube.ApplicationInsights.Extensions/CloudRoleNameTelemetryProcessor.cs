using OpenTelemetry;
using System;
using System.Diagnostics;

namespace CodeCube.ApplicationInsights.Extensions
{
    public sealed class CloudRoleNameTelemetryProcessor : BaseProcessor<Activity>
    {
        private readonly string _serviceName;

        public CloudRoleNameTelemetryProcessor(CloudRoleNameTelemetryOptions options)
        {
            if (options == null) throw new ArgumentNullException(nameof(options));

            _serviceName = options.ServiceName;
        }

        public override void OnEnd(Activity activity)
        {
            // 'service.name' is de OpenTelemetry standaard voor Cloud Role Name
            activity.SetTag("service.name", _serviceName);
        }
    }
}
