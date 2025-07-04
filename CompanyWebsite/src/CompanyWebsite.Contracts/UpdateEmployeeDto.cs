namespace CompanyWebsite.Contracts;

public record UpdateEmployeeDto(string FullName, DateTime BirthDate, DateTime HireDate, decimal Salary, Guid DepartmentId);