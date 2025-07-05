namespace CompanyWebsite.Contracts.Employees;

public record CreateEmployeeDto(string FullName, DateTime BirthDate, DateTime HireDate, decimal Salary, Guid DepartmentId);