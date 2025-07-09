using CompanyWebsite.Application.Abstractions;
using CompanyWebsite.Contracts.Employees;
using CompanyWebsite.Contracts.Employees.Dtos;

namespace CompanyWebsite.Application.Employees.Features.CreateEmployee;

public record CreateEmployeeCommand(CreateEmployeeDto EmployeeDto) : ICommand;