using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

// ReSharper disable UnusedMember.Global

namespace EventUnion.CommonResources;

[ExcludeFromCodeCoverage]
// ReSharper disable once UnusedType.Global
public static class EnvironmentDefaults
{
    // ReSharper disable once MemberCanBePrivate.Global
    public const string IntegrationTestEnvironment = "IntegrationTest";
    public const string LocalEnvironment = "Local";
    public const string ReleaseEnvironment = "Release";

    public static bool IsDevelopmentEnvironment(IWebHostEnvironment builderEnvironment) =>
        builderEnvironment.IsDevelopment()
        || builderEnvironment.IsEnvironment(LocalEnvironment);
    
    public static bool IsDevelopmentEnvironment(WebApplication app) =>
        app.Environment.IsDevelopment()
        || app.Environment.IsEnvironment(LocalEnvironment);
    
    public static bool IsHomologationEnvironment(IWebHostEnvironment builderEnvironment) =>
        builderEnvironment.IsEnvironment(ReleaseEnvironment);

    public static bool IsHomologationEnvironment(WebApplication app) =>
        app.Environment.IsEnvironment(ReleaseEnvironment);
}