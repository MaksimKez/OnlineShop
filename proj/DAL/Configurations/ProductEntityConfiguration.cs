using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace DAL.Configurations;

public class ProductEntityConfiguration : IEntityTypeConfiguration<ProductEntity>
{
    public void Configure(EntityTypeBuilder<ProductEntity> builder)
    {
        builder.HasKey(p => p.Id);
        builder.Property(p => p.ProductType)
            .HasConversion<int>()
            .IsRequired();
        builder.Property(p => p.PricePerUnit)
            .IsRequired();
        builder.Property(p => p.PhotoUrl);
        builder.Property(p => p.Description);
        builder.Property(p => p.StockQuantity);
    }
}