using EventUnion.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventUnion.Infrastructure.Mappings.Users;

public class UserTagMap : IEntityTypeConfiguration<UserTag>
{
    public void Configure(EntityTypeBuilder<UserTag> builder)
    {
        builder.ToTable("user_tag");
        
        builder.HasKey(ut => ut.UserTagId);

        builder.Property(ut => ut.UserTagId)
            .ValueGeneratedOnAdd()
            .IsRequired();
        
        builder.HasOne(ut => ut.User)
            .WithMany()
            .IsRequired();
        
        builder.HasOne(ut => ut.Tag)
            .WithMany()
            .IsRequired();
    }
}