using MediatR;

namespace EventUnion.CommonResources.Interfaces;

public interface IDomainEvent : INotification
{
    public Guid Id { get; init; }
}