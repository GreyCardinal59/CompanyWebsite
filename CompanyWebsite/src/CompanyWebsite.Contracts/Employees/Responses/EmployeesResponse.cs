using CompanyWebsite.Contracts.Employees.Dtos;

namespace CompanyWebsite.Contracts.Employees.Responses;

public record EmployeesResponse(IEnumerable<EmployeesDto> Employees, long TotalCount);