using Microsoft.EntityFrameworkCore;

namespace Event.Api.Database;

public class EventDbContext : DbContext
{
    public EventDbContext(DbContextOptions options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
    }

    public DbSet<Entities.Event> Events { get; set; }
}