using EventUnion.Domain.Events;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventUnion.Infrastructure.Mappings.Events;

public class PlaceAddressMap : IEntityTypeConfiguration<EventAddress>
{
    public void Configure(EntityTypeBuilder<EventAddress> builder)
    {
        builder.ToTable("EventAddress");

        builder.HasKey(pa => pa.EventAddressId);

        builder.Property(pa => pa.EventAddressId)
            .ValueGeneratedOnAdd()
            .IsRequired();
        
        builder.HasOne(pa => pa.Event)
            .WithMany()
            .IsRequired();

        builder.HasOne(pa => pa.Address)
            .WithMany()
            .IsRequired();
    }
}