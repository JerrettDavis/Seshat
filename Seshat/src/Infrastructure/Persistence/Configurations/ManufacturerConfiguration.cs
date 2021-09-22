using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Seshat.Domain.Entities;
using Seshat.Infrastructure.Persistence.Extensions;

namespace Seshat.Infrastructure.Persistence.Configurations
{
    public class ManufacturerConfiguration :
        IEntityTypeConfiguration<Manufacturer>
    {
        public void Configure(EntityTypeBuilder<Manufacturer> builder)
        {
            builder.HasKey("_id");

            builder.Property("_id")
                .HasColumnName("Id")
                .IsRequired()
                .ValueGeneratedOnAdd()
                .UseIdentityColumn();
            
            builder.Property(e => e.Name)
                .IsRequired();
            
            builder.AddPublicIdGenerator();
        }
    }
}