namespace Event.Api.Shared;

public record Error(string Code, string Message)
{
    public static readonly Error None = new(string.Empty, string.Empty);

    public static readonly Error NullValue = new("Error.NullValue", "O valor do resultado esperado é nulo.");

    public static readonly Error ConditionFailed = new("Error.ConditionNotMet", "A condição em questão não foi atendida.");
}