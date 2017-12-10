using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day9
{
    class Program
    {
        static void Main(string[] args)
        {
            var inputFile = File.OpenText("../../input.txt");
            string input = inputFile.ReadToEnd().Trim();

            int toRemove;
            while ((toRemove = input.IndexOf('!')) != -1)
            {
                input = input.Remove(toRemove, 2);
            }

            toRemove = 0;
            int removed = 0;
            while ((toRemove = input.IndexOf('<')) != -1)
            {
                int last = input.IndexOf('>', toRemove);
                if (last != -1)
                {
                    int count = last - toRemove + 1;
                    input = input.Remove(toRemove, count);
                    removed += count - 2;
                }
                else
                {
                    break;
                }
            }

            int enclosing = 0;
            int score = 0;
            foreach (var character in input)
            {
                if (character == '{')
                {
                    enclosing++;
                }
                else if (character == '}')
                {
                    score += enclosing;
                    enclosing--;
                }
            }

            Console.WriteLine(score);
            Console.WriteLine(removed);
        }
    }
}
