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

namespace CompanyWebsite.Application.Employees.Features.UpdateEmployee;

public class UpdateEmployeeHandler(
    IEmployeesRepository employeesRepository,
    IValidator<UpdateEmployeeDto> updateEmployeeValidator,
    ILogger<UpdateEmployeeHandler> logger) : ICommandHandler<Guid, UpdateEmployeeCommand>
{
    public async Task<Result<Guid, Failure>> Handle(UpdateEmployeeCommand command, CancellationToken cancellationToken)
    {
        var validationResult = await updateEmployeeValidator.ValidateAsync(command.UpdateEmployeeDto, cancellationToken);
        if (!validationResult.IsValid)
        {
            return validationResult.ToErrors();
        }

        var employeeResult = await employeesRepository.GetByIdAsync(command.EmployeeId, cancellationToken);
        if (employeeResult.IsFailure)
        {
            return employeeResult.Error;
        }
        
        var employee = employeeResult.Value;
        Department department = null!;

        if (!string.IsNullOrEmpty(command.UpdateEmployeeDto.NewDepartmentName))
        {
            var existingDepartment = await employeesRepository.GetDepartmentByNameAsync(command.UpdateEmployeeDto.NewDepartmentName, cancellationToken);
            if (existingDepartment != null)
            {
                return Errors.Departments.AlreadyExists().ToFailure();
            }
            
            department = new Department(
                Guid.NewGuid(),
                command.UpdateEmployeeDto.NewDepartmentName);
            await employeesRepository.AddDepartmentAsync(department, cancellationToken);
        }
        else if (command.UpdateEmployeeDto.DepartmentId.HasValue)
        {
            department = await employeesRepository.GetDepartmentByIdAsync(command.UpdateEmployeeDto.DepartmentId.Value, cancellationToken);
            if (department == null)
            {
                return Errors.General.NotFound(command.UpdateEmployeeDto.DepartmentId.Value).ToFailure();
            }
        }
        
        employee.FullName = command.UpdateEmployeeDto.FullName;
        employee.BirthDate = command.UpdateEmployeeDto.BirthDate;
        employee.HireDate = command.UpdateEmployeeDto.HireDate;
        employee.Salary = command.UpdateEmployeeDto.Salary;
        employee.DepartmentId = department?.Id ?? employee.DepartmentId;
        
        await employeesRepository.SaveAsync(employee, cancellationToken);
        
        logger.LogInformation("Employee updated with id: {employeeId}", command.EmployeeId);
        
        return command.EmployeeId;
    }
}