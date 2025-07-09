using CompanyWebsite.Application.Abstractions;
using CompanyWebsite.Application.Database;
using CompanyWebsite.Application.Extensions;
using CompanyWebsite.Contracts.Employees.Dtos;
using CompanyWebsite.Domain.Departments;
using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using Shared;

namespace CompanyWebsite.Application.Employees.Features.AddDepartment;

public class AddDepartmentHandler(
    IEmployeesRepository employeesRepository,
    IValidator<AddDepartmentDto> addDepartmentValidator,
    ITransactionManager transactionManager,
    ILogger<AddDepartmentHandler> logger) : ICommandHandler<Guid, AddDepartmentCommand>
{
    public async Task<Result<Guid, Failure>> Handle(AddDepartmentCommand command, CancellationToken cancellationToken)
    {
        var validationResult = await addDepartmentValidator.ValidateAsync(command.AddDepartmentDto, cancellationToken);
        if (!validationResult.IsValid)
        {
            return validationResult.ToErrors();
        }
        
        var transaction = await transactionManager.BeginTransactionAsync(cancellationToken);
        
        var employeeResult = await employeesRepository.GetByIdAsync(command.EmployeeId, cancellationToken);
        if (employeeResult.IsFailure)
        {
            return employeeResult.Error;
        }
        
        var employee = employeeResult.Value;
        
        var existingDepartment = await employeesRepository.GetDepartmentByNameAsync(command.AddDepartmentDto.Name, cancellationToken);
        Department department;
        if (existingDepartment != null)
        {
            department = existingDepartment;
        }
        else
        {
            department = new Department(
                Guid.NewGuid(),
                command.AddDepartmentDto.Name);
            await employeesRepository.AddDepartmentAsync(department, cancellationToken);
        }
        
        employee.DepartmentId = department.Id;
        
        await employeesRepository.SaveAsync(employee, cancellationToken);

        transaction.Commit();
        
        logger.LogInformation("Department added with id: {departmentId}", department.Id);
        
        return department.Id;
    }
}