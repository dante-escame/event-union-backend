using EventUnion.Domain.Addresses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventUnion.Infrastructure.Mappings.Addresses;

public class PhoneMap : IEntityTypeConfiguration<Phone>
{
    public void Configure(EntityTypeBuilder<Phone> builder)
    {
        builder.ToTable("phone");
        
        builder.HasKey(p => p.PhoneId);
        
        builder.Property(p => p.UserId).IsRequired();
        
        builder.HasOne(p => p.User)
            .WithOne()
            .HasForeignKey<Phone>(p => p.UserId);
        
        builder.Property(p => p.Value).IsRequired().HasMaxLength(15);
    }
}