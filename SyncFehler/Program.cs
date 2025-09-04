using System.Collections.Concurrent;
namespace SyncFehler
{
    internal class TestKlasse 
    {

        public string Text { get; set; }
        public int Value { get; set; }

    }
    internal class Program
    {
        static void Main(string[] args)
        {
            //List<string> list = new List<string>() { "awdi", "hallo" };

            //TestKlasse obj = new TestKlasse() { Text = "Hallo", Value = 20 };
            //TestKlasse obj2 = new TestKlasse() { Text = "YIpie", Value = 42 };
           
            BlockingCollection<string> list = new BlockingCollection<string>() {  "Hallo", "aijiaja" }; ;
            for (int i = 0; i < 100; i++)
            {
            Thread th1 = new Thread(() => ListeFunktion(list));
            Thread th2 = new Thread(() => ListeFunktion(list));
                th1.IsBackground = true;
                th2.IsBackground = true;
                th1.Start();
                th2.Start();
            }
           
            
            Console.WriteLine("Fertig");
            Console.ReadLine();
        }
        private static void ListeFunktion(BlockingCollection<string> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                string temp = list.Take().ToString();
                temp = temp.ToUpper() + "afwa";
                list.Add(temp);


            }
            foreach (string s in list)
                Console.WriteLine(s + Thread.CurrentThread.ManagedThreadId);
        }
        private static void MeineFunktion(TestKlasse obj, int wert)
        {
            Console.WriteLine(obj.Text);
            obj.Value = wert;
            Thread.Sleep(1000);
            Console.WriteLine(obj.Value);
            Thread.Sleep(1000);

        }
    }

}
