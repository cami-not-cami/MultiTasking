using System.Diagnostics;

namespace ParallelForeach
{
    class TestObject 
    {
        static int Counter  { get; set; } =0;
        private int Id { get; set; }
        public TestObject() 
        {
            Counter++;
            Id = Counter;

        }
        public void CallOut()
        {
            Console.WriteLine($"awkdaojwo {Id}");
            Thread.Sleep(300);
            Console.WriteLine($"yippe {Id}");
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            List<TestObject> list = new List<TestObject>();
            for (int i = 0; i < 10; i++)
            {
                list.Add(new TestObject());
            }
            Stopwatch sw = Stopwatch.StartNew();
            Console.WriteLine("classic foreach");
            foreach(TestObject obj in list)
            {
                obj.CallOut();
            }
            sw.Stop();
            Console.WriteLine($"Dauer classic foreach: {sw.ElapsedMilliseconds} ms");
            sw.Restart();
            Console.WriteLine("Parallel foreach");
            Parallel.ForEach(list,obj => obj.CallOut());
            sw.Stop();
            Console.WriteLine($"Dauer parallel foreach: {sw.ElapsedMilliseconds} ms");

        }
    }
}
