using CompanyWebsite.Application.Database;
using CompanyWebsite.Application.Employees;
using CompanyWebsite.Infrastructure.Mssql.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CompanyWebsite.Infrastructure.Mssql;

public static class DependencyInjection
{
    public static IServiceCollection AddMssqlInfrastructure(
        this IServiceCollection services,
        string connectionString) // IConfiguration configuration
    {
        services.AddDbContext<EmployeesDbContext>(options => 
            options.UseSqlServer(connectionString));
        
        services.AddScoped<IEmployeesReadDbContext>(provider => 
            provider.GetRequiredService<EmployeesDbContext>());
        
        // string connectionString = configuration.GetConnectionString("DefaultConnection")
        //                           ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
        // services.AddDbContext<EmployeesDbContext>(options => options.UseSqlServer(connectionString));
        services.AddScoped<IEmployeesRepository, EmployeesRepository>();

        services.AddScoped<ITransactionManager>(sp => new TransactionManager(connectionString));
        
        return services; 
    }
}