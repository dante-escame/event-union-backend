using Microsoft.EntityFrameworkCore;

namespace Event.Api.Infrastructure;

public class EventDbContext(DbContextOptions options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder) { }

    // Event Context
    public DbSet<Entities.Event> Event { get; set; }
    public DbSet<Entities.EventType> EventType { get; set; }
    public DbSet<Entities.Tag> Tag { get; set; }
    public DbSet<Entities.Target> Target { get; set; }
    public DbSet<Entities.Sponsor> Sponsor { get; set; }
    public DbSet<Entities.EventSpace> EventSpace { get; set; }
    public DbSet<Entities.EventParameter> EventParameter { get; set; }
    public DbSet<Entities.EventParticipant> EventParticipant { get; set; }
    public DbSet<Entities.Place> Place { get; set; }
    public DbSet<Entities.PlaceAddress> PlaceAddress { get; set; }
    public DbSet<Entities.Participant> Participant { get; set; }
    
    // User Context
    public DbSet<Entities.User> User { get; set; }
    public DbSet<Entities.Interest> Interest { get; set; }
    public DbSet<Entities.Phone> Phone { get; set; }
    public DbSet<Entities.Person> People { get; set; }
    public DbSet<Entities.Company> Company { get; set; }
    public DbSet<Entities.UserAddress> UserAddress { get; set; }
    public DbSet<Entities.Address> Address { get; set; }
}