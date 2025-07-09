using CompanyWebsite.Domain.Employees;

namespace CompanyWebsite.Domain.Departments;

public class Department
{
    public Department(Guid id, string name)
    {
        Id = id;
        Name = name;
    }
    
    public Guid Id { get; set; }

    public string Name { get; set; }

    // public required List<Employee> Employees { get; set; }
}