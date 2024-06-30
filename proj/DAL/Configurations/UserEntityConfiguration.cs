using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Configurations;

public class UserEntityConfiguration : IEntityTypeConfiguration<UserEntity>
{
    public void Configure(EntityTypeBuilder<UserEntity> builder)
    {
        builder.Property(us => us.Username)
            .IsRequired();
        builder.Property(us => us.FirstName)
            .HasMaxLength(20);
        builder.Property(us => us.SecondName)
            .HasMaxLength(20);
        builder.Property(us => us.Email);
        builder.Property(us => us.PhoneNumber);
        builder.Property(us => us.DateOfBirth)
            .IsRequired();

        builder.HasOne(us => us.Cart)
            .WithOne()
            .HasForeignKey<UserEntity>(us => us.CartId);

        builder.HasMany(us => us.Orders)
            .WithOne(od => od.User)
            .HasForeignKey(od => od.UserId);

    }
}