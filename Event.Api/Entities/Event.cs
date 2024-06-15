using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Event.Api.Entities;

public class Event
{
    public Event(Guid eventId, Guid userOwnerId, string name, string description, DateTime startDate, DateTime endDate,
        int eventTypeId, int targetId, EventType eventType, Target target, ICollection<Tag> tags,
        ICollection<Sponsor> sponsors, ICollection<EventSpace> eventSpaces, ICollection<EventParameter> eventParameters,
        ICollection<EventParticipant> eventParticipants)
    {
        EventId = eventId;
        UserOwnerId = userOwnerId;
        Name = name;
        Description = description;
        StartDate = startDate;
        EndDate = endDate;
        EventTypeId = eventTypeId;
        TargetId = targetId;
        EventType = eventType;
        Target = target;
        Tags = tags;
        Sponsors = sponsors;
        EventSpaces = eventSpaces;
        EventParameters = eventParameters;
        EventParticipants = eventParticipants;
    }

    [Key]
    public Guid EventId { get; set; }
    public Guid UserOwnerId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int EventTypeId { get; set; }
    public int TargetId { get; set; }

    // Nav Properties
    public EventType EventType { get; set; }
    public Target Target { get; set; }
    public ICollection<Tag> Tags { get; set; }
    public ICollection<Sponsor> Sponsors { get; set; }
    public ICollection<EventSpace> EventSpaces { get; set; }
    public ICollection<EventParameter> EventParameters { get; set; }
    public ICollection<EventParticipant> EventParticipants { get; set; }
}

public class Tag
{
    public Tag(int tagId, Guid eventId, string name, string value, Event @event)
    {
        TagId = tagId;
        EventId = eventId;
        Name = name;
        Value = value;
        Event = @event;
    }

    [Key]
    public int TagId { get; set; }
    public Guid EventId { get; set; }
    public string Name { get; set; }
    public string Value { get; set; }

    // Nav Properties
    public Event Event { get; set; }
}

public class EventType
{
    [Key]
    public int EventTypeId { get; set; }
    public string Description { get; set; }
}

public class Sponsor
{
    public Sponsor(int sponsorId, Guid eventId, Event @event)
    {
        SponsorId = sponsorId;
        EventId = eventId;
        Event = @event;
    }

    [Key]
    public int SponsorId { get; set; }
    public Guid EventId { get; set; }

    // Nav Properties
    public Event Event { get; set; }
}

public class Target
{
    public Target(int targetId, string description)
    {
        TargetId = targetId;
        Description = description;
    }

    [Key]
    public int TargetId { get; set; }
    public string Description { get; set; }
}

public class EventSpace
{
    public EventSpace(int eventSpaceId, Guid eventId, int placeId, DateTime startDate, DateTime endDate, Event @event, Place place)
    {
        EventSpaceId = eventSpaceId;
        EventId = eventId;
        PlaceId = placeId;
        StartDate = startDate;
        EndDate = endDate;
        Event = @event;
        Place = place;
    }

    [Key]
    public int EventSpaceId { get; set; }
    public Guid EventId { get; set; }
    public int PlaceId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    // Nav Properties
    public Event Event { get; set; }
    public Place Place { get; set; }
}

public class Place
{
    public Place(int placeId, string name, int capacity, PlaceAddress placeAddress)
    {
        PlaceId = placeId;
        Name = name;
        Capacity = capacity;
        PlaceAddress = placeAddress;
    }

    [Key]
    public int PlaceId { get; set; }
    public string Name { get; set; }
    public int Capacity { get; set; }

    // Nav Properties
    public PlaceAddress PlaceAddress { get; set; }
}

public class PlaceAddress
{
    public PlaceAddress(int addressId, int placeId, Place place)
    {
        AddressId = addressId;
        PlaceId = placeId;
        Place = place;
    }

    [Key]
    public int AddressId { get; set; }
    public int PlaceId { get; set; }

    // Nav Properties
    public Place Place { get; set; }
}

public class EventParameter
{
    public EventParameter(int eventParameterId, Guid eventId, string key, string value, bool active, Event @event)
    {
        EventParameterId = eventParameterId;
        EventId = eventId;
        Key = key;
        Value = value;
        Active = active;
        Event = @event;
    }

    [Key]
    public int EventParameterId { get; set; }
    public Guid EventId { get; set; }
    public string Key { get; set; }
    public string Value { get; set; }
    public bool Active { get; set; }

    // Nav Properties
    public Event Event { get; set; }
}

public class EventParticipant
{
    public EventParticipant(int eventParticipantId, Guid eventId, Guid participantId, string inviteEmail, Event @event, Participant participant)
    {
        EventParticipantId = eventParticipantId;
        EventId = eventId;
        ParticipantId = participantId;
        InviteEmail = inviteEmail;
        Event = @event;
        Participant = participant;
    }

    [Key]
    public int EventParticipantId { get; set; }
    public Guid EventId { get; set; }
    public Guid ParticipantId { get; set; }
    public string InviteEmail { get; set; }

    // Nav Properties
    public Event Event { get; set; }
    public Participant Participant { get; set; }
}

public class Participant
{
    public Participant(Guid participantId, string name, string email, string cpf, DateTime birthdate)
    {
        ParticipantId = participantId;
        Name = name;
        Email = email;
        Cpf = cpf;
        Birthdate = birthdate;
    }

    [Key]
    public Guid ParticipantId { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Cpf { get; set; }
    public DateTime Birthdate { get; set; }
}