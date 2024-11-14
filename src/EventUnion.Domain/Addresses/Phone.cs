// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable UnusedType.Global
// ReSharper disable UnusedMember.Local

using System.Diagnostics.CodeAnalysis;
using EventUnion.Domain.Users;

// ReSharper disable UnusedAutoPropertyAccessor.Local

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

namespace EventUnion.Domain.Addresses;

public class Phone
{
    public Guid PhoneId { get; private set; }
    public Guid UserId { get; private set; }
    public User User { get; private set; }
    public string Value { get; private set; }

    public Phone(Guid phoneId, User user, string value)
    {
        User = user;
        Value = value;
    }
    
    [ExcludeFromCodeCoverage]
    private Phone() { }
}