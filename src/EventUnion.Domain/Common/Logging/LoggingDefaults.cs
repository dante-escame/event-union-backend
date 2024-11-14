using System.Diagnostics.CodeAnalysis;

namespace EventUnion.CommonResources.Logging;

[ExcludeFromCodeCoverage]
public static class LoggingDefaults
{
    public const string ErrorLogProperty = "Error";
    public const string CorrelationLogProperty = "CorrelationId";
    public const string InputLogProperty = "CorrelationId";
    public const string OutputLogProperty = "CorrelationId";
}