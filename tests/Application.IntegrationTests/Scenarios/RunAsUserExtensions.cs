using System;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Scenario;
using Seshat.Application.Common.Interfaces;
using Seshat.Application.IntegrationTests.Mocks;
using Seshat.Infrastructure.Identity;

namespace Seshat.Application.IntegrationTests.Scenarios
{
    public static class RunAsUserExtensions
    {
        public static TScenarioBuilder RunAs<TScenarioBuilder>(
            this TScenarioBuilder scenarioBuilder,
            string userName,
            string password,
            string[] roles,
            Action<string>? setUserIdCallback = null)
            where TScenarioBuilder : IScenarioBuilder
        {
            return (TScenarioBuilder) scenarioBuilder.With(async services =>
            {
                var userManager = services.ServiceProvider.GetService<UserManager<ApplicationUser>>()!;
                var user = new ApplicationUser {UserName = userName, Email = userName};
                var result = await userManager.CreateAsync(user, password);
                var currentUserService = (CurrentUserServiceMock) 
                    services.ServiceProvider
                        .GetService<ICurrentUserService>()!;

                if (roles.Any())
                {
                    var roleManager = services.ServiceProvider.GetService<RoleManager<IdentityRole>>()!;

                    foreach (var role in roles)
                    {
                        await roleManager.CreateAsync(new IdentityRole(role));
                    }

                    await userManager.AddToRolesAsync(user, roles);
                }

                if (result.Succeeded)
                {
                    currentUserService.OverrideUserId(user.Id);
                    return user.Id;
                }

                var errors = string.Join(Environment.NewLine, result.ToApplicationResult().Errors);
                
                throw new Exception($"Unable to create {userName}.{Environment.NewLine}{errors}");
            }, id => setUserIdCallback?.Invoke(id!.ToString()!));
        }

        public static TScenarioBuilder RunAsDefaultUser<TScenarioBuilder>(
            this TScenarioBuilder scenarioBuilder,
            Action<string>? setUserIdCallback = null)
            where TScenarioBuilder : IScenarioBuilder
        {
            return  scenarioBuilder
                .RunAs("test@local", "Testing1234!", Array.Empty<string>(), setUserIdCallback);
        }
        
        public static TScenarioBuilder RunAsAdministrator<TScenarioBuilder>(
            this TScenarioBuilder scenarioBuilder,
            Action<string>? setUserIdCallback = null)
            where TScenarioBuilder : IScenarioBuilder
        {
            return  scenarioBuilder
                .RunAs("administrator@local", "dministrator1234!", Array.Empty<string>(), setUserIdCallback);
        }
    }
}