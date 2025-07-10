namespace CompanyWebsite.Contracts.Employees.Dtos;

public record GetEmployeesDto(
    int Page, 
    int PageSize, 
    string? Search = null,
    string? DepartmentFilter = null,
    string? FullNameFilter = null,
    string? BirthDateFilter = null,
    string? HireDateFilter = null,
    string? SalaryFilter = null,
    string? SortBy = null,
    string? SortDirection = null);