using MediatR;
using Microsoft.Extensions.DependencyInjection;
using EventUnion.Infrastructure;

namespace EventUnion.IntegrationTests;

public abstract class BaseIntegrationTest : IClassFixture<IntegrationTestWebAppFactory>
{
    protected readonly ISender Sender;
    protected readonly EventUnionDbContext DbContext;

    protected BaseIntegrationTest(IntegrationTestWebAppFactory factory)
    {
        var serviceScopeFactory = factory.Services.GetRequiredService<IServiceScopeFactory>();
        var serviceScope = serviceScopeFactory.CreateScope();

        Sender = serviceScope.ServiceProvider.GetRequiredService<ISender>();
        DbContext = serviceScope.ServiceProvider.GetRequiredService<EventUnionDbContext>();
    }
}