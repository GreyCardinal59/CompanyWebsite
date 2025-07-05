using CompanyWebsite.Domain.Departments;

namespace CompanyWebsite.Domain.Employees;

public class Employee
{
    public Employee(Guid id, string fullName, DateTime birthDate, DateTime hireDate, decimal salary, Guid departmentId)
    {
        Id = id;
        FullName = fullName;
        BirthDate = birthDate;
        HireDate = hireDate;
        Salary = salary;
        DepartmentId = departmentId;
    }

    public Guid Id { get; set; }

    public string FullName { get; set; }

    public DateTime BirthDate { get; set; }

    public DateTime HireDate { get; set; }

    public decimal Salary { get; set; }

    public Guid DepartmentId { get; set; }

    public Department Department { get; set; }
}