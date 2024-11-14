// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable UnusedType.Global
// ReSharper disable UnusedMember.Local
// ReSharper disable UnusedMember.Global
// ReSharper disable ClassNeverInstantiated.Global

using System.Diagnostics.CodeAnalysis;
using EventUnion.Domain.Users;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

namespace EventUnion.Domain.Events;

public class EventUser
{
    // ReSharper disable once UnusedAutoPropertyAccessor.Local
    public Guid EventUserId { get; private set; }
    public Event Event { get; private set; }
    public User User { get; private set; }
    public string InviteEmail { get; private set; }

    public EventUser(Event eventObj, User user, string inviteEmail)
    {
        Event = eventObj;
        User = user;
        InviteEmail = inviteEmail;
    }

    [ExcludeFromCodeCoverage]
    private EventUser () { }
}