using System.Diagnostics.CodeAnalysis;
using CSharpFunctionalExtensions;
using EventUnion.Domain.Common.Errors;

namespace EventUnion.CommonResources;

public class Error : ValueObject
{
    public Error(string code, string message)
    {
        Code = code;
        Message = message;
    }

    public string Code { get; private set; } = null!;
    public string Message { get; private set; } = null!;

    protected const string Separator = "||";
    
    public string Serialize()
    {
        return $"{Code}{Separator}{Message}";
    }
    
    public static Error? Deserialize(string serialized)
    {
        if (serialized == "A non-empty request body is required.")
            return CommonError.ValueIsEmpty("Body");

        var data = serialized.Split([Separator], StringSplitOptions.RemoveEmptyEntries);

        if (data.Length < 2) return null;

        return new Error(data[0], data[1]);
    }
    
    public override string ToString() => Serialize();
    
    protected override IEnumerable<IComparable> GetEqualityComponents()
    {
        yield return Code;
    }
    
    // EFCore Constructor
    // ReSharper disable once UnusedMember.Local
    [ExcludeFromCodeCoverage]
    private Error() { }
}