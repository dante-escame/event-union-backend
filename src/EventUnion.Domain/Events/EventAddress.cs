// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable UnusedType.Global
// ReSharper disable UnusedMember.Local
// ReSharper disable UnusedMember.Global
// ReSharper disable ClassNeverInstantiated.Global

using System.Diagnostics.CodeAnalysis;
using EventUnion.Domain.Addresses;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

namespace EventUnion.Domain.Events;

public class EventAddress
{
    // ReSharper disable once UnusedAutoPropertyAccessor.Local
    public Guid EventAddressId { get; private set; }
    public Event Event { get; private set; }
    public Address Address { get; private set; }

    public EventAddress(Event eventObj, Address address)
    {
        Event = eventObj;
        Address = address;
    }

    [ExcludeFromCodeCoverage]
    private EventAddress() { }
}