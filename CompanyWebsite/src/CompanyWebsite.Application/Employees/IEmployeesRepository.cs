using CompanyWebsite.Application.Employees.Features.GetEmployees;
using CompanyWebsite.Domain.Departments;
using CompanyWebsite.Domain.Employees;
using CSharpFunctionalExtensions;
using Shared;

namespace CompanyWebsite.Application.Employees;

public interface IEmployeesRepository
{
    Task<Guid> AddAsync(Employee employee, CancellationToken cancellationToken);

    Task<Guid> SaveAsync(Employee employee, CancellationToken cancellationToken);

    Task<Guid> DeleteAsync(Guid employeeId, CancellationToken cancellationToken);

    Task<IEnumerable<Employee>> GetEmployeesAsync(GetEmployeesQuery query, CancellationToken cancellationToken);

    Task<Result<Employee, Failure>> GetByIdAsync(Guid employeeId, CancellationToken cancellationToken);
    
    Task<Department?> GetDepartmentByNameAsync(string name, CancellationToken cancellationToken);
    
    Task<Department?> GetDepartmentByIdAsync(Guid departmentId, CancellationToken cancellationToken);
    
    Task AddDepartmentAsync(Department department, CancellationToken cancellationToken);
    
    Task<IEnumerable<Department>> GetAllDepartmentsAsync(CancellationToken cancellationToken);
}