using CompanyWebsite.Contracts.Employees;
using CompanyWebsite.Contracts.Employees.Dtos;
using FluentValidation;

namespace CompanyWebsite.Application.Employees.Features.AddDepartment;

public class AddDepartmentValidator: AbstractValidator<AddDepartmentDto>
{
    public AddDepartmentValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Название не может быть пустым")
            .MaximumLength(50).WithMessage("Название невалидно");
    }
}