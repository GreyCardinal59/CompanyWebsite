using CompanyWebsite.Application.Employees;
using CompanyWebsite.Contracts.Employees.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace CompanyWebsite.Presenters.Employees;

[ApiController]
[Route("api/[controller]")]
public class DepartmentsController(IEmployeesRepository employeesRepository) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var departments = await employeesRepository.GetAllDepartmentsAsync(cancellationToken);
        
        var result = departments.Select(d => new DepartmentDto(d.Id, d.Name)).ToList();
        
        return Ok(result);
    }
} 