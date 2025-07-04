namespace CompanyWebsite.Contracts;

public record GetEmployeesDto(string Search, int Page, int PageSize);