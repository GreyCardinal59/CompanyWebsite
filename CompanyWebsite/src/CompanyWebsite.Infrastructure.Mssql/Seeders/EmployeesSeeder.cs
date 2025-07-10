using CompanyWebsite.Domain.Departments;
using CompanyWebsite.Domain.Employees;

namespace CompanyWebsite.Infrastructure.Mssql.Seeders;

public class EmployeesSeeder(EmployeesDbContext dbContext)
{
    public void Seed()
    {
        SeedDepartments();
        SeedEmployees();
    }

    private void SeedDepartments()
    {
        if (dbContext.Departments.Any())
            return;

        var departments = new List<Department>
        {
            new(Guid.NewGuid(), "IT"),
            new(Guid.NewGuid(), "HR"),
            new(Guid.NewGuid(), "Маркетинг"),
            new(Guid.NewGuid(), "Финансы"),
            new(Guid.NewGuid(), "Операции")
        };

        dbContext.Departments.AddRange(departments);
        dbContext.SaveChanges();
    }

    private void SeedEmployees()
    {
        if (dbContext.Employees.Any())
            return;

        var departments = dbContext.Departments.ToList();
        
        if (!departments.Any())
            return;

        var itDepartment = departments.First(d => d.Name == "IT");
        var hrDepartment = departments.First(d => d.Name == "HR");
        var marketingDepartment = departments.First(d => d.Name == "Маркетинг");

        var employees = new List<Employee>
        {
            new(
                Guid.NewGuid(),
                "Иванов Иван Иванович",
                new DateTime(1985, 5, 15),
                new DateTime(2020, 3, 1),
                120000,
                itDepartment.Id
            ),
            new(
                Guid.NewGuid(),
                "Петрова Мария Александровна",
                new DateTime(1990, 8, 21),
                new DateTime(2021, 6, 15),
                95000,
                hrDepartment.Id
            ),
            new(
                Guid.NewGuid(),
                "Смирнов Алексей Петрович",
                new DateTime(1988, 3, 10),
                new DateTime(2019, 9, 5),
                110000,
                marketingDepartment.Id
            )
        };

        dbContext.Employees.AddRange(employees);
        dbContext.SaveChanges();
    }
}