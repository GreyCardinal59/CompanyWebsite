using CompanyWebsite.Application.Abstractions;
using CompanyWebsite.Application.Employees.Exceptions;
using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using Shared;

namespace CompanyWebsite.Application.Employees.Features.DeleteEmployee;

public class DeleteEmployeeHandler(
    IEmployeesRepository employeesRepository,
    ILogger<DeleteEmployeeHandler> logger) : ICommandHandler<Guid, DeleteEmployeeCommand>
{
    public async Task<Result<Guid, Failure>> Handle(DeleteEmployeeCommand command, CancellationToken cancellationToken)
    {
        var employeeResult = await employeesRepository.GetByIdAsync(command.EmployeeId, cancellationToken);
        if (employeeResult.IsFailure)
        {
            return Errors.General.NotFound(command.EmployeeId).ToFailure();
        }
        
        var employeeId = await employeesRepository.DeleteAsync(employeeResult.Value.Id, cancellationToken);
        
        logger.LogInformation("Удален сотрудник с Id: {employeeId}", employeeId);
        
        return employeeId;
    }
}