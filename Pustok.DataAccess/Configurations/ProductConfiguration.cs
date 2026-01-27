using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pustok.Core.Entities;

namespace Pustok.DataAccess.Configurations;

internal class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(p => p.Description)
            .IsRequired()
            .HasMaxLength(1024);

        builder.Property(p => p.ImagePath)
            .IsRequired()
            .HasMaxLength(1024);

        builder.Property(p => p.Price)
            .IsRequired().HasPrecision(10, 2);
    }
}
