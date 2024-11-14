// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable UnusedType.Global
// ReSharper disable UnusedMember.Local
// ReSharper disable UnusedMember.Global
// ReSharper disable ClassNeverInstantiated.Global

using System.Diagnostics.CodeAnalysis;
// ReSharper disable MemberCanBePrivate.Global

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

namespace EventUnion.Domain.Events;

public class Target
{
    public static readonly Target Kids = new((int)TargetEnum.Kids, "Infantil");
    public static readonly Target Young = new((int)TargetEnum.Young, "Jovem");
    public static readonly Target YoungAdult = new((int)TargetEnum.YoungAdult, "Jovem Adulto");
    public static readonly Target Adult = new((int)TargetEnum.Adult, "Adulto");
    public static readonly Target Old = new((int)TargetEnum.Old, "Idoso");

    public static readonly IReadOnlyList<Target> All = [ Kids, Young, YoungAdult, Adult, Old ];
    
    public int TargetId { get; private set; }
    public string Name { get; private set; }

    public Target(int targetId, string name)
    {
        TargetId = targetId;
        Name = name;
    }

    // ReSharper disable once MemberCanBePrivate.Global
    public enum TargetEnum
    {
        Kids = 1,
        Young,
        YoungAdult,
        Adult,
        Old
    }

    [ExcludeFromCodeCoverage]
    private Target() { }
}