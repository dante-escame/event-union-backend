// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable UnusedType.Global
// ReSharper disable UnusedMember.Local

using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using EventUnion.Domain.ValueObjects;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

namespace EventUnion.Domain.Users;

public class Company : User
{
    [NotMapped] public const int CnpjMaxLength = 14;
    [NotMapped] public const int NameMaxLength = 256;
    [NotMapped] public const int SpecializationMaxLength = 90;
    
    public Guid CompanyId { get; private set; }
    public Cnpj Cnpj { get; private set; }
    public FullName LegalName { get; private set; }
    public FullName TradeName { get; private set; }
    public string Specialization { get; private set; }

    public Company(Guid userId, Email email, string password, string criptKey, string iv,
        Guid companyId, Cnpj cnpj, FullName legalName, FullName tradeName, string specialization)
        : base(userId, email, password, criptKey, iv)
    {
        CompanyId = companyId;
        Cnpj = cnpj;
        LegalName = legalName;
        TradeName = tradeName;
        Specialization = specialization;
    }

    [ExcludeFromCodeCoverage]
    private Company() {}
}
