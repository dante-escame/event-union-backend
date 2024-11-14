// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable UnusedType.Global
// ReSharper disable UnusedMember.Local
// ReSharper disable UnusedMember.Global
// ReSharper disable ClassNeverInstantiated.Global

using System.Diagnostics.CodeAnalysis;
// ReSharper disable MemberCanBePrivate.Global

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

namespace EventUnion.Domain.Events;

public class EventType
{
    public static readonly EventType Party = new((int)EventTypeEnum.Party, "Festivo");
    public static readonly EventType Sport = new((int)EventTypeEnum.Sport, "Esportivo");
    public static readonly EventType Formal = new((int)EventTypeEnum.Formal, "Formal");
    
    public static readonly IReadOnlyList<EventType> All = [ Party, Sport, Formal ];
    
    public int EventTypeId { get; private set; }
    public string Name { get; private set; }

    public EventType(int eventTypeId, string name)
    {
        EventTypeId = eventTypeId;
        Name = name;
    }
    
    // ReSharper disable once MemberCanBePrivate.Global
    public enum EventTypeEnum
    {
        Party = 1,
        Sport,
        Formal
    }

    [ExcludeFromCodeCoverage]
    private EventType() { }
}