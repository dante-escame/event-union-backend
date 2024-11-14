// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable UnusedType.Global
// ReSharper disable UnusedMember.Local

using System.Diagnostics.CodeAnalysis;
using EventUnion.Domain.Events;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

namespace EventUnion.Domain.Users;

public class UserTag
{
    // ReSharper disable once UnusedAutoPropertyAccessor.Local
    public Guid UserTagId { get; private set; }
    public Tag Tag { get; private set; }
    public User User { get; private set; }

    public UserTag(Tag tag, User user)
    {
        Tag = tag;
        User = user;
    }
    
    [ExcludeFromCodeCoverage]
    private UserTag() { }
}