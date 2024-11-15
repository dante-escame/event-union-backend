using EventUnion.Domain.Addresses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventUnion.Infrastructure.Mappings.Addresses;

public class PhoneMap : IEntityTypeConfiguration<Phone>
{
    public void Configure(EntityTypeBuilder<Phone> builder)
    {
        builder.ToTable("Phone");
        
        builder.HasKey(p => p.UserId);
        
        builder.HasOne(p => p.User)
            .WithOne()
            .HasForeignKey<Phone>(p => p.UserId);
        
        builder.Property(p => p.Value).IsRequired().HasMaxLength(15);
    }
}