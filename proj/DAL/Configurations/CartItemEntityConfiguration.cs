using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Configurations;

public class CartItemEntityConfiguration : IEntityTypeConfiguration<CartItemEntity>
{
    public void Configure(EntityTypeBuilder<CartItemEntity> builder)
    {
        builder.HasKey(ci => ci.Id);
        builder.HasOne(ci => ci.Product)
            .WithOne()
            .HasForeignKey<CartItemEntity>(ci => ci.ProductId);
        builder.Property(ci => ci.Quantity);
    }
}