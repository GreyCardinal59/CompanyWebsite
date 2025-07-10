using CompanyWebsite.Infrastructure.Mssql;
using CompanyWebsite.Infrastructure.Mssql.Seeders;
using Microsoft.EntityFrameworkCore;

namespace CompanyWebsite.Web;

public static class SeederExtensions
{
    public static WebApplication ApplyMigrations(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var services = scope.ServiceProvider;
        
        try
        {
            var context = services.GetRequiredService<EmployeesDbContext>();
            
            context.Database.Migrate();
            
            var employeesSeeder = new EmployeesSeeder(context);
            employeesSeeder.Seed();
            
            app.Logger.LogInformation("Database migrations applied and data seeded successfully.");
        }
        catch (Exception ex)
        {
            app.Logger.LogError(ex, "An error occurred while applying migrations or seeding the database.");
        }
        
        return app;
    }
}