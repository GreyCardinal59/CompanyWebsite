using CompanyWebsite.Application.Abstractions;
using CompanyWebsite.Contracts.Employees;
using CompanyWebsite.Contracts.Employees.Dtos;

namespace CompanyWebsite.Application.Employees.Features.UpdateEmployee;

public record UpdateEmployeeCommand(Guid EmployeeId, UpdateEmployeeDto UpdateEmployeeDto) : ICommand;