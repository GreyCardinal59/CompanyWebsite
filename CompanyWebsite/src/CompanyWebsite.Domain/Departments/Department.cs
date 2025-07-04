namespace CompanyWebsite.Domain.Entities;

public class Department
{
    public Guid Id { get; set; }

    public required string Name { get; set; }

    public required List<Employee> Employees { get; set; }
}