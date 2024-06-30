using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Configurations;

public class OrderConfiguration : IEntityTypeConfiguration<OrderEntity>
{
    public void Configure(EntityTypeBuilder<OrderEntity> builder)
    {
        builder.HasKey(od => od.Id);
        builder.Property(od => od.DeliveryDateTime);
        builder.Property(od => od.PaymentDateTime);
        builder.HasKey(od => od.UserId);
    }
}