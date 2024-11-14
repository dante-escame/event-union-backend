using CSharpFunctionalExtensions;
using EventUnion.CommonResources;

namespace EventUnion.Domain.Common.Interfaces;

public interface IUnitOfWork
{
    Task AddAsync<T>(T entity, CancellationToken cancellationToken) where T : class;
    Task<UnitResult<Error>> SaveChangesAsync(CancellationToken cancellationToken = default);
}
