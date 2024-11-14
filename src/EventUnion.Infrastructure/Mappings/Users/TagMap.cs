using EventUnion.Domain.Events;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventUnion.Infrastructure.Mappings.Users;

public class TagMap : IEntityTypeConfiguration<Tag>
{
    public void Configure(EntityTypeBuilder<Tag> builder)
    {
        builder.ToTable("Tag");
        
        builder.HasKey(i => i.TagId);
        
        builder.Property(name => name)
            .HasColumnName("name")
            .HasMaxLength(Tag.NameMaxLength)
            .IsRequired();

        builder.HasData(Tag.All);
    }
}