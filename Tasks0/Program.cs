using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Task0
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Clear();

            var watch = new Stopwatch();
            watch.Start();

            //Create Task t1
            var t1 = PrintHelloAsync(1);
            t1.Wait();

            //Create Task t2
            var t2 = PrintHelloAsync(2);

            //Create Task t3
            var t3 = PrintHelloAsync(3);
            // t3.Wait();

            Task.WaitAll(t1, t2, t3);

            watch.Stop();
            Console.WriteLine($"Main terminated. Execution time: {watch.ElapsedMilliseconds:N0}ms");
        }

        static Task PrintHelloAsync(int threadNum) => Task.Run(() => PrintHello(threadNum));

        static async Task PrintHello(int threadNum)
        {
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine($"Hello{i} from Thread{threadNum}");
                await Task.Delay(100 * i);
            }
        }
    }
}
// Exercises
//1. Create and start a Task t1 that loops 5 times and in each loop prints out "Hello{i} from Thread1" and sleeps 2 second
//2. Create and start a Task t2 that loops 10 times and in each loop prints out "Hello{i} from Thread2" and sleeps 1 second
//3. Create and start a Task t3 that loops 15 times and in each loop prints out "Hello{i} from Thread3" and sleeps 0,5 second
//4. Change the order of execution using t1.Wait() so that t2 and t3 starts after t1 has completed execution
//5. Experiment by changig the t1/t2/t3.Wait() and see what happends
