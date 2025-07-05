namespace CompanyWebsite.Contracts.Employees;

public record GetEmployeesDto(string Search, int Page, int PageSize);