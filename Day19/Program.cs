using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day19
{
    class Program
    {
        static void Main(string[] args)
        {
            var inputFile = File.OpenText("../../input.txt");
            string input = inputFile.ReadToEnd().TrimEnd('\n');

            List<string> board = new List<string>();
            foreach (var line in input.Split('\n'))
            {
                board.Add(line.TrimEnd('\n'));
            }

            Diagram diag = new Diagram(board);
            diag.GoGoGo();
            Console.WriteLine(diag.Travel);
            Console.WriteLine(diag.Steps);
        }
    }
}
