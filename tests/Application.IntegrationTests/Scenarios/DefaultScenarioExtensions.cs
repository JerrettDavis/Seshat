using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Scenario;
using Scenario.EFCore;
using Seshat.Application.Common.Interfaces;
using Seshat.Application.IntegrationTests.Mocks;
using Seshat.Infrastructure.Persistence;
using Seshat.WebUI;

namespace Seshat.Application.IntegrationTests.Scenarios
{
    public static class DefaultScenarioExtensions
    {
        public static TScenarioBuilder UseDefaults<TScenarioBuilder>(
            this TScenarioBuilder builder)
            where TScenarioBuilder : IScenarioBuilder =>
            builder.ConfigureStartup()
                .WithMigrations<TScenarioBuilder, ApplicationDbContext>()
                .MockCurrentUserService();
        
        public static TScenarioBuilder ConfigureStartup<TScenarioBuilder>(
            this TScenarioBuilder scenarioBuilder) 
            where TScenarioBuilder : IScenarioBuilder =>
            (TScenarioBuilder)scenarioBuilder.Use(services =>
            {
                var builder = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", true, true)
                    .AddEnvironmentVariables()
                    .AddUserSecrets<TestBase>();

                var configuration = builder.Build();
                var startup = new Startup(configuration);
                
                services.AddLogging();
                
                var mock = Mock.Of<IWebHostEnvironment>(w =>
                    w.EnvironmentName == "Development" &&
                    w.ApplicationName == "Seshat.WebUI");
                
                services.AddSingleton(mock);
                services.AddSingleton<IConfiguration>(configuration);

                startup.ConfigureServices(services);
            });

        public static TScenarioBuilder MockCurrentUserService<TScenarioBuilder>(
            this TScenarioBuilder builder) 
            where TScenarioBuilder : IScenarioBuilder =>
            (TScenarioBuilder)builder.Use(services =>
            {
                // Replace service registration for ICurrentUserService
                // Remove existing registration
                var currentUserServiceDescriptor = services
                    .FirstOrDefault(d => d.ServiceType == typeof(ICurrentUserService));

                services.Remove(currentUserServiceDescriptor!);

                // Register testing version
                services.AddSingleton<ICurrentUserService>(_ => new CurrentUserServiceMock());
            });
    }
}