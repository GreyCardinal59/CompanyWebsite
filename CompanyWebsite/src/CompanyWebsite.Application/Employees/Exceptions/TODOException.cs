using CompanyWebsite.Application.Exceptions;
using Shared;

namespace CompanyWebsite.Application.Employees.Exceptions;

public class TODOException : BadRequestException
{
    public TODOException()
        : base([Errors.Employees.TODO()])
    {
    }
}