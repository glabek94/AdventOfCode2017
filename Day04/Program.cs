using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day4
{
    class Program
    {
        static void Main(string[] args)
        {
            var inputFile = File.OpenText("../../input.txt");
            string input = inputFile.ReadToEnd();

            var lines = input.Split('\n').ToList<string>();

            int properPasses = 0;
            foreach (var line in lines)
            {
                var currLine = line.Trim();

                var pass = currLine.Split(' ').ToList<string>();

                var goodPass = true;
                for (int i = 0; i < pass.Count; i++)
                {
                    for (int j = i + 1; j < pass.Count; j++)
                    {
                        //if(pass[i] == pass[j]) //part one
                        if (Program.IsAnagram(pass[i], pass[j])) //part two
                        {
                            goodPass = false;
                            break;
                        }
                    }

                    if (!goodPass)
                    {
                        break;
                    }
                }

                if (goodPass)
                {
                    properPasses++;
                }
            }

            Console.WriteLine(properPasses);
        }

        static bool IsAnagram(string a, string b)
        {
            return a.OrderBy(c => c).SequenceEqual(b.OrderBy(c => c)); //sort chars and compare sorted
        }
    }
}
