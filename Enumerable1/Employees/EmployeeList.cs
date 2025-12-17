using System.Collections;
using Models.Employees;
using Models.Employees.Interfaces;
using Seido.Utilities.SeedGenerator;

namespace Enumerable1.Employees;

public class EmployeeList : IEmployeeList, ISeed<EmployeeList>, IEnumerable<IEmployee>
{
    private List<IEmployee> _employees = new List<IEmployee>();

    public IEmployee this[int index] => _employees[index];
    public int Count
    {
        get => _employees.Count;
    }
    public bool Seeded { get; set; } = false;

    public IEnumerable<IEmployee> Filter(Func<ICreditCard, IEmployee, bool> predicate)
    {
        foreach (var emp in _employees)
        {
            foreach (var card in emp.CreditCards)
            {
                if (predicate(card, emp))
                    yield return emp;
            }
        }
    }

    public IEnumerator<IEmployee> GetEnumerator()
    {
        foreach (var emp in _employees)
            yield return emp;
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public EmployeeList() { }

    public EmployeeList Seed(SeedGenerator seeder)
    {
        _employees = seeder.ItemsToList<Employee>(100).ToList<IEmployee>();
        Seeded = true;
        return this;
    }
}
