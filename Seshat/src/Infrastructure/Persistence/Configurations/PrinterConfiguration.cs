using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Seshat.Domain.Entities;
using Seshat.Infrastructure.Persistence.Extensions;

namespace Seshat.Infrastructure.Persistence.Configurations
{
    public class PrinterConfiguration :
        IEntityTypeConfiguration<Printer>
    {
        public void Configure(EntityTypeBuilder<Printer> builder)
        {
            builder.HasKey("_id");

            builder.Property("_id")
                .HasColumnName("Id")
                .IsRequired()
                .ValueGeneratedOnAdd()
                .UseIdentityColumn();
            
            builder.Property(e => e.Model)
                .IsRequired();
            
            builder.AddPublicIdGenerator();
        }
    }
}