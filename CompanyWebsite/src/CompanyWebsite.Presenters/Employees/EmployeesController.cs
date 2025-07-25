using CompanyWebsite.Application.Abstractions;
using CompanyWebsite.Application.Employees;
using CompanyWebsite.Application.Employees.Features.AddDepartment;
using CompanyWebsite.Application.Employees.Features.CreateEmployee;
using CompanyWebsite.Application.Employees.Features.DeleteEmployee;
using CompanyWebsite.Application.Employees.Features.GetEmployees;
using CompanyWebsite.Application.Employees.Features.UpdateEmployee;
using CompanyWebsite.Contracts.Employees;
using CompanyWebsite.Contracts.Employees.Dtos;
using CompanyWebsite.Contracts.Employees.Responses;
using CompanyWebsite.Presenters.ResponseExtensions;
using Microsoft.AspNetCore.Mvc;

namespace CompanyWebsite.Presenters.Employees;

[ApiController]
[Route("api/[controller]")]
public class EmployeesController() : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create(
        [FromServices] ICommandHandler<Guid, CreateEmployeeCommand> handler,
        [FromBody] CreateEmployeeDto request,
        CancellationToken cancellationToken)
    {
        var command = new CreateEmployeeCommand(request);
        
        var result = await handler.Handle(command, cancellationToken); 
        return result.IsFailure ? result.Error.ToResponse() : Ok(result.Value);
    }

    [HttpGet]
    public async Task<IActionResult> Get(
        [FromServices] IQueryHandler<EmployeesResponse, GetEmployeesQuery> handler,
        [FromQuery] GetEmployeesDto request,
        CancellationToken cancellationToken)
    {
        var query = new GetEmployeesQuery(request);
        
        var result = await handler.Handle(query, cancellationToken);

        return Ok(result);
    } 

    [HttpGet("{employeeId:guid}")]
    public async Task<IActionResult> GetById([FromRoute] Guid employeeId, CancellationToken cancellationToken)
    {
        return Ok("Employee found");
    }

    [HttpPut("{employeeId:guid}")]
    public async Task<IActionResult> Update(
        [FromServices] ICommandHandler<Guid, UpdateEmployeeCommand> handler,
        [FromRoute] Guid employeeId,
        [FromBody] UpdateEmployeeDto request,
        CancellationToken cancellationToken)
    {
        var command = new UpdateEmployeeCommand(employeeId, request);
        
        var result = await handler.Handle(command, cancellationToken);
        return result.IsFailure ? result.Error.ToResponse() : Ok(result.Value);
    }

    [HttpDelete("{employeeId:guid}")]
    public async Task<IActionResult> Delete(
        [FromServices] ICommandHandler<Guid, DeleteEmployeeCommand> handler,
        [FromRoute] Guid employeeId,
        CancellationToken cancellationToken)
    {
        var command = new DeleteEmployeeCommand(employeeId);

        var result = await handler.Handle(command, cancellationToken);
        return result.IsFailure ? result.Error.ToResponse() : Ok(result.Value);
    }
    
    [HttpPost("{employeeId:guid}/departments")]
    public async Task<IActionResult> AddDepartment(
        [FromServices] ICommandHandler<Guid, AddDepartmentCommand> handler,
        [FromRoute] Guid employeeId,
        [FromBody] AddDepartmentDto request,
        CancellationToken cancellationToken)
    {
        var command = new AddDepartmentCommand(employeeId, request);
        
        var result = await handler.Handle(command, cancellationToken);
        return result.IsFailure ? result.Error.ToResponse() : Ok(result.Value);
    }
}