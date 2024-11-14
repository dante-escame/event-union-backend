using Microsoft.EntityFrameworkCore;
using EventUnion.CommonResources.Interfaces;
using EventUnion.CommonResources;
using MediatR;

namespace EventUnion.Infrastructure;

public class EventUnionDbContext(
    IPublisher publisher,
    DbContextOptions<EventUnionDbContext> options
    ) : DbContext(options)
{
    // TODO: melhorar solucao
    // TODO: adicionar todos os tipos de Enumerados("Entidades de Dominio") aqui
    private static readonly Type[] EnumerationTypes = 
    [
        // TODO typeof(Recurrence)
    ];

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        ConfigureMessaging(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(EventUnionDbContext).Assembly);
    }

    private static void ConfigureMessaging(ModelBuilder modelBuilder)
    {
        modelBuilder.Ignore<IDomainEvent>();
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        ConfigureEnumerationTypes();

        await DispatchDomainEvents(cancellationToken);

        return await base.SaveChangesAsync(cancellationToken);
    }

    private async Task DispatchDomainEvents(CancellationToken cancellationToken)
    {
        var domainEvents = ChangeTracker.Entries<IAggregateRoot>()
            .Select(entry => entry.Entity)
            .SelectMany(entity =>
            {
                var domainEvents = entity.DomainEvents;

                entity.ClearDomainEvents();

                return domainEvents;
            })
            .ToList();

        foreach (var domainEvent in domainEvents)
            await publisher.Publish(domainEvent, cancellationToken);
    }

    private void ConfigureEnumerationTypes()
    {
        var enumerationEntries = ChangeTracker.Entries()
            .Where(x => EnumerationTypes.Contains(x.Entity.GetType()));

        foreach (var enumerationEntry in enumerationEntries)
            enumerationEntry.State = EntityState.Unchanged;
    }
}