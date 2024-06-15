using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Event.Api.Entities;

public class Event
{
    [Key]
    public Guid EventId { get; set; }
    public Guid OwnerId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int EventTypeId { get; set; }
    public int TargetId { get; set; }
    
    public virtual ICollection<Sponsor> Sponsors { get; set; }
    [ForeignKey("EventTypeId")]
    public virtual EventType EventType { get; set; }
    [ForeignKey("TargetId")]
    public virtual Target Target { get; set; }
}

public class EventType
{
    [Key]
    public Guid EventTypeId { get; set; }
    public string Description { get; set; }
}

public class Sponsor
{
    [Key]
    public Guid SponsorId { get; set; }

    [ForeignKey("EventId")]
    public Guid EventId { get; set; }
    public virtual Event Event { get; set; }
}

public class Target
{
    [Key]
    public Guid TargetId { get; set; }
}

public class EventSpace
{
    [Key]
    public int EventSpaceId { get; set; }
    public string Description { get; set; }
    public int PlaceId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}