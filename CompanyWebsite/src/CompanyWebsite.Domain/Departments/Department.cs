using CompanyWebsite.Domain.Employees;

namespace CompanyWebsite.Domain.Departments;

public class Department
{
    public Guid Id { get; set; }

    public required string Name { get; set; }

    public required List<Employee> Employees { get; set; }
}