using System.Diagnostics.CodeAnalysis;

namespace EventUnion.CommonResources;

[ExcludeFromCodeCoverage]
public class ResourceLocator<T>(T identifier)
{
    public T Identifier { get; init; } = identifier;
}