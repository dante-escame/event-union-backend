using EventUnion.Domain.Events;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventUnion.Infrastructure.Mappings.Events;

public class TargetMap : IEntityTypeConfiguration<Target>
{
    public void Configure(EntityTypeBuilder<Target> builder)
    {
        builder.ToTable("Target");

        builder.HasKey(t => t.TargetId);
        
        builder.Property(t => t.TargetId)
            .IsRequired();

        builder.Property(t => t.Name)
            .IsRequired();

        builder.HasData(Target.All);
    }
}