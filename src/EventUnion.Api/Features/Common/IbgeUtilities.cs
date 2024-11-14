using rmdev.ibge.localidades;

namespace EventUnion.Api.Features.Common;

public class IbgeUtilities
{
    public static async Task<List<string>> GetCitiesByState(string state)
    {
        var api = new IBGEClientFactory().Build();
        var cities = await api.BuscarMunicipioPorUFAsync(GetUfIdByState(state));

        return cities.Select(x => x.Nome).ToList();
    }

    private static int GetUfIdByState(string state)
    {
        return state switch
        {
            "Rondônia" => 11,
            "Acre" => 12,
            "Amazonas" => 13,
            "Roraima" => 14,
            "Pará" => 15,
            "Amapá" => 16,
            "Tocantins" => 17,
            "Maranhão" => 21,
            "Piauí" => 22,
            "Ceará" => 23,
            "Rio Grande do Norte" => 24,
            "Paraíba" => 25,
            "Pernambuco" => 26,
            "Alagoas" => 27,
            "Sergipe" => 28,
            "Bahia" => 29,
            "Minas Gerais" => 31,
            "Espírito Santo" => 32,
            "Rio de Janeiro" => 33,
            "São Paulo" => 35,
            "Paraná" => 41,
            "Santa Catarina" => 42,
            "Rio Grande do Sul" => 43,
            "Mato Grosso do Sul" => 50,
            "Mato Grosso" => 51,
            "Goiás" => 52,
            "Distrito Federal" => 53,
            _ => -1
        };
    }

    public static List<string> GetStates()
    {
        return ["Rondônia", "Acre", "Amazonas", "Roraima", "Pará", "Amapá", "Tocantins",
                "Maranhão", "Piauí", "Ceará", "Rio Grande do Norte", "Paraíba", "Pernambuco",
                "Alagoas", "Sergipe", "Bahia", "Minas Gerais", "Espírito Santo", "Rio de Janeiro",
                "São Paulo", "Paraná", "Santa Catarina", "Rio Grande do Sul", "Mato Grosso do Sul",
                "Mato Grosso", "Goiás", "Distrito Federal"];
    }
    
    public static List<string> GetCountries()
    {
        return ["Brasil"];
    }
}