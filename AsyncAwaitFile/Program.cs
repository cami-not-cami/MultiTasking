using System.Text;
using System.Threading.Tasks;
namespace AsyncAwaitFile
{

    internal class Program
    {
        static void Main(string[] args)
        {
            //User darf text eingeben welcher in einem Textfile gespeichert wird
            List<string> lines = new List<string>();
            string input;
            Console.WriteLine("Bitte Text eingeben, Leereingabe für ende ");
            while((input = Console.ReadLine()) != "")
            {
                lines.Add(input);
            }
            Console.WriteLine($"Speichere {lines.Count} Zeilen in Dateien!");
            //Aufruf der FUnktion
            //TextSchreiben(lines, "../../../MeinText1.txt");
            TextSchreibenAsync(lines, "../../../MeinText2.txt");
            //ende

        }

        //Synchrone Variante
        private static void TextSchreiben(List<string> lines, string filePath)
        {
            Console.WriteLine($"Beginne mit Schreiben in  {filePath}");
            using (FileStream file = new FileStream(filePath, FileMode.Create, FileAccess.Write))
            using(StreamWriter writer = new StreamWriter(file,Encoding.UTF8))
            {
                foreach (string line in lines)
                {
                    writer.WriteLine(line);
                    
                }
                writer.Flush();
            }
            Console.WriteLine($"Fertig mit Schreiben in  {filePath}");
        }

        //Asynchrone Variante
        private async static void TextSchreibenAsync(List<string> lines, string filePath)
        {
            Console.WriteLine($"Beginne mit Schreiben in  {filePath}");
            using (FileStream file = new FileStream(filePath, FileMode.Create, FileAccess.Write))
            using (StreamWriter writer = new StreamWriter(file, Encoding.UTF8))
            {
                foreach (string line in lines)
                {
                   await writer.WriteLineAsync(line);

                }
               await writer.FlushAsync();
            }
            Console.WriteLine($"Fertig mit Schreiben in  {filePath}");
        }
    }
}
