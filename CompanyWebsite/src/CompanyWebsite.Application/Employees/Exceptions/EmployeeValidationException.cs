using CompanyWebsite.Application.Exceptions;
using Shared;

namespace CompanyWebsite.Application.Employees.Exceptions;

public class EmployeeValidationException : BadRequestException
{
    public EmployeeValidationException(Error[] errors) 
        : base(errors)
    {
    }
}
