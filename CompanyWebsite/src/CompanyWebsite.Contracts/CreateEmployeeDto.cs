namespace CompanyWebsite.Contracts;

public record CreateEmployeeDto(string FullName, DateTime BirthDate, DateTime HireDate, decimal Salary, Guid DepartmentId);