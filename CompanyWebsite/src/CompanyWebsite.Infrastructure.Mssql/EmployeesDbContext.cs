using CompanyWebsite.Domain.Departments;
using CompanyWebsite.Domain.Employees;
using Microsoft.EntityFrameworkCore;

namespace CompanyWebsite.Infrastructure.Mssql;

public class EmployeesDbContext(DbContextOptions<EmployeesDbContext> options) : DbContext(options)
{
    public DbSet<Employee> Employees { get; set; }
    
    public DbSet<Department> Departments { get; set; }
}