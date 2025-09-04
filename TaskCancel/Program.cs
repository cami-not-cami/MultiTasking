using System.Diagnostics;

namespace TaskCancel
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
            try
            {
                CancellationTokenSource ctSource = new CancellationTokenSource();
                CancellationToken ctoken = ctSource.Token;

                Task<double> task1 = new Task<double>(() => { return Calc(123.456, 45.56, 100,ctoken); },ctoken);
                task1.Start();
                Task<double> task2 = new Task<double>(() => { return Calc(-0.681, 22.09, 100,ctoken); },ctoken);
                task2.Start();
                //laufen der beiden tasks
                Console.WriteLine("warten auf tasks");
                Console.ReadLine();
                //wenn user zu frühz retrurn drückt, soll Cancel aufgerufen werden
                if(!task1.IsCompleted || !task2.IsCompleted)
                {
                    ctSource.Cancel();
                }

                task1.Wait();
                task2.Wait();
                Console.WriteLine($"Task1 result " + $"{task1.Result}" + $"Status {task1.Status}");
                Console.WriteLine($"Task2 result " + $"{task2.Result}" + $"Status {task2.Status}");


            }
            catch (AggregateException e)
            {
                Console.WriteLine($"## Aggregate Exception: {e.Message}");
                foreach( var inner in e.InnerExceptions)
                {
                    Console.WriteLine($"\t Inner Exception: {inner.Message}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"## Exception: {ex.Message}");
            }

            sw.Stop();

            Console.WriteLine($"Dauer: {sw.ElapsedMilliseconds} ms");
            Console.WriteLine("ende");

        }

        static double Calc(double start, double factor, int loops,CancellationToken cToken)
        {
            Console.WriteLine($"Thread start: {Thread.CurrentThread.ManagedThreadId}");
            for (int count = 0; count < loops; count++)
            {
                //Token wirft einen Error wenn ein CancelRequested gestellt wird
                cToken.ThrowIfCancellationRequested();
                start *= factor;
                Thread.Sleep(100);
            }
            Console.WriteLine($"Thread end: {Thread.CurrentThread.ManagedThreadId}");
            return start;
        }
    }
}
