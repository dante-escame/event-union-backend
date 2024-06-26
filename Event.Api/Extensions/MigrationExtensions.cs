﻿using Event.Api.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Event.Api.Extensions;

public static class MigrationExtensions
{
    public static void ApplyMigrations(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();

        var dbContext = scope.ServiceProvider.GetRequiredService<EventDbContext>();

        dbContext.Database.Migrate();
    }
}