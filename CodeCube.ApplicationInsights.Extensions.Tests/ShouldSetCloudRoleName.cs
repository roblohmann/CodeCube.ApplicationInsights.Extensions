using System;
using CodeCube.ApplicationInsights.Extensions.TelemetryInitializers;
using Microsoft.ApplicationInsights.DataContracts;
using Xunit;

namespace CodeCube.ApplicationInsights.Extensions.Tests
{
    public class ShouldSetCloudRoleName
    {
        [Fact]
        public void Succeeds()
        {
            //Setup
            var cloudRoleName = "My_Test_Application";

            var options = new CloudRoleNameTelemetryOptions {RoleName = cloudRoleName};

            var sut = new CloudRoleNameTelemetryInitializer(options);
            var telemetry = new RequestTelemetry();

            //Act
            sut.Initialize(telemetry);

            //Assert
            Assert.Equal(cloudRoleName, telemetry.Context.Cloud.RoleName);
        }
    }
}