using Models.Employees.Interfaces;
using Newtonsoft.Json;
using Seido.Utilities.SeedGenerator;

namespace Models.Employees;

// Implement the IEmployee interface here
public class Employee : IEmployee, ISeed<Employee>
{
    private Guid _employeeId;

    public Guid EmployeeId
    {
        get => _employeeId;
        init => _employeeId = Guid.NewGuid();
    }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime HireDate { get; set; }
    public WorkRole Role { get; set; }
    public List<ICreditCard> CreditCards { get; set; }
    public bool Seeded { get; set; } = false;

    public Employee() { }

    public Employee Seed(SeedGenerator seeder)
    {
        FirstName = seeder.FirstName;
        LastName = seeder.LastName;
        HireDate = seeder.DateAndTime();
        Role = seeder.FromEnum<WorkRole>();
        CreditCards = seeder.ItemsToList<CreditCard>(seeder.Next(0, 5)).ToList<ICreditCard>();
        Seeded = true;
        return this;
    }

    public Employee(IEmployee org)
    {
        EmployeeId = org.EmployeeId;
        FirstName = org.FirstName;
        LastName = org.LastName;
        HireDate = org.HireDate;
        Role = org.Role;
        foreach (var card in org.CreditCards)
            CreditCards.Add(new CreditCard(card));
    }
}
