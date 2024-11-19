using EventUnion.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventUnion.Infrastructure.Mappings.Users;

public class UserAddressMap : IEntityTypeConfiguration<UserAddress>
{
    public void Configure(EntityTypeBuilder<UserAddress> builder)
    {
        builder.ToTable("user_address");
        
        builder.HasKey(ua => ua.UserAddressId);

        builder.Property(ua => ua.UserAddressId)
            .ValueGeneratedOnAdd()
            .IsRequired();
        
        builder.HasOne(ua => ua.User)
            .WithOne()
            .HasForeignKey<UserAddress>()
            .IsRequired();
        
        builder.HasOne(ua => ua.Address)
            .WithOne()
            .HasForeignKey<UserAddress>()
            .IsRequired();
    }
}