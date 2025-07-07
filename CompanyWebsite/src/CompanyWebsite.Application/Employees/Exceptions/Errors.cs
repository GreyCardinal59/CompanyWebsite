using Shared;

namespace CompanyWebsite.Application.Employees.Exceptions;

public partial class Errors
{
    public static class Employees
    {
        public static Error TODO() 
            => Error.Failure("TODO", "Нельзя ...");
    }
}