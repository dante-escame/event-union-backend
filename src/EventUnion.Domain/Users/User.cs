// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable UnusedType.Global
// ReSharper disable UnusedMember.Local

using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using EventUnion.Domain.ValueObjects;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

namespace EventUnion.Domain.Users;

public abstract class User
{
    [NotMapped] public const int EmailMaxLength = 128;
    
    public Guid UserId { get; private set; }
    public Email Email { get; private set; }
    public string Password { get; private set; }
    public string CriptKey { get; private set; }
    public string Iv { get; set; }
    
    protected User(Guid userId, Email email, string password, string criptKey, string iv)
    {
        UserId = userId;
        Email = email;
        Password = password;
        CriptKey = criptKey;
        Iv = iv;
    }
    
    [ExcludeFromCodeCoverage]
    // ReSharper disable once PublicConstructorInAbstractClass
    public User() { }
}