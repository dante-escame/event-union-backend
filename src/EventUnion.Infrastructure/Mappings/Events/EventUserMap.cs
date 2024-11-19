using EventUnion.Domain.Events;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventUnion.Infrastructure.Mappings.Events;

public class EventUserMap : IEntityTypeConfiguration<EventUser>
{
    public void Configure(EntityTypeBuilder<EventUser> builder)
    {
        builder.ToTable("event_user");

        builder.HasKey(eu => eu.EventUserId);

        builder.Property(eu => eu.EventUserId)
            .ValueGeneratedOnAdd()
            .IsRequired();
        
        builder.HasOne(eu => eu.Event)
            .WithMany()
            .IsRequired();

        builder.HasOne(eu => eu.User)
            .WithMany()
            .IsRequired();

        builder.Property(eu => eu.InviteEmail)
            .IsRequired();
    }
}