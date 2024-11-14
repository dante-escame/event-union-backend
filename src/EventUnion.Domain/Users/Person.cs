// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable UnusedType.Global
// ReSharper disable UnusedMember.Local

using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using EventUnion.Domain.ValueObjects;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

namespace EventUnion.Domain.Users;

public class Person : User
{
    [NotMapped] public const int NameMaxLength = 256;
    [NotMapped] public const int CpfMaxLength = 11;
    
    public Guid PersonId { get; private set; }
    public FullName Name { get; private set; }
    public Cpf Cpf { get; private set; }
    public Birthdate Birthdate { get; private set; }

    public Person(Guid userId, Email email, string password, string criptKey, string iv, 
        Guid personId, FullName name, Cpf cpf, Birthdate birthdate)
        : base(userId, email, password, criptKey, iv)
    {
        PersonId = personId;
        Name = name;
        Cpf = cpf;
        Birthdate = birthdate;
    }
    
    [ExcludeFromCodeCoverage]
    private Person() {}
}