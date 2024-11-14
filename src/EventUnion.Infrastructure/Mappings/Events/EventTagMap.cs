using EventUnion.Domain.Events;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventUnion.Infrastructure.Mappings.Events;

public class EventTagMap : IEntityTypeConfiguration<EventTag>
{
    public void Configure(EntityTypeBuilder<EventTag> builder)
    {
        builder.ToTable("EventTag");
        
        builder.HasKey(et => et.EventTagId);

        builder.Property(et => et.EventTagId)
            .ValueGeneratedOnAdd()
            .IsRequired();
        
        builder.HasOne(et => et.Event)
            .WithMany()
            .IsRequired();
        
        builder.HasOne(et => et.Tag)
            .WithMany()
            .IsRequired();
    }
}