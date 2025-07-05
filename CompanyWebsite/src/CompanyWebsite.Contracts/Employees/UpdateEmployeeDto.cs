namespace CompanyWebsite.Contracts.Employees;

public record UpdateEmployeeDto(string FullName, DateTime BirthDate, DateTime HireDate, decimal Salary, Guid DepartmentId);