using System.Diagnostics;

namespace taskBeispiel
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Calc(123.456, 45.56,100);
            //Calc(-0.681, 22.09, 100);
            Console.WriteLine("Start");
            Console.WriteLine($"Hauptprogramm: {Thread.CurrentThread.ManagedThreadId}");
            Stopwatch sw = new Stopwatch();
            sw.Start();
            Task<double> task1 = new Task<double>(() => { return Calc(123.456, 45.56, 100); });
            task1.Start();
            Task<double> task2 = new Task<double>(() => { return Calc(-0.681, 22.09, 100); });
            task2.Start();
            task1.Wait();
            task2.Wait();
            Console.WriteLine($"Task1 result " +  $"{task1.Result}" +$"Status {task1.Status}");
            Console.WriteLine($"Task2 result " + $"{task2.Result}" + $"Status {task2.Status}");

            Parallel.Invoke(
                () => {  Calc(123.456, 45.56, 100); },
                () => {  Calc(-0.681, 22.09, 100); }
                );
            sw.Stop();
            Console.WriteLine($"Dauer: {sw.ElapsedMilliseconds} ms");
            Console.WriteLine("ende");

        }

        static double Calc(double start, double factor, int loops)
        {
            Console.WriteLine($"Thread start: {Thread.CurrentThread.ManagedThreadId}");
            for (int count = 0; count < loops; count++)
            { 
                start *= factor;
                Thread.Sleep(100);
            }
            Console.WriteLine($"Thread end: {Thread.CurrentThread.ManagedThreadId}");
            return start;
        }
    }
}
