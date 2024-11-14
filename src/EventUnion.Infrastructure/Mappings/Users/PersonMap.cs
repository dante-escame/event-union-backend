using EventUnion.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventUnion.Infrastructure.Mappings.Users;

public class PersonMap : IEntityTypeConfiguration<Person>
{
    public void Configure(EntityTypeBuilder<Person> builder)
    {
        builder.ToTable("Person");
        
        builder.Property(p => p.PersonId).IsRequired();
        
        builder.OwnsOne(p => p.Name, x =>
        {
            x.Property(name => name.Value)
                .HasColumnName("name")
                .HasMaxLength(Person.NameMaxLength)
                .IsRequired();
        });
        
        builder.OwnsOne(p => p.Cpf, x =>
        {
            x.Property(name => name.Value)
                .HasColumnName("cpf")
                .HasMaxLength(Person.CpfMaxLength)
                .IsRequired();
        });
        
        builder.OwnsOne(p => p.Birthdate, x =>
        {
            x.Property(birthdate => birthdate.Value)
                .HasColumnName("birthdate")
                .IsRequired();
        });
    }
}