using CompanyWebsite.Application.Abstractions;
using CompanyWebsite.Contracts.Employees.Dtos;

namespace CompanyWebsite.Application.Employees.Features.GetEmployees;

public record GetEmployeesQuery(GetEmployeesDto Dto) : IQuery;