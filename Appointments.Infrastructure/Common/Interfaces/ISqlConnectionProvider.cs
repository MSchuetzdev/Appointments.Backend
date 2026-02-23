using System.Data.Common;

namespace Appointments.Infrastructure.Common.Interfaces;

/// <summary>
/// Provider for sql connection
/// </summary>
public interface ISqlConnectionProvider
{
    /// <summary>
    /// Creates a new connection to database
    /// </summary>
    /// <param name="connectionName"></param>
    /// <returns></returns>
    Task<DbConnection> GetConnection(string connectionName = "default"); 
}