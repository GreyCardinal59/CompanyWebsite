using CompanyWebsite.Application.Employees;
using CompanyWebsite.Application.Employees.Exceptions;
using CompanyWebsite.Application.Employees.Features.GetEmployees;
using CompanyWebsite.Domain.Departments;
using CompanyWebsite.Domain.Employees;
using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace CompanyWebsite.Infrastructure.Mssql.Repositories;

public class EmployeesRepository(EmployeesDbContext dbContext) : IEmployeesRepository
{
    public async Task<Guid> AddAsync(Employee employee, CancellationToken cancellationToken)
    {
        await dbContext.Employees.AddAsync(employee, cancellationToken);
        
        await dbContext.SaveChangesAsync(cancellationToken);
        
        return employee.Id;
    }

    public async Task<Guid> SaveAsync(Employee employee, CancellationToken cancellationToken)
    {
        dbContext.Employees.Attach(employee);
        await dbContext.SaveChangesAsync(cancellationToken);
        
        return employee.Id;
    }

    public async Task<Guid> DeleteAsync(Guid employeeId, CancellationToken cancellationToken)
    {
        var employee = await dbContext.Employees.FindAsync(new object[] { employeeId }, cancellationToken);
        
        if (employee is null)
        {
            throw new EmployeeNotFoundException(employeeId);
        }
        
        dbContext.Employees.Remove(employee);
        await dbContext.SaveChangesAsync(cancellationToken);
        
        return employeeId;
    }

    public Task<IEnumerable<Employee>> GetEmployeesAsync(GetEmployeesQuery query, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<Result<Employee, Failure>> GetByIdAsync(Guid employeeId, CancellationToken cancellationToken)
    {
        var employee = await dbContext.Employees
            .Include(x => x.Department)
            .FirstOrDefaultAsync(x => x.Id == employeeId, cancellationToken);
        
        if (employee is null)
        {
            return Errors.General.NotFound(employeeId).ToFailure();
        }
        
        return employee;
    }

    public async Task<Department?> GetDepartmentByNameAsync(string name, CancellationToken cancellationToken)
    {
        return await dbContext.Departments
            .FirstOrDefaultAsync(d => d.Name == name, cancellationToken);
    }

    public async Task<Department?> GetDepartmentByIdAsync(Guid departmentId, CancellationToken cancellationToken)
    {
        return await dbContext.Departments
            .FirstOrDefaultAsync(d => d.Id == departmentId, cancellationToken);
    }

    public async Task AddDepartmentAsync(Department department, CancellationToken cancellationToken)
    {
        await dbContext.Departments.AddAsync(department, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
    }
    
    public async Task<IEnumerable<Department>> GetAllDepartmentsAsync(CancellationToken cancellationToken)
    {
        return await dbContext.Departments.ToListAsync(cancellationToken);
    }
}