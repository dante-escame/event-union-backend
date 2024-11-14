// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable UnusedType.Global
// ReSharper disable UnusedMember.Local

using System.Diagnostics.CodeAnalysis;
using EventUnion.Domain.Addresses;

// ReSharper disable UnusedAutoPropertyAccessor.Local

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

namespace EventUnion.Domain.Users;

public class UserAddress
{
    public Guid UserAddressId { get; private set; }
    public User User { get; private set; }
    public Address Address { get; private set; }

    public UserAddress(User user, Address address)
    {
        User = user;
        Address = address;
    }
    
    [ExcludeFromCodeCoverage]
    private UserAddress() { }
}