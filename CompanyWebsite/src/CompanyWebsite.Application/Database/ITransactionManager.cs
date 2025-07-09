using System.Data;

namespace CompanyWebsite.Application.Database;

public interface ITransactionManager // IUnitOfWork
{
    Task<IDbTransaction> BeginTransactionAsync(CancellationToken cancellationToken);
}