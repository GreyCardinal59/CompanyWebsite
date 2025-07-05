using CompanyWebsite.Contracts.Employees;

namespace CompanyWebsite.Application.Employees;

public interface IEmployeesService
{
    Task<Guid> Create(CreateEmployeeDto request, CancellationToken cancellationToken);
}