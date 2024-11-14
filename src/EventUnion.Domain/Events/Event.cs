// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable UnusedType.Global
// ReSharper disable UnusedMember.Local
// ReSharper disable UnusedMember.Global

using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using EventUnion.Domain.Users;
using EventUnion.Domain.ValueObjects;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

namespace EventUnion.Domain.Events;

public class Event
{
    [NotMapped] public const int NameMaxLength = 256;
    
    public Guid EventId { get; private set; }
    
    public FullName Name { get; private set; }
    public string Description { get; private set; }
    public Period Period { get; private set; }
    public bool Private { get; private set; }
    public string Image { get; private set; }
    
    public User UserOwner { get; private set; }
    public EventType EventType { get; private set; }
    public Target Target { get; private set; }
    public Domain.Addresses.Address Address { get; private set; }

    public Event(Guid eventId, User userOwner, EventType eventType, 
        FullName name, Target target, Domain.Addresses.Address address, string description, 
        Period period, bool privateObj, string image)
    {
        EventId = eventId;
        UserOwner = userOwner;
        EventType = eventType;
        Name = name;
        Description = description;
        Period = period;
        Target = target;
        Address = address;
        Private = privateObj;
        Image = image;
    }

    [ExcludeFromCodeCoverage]
    private Event() { }
}