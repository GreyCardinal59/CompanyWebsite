namespace CompanyWebsite.Contracts.Employees.Dtos;

public record GetEmployeesDto(int Page, int PageSize, string? Search);