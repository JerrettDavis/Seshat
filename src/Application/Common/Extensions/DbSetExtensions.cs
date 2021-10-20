using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Seshat.Domain.Common;

namespace Seshat.Application.Common.Extensions
{
    public static class DbSetExtensions
    {
        public static Task<bool> PublicEntityExistsAsync<TEntity>(
            this DbSet<TEntity> dbSet,
            string publicId, 
            CancellationToken cancellationToken = default) 
            where TEntity : class, IPublicEntity =>
            dbSet.AnyAsync(e => e.PublicIdentifier == publicId, 
                cancellationToken);

        public static Task<TEntity> PublicEntitySingleAsync<TEntity>(
            this DbSet<TEntity> dbSet,
            string publicId,
            CancellationToken cancellationToken = default)
            where TEntity : class, IPublicEntity =>
            dbSet.SingleAsync(e => e.PublicIdentifier == publicId,
                cancellationToken);
    }
}