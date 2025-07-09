namespace CompanyWebsite.Contracts.Employees.Dtos;

public record EmployeesDto(
    Guid Id,
    string FullName,
    DateTime BirthDate,
    DateTime HireDate,
    decimal Salary,
    string DepartmentName);