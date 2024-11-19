using EventUnion.Domain.Events;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventUnion.Infrastructure.Mappings.Users;

public class TagMap : IEntityTypeConfiguration<Tag>
{
    public void Configure(EntityTypeBuilder<Tag> builder)
    {
        builder.ToTable("tag");
        
        builder.HasKey(i => i.TagId);
        
        builder.Property(tag => tag.Name)
            .HasColumnName("name")
            .HasMaxLength(Tag.NameMaxLength)
            .IsRequired();

        builder.HasData(Tag.All);
    }
}