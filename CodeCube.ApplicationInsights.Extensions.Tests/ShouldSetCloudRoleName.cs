using System;
using System.Diagnostics;
using Xunit;

namespace CodeCube.ApplicationInsights.Extensions.Tests
{
    public class ShouldSetCloudRoleName
    {
        [Fact]
        public void Processor_SetsServiceNameTag_OnActivity()
        {
            var options = new CloudRoleNameTelemetryOptions { ServiceName = "My_Test_Application" };
            var sut = new CloudRoleNameTelemetryProcessor(options);

            using var activity = CreateActivity();
            sut.OnEnd(activity);

            Assert.Equal("My_Test_Application", activity.GetTagItem("service.name"));
        }

        [Fact]
        public void Processor_NullOptions_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new CloudRoleNameTelemetryProcessor(null!));
        }

        [Fact]
        public void Processor_OverwritesExistingServiceNameTag()
        {
            var options = new CloudRoleNameTelemetryOptions { ServiceName = "NewName" };
            var sut = new CloudRoleNameTelemetryProcessor(options);

            using var activity = CreateActivity();
            activity.SetTag("service.name", "OldName");

            sut.OnEnd(activity);

            Assert.Equal("NewName", activity.GetTagItem("service.name"));
        }

        [Fact]
        public void AddCloudRoleNameTelemetryProcessor_NullOrWhitespace_ThrowsArgumentException()
        {
            var services = new Microsoft.Extensions.DependencyInjection.ServiceCollection();

            Assert.Throws<ArgumentException>(() => services.AddCloudRoleNameTelemetryProcessor(string.Empty));
            Assert.Throws<ArgumentException>(() => services.AddCloudRoleNameTelemetryProcessor("   "));
        }

        [Fact]
        public void AddCloudRoleNameTelemetryProcessor_NullAction_ThrowsArgumentNullException()
        {
            var services = new Microsoft.Extensions.DependencyInjection.ServiceCollection();

            Assert.Throws<ArgumentNullException>(() => services.AddCloudRoleNameTelemetryProcessor((Action<CloudRoleNameTelemetryOptions>)null!));
        }

        private static Activity CreateActivity()
        {
            var source = new ActivitySource("TestSource");
            var listener = new ActivityListener
            {
                ShouldListenTo = _ => true,
                Sample = (ref ActivityCreationOptions<ActivityContext> _) => ActivitySamplingResult.AllData
            };
            ActivitySource.AddActivityListener(listener);

            return source.StartActivity("TestActivity")!;
        }
    }
}
