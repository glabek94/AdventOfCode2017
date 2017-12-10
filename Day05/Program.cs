using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day5
{
    class Program
    {
        static void Main(string[] args)
        {
            var inputFile = File.OpenText("../../input.txt");
            string input = inputFile.ReadToEnd().Trim();

            List<int> shifts = new List<int>();
            foreach (string a in input.Split('\n'))
            {
                int toAdd = int.Parse(a.Trim());
                shifts.Add(toAdd);
            }

            int currentPos = 0;
            int moves = 0;
            while(currentPos < shifts.Count)
            {
                int shift = shifts[currentPos];

                //shifts[currentPos]++; //part one
                if (shift > 2) //part two
                {
                    shifts[currentPos]--;
                }
                else
                {
                    shifts[currentPos]++;
                }
                currentPos += shift;
                moves++;
            }
            Console.WriteLine(moves);
        }
    }
}
