namespace CompanyWebsite.Domain.Entities;

public class Employee
{
    public Guid Id { get; set; }
    
    public required string FullName { get; set; }

    public required DateTime BirthDate { get; set; }

    public required DateTime HireDate { get; set; }

    public required decimal Salary { get; set; }

    public required Guid DepartmentId { get; set; }
    
    public required Department Department { get; set; }
}