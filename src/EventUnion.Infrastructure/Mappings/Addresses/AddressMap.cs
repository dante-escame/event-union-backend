using EventUnion.Domain.Addresses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventUnion.Infrastructure.Mappings.Addresses;

public class AddressMap : IEntityTypeConfiguration<Address>
{
    public void Configure(EntityTypeBuilder<Address> builder)
    {
        builder.ToTable("address");
        
        builder.HasKey(a => a.AddressId);
        
        builder.Property(a => a.ZipCode)
            .HasColumnName("zip_code")
            .HasMaxLength(Address.ZipCodeMaxLength)
            .IsRequired();

        builder.Property(a => a.Street)
            .HasColumnName("street")
            .HasMaxLength(Address.StreetMaxLength)
            .IsRequired();

        builder.Property(a => a.Neighborhood)
            .HasColumnName("neighborhood")
            .HasMaxLength(Address.NeighborhoodMaxLength)
            .IsRequired();

        builder.Property(a => a.Number)
            .HasColumnName("number")
            .HasMaxLength(Address.NumberMaxLength)
            .IsRequired();

        builder.Property(a => a.AdditionalInfo)
            .HasColumnName("additional_info")
            .IsRequired(false);

        builder.Property(a => a.State)
            .HasColumnName("state")
            .HasMaxLength(Address.StateMaxLength)
            .IsRequired();

        builder.Property(a => a.Country)
            .HasColumnName("country")
            .HasMaxLength(Address.CountryMaxLength)
            .IsRequired();

        builder.Property(a => a.City)
            .HasColumnName("city")
            .HasMaxLength(Address.CityMaxLength)
            .IsRequired();
        
        builder.Property(a => a.ZipCode).IsRequired().HasMaxLength(10);
        builder.Property(a => a.Street).IsRequired().HasMaxLength(256);
        builder.Property(a => a.Neighborhood).HasMaxLength(256);
        builder.Property(a => a.Number).HasMaxLength(10);
        builder.Property(a => a.AdditionalInfo).HasMaxLength(256);
        builder.Property(a => a.State).IsRequired().HasMaxLength(2);
        builder.Property(a => a.Country).IsRequired().HasMaxLength(100);
    }
}