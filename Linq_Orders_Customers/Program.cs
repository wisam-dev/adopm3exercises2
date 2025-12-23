using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace Linq_Orders_Customers
{
    static class LinqExtensions
    {
        public static void Print<T>(this IEnumerable<T> collection)
        {
            collection.ToList().ForEach(item => Console.WriteLine(item));
        }
    }

    public class OrderCustomer
    {
        public IOrder order { get; set; }
        public ICustomer customer { get; set; }
    }

    public class CustomerOrders
    {
        public ICustomer customer { get; set; }
        public IEnumerable<IOrder> orders { get; set; }
    }

    class Program
    {
        const int NrOfCustomers = 10_000;
        const int MaxNrOfOrdersPerCustomer = 20;

        static void Main(string[] args)
        {
            Console.Clear();

            //Create Order and customer Lists
            List<Order> OrderList = new List<Order>();
            List<Customer> CustomerList = new List<Customer>();

            var rnd = new Random();
            for (int c = 0; c < NrOfCustomers; c++)
            {
                var cus = Customer.Factory.CreateWithRandomData();
                CustomerList.Add(cus);

                //Create a random number of order for the customer. Could be 0
                for (int o = 0; o < rnd.Next(0, MaxNrOfOrdersPerCustomer + 1); o++)
                    OrderList.Add(Order.Factory.CreateWithRandomData(cus.CustomerID));
            }

            ///Exercises:
            //1.    Antalet kunder, Antalet kunder i Sverige, Äldsta kundens födelsedag, Yngsta kundens födelsedag
            Console.WriteLine($"CustomerCount: {CustomerList.Count}");
            Console.WriteLine(
                $"CustomerCountInSweden: {CustomerList.Count(c => c.Country == "Sverige")}"
            );
            Console.WriteLine($"OldestCustomer: {CustomerList.Min(c => c.BirthDate)}");
            Console.WriteLine($"YoungestCustomer: {CustomerList.Max(c => c.BirthDate)}");

            //2.    Använd GroupBy för att lista antalet kunder per land
            CustomerList
                .GroupBy(c => c.Country)
                .ToList()
                .ForEach(g => Console.WriteLine($"{g.Key}: {g.Count()}"));

            //3.    Antalet kunder med ett efternamn som slutar på 'son'
            Console.WriteLine(
                $"LastNamesEndsWithSon: {CustomerList.Where(c => c.LastName.EndsWith("son")).Count()}"
            );

            //4.    Antalet ordrar och totalt ordervärde av de 5 största ordrarna
            Console.WriteLine($"5LargestOrders:");
            OrderList
                .OrderByDescending(o => o.Total)
                .Take(5)
                .ToList()
                .ForEach(o => Console.WriteLine($"OrderID: {o.OrderID}, Total: {o.Total:C}  "));

            //5.    Använd Join för att lista kund och ordervärde för de 5 största ordrarna.
            //          Hint: använd Join för att skapa en lista av  OrderCustomer
            Console.WriteLine($"5LargestOrdersWithCustomer:");
            OrderList
                .Join(
                    CustomerList,
                    o => o.CustomerID,
                    c => c.CustomerID,
                    (o, c) => new OrderCustomer { order = o, customer = c }
                )
                .OrderByDescending(oc => oc.order.Total)
                .Take(5)
                .ToList()
                .ForEach(oc =>
                    Console.WriteLine(
                        $"Customer: {oc.customer.FirstName} {oc.customer.LastName}, Order Total: {oc.order.Total:C}"
                    )
                );
            //6.    Använd GroupJoin för att lista de 5 största kunderna baserat på ordervärde
            //          Hint: använd GroupJoin för att skapa en lista av  CustomerOrders
            Console.WriteLine($"5LargestCustomersByOrderValue:");
            CustomerList
                .GroupJoin(
                    OrderList,
                    c => c.CustomerID,
                    o => o.CustomerID,
                    (c, orders) => new { customer = c, totalOrderValue = orders.Sum(o => o.Total) }
                )
                .OrderByDescending(co => co.totalOrderValue)
                .Take(5)
                .ToList()
                .ForEach(co =>
                    Console.WriteLine(
                        $"Customer: {co.customer.FirstName} {co.customer.LastName}, Order Total: {co.totalOrderValue:C}"
                    )
                );
        }
    }
}
