// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable UnusedType.Global
// ReSharper disable UnusedMember.Local
// ReSharper disable UnusedMember.Global
// ReSharper disable ClassNeverInstantiated.Global

using System.Diagnostics.CodeAnalysis;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

namespace EventUnion.Domain.Events;

public class Sponsor
{
    public Guid SponsorId { get; private set; }
    public Event Event { get; private set; }
    public string Image { get; private set; }

    public Sponsor(Guid sponsorId, Event eventObj, string image)
    {
        SponsorId = sponsorId;
        Event = eventObj;
        Image = image;
    }

    [ExcludeFromCodeCoverage]
    private Sponsor() { }
}