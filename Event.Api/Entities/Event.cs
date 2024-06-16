using System.ComponentModel.DataAnnotations;

// ReSharper disable CollectionNeverUpdated.Global
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

namespace Event.Api.Entities;

public class Event(
    Guid eventId,
    Guid userOwnerId,
    string name,
    string description,
    DateTime startDate,
    DateTime endDate,
    int eventTypeId,
    int targetId)
{
    [Key]
    public Guid EventId { get; set; } = eventId;

    public Guid UserOwnerId { get; set; } = userOwnerId;
    public string Name { get; set; } = name;
    public string Description { get; set; } = description;
    public DateTime StartDate { get; set; } = startDate;
    public DateTime EndDate { get; set; } = endDate;
    public int EventTypeId { get; set; } = eventTypeId;
    public int TargetId { get; set; } = targetId;

    // Nav Properties
    public EventType EventType { get; set; }
    public Target Target { get; set; }
    public ICollection<Tag> Tags { get; set; }
    public ICollection<Sponsor> Sponsors { get; set; }
    public ICollection<EventSpace> EventSpaces { get; set; }
    public ICollection<EventParameter> EventParameters { get; set; }
    public ICollection<EventParticipant> EventParticipants { get; set; }
}

public class Tag(int tagId, Guid eventId, string name, string value)
{
    [Key]
    public int TagId { get; set; } = tagId;

    public Guid EventId { get; set; } = eventId;
    public string Name { get; set; } = name;
    public string Value { get; set; } = value;

    // Nav Properties
    public Event Event { get; set; }
}

public class EventType(int eventTypeId, string description)
{
    [Key]
    public int EventTypeId { get; set; } = eventTypeId;

    public string Description { get; set; } = description;
}

public class Sponsor(int sponsorId, Guid eventId)
{
    [Key]
    public int SponsorId { get; set; } = sponsorId;

    public Guid EventId { get; set; } = eventId;

    // Nav Properties
    public Event Event { get; set; }
}

public class Target(int targetId, string description)
{
    [Key]
    public int TargetId { get; set; } = targetId;

    public string Description { get; set; } = description;
}

public class EventSpace(
    int eventSpaceId,
    Guid eventId,
    int placeId,
    DateTime startDate,
    DateTime endDate)
{
    [Key]
    public int EventSpaceId { get; set; } = eventSpaceId;

    public Guid EventId { get; set; } = eventId;
    public int PlaceId { get; set; } = placeId;
    public DateTime StartDate { get; set; } = startDate;
    public DateTime EndDate { get; set; } = endDate;

    // Nav Properties
    public Event Event { get; set; }
    public Place Place { get; set; }
}

public class Place(int placeId, string name, int capacity)
{
    [Key]
    public int PlaceId { get; set; } = placeId;

    public string Name { get; set; } = name;
    public int Capacity { get; set; } = capacity;

    // Nav Properties
    public PlaceAddress PlaceAddress { get; set; }
}

public class PlaceAddress(int addressId, int placeId)
{
    [Key]
    public int AddressId { get; set; } = addressId;

    public int PlaceId { get; set; } = placeId;

    // Nav Properties
    public Place Place { get; set; }
}

public class EventParameter(int eventParameterId, Guid eventId, string key, string value, bool active)
{
    [Key]
    public int EventParameterId { get; set; } = eventParameterId;

    public Guid EventId { get; set; } = eventId;
    public string Key { get; set; } = key;
    public string Value { get; set; } = value;
    public bool Active { get; set; } = active;

    // Nav Properties
    public Event Event { get; set; }
}

public class EventParticipant(
    int eventParticipantId,
    Guid eventId,
    Guid participantId,
    string inviteEmail)
{
    [Key]
    public int EventParticipantId { get; set; } = eventParticipantId;

    public Guid EventId { get; set; } = eventId;
    public Guid ParticipantId { get; set; } = participantId;
    public string InviteEmail { get; set; } = inviteEmail;

    // Nav Properties
    public Event Event { get; set; }
    public Participant Participant { get; set; }
}

public class Participant(Guid participantId, string name, string email, string cpf, DateTime birthdate)
{
    [Key]
    public Guid ParticipantId { get; set; } = participantId;

    public string Name { get; set; } = name;
    public string Email { get; set; } = email;
    public string Cpf { get; set; } = cpf;
    public DateTime Birthdate { get; set; } = birthdate;
}