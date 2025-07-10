using CompanyWebsite.Application.Abstractions;
using CompanyWebsite.Application.Employees.Exceptions;
using CompanyWebsite.Application.Extensions;
using CompanyWebsite.Contracts.Employees;
using CompanyWebsite.Contracts.Employees.Dtos;
using CompanyWebsite.Domain.Departments;
using CompanyWebsite.Domain.Employees;
using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using Shared;

namespace CompanyWebsite.Application.Employees.Features.CreateEmployee;

public class CreateEmployeeHandler(
    IEmployeesRepository employeesRepository,
    IValidator<CreateEmployeeDto> validator,
    ILogger<CreateEmployeeHandler> logger) : ICommandHandler<Guid, CreateEmployeeCommand>
{
    public async Task<Result<Guid, Failure>> Handle(CreateEmployeeCommand command, CancellationToken cancellationToken)
    {
        var validationResult = await validator.ValidateAsync(command.EmployeeDto, cancellationToken);
        if (!validationResult.IsValid)
        {
            return validationResult.ToErrors();
        }
        
        Department department = null;

        if (!string.IsNullOrEmpty(command.EmployeeDto.NewDepartmentName))
        {
            var existingDepartment = await employeesRepository.GetDepartmentByNameAsync(command.EmployeeDto.NewDepartmentName, cancellationToken);
            if (existingDepartment != null)
            {
                return Errors.Departments.AlreadyExists().ToFailure();
            }
            
            department = new Department(
                Guid.NewGuid(),
                command.EmployeeDto.NewDepartmentName);
            await employeesRepository.AddDepartmentAsync(department, cancellationToken);
        }
        else if (command.EmployeeDto.DepartmentId.HasValue)
        {
            department = await employeesRepository.GetDepartmentByIdAsync(command.EmployeeDto.DepartmentId.Value, cancellationToken);
            if (department == null)
            {
                return Errors.General.NotFound(command.EmployeeDto.DepartmentId.Value).ToFailure();
            }
        }
        else
        {
            return Errors.Departments.Required().ToFailure();
        }
        
        var employeeId = Guid.NewGuid();

        var employee = new Employee(
            employeeId,
            command.EmployeeDto.FullName,
            command.EmployeeDto.BirthDate,
            command.EmployeeDto.HireDate,
            command.EmployeeDto.Salary,
            department.Id);
        
        await employeesRepository.AddAsync(employee, cancellationToken);
        
        logger.LogInformation("Employee created with id: {employeeId}", employeeId);

        return employeeId;
    }
}