using System.Data;
using EventUnion.Domain.Common.Interfaces;
using Microsoft.Extensions.Options;
using Npgsql;

namespace EventUnion.Infrastructure;

public class DbConnectionFactory(IOptions<ConnectionStringOptions> connectionStringsOptions)
    : IDbConnectionFactory
{
    private readonly string _connectionString = connectionStringsOptions.Value.ConnectionString!;

    public IDbConnection CreateOpenConnection()
    {
        var connection = new NpgsqlConnection(_connectionString);

        connection.Open();

        return connection;
    }
}