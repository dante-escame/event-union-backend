using System.Diagnostics.CodeAnalysis;

namespace EventUnion.CommonResources.Response;

[ExcludeFromCodeCoverage]
public static class StandardMessage
{
    public static string CommandSuccess()
        => $"Operação realizada com sucesso!";

    public static string CommandFail()
        => $"Falha ao realizar operação!";

    public static string QuerySuccess()
        => "Dados obtidos com sucesso!";

    public static string QueryFail()
        => "Falha ao obter dados!";

    public static string NotFound()
        => "Nenhum dado foi encontrado!";

    public static string CreateSuccess(string entityName)
        => string.IsNullOrEmpty(entityName)
            ? "Recurso criado com sucesso!"
            : $"{entityName} gerado(a) com sucesso!";

    public static string CreateFail(string entityName)
        => string.IsNullOrEmpty(entityName)
            ? "Falha ao criar novo recurso!"
            : $"Falha ao gerar {entityName}!";

    public static string UpdateSuccess()
        => "Dados atualizados com sucesso!";

    public static string UpdateFail()
        => "Falha ao atualizar os dados!";

    public static string DeleteSuccess()
        => "Dados excluídos com sucesso!";

    public static string DeleteFail()
        => "Falha ao excluir os dados!";
    
    public static string RequestFail()
        => "Falha ao processar requisição!";
}