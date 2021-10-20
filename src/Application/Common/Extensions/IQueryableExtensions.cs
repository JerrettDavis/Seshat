using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Seshat.Domain.Common;

namespace Seshat.Application.Common.Extensions
{
    // ReSharper disable once UnusedType.Global
    public static class IQueryableExtensions
    {
        public static Task<bool> PublicEntityExistsAsync<TEntity>(
            this IQueryable<TEntity> queryable,
            string publicId, 
            CancellationToken cancellationToken = default) 
            where TEntity : class, IPublicEntity =>
            queryable.AnyAsync(e => e.PublicIdentifier == publicId, 
                cancellationToken);

        public static Task<TEntity> PublicEntitySingleAsync<TEntity>(
            this IQueryable<TEntity> queryable,
            string publicId,
            CancellationToken cancellationToken = default)
            where TEntity : class, IPublicEntity =>
            queryable.SingleAsync(e => e.PublicIdentifier == publicId,
                cancellationToken);
    }
}