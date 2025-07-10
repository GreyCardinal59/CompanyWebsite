using CompanyWebsite.Application.Exceptions;
using Shared;

namespace CompanyWebsite.Application.Employees.Exceptions;

public class EmployeeNotFoundException : NotFoundException
{
    protected EmployeeNotFoundException(Error[] errors)
        : base(errors)
    {
    }
    
    public EmployeeNotFoundException(Guid employeeId)
        : base(new[] { Errors.General.NotFound(employeeId) })
    {
    }
}