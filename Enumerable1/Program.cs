using System;
using System.Linq;
using Enumerable1.Employees;
using Models.Employees;
using Models.Employees.Interfaces;
using Seido.Utilities.SeedGenerator;

var seeder = new SeedGenerator();
var employeeList = new EmployeeList().Seed(seeder);

Console.WriteLine($"Total employees: {employeeList.Count}\n");

foreach (var employee in employeeList)
{
    Console.WriteLine($"{employee.FirstName} {employee.LastName} - {employee.Role}");
    Console.WriteLine($"  Credit Cards: {employee.CreditCards.Count}");
    foreach (var card in employee.CreditCards)
    {
        Console.WriteLine($"    - {card.Issuer}: {card.Number}");
    }
}

var managementWithAmex = employeeList.Filter(
    (card, employee) =>
        employee.Role == WorkRole.Management && card.Issuer == CardIssuer.AmericanExpress
);

Console.WriteLine("\n=== Management Employees with American Express ===");
foreach (var employee in managementWithAmex)
{
    Console.WriteLine($"{employee.FirstName} {employee.LastName} - {employee.Role}");
    var amexCards = employee.CreditCards.Where(c => c.Issuer == CardIssuer.AmericanExpress);
    foreach (var card in amexCards)
    {
        Console.WriteLine(
            $"  AMEX: {card.Number} (Exp: {card.ExpirationMonth}/{card.ExpirationYear})"
        );
    }
}

var employeeCountPerRole = employeeList
    .GroupBy(emp => emp.Role)
    .Select(group => new { Role = group.Key, Count = group.Count() });

Console.WriteLine("\n=== Employee Count per Role ===");
foreach (var roleGroup in employeeCountPerRole)
    Console.WriteLine($"{roleGroup.Role}: {roleGroup.Count}");

var employeesWithNoCards = employeeList.Filter((card, employee) => employee.CreditCards.Count == 0);

Console.WriteLine("\n=== Employees with No Credit Cards ===");
foreach (var employee in employeesWithNoCards)
    Console.WriteLine($"{employee.FirstName} {employee.LastName} - {employee.Role}");

var employeesWith4Cards = employeeList.Filter((card, employee) => employee.CreditCards.Count == 4);

Console.WriteLine("\n=== Employees with Exactly 4 Credit Cards ===");
foreach (var employee in employeesWith4Cards)
    Console.WriteLine($"{employee.FirstName} {employee.LastName} - {employee.Role}");
