using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualBasic;

namespace Linq_Orders
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Clear();

            List<IOrder> OrderList = new List<IOrder>();
            for (int i = 0; i < 50; i++)
                OrderList.Add(Order.Factory.CreateWithRandomData());

            Console.WriteLine($"OrderCount: {OrderList.Count()}");
            Console.WriteLine($"OrderSum: {Math.Round(OrderList.Sum(o => o.Total), 2)} SEK");

            Console.WriteLine($"Biggest 5:");
            OrderList
                .OrderByDescending(o => o.NrOfArticles)
                .Take(5)
                .ToList()
                .ForEach(Console.WriteLine);

            Console.WriteLine($"OrderCountUnder 1000kr: {OrderList.Count(o => o.Total < 1000)}");

            Console.WriteLine(
                $"OrderFrieghtSum: {Math.Round(OrderList.Where(o => o.Total < 1000).Sum(o => o.Freight), 2)} SEK"
            );

            Console.WriteLine($"AllCountries:");
            OrderList
                .Select(o => o.Country)
                .Distinct()
                .ToList()
                .ForEach(c => Console.Write($"{c} "));

            Console.WriteLine();

            Console.WriteLine(
                $"OrdersDeliveredLate: {OrderList.Count(o => o.DeliveryDate.HasValue && (o.DeliveryDate.Value.Date - o.OrderDate.Date).TotalDays > 15)}"
            );
            Console.WriteLine(
                $"ordersInFinland: {OrderList.Where(o => o.Country == "Finland").Count()}"
            );
            Console.WriteLine($"orderSumInFinland: ");
            OrderList
                .Where(o => o.Country == "Finland")
                .ToList()
                .ForEach(o => Console.Write($"{Math.Round(o.Total, 2)} SEK, "));

            Console.WriteLine("Grouped By Country");
            OrderList
                .GroupBy(o => o.Country)
                .Select(o => new
                {
                    country = o.Key,
                    count = o.Count(),
                    total = o.Sum(o => o.Total),
                })
                .ToList()
                .ForEach(x =>
                    Console.WriteLine($"Country: {x.country}, Count: {x.count}, Total: {x.total}")
                );

            Console.WriteLine("5 Largest By Country");
            OrderList
                .GroupBy(o => o.Country)
                .Select(o => new
                {
                    country = o.Key,
                    items = o.OrderByDescending(o => o.NrOfArticles).Take(5).ToList(),
                })
                .ToList()
                .ForEach(x =>
                {
                    Console.WriteLine($"Country: {x.country}, Largest Orders:");
                    x.items.ForEach(Console.WriteLine);
                });

            Console.WriteLine(
                $"AverageDeliveryTime: {Math.Round(OrderList.Average(o => (o.DeliveryDate.Value.Date - o.OrderDate.Date).TotalDays))} days"
            );
        }
    }
}
//Exercises:
//1. Skriv ut antal ordrar, värdet av alla ordrar (tips Sum), de 5 största ordrarna, antal ordrar < 1000kr, summan av frakt för alla ordrar < 1000kr
//2. Skriv ut en lista på alla länder som det kommit ordrar från. Varje land ska skrivas ut bara en gång (tips Distinct)
//3. Skriv ut antal ordrar där leverans skett mer an 15 dagar efter orderdatum (tips Where)
//4. Antalet ordrar och värdet av alla ordrar i Finland

//5. Utmaning: Använd GroupBy för att lista land, antalet ordrar och värdet av ordrarna per land
//6. Utmaning: Använd GroupBy för att lista de 5 största ordrarna per land
//7. Utmaning: Använd Average för att räkna ut medel leveranstiden för alla ordrar
