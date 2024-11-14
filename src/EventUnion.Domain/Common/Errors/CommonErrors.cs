using EventUnion.CommonResources;

namespace EventUnion.Domain.Common.Errors;

public static class CommonError
{
    public const string InternalServerErrorCode = "internal.server.error";
    public const string EntityNotFoundErrorCode = "request.resource.not_found";

    public static Error NotFound()
        => new (EntityNotFoundErrorCode, "O recurso solicitado não foi encontrado.");  
    
    public static Error EntityNotFound(string resourceName, string identifier)
        => new (EntityNotFoundErrorCode, 
            $"O recurso '{resourceName}' com id '{identifier}' não foi encontrado.");

    public static Error RequestIsInvalid(string reason)
        => new("request.body.invalid", $"A solicitação é inválida: {reason}.");

    public static Error ResponseIsInvalid(string reason)
        => new("request.response.invalid", $"A resposta é inválida: {reason}.");
    
    public static Error ValueIsEmpty(string fieldName)
        => new ("request.field.empty", $"O valor '{fieldName}' não pode ser vazio.");

    public static Error ValueIsNotNull(string? fieldName, string? when)
        =>  new ("request.field.not_null", fieldName is null 
            ? "O valor deve ser nulo."
            : $"O valor deve ser nulo para o campo '{fieldName}' {when}.");

    public static Error ValueIsTooShort(string fieldName, int minLength)
        => new("request.field.short",
            $"O valor é muito curto para o campo '{fieldName}'. O mínimo esperado é '{minLength}' caracteres.");

    public static Error ValueIsTooLong(string fieldName, int maxLength)
        => new("request.field.long",
            $"O valor '{fieldName}' deve ter no máximo {maxLength} caracteres.");

    public static Error IsNotInEnum(string fieldName)
        => new("request.enum.invalid",
            $"O valor '{fieldName}' é inválido.");

    public static Error NotUnique(string listName, string uniqueKeyName)
        => new("request.list.duplicated", $"A lista '{listName}' deve conter campos '{uniqueKeyName}' únicos.");

    public static Error NotPersisted() =>
        new(InternalServerErrorCode, "Os dados não foram persistidos.");

    public static Error InternalServerError() =>
        new(InternalServerErrorCode, "Erro interno do servidor.");

    public static Error InternalServerError(string message) =>
        new(InternalServerErrorCode, $"Erro interno do servidor: {message}.");

    public static Error InternalServerError(Exception ex) =>
        new(InternalServerErrorCode, ex.ToString());

    public static Error ValueIsBelow(string fieldName, string minValue)
        => new("request.value.below_minimum", $"O campo '{fieldName}' deve ter um valor maior ou igual a {minValue}.");

    public static Error ValueIsOutOfLimits(string fieldName, string minValue, string maxValue)
        => new("request.value.out_range", $"O campo '{fieldName}' deve estar entre {minValue} e {maxValue}.");

    public static Error ValueIsInvalid(string fieldName)
        => new("request.value.invalid", $"O valor '{fieldName}' é inválido.");

    public static Error ValueIsNegative(string fieldName)
        => new("request.value.below_zero", $"O valor '{fieldName}' não pode ser negativo.");

    public static Error ValueIsNotAlphanumeric(string fieldName)
        => new("request.value.not_alphanumeric", $"O valor '{fieldName}' deve conter apenas letras e números.");

    public static Error ValueIsNotNumeric(string fieldName)
        => new("request.value.not_numeric", $"O valor '{fieldName}' deve conter apenas números.");

    public static Error ValueLengthIsInvalid(string fieldName, int length)
        => new("request.value.invalid_length", $"O valor '{fieldName}' deve ter {length} dígitos.");

    public static Error EmptyResponse()
        => new("request.response.empty", $"A requisição retornou um corpo vazio.");
    
    public static Error InvalidInterval()
        => new("request.response.empty", $"O intervalo mencionado é inválido.");
}
