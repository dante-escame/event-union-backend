using EventUnion.Domain.Events;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventUnion.Infrastructure.Mappings.Events;

public class EventTypeMap : IEntityTypeConfiguration<EventType>
{
    public void Configure(EntityTypeBuilder<EventType> builder)
    {
        builder.ToTable("EventType");

        builder.HasKey(et => et.EventTypeId);

        builder.Property(et => et.EventTypeId)
            .IsRequired();

        builder.Property(et => et.Name)
            .IsRequired();

        builder.HasData(EventType.All);
    }
}