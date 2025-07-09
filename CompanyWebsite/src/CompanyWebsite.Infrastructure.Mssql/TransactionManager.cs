using System.Data;
using CompanyWebsite.Application.Database;
using Microsoft.Data.SqlClient;

namespace CompanyWebsite.Infrastructure.Mssql;

public class TransactionManager(string connectionString) : ITransactionManager
{
    public async Task<IDbTransaction> BeginTransactionAsync(CancellationToken cancellationToken)
    {
        var connection = new SqlConnection(connectionString);
        await connection.OpenAsync(cancellationToken);
        return await connection.BeginTransactionAsync(cancellationToken);
    }
}