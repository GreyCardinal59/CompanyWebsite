using Shared;

namespace CompanyWebsite.Application.Employees.Exceptions;

public partial class Errors
{
    public static class General
    {
        public static Error NotFound(Guid id) 
            => Error.Failure("record.not.found", "Запись по ID {id} не найдена");
    }
    
    public static class Employees
    {
        public static Error TODO() 
            => Error.Failure("TODO", "Нельзя ...");
    }

    public static class Departments
    {
        public static Error AlreadyExists()
            => Error.Failure("value.is.conflict", "Отдел с таким именем уже существует");
        
        public static Error Required()
            => Error.Failure("value.is.required", "Отдел не может быть пустым");
    }
}