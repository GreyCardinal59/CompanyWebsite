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
    ILogger<UpdateEmployeeHandler> logger)
{
    public async Task<Result<Employee, Failure>> Update(Guid employeeId, UpdateEmployeeDto request, CancellationToken cancellationToken)
    {
        var validationResult = await updateEmployeeValidator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            return validationResult.ToErrors();
        }

        var employeeResult = await employeesRepository.GetByIdAsync(employeeId, cancellationToken);
        if (employeeResult.IsFailure)
        {
            return employeeResult.Error;
        }
        
        var employee = employeeResult.Value;
        Department department = null;

        if (!string.IsNullOrEmpty(request.NewDepartmentName))
        {
            var existingDepartment = await employeesRepository.GetDepartmentByNameAsync(request.NewDepartmentName, cancellationToken);
            if (existingDepartment != null)
            {
                return Errors.Departments.AlreadyExists().ToFailure();
            }
            
            department = new Department(
                Guid.NewGuid(),
                request.NewDepartmentName);
            await employeesRepository.AddDepartmentAsync(department, cancellationToken);
        }
        else if (request.DepartmentId.HasValue)
        {
            department = await employeesRepository.GetDepartmentByIdAsync(request.DepartmentId.Value, cancellationToken);
            if (department == null)
            {
                return Errors.General.NotFound(request.DepartmentId.Value).ToFailure();
            }
        }
        
        employee.FullName = request.FullName;
        employee.BirthDate = request.BirthDate;
        employee.HireDate = request.HireDate;
        employee.Salary = request.Salary;
        employee.DepartmentId = department?.Id ?? employee.DepartmentId;
        
        await employeesRepository.SaveAsync(employee, cancellationToken);
        
        logger.LogInformation("Employee updated with id: {employeeId}", employeeId);
        
        return employee;
    }
}