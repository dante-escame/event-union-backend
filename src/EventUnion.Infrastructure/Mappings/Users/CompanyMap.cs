using EventUnion.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventUnion.Infrastructure.Mappings.Users;

public class CompanyMap : IEntityTypeConfiguration<Company>
{
    public void Configure(EntityTypeBuilder<Company> builder)
    {
        builder.ToTable("Company");
        
        builder.Property(c => c.CompanyId).IsRequired();
        
        builder.OwnsOne(c => c.Cnpj, x =>
        {
            x.Property(cnpj => cnpj.Value)
                .HasColumnName("cnpj")
                .HasMaxLength(Company.CnpjMaxLength)
                .IsRequired();
        });
        
        builder.OwnsOne(c => c.LegalName, x =>
        {
            x.Property(legalName => legalName.Value)
                .HasColumnName("legal_name")
                .HasMaxLength(Company.NameMaxLength)
                .IsRequired();
        });
        
        builder.OwnsOne(c => c.TradeName, x =>
        {
            x.Property(tradeName => tradeName.Value)
                .HasColumnName("trade_name")
                .HasMaxLength(Company.NameMaxLength)
                .IsRequired();
        });

        builder.Property(c => c.Specialization)
            .HasMaxLength(Company.SpecializationMaxLength)
            .IsRequired();
    }
}