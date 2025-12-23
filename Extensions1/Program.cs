using System;

namespace Extensions1
{
    public static class ExtensionMethods
    {
        public static string Truncate(this string s, int maxLength, string suffix = "...") =>
            s.Length > maxLength ? s.Substring(0, maxLength) + suffix : s;

        public static string ToTitleCase(this string str) =>
            string.IsNullOrWhiteSpace(str)
                ? "Can not be empty!"
                : string.Join(" ", str.Split(" ").Select(s => char.ToUpper(s[0]) + s.Substring(1)));

        public static IEnumerable<T> TakeEveryNth<T>(this IEnumerable<T> source, int n)
        {
            int index = 0;
            foreach (var item in source)
            {
                if (index % n == 0)
                    yield return item;
                index++;
            }
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Clear();

            Console.WriteLine("Hello Extensions!");

            Console.WriteLine("Hello".Truncate(2));
            Console.WriteLine("".Truncate(2));
            Console.WriteLine("hello world from extensions".ToTitleCase());
            Console.WriteLine("".ToTitleCase());

            Console.WriteLine("Taking every 3rd element from a list:");
            var numbers = Enumerable.Range(1, 20);
            var everyThird = numbers.TakeEveryNth(3);
        }
    }
}
