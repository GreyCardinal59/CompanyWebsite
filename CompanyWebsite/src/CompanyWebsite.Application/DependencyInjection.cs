using CompanyWebsite.Application.Employees;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace CompanyWebsite.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);
        
        services.AddScoped<IEmployeesService, EmployeesService>();

        return services;
    }
}