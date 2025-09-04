using System.Diagnostics;

namespace Threads
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Start");
            Console.WriteLine($"Hauptprogramm: {Thread.CurrentThread.ManagedThreadId}");
            Stopwatch sw = new Stopwatch();
            sw.Start();

            //Berechnung Sequentiell dauer knapp 22 sec
            //Calc(123.456, 45.56,100);
            //Calc(-0.681, 22.09, 100);

            //Parallele Abarbeitung dauert knapp 11 sec
            Thread thread1 = new Thread(() => Calc(123.456, 45.56, 100));
            thread1.IsBackground = true;
            thread1.Start();

            Thread thread2 = new Thread(() => Calc(-0.681, 22.09, 100));
            thread2.IsBackground = true;
            thread2.Start();

            Console.WriteLine($"Thread State1 :{thread1.ThreadState}"
            + $"Thread State2 :{thread2.ThreadState}");

            // Auf Beendigung der Threads warten
            thread1.Join();
            thread2.Join();
            //join tells main to also include these threads to be finished before it continues

            Console.WriteLine($"Thread State1 :{thread1.ThreadState}"
              + $"Thread State2 :{thread2.ThreadState}");

            sw.Stop();
            Console.WriteLine($"Dauer: {sw.ElapsedMilliseconds} ms");
            Console.WriteLine("ende");

        }
        static void Calc(double start,double factor, int loops)
        {
            Console.WriteLine($"Thread start: {Thread.CurrentThread.ManagedThreadId}");
            for(int count = 0; count< loops; count++)
            {
                start *= factor;
                Thread.Sleep(100);
            }
            Console.WriteLine($"Thread end: {Thread.CurrentThread.ManagedThreadId}");

        }
    }
}
