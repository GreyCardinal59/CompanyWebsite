using CompanyWebsite.Contracts.Employees;
using CompanyWebsite.Contracts.Employees.Dtos;
using FluentValidation;

namespace CompanyWebsite.Application.Employees.Features.UpdateEmployee;

public class UpdateEmployeeValidator : AbstractValidator<UpdateEmployeeDto>
{
    public UpdateEmployeeValidator()
    {
        RuleFor(x => x.FullName)
            .NotEmpty().WithMessage("Ф.И.О. не может быть пустым")
            .MaximumLength(50).WithMessage("Имя невалидно");
        
        RuleFor(x => x.BirthDate).NotEmpty();
        
        RuleFor(x => x.HireDate).NotEmpty();
        
        RuleFor(x => x.Salary)
            .NotEmpty().WithMessage("Зарплата не может быть пустой")
            .InclusiveBetween(1, 1000000).WithMessage("Зарплата вне допустимого диапазона");
        
        RuleFor(x => x.DepartmentId).NotEmpty();
    }
}