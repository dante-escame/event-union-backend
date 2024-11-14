using System.Data;

namespace EventUnion.Domain.Common.Interfaces;

public interface IDbConnectionFactory
{
    IDbConnection CreateOpenConnection();
}