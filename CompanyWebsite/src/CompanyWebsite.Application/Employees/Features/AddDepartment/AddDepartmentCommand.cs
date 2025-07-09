using CompanyWebsite.Application.Abstractions;
using CompanyWebsite.Contracts.Employees;
using CompanyWebsite.Contracts.Employees.Dtos;

namespace CompanyWebsite.Application.Employees.Features.AddDepartment;

public record AddDepartmentCommand(Guid EmployeeId, AddDepartmentDto AddDepartmentDto) : ICommand;