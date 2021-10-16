using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Scenario;
using Seshat.Application.Common.Extensions;
using Seshat.Domain.Common;
using Seshat.Infrastructure.Persistence;

namespace Seshat.Application.IntegrationTests.Scenarios
{
    public static class ScenarioUtilityExtensions
    {
        public static async Task<TResponse> SendAsync<TResponse>(
            this IScenario scenario,
            IRequest<TResponse> request)
        {
            using var scope = scenario.CreateScope();
            var mediator = scope.ServiceProvider.GetService<ISender>()!;

            return await mediator.Send(request);
        }
        
        public static async Task<TEntity> FindAsync<TEntity>(
            this IScenario scenario,
            params object[] keyValues)
            where TEntity : class
        {
            using var scope = scenario.CreateScope();
            var context = scope.ServiceProvider.GetService<ApplicationDbContext>()!;

            return await context.FindAsync<TEntity>(keyValues);
        }
        
        public static async Task<TEntity> GetPublicEntity<TEntity>(
            this IScenario scenario,
            string id)
            where TEntity : class, IPublicEntity
        {
            using var scope = scenario.CreateScope();
            var context = scope.ServiceProvider.GetService<ApplicationDbContext>()!;
            
            return await context.Set<TEntity>().PublicEntitySingleAsync(id);
        }

        public static async Task AddAsync<TEntity>(
            this IScenario scenario,
            TEntity entity)
            where TEntity : class
        {
            using var scope = scenario.CreateScope();
            var context = scope.ServiceProvider.GetService<ApplicationDbContext>()!;

            context.Add(entity);

            await context.SaveChangesAsync();
        }

        public static async Task<int> CountAsync<TEntity>(
            this IScenario scenario) where TEntity : class
        {
            using var scope = scenario.CreateScope();
            var context = scope.ServiceProvider.GetService<ApplicationDbContext>()!;

            return await context.Set<TEntity>().CountAsync();
        }
    }
}