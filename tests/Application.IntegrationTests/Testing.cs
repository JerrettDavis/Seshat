using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Respawn;
using Scenario;
using Seshat.Application.Common.Interfaces;
using Seshat.Application.IntegrationTests.Mocks;
using Seshat.Application.IntegrationTests.Scenarios;

namespace Seshat.Application.IntegrationTests
{
    [SetUpFixture]
    public class Testing
    {
        private static IScenarioBuilder _scenarioBuilder = null!;

        [OneTimeSetUp]
        public void RunBeforeAnyTests()
        {
            _scenarioBuilder = new ScenarioBuilder()
                .UseDefaults()
                .Use(services =>
                {
                    services.AddSingleton(new Checkpoint
                    {
                        TablesToIgnore = new[] { "__EFMigrationsHistory" }
                    });
                });
        }

        public static IScenarioBuilder GetScenarioBuilder()
        {
            return _scenarioBuilder;
        }

        public static void ResetState()
        {
            _scenarioBuilder
                .With(async services =>
                {
                    var configuration = services.ServiceProvider.GetService<IConfiguration>();
                    await services.ServiceProvider.GetService<Checkpoint>()!
                        .Reset(configuration.GetConnectionString("DefaultConnection"));

                    var userService = services.ServiceProvider
                        .GetService<ICurrentUserService>() as CurrentUserServiceMock;
                    userService!.OverrideUserId(null);

                    return null!;
                });
        }

        [OneTimeTearDown]
        public void RunAfterAnyTests()
        {
        }
    }
}