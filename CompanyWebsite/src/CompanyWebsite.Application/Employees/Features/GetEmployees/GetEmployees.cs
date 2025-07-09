using CompanyWebsite.Application.Abstractions;
using CompanyWebsite.Contracts.Employees.Dtos;
using CompanyWebsite.Contracts.Employees.Responses;
using Microsoft.EntityFrameworkCore;

namespace CompanyWebsite.Application.Employees.Features.GetEmployees;

public class GetEmployees(IEmployeesReadDbContext employeesDbContext) : IQueryHandler<EmployeesResponse, GetEmployeesQuery>
{
    public async Task<EmployeesResponse> Handle(
        GetEmployeesQuery query, 
        CancellationToken cancellationToken)
    {
        var employees = await employeesDbContext.ReadEmployees
            .Include(e => e.Department)
            .Skip((query.Dto.Page - 1) * query.Dto.PageSize)
            .Take(query.Dto.PageSize)
            .ToListAsync(cancellationToken);  
        
        long count = await employeesDbContext.ReadEmployees.LongCountAsync(cancellationToken);

        var employeesDto = employees.Select(e => new EmployeesDto(
            e.Id,
            e.FullName,
            e.BirthDate,
            e.HireDate,
            e.Salary,
            e.Department.Name));
        
        return new EmployeesResponse(employeesDto, count);
    }
}