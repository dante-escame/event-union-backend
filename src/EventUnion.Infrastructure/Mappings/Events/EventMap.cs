using EventUnion.Domain.Events;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventUnion.Infrastructure.Mappings.Events;

public class EventMap : IEntityTypeConfiguration<Event>
{
    public void Configure(EntityTypeBuilder<Event> builder)
    {
        builder.ToTable("Event");

        builder.HasKey(e => e.EventId);

        builder.Property(e => e.EventId)
            .IsRequired();

        builder.OwnsOne(c => c.Name, x =>
        {
            x.Property(name => name.Value)
                .HasColumnName("name")
                .HasMaxLength(Event.NameMaxLength)
                .IsRequired();
        });

        builder.Property(e => e.Description)
            .IsRequired();

        builder.OwnsOne(p => p.Period, x =>
        {
            x.Property(period => period.StartDate)
                .HasColumnName("start_date")
                .IsRequired();
            
            x.Property(period => period.EndDate)
                .HasColumnName("end_date")
                .IsRequired();
        });

        builder.HasOne(e => e.UserOwner)
            .WithMany()
            .IsRequired();

        builder.HasOne(e => e.EventType)
            .WithMany()
            .IsRequired();

        builder.HasOne(e => e.Target)
            .WithMany()
            .IsRequired();
    }
}