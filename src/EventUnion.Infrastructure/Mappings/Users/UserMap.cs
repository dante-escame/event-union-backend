using EventUnion.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventUnion.Infrastructure.Mappings.Users;

public class UserMap : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("User");

        builder.UseTptMappingStrategy();
        
        builder.Property(u => u.UserId)
            .ValueGeneratedOnAdd()
            .IsRequired();
        
        builder.OwnsOne(u => u.Email, x =>
        {
            x.Property(email => email.Value)
                .HasColumnName("email")
                .HasMaxLength(User.EmailMaxLength)
                .IsRequired();
        });
        
        builder.Property(u => u.Password).IsRequired();
        
        builder.Property(u => u.CriptKey).IsRequired();
        builder.Property(u => u.Iv).IsRequired();
    }
}