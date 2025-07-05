using CompanyWebsite.Contracts.Employees;
using CompanyWebsite.Domain.Employees;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace CompanyWebsite.Application.Employees;

public class EmployeesService(
    IEmployeesRepository employeesRepository,
    IValidator<CreateEmployeeDto> validator,
    ILogger<EmployeesService> logger) : IEmployeesService
{
    public async Task<Guid> Create(CreateEmployeeDto request, CancellationToken cancellationToken)
    {
        // Валидация входных данных
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        // Валидация бизнес-логики

        // Создание сущности
        var employeeId = Guid.NewGuid();

        var employee = new Employee(
            employeeId,
            request.FullName,
            request.BirthDate,
            request.HireDate,
            request.Salary,
            request.DepartmentId);

        // Сохранение в БД
        await employeesRepository.AddAsync(employee, cancellationToken);

        // Логирование (успех, ошибка)
        logger.LogInformation("Employee created with id: {employeeId}", employeeId);
        
        return employeeId;
    }

    // public async Task Update(Guid employeeId, UpdateEmployeeDto request, CancellationToken cancellationToken)
    // {
    //     return;
    // }
    //
    // public async Task Delete(Guid employeeId, CancellationToken cancellationToken)
    // {
    //     return;
    // }
}