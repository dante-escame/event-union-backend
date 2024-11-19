using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventUnion.Infrastructure.Mappings.Events;

public class EventAddress : IEntityTypeConfiguration<Domain.Events.EventAddress>
{
    public void Configure(EntityTypeBuilder<Domain.Events.EventAddress> builder)
    {
        builder.ToTable("event_address");

        builder.HasKey(ea => ea.EventAddressId);

        builder.Property(ea => ea.EventAddressId)
            .ValueGeneratedOnAdd()
            .IsRequired();
        
        builder.HasOne(ea => ea.Event)
            .WithMany()
            .IsRequired();

        builder.HasOne(ea => ea.Address)
            .WithMany()
            .IsRequired();
    }
}