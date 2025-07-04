using CompanyWebsite.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace CompanyWebsite.Presenters;

[ApiController]
[Route("[controller]")]
public class EmployeesController : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateEmployeeDto employeeDto, CancellationToken cancellationToken)
    {
        return Ok("Employee created");
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] GetEmployeesDto request, CancellationToken cancellationToken)
    {
        return Ok("Employees found");
    }

    [HttpGet("{employeeId:guid}")]
    public async Task<IActionResult> GetById([FromRoute] Guid employeeId, CancellationToken cancellationToken)
    {
        return Ok("Employee found");
    }

    [HttpPut("{employeeId:guid}")]
    public async Task<IActionResult> Update([FromRoute] Guid employeeId, [FromBody] UpdateEmployeeDto request, CancellationToken cancellationToken)
    {
        return Ok("Employees is updated");
    }

    [HttpDelete("{employeeId:guid}")]
    public async Task<IActionResult> Delete([FromRoute] Guid employeeId, CancellationToken cancellationToken)
    {
        return Ok("Employee is deleted");
    }
}