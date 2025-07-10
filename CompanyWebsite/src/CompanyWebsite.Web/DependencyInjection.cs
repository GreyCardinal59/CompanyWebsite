using System.Collections.Immutable;
using System.ComponentModel;
using CompanyWebsite.Application;
using CompanyWebsite.Infrastructure.Mssql;

namespace CompanyWebsite.Web;

public static class DependencyInjection
{
    public static IServiceCollection AddProgramDependencies(
        this IServiceCollection services, IConfiguration configuration) => 
        services
            .AddWebDependencies()
            .AddApplication()
            .AddMssqlInfrastructure(configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found."));
    
    private static IServiceCollection AddWebDependencies(this IServiceCollection services)
    { 
        services.AddControllers().AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.Converters.Add(new DateTimeConverter("yyyy-MM-dd", "dd.MM.yyyy"));
        });
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        
        return services;
    }
}