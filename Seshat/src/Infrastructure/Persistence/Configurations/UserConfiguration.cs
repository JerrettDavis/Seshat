using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Seshat.Domain.Entities;
using Seshat.Infrastructure.Persistence.Extensions;

namespace Seshat.Infrastructure.Persistence.Configurations
{
    public class UserConfiguration :
        IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey("_id");
            
            builder.Property("_id")
                .HasColumnName("Id")
                .IsRequired();
            
            builder.AddPublicIdGenerator();
        }
    }
}