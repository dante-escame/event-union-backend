using CSharpFunctionalExtensions;
using EventUnion.CommonResources;
using EventUnion.Domain.Common.Errors;
using EventUnion.Domain.Common.Interfaces;

namespace EventUnion.Infrastructure;

public class UnitOfWork(EventUnionDbContext persistenceContext) : IUnitOfWork
{
    public async Task AddAsync<T>(T entity, CancellationToken cancellationToken) where T : class
    {
        await persistenceContext.Set<T>().AddAsync(entity, cancellationToken);
    }

    public async Task<UnitResult<Error>> SaveChangesAsync(CancellationToken cancellationToken)
    {
        var result = await persistenceContext.SaveChangesAsync(cancellationToken);

        return result < 0
            ? (UnitResult<Error>)CommonError.NotPersisted()
            : UnitResult.Success<Error>();
    }
}