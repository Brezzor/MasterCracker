using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterCracker.repos
{
    internal class ChunksRepo
    {
        private static List<string[]> _chunks = new List<string[]>();

        public static void Initialize()
        {
            List<string> _dictionaryEnteries = new List<string>();
            Console.WriteLine("Reading dictonary enteries");
            using (FileStream fs = new FileStream("webster-dictionary.txt", FileMode.Open, FileAccess.Read))
            using (StreamReader sr = new StreamReader(fs))
            {
                while (!sr.EndOfStream)
                {
                    string? dictionaryEntry = sr.ReadLine();
                    _dictionaryEnteries.Add(dictionaryEntry!);
                }
            }
            Console.WriteLine("Dictonary enteries read");

            Console.WriteLine("Creating chunks from dictonary enteries");
            _chunks = _dictionaryEnteries.Chunk(10000).ToList();
            Console.WriteLine("Chunks created");
        }
    }
}
