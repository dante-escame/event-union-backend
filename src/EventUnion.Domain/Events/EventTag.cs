// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable UnusedType.Global
// ReSharper disable UnusedMember.Local

using System.Diagnostics.CodeAnalysis;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

namespace EventUnion.Domain.Events;

public class EventTag
{
    // ReSharper disable once UnusedAutoPropertyAccessor.Local
    public Guid EventTagId { get; private set; }
    public Tag Tag { get; private set; }
    public Event Event { get; private set; }

    public EventTag(Tag tag, Event eventObj)
    {
        Tag = tag;
        Event = eventObj;
    }
    
    [ExcludeFromCodeCoverage]
    private EventTag() { }
}