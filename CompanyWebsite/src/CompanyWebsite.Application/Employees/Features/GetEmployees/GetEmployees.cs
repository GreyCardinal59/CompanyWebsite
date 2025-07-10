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
        var queryable = employeesDbContext.ReadEmployees
            .Include(e => e.Department)
            .AsQueryable();
        
        queryable = ApplyFilters(queryable, query.Dto);
        
        queryable = ApplySorting(queryable, query.Dto);
        
        long count = await queryable.LongCountAsync(cancellationToken);
        
        var employees = await queryable
            .Skip((query.Dto.Page - 1) * query.Dto.PageSize)
            .Take(query.Dto.PageSize)
            .ToListAsync(cancellationToken);  

        var employeesDto = employees.Select(e => new EmployeesDto(
            e.Id,
            e.FullName,
            e.BirthDate,
            e.HireDate,
            e.Salary,
            e.Department?.Name ?? string.Empty));
        
        return new EmployeesResponse(employeesDto, count);
    }
    
    private IQueryable<Domain.Employees.Employee> ApplyFilters(
        IQueryable<Domain.Employees.Employee> queryable, 
        GetEmployeesDto dto)
    {
        // Фильтр по отделу
        if (!string.IsNullOrWhiteSpace(dto.DepartmentFilter))
        {
            queryable = queryable.Where(e => e.Department.Name.Contains(dto.DepartmentFilter));
        }
        
        // Фильтр по ФИО
        if (!string.IsNullOrWhiteSpace(dto.FullNameFilter))
        {
            queryable = queryable.Where(e => e.FullName.Contains(dto.FullNameFilter));
        }
        
        // Фильтр по дате рождения
        if (!string.IsNullOrWhiteSpace(dto.BirthDateFilter))
        {
            if (DateTime.TryParse(dto.BirthDateFilter, out var birthDate))
            {
                queryable = queryable.Where(e => e.BirthDate.Date == birthDate.Date);
            }
        }
        
        // Фильтр по дате приема на работу
        if (!string.IsNullOrWhiteSpace(dto.HireDateFilter))
        {
            if (DateTime.TryParse(dto.HireDateFilter, out var hireDate))
            {
                queryable = queryable.Where(e => e.HireDate.Date == hireDate.Date);
            }
        }
        
        // Фильтр по зарплате
        if (!string.IsNullOrWhiteSpace(dto.SalaryFilter) && decimal.TryParse(dto.SalaryFilter, out var salary))
        {
            queryable = queryable.Where(e => e.Salary == salary);
        }
        
        return queryable;
    }
    
    private IQueryable<Domain.Employees.Employee> ApplySorting(
        IQueryable<Domain.Employees.Employee> queryable, 
        GetEmployeesDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.SortBy))
            return queryable;
            
        bool isAscending = string.IsNullOrWhiteSpace(dto.SortDirection) || 
                           dto.SortDirection.Equals("asc", StringComparison.OrdinalIgnoreCase);
            
        queryable = dto.SortBy.ToLowerInvariant() switch
        {
            "departmentname" => isAscending 
                ? queryable.OrderBy(e => e.Department.Name)
                : queryable.OrderByDescending(e => e.Department.Name),
                
            "fullname" => isAscending 
                ? queryable.OrderBy(e => e.FullName)
                : queryable.OrderByDescending(e => e.FullName),
                
            "birthdate" => isAscending 
                ? queryable.OrderBy(e => e.BirthDate)
                : queryable.OrderByDescending(e => e.BirthDate),
                
            "hiredate" => isAscending 
                ? queryable.OrderBy(e => e.HireDate)
                : queryable.OrderByDescending(e => e.HireDate),
                
            "salary" => isAscending 
                ? queryable.OrderBy(e => e.Salary)
                : queryable.OrderByDescending(e => e.Salary),
                
            _ => queryable
        };
        
        return queryable;
    }
}