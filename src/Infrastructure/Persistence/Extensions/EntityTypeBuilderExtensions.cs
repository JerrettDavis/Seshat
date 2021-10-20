using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Seshat.Domain.Common;
using Seshat.Infrastructure.Persistence.Generators;

namespace Seshat.Infrastructure.Persistence.Extensions
{
    public static class EntityTypeBuilderExtensions
    {
        public static void AddPublicIdGenerator<TEntity>(
            this EntityTypeBuilder<TEntity> builder) 
            where TEntity : class, IPublicEntity
        {
            builder.Property(e => e.PublicIdentifier)
                .HasValueGenerator<PublicIdValueGenerator>();
        }
    }
}