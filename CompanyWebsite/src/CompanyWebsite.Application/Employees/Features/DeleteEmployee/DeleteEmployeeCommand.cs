using CompanyWebsite.Application.Abstractions;

namespace CompanyWebsite.Application.Employees.Features.DeleteEmployee;

public record DeleteEmployeeCommand(Guid EmployeeId) : ICommand;