using System;
using System.Threading;
using System.Threading.Tasks;

namespace asyncawait
{
    class Program
    {
        static async Task<int> CalculateAsync()
        {
            Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId}: CalculateAsync START");
            var task = Task.Run(new Func<int>(Calculate));
            Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId}: CalculateAsync Before");
            var result = await task;
            Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId}: CalculateAsync After");
            return result;
        }
        static int Calculate()
        {
            Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId}: Calculate START");
            int total = 0;
            for (int i = 1; i <= 100; ++i)
                total += i;
            Thread.Sleep(2345);
            Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId}: Calculate END");
            return total;
        }
        static void Main(string[] args)
        {
            var task = CalculateAsync();
            var result = task.Result;

            Console.WriteLine(result);
        }
    }
}
