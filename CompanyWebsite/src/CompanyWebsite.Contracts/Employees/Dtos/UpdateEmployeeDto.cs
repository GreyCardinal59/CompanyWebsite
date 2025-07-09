namespace CompanyWebsite.Contracts.Employees.Dtos;

public record UpdateEmployeeDto(
    string FullName,
    DateTime BirthDate,
    DateTime HireDate,
    decimal Salary,
    Guid? DepartmentId,
    string? NewDepartmentName);