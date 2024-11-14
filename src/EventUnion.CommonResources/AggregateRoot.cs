using System.Diagnostics.CodeAnalysis;
using CSharpFunctionalExtensions;
using EventUnion.CommonResources.Interfaces;

namespace EventUnion.CommonResources;

public interface IAggregateRoot
{
    // ReSharper disable once UnusedMember.Global
    IEnumerable<IDomainEvent> DomainEvents { get; }
    // ReSharper disable once UnusedMember.Global
    void ClearDomainEvents();
}

// ReSharper disable once UnusedType.Global
[ExcludeFromCodeCoverage]
public abstract class AggregateRoot<TId> : Entity<TId>, IAggregateRoot where TId : IComparable<TId>
{
    private readonly List<IDomainEvent> _domainEvents = [];
    public IEnumerable<IDomainEvent> DomainEvents => _domainEvents;

    // ReSharper disable once UnusedMember.Global
    protected void RaiseDomainEvent(IDomainEvent domainEvent) => _domainEvents.Add(domainEvent);

    public void ClearDomainEvents() => _domainEvents.Clear();
}