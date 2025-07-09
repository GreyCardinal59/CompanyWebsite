using CompanyWebsite.Domain.Employees;

namespace CompanyWebsite.Application.Employees;

public interface IEmployeesReadDbContext
{
    IQueryable<Employee> ReadEmployees { get; }
}