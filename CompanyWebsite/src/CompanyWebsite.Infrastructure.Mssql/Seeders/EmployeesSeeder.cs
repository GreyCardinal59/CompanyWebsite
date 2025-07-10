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
            new(Guid.NewGuid(), "Операции"),
        };

        dbContext.Departments.AddRange(departments);
        dbContext.SaveChanges();
    }

    private void SeedEmployees()
    {
        if (dbContext.Employees.Any())
            return;

        var departments = dbContext.Departments.ToList();
        
        if (departments.Count == 0)
            return;

        var itDepartment = departments.First(d => d.Name == "IT");
        var hrDepartment = departments.First(d => d.Name == "HR");
        var marketingDepartment = departments.First(d => d.Name == "Маркетинг");
        var financesDepartment = departments.First(d => d.Name == "Финансы");
        var operationsDepartment = departments.First(d => d.Name == "Операции");

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
                new DateTime(1945, 8, 21),
                new DateTime(2021, 6, 15),
                95000,
                hrDepartment.Id
            ),
            new(
                Guid.NewGuid(),
                "Смирнова Елена Павловна",
                new DateTime(1976, 3, 10),
                new DateTime(2019, 9, 5),
                110000,
                marketingDepartment.Id
            ),
            new(
                Guid.NewGuid(),
                "Иванов Илья Сергеевич",
                new DateTime(1985, 5, 15),
                new DateTime(2020, 1, 10),
                95000,
                financesDepartment.Id
            ),
            new(
                Guid.NewGuid(),
                "Петрова Елена Петровна",
                new DateTime(1990, 8, 21),
                new DateTime(2021, 6, 15),
                120000,
                operationsDepartment.Id
            ),
            new(
                Guid.NewGuid(),
                "Смирнов Алексей Петрович",
                new DateTime(1988, 3, 10),
                new DateTime(2019, 9, 5),
                110000,
                marketingDepartment.Id
            ),
            new(
                Guid.NewGuid(),
                "Иванов Игорь Олегович",
                new DateTime(1952, 5, 15),
                new DateTime(2020, 1, 10),
                95000,
                financesDepartment.Id
            ),
            new(
                Guid.NewGuid(),
                "Петрова Елена Петровна",
                new DateTime(1990, 7, 14),
                new DateTime(2021, 6, 15),
                120000,
                operationsDepartment.Id
            ),
            new(
                Guid.NewGuid(),
                "Смирнов Андрей Игоревич",
                new DateTime(1988, 3, 10),
                new DateTime(2019, 9, 5),
                110000,
                marketingDepartment.Id
            ),
            new(
                Guid.NewGuid(),
                "Иванов Сергей Владимирович",
                new DateTime(1985, 5, 15),
                new DateTime(2020, 1, 10),
                95000,
                financesDepartment.Id
            ),
            new(
                Guid.NewGuid(),
                "Морозова Ирина Петровна",
                new DateTime(1986, 8, 25),
                new DateTime(2017, 10, 11),
                13000,
                marketingDepartment.Id
            ),
            new(
                Guid.NewGuid(),
                "Громов Алексей Денисович",
                new DateTime(1993, 1, 30),
                new DateTime(2020, 4, 3),
                93000,
                marketingDepartment.Id
            ),
            new(
                Guid.NewGuid(),
                "Борисов Константин Николаевич",
                new DateTime(1981, 9, 11),
                new DateTime(2013, 1, 28),
                145000,
                itDepartment.Id
            ),
            new(
                Guid.NewGuid(),
                "Киселев Владимир Андреевич",
                new DateTime(1990, 10, 14),
                new DateTime(2019, 6, 25), 
                102000, marketingDepartment.Id
            ),
            new(
                Guid.NewGuid(),
                "Тихонов Михаил Сергеевич",
                new DateTime(1982, 3, 7),
                new DateTime(2014, 4, 18),
                140000,
                operationsDepartment.Id
            ),
            new(
                Guid.NewGuid(),
                "Борисов Константин Николаевич",
                new DateTime(1981, 9, 11),
                new DateTime(2013, 1, 28),
                145000, 
                hrDepartment.Id
            ),
        };

        dbContext.Employees.AddRange(employees);
        dbContext.SaveChanges();
    }
}