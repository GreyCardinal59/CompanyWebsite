using CompanyWebsite.Contracts.Employees;
using CompanyWebsite.Contracts.Employees.Dtos;
using FluentValidation;

namespace CompanyWebsite.Application.Employees.Features.CreateEmployee;

public class CreateEmployeeValidator : AbstractValidator<CreateEmployeeDto>
{
    public CreateEmployeeValidator()
    {
        RuleFor(x => x.FullName)
            .NotEmpty().WithMessage("Ф.И.О. не может быть пустым")
            .MaximumLength(50).WithMessage("Имя невалидно");
        
        RuleFor(x => x.BirthDate).NotEmpty();
        
        RuleFor(x => x.HireDate).NotEmpty();
        
        RuleFor(x => x.Salary)
            .NotEmpty().WithMessage("Зарплата не может быть пустой")
            .InclusiveBetween(1, 1000000).WithMessage("Зарплата вне допустимого диапазона");
        
        // Проверяем, что указан либо DepartmentId, либо NewDepartmentName
        When(x => !x.DepartmentId.HasValue, () => {
            RuleFor(x => x.NewDepartmentName)
                .NotEmpty()
                .WithMessage("Необходимо указать название нового отдела, если не выбран существующий");
        });
        
        When(x => string.IsNullOrEmpty(x.NewDepartmentName), () => {
            RuleFor(x => x.DepartmentId)
                .NotNull()
                .WithMessage("Необходимо выбрать существующий отдел, если не указано название нового");
        });
    }
}