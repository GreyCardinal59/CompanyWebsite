using CompanyWebsite.Contracts.Employees;
using FluentValidation;

namespace CompanyWebsite.Application.Employees;

public class CreateEmployeeValidator : AbstractValidator<CreateEmployeeDto>
{
    public CreateEmployeeValidator()
    {
        RuleFor(x => x.FullName).NotEmpty().MaximumLength(50).WithMessage("Имя невалидно");
        RuleFor(x => x.BirthDate).NotEmpty();
        RuleFor(x => x.HireDate).NotEmpty();
        RuleFor(x => x.Salary).NotEmpty().InclusiveBetween(1, 1000000);
        RuleFor(x => x.DepartmentId).NotEmpty();
    }
}