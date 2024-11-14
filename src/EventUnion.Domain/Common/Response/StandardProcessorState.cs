using System.Diagnostics.CodeAnalysis;

namespace EventUnion.CommonResources.Response;

[ExcludeFromCodeCoverage]
public class StandardProcessorState<TUser>  where TUser : class
{
    public string CustomMessage { get; private set; } = string.Empty;
    public bool CustomStandardMessageDefined { get; private set; }
    public string EntityName { get; private set; } = string.Empty;
    public TUser? User { get; private set; } 

    public void SetMessage(string message)
    {
        CustomMessage = message;
        CustomStandardMessageDefined = true;
    }

    public void SetUser(TUser user)
    {
        User = user;
    }

    public void RemoveMessage()
    {
        CustomMessage = string.Empty;
        CustomStandardMessageDefined = false;
    }

    public void SetEntityName(string entityName)
    {
        EntityName = entityName;
    }
}