using System.IO;
using System.Text;

namespace WordCounter
{
    internal class Program
    {
        static void Main(string[] args)
        {
            WordCounter counter = new WordCounter();

            string text = ReadFile("./texts/jane-eyre.txt");

            counter.CountChar(text);
            Console.WriteLine(counter.GetSummary());
        }

        static string ReadFile(string path)
        {
            StreamReader sr = new StreamReader(path,Encoding.UTF8);
            string str = sr.ReadToEnd();
            sr.Close();
            return str;
        }
    }
}