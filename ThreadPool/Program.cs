using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace threadPool
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Start");
            Console.WriteLine($"Hauptprogramm: {Thread.CurrentThread.ManagedThreadId}");
            Stopwatch sw = new Stopwatch();
            sw.Start();
            //ThreadPool ist eine Sammlung von Threads, wird am Anfgang des Programms angelegt
            //lässt einfach neue Threads erzeugen und ausführen
            int CountWorker, CountIOS;
            ThreadPool.GetMaxThreads(out CountWorker, out CountIOS);
            ThreadPool.SetMaxThreads(5, 5);
            Console.WriteLine($"Worker Threads: {CountWorker}, IO Threads: {CountIOS}");
            Random random = new Random();
            for (int i = 0; i < 32000; i++)
            {
                CalcParam param = new CalcParam()
                {
                    Start = random.NextDouble() * 100,
                    Factor = random.NextDouble() * 100,
                    Loops = random.Next(40, 200)
                };
                ThreadPool.QueueUserWorkItem(ThreadCal,param);

            }
            Console.WriteLine("enter zum beenden");
            Console.ReadLine();
            sw.Stop();
            Console.WriteLine($"Dauer: {sw.ElapsedMilliseconds} ms");
            Console.WriteLine("ende");



        }
        private static void ThreadCal(object? state)
        {
            CalcParam param = (CalcParam)state;
            Calc(param.Start, param.Factor, param.Loops);
        }
        static void Calc(double start, double factor, int loops)
        {
            Console.WriteLine($"Thread start: {Thread.CurrentThread.ManagedThreadId}");
            for (int count = 0; count < loops; count++)
            {
                Thread thread = new Thread(NewThread);
                thread.IsBackground = true;
                thread.Start();
                start *= factor;
                Thread.Sleep(100);
            }
            Console.WriteLine($"Thread end: {Thread.CurrentThread.ManagedThreadId}");

        }
        static void NewThread()
        {
            Thread.Sleep(10000);
        }
    }

    class CalcParam
    {
        public double Start { get; set; }
        public double Factor { get; set; }
        public int Loops { get; set; }
    }
}
