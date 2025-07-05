using CompanyWebsite.Domain.Employees;

namespace CompanyWebsite.Application.Employees;

public interface IEmployeesRepository
{
    Task<Guid> AddAsync(Employee employee, CancellationToken cancellationToken);

    Task<Guid> UpdateAsync(Employee employee, CancellationToken cancellationToken);

    Task<Guid> DeleteAsync(Guid employeeId, CancellationToken cancellationToken);

    Task<Employee> GetByIdAsync(Guid employeeId, CancellationToken cancellationToken);
}