using CompanyWebsite.Application.Employees;
using CompanyWebsite.Infrastructure.Mssql.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace CompanyWebsite.Infrastructure.Mssql;

public static class DependencyInjection
{
    public static IServiceCollection AddMssqlInfrastructure(
        this IServiceCollection services) // IConfiguration configuration
    {
        services.AddDbContext<EmployeesDbContext>();
        
        // string connectionString = configuration.GetConnectionString("DefaultConnection")
        //                           ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
        // services.AddDbContext<EmployeesDbContext>(options => options.UseSqlServer(connectionString));
        services.AddScoped<IEmployeesRepository, EmployeesRepository>(); 
        
        return services; 
    }
}