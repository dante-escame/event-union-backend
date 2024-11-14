using CSharpFunctionalExtensions;
using EventUnion.CommonResources;
using EventUnion.Domain.Common.Errors;

// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable AutoPropertyCanBeMadeGetOnly.Local

namespace EventUnion.Domain.ValueObjects;

// ReSharper disable once UnusedType.Global
public class Period : ValueObject
{
    public DateTime StartDate { get; private set; }
    public DateTime EndDate { get; private set; }
    
    // ReSharper disable once UnusedMember.Global
    public static Result<Period, Error> Create(DateTime startDate, DateTime endDate)
    {
        if (endDate < startDate)
            return CommonError.InvalidInterval();

        return new Period(startDate, endDate);
    }
    
    private Period(DateTime startDate, DateTime endDate)
    {
        StartDate = startDate;
        EndDate = endDate;
    }
    
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return StartDate;
        yield return EndDate;
    }
}