using System;
using System.Threading;
using System.Threading.Tasks;

namespace locktest
{
    class Program
    {
        static void inappropriateParallel()
        {
            const int ThreadNum = 20;
            const int LoopNum = 20;
            int num = 0;

            Parallel.For(0, ThreadNum, i =>
            {
                for (int j = 0; j < LoopNum; j++)
                {
                    int tmp = num;
                    Thread.Sleep(1);
                    num = tmp + 1;
                }
            });
            Console.WriteLine("inappropriate result");
            Console.WriteLine("{0} ({1})\n", num, ThreadNum * LoopNum);
        }
        static void exclusiveParallel()
        {
            const int ThreadNum = 20;
            const int LoopNum = 20;
            int num = 0;

            var syncObject = new object();

            Parallel.For(0, ThreadNum, i =>
            {
                for (int j = 0; j < LoopNum; j++)
                {
                    lock (syncObject)
                    {
                        int tmp = num;
                        Thread.Sleep(1);
                        num = tmp + 1;
                    }
                }
            });
            Console.WriteLine("appropriate result");
            Console.WriteLine("{0} ({1})\n", num, ThreadNum * LoopNum);
        }
        static void Main(string[] args)
        {
            inappropriateParallel();
            exclusiveParallel();
        }
    }
}
