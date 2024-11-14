using EventUnion.CommonResources;

namespace EventUnion.Domain.Common.Errors;

public static class EventError
{
    public const string UserNotFoundCode = "user.resource.not_found";

    public static Error UserNotFound()
        => new (UserNotFoundCode, "Usuário não encontrado.");  
}
