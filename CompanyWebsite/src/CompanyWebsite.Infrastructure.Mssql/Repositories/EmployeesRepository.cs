using CompanyWebsite.Application.Employees;
using CompanyWebsite.Domain.Employees;
using Microsoft.EntityFrameworkCore;

namespace CompanyWebsite.Infrastructure.Mssql.Repositories;

public class EmployeesRepository(EmployeesDbContext dbContext) : IEmployeesRepository
{
    public async Task<Guid> AddAsync(Employee employee, CancellationToken cancellationToken)
    {
        await dbContext.Employees.AddAsync(employee, cancellationToken);
        
        await dbContext.SaveChangesAsync(cancellationToken);
        
        return employee.Id;
    }

    public async Task<Guid> UpdateAsync(Employee employee, CancellationToken cancellationToken)
    {
        return employee.Id;
    }

    public async Task<Guid> DeleteAsync(Guid employeeId, CancellationToken cancellationToken)
    {
        return employeeId;
    }

    public async Task<Employee> GetByIdAsync(Guid employeeId, CancellationToken cancellationToken)
    {
        var employee = await dbContext.Employees
            .FirstOrDefaultAsync(x => x.Id == employeeId, cancellationToken);
        
        return employee;
    }
}