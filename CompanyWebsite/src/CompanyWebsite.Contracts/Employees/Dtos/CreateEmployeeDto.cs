namespace CompanyWebsite.Contracts.Employees.Dtos;

public record CreateEmployeeDto(
    string FullName,
    DateTime BirthDate,
    DateTime HireDate,
    decimal Salary,
    Guid? DepartmentId, // null, если создаем новый отдел
    string? NewDepartmentName); // название нового отдела при создании