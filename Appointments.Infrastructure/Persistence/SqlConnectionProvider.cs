using System.Data.Common;
using System.Data.SqlClient;
using Appointments.Infrastructure.Common.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace Appointments.Infrastructure.Persistence;

/// <inheritdoc/>>
public class SqlConnectionProvider(IConfiguration configuration) : ISqlConnectionProvider
{
    /// <inheritdoc/>>
    public async Task<DbConnection> GetConnection(string connectionName)
    {
        var connectionString = configuration.GetConnectionString(connectionName);

        if (string.IsNullOrEmpty(connectionString))
        {
            throw new Exception($"Connectionstring: {connectionString} was not found");
        }

        var connection = new NpgsqlConnection(connectionString);

        await connection.OpenAsync();
        return connection;
    }
}