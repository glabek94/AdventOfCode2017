using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day12
{
    class Program
    {
        static Dictionary<int, List<int>> programs = new Dictionary<int, List<int>>(); //programs before processing
        static List<int?> list = new List<int?>(); //processed programs

        static void Main(string[] args)
        {
            var inputFile = File.OpenText("../../input.txt");
            string input = inputFile.ReadToEnd().Trim();

            foreach (string line in input.Split('\n'))
            {
                string curLine = line.Trim();

                var lineParts = line.Split('-');
                var program = lineParts[0];
                program = program.Remove(program.Length - 2);

                var pipes = lineParts[1];
                pipes = pipes.Remove(0, 2);

                List<int> pipesList = new List<int>();
                foreach (var pipe in pipes.Split(','))
                {
                    pipesList.Add(int.Parse(pipe));
                }
                programs.Add(int.Parse(program), pipesList);
            }

            AddProgram(0); //make group with program 0
            Console.WriteLine(list.Count);

            int i = 1;

            while (programs.Count != 0) //make all other groups
            {
                AddProgram(programs.First().Key);
                i++;
            }
            Console.WriteLine(i);
        }

        static void AddProgram(int? program)
        {
            if (list.Find(x => x == program) == null) //program isn't on list
            {
                list.Add(program);
                foreach (var pipe in programs[(int)program])
                {
                    AddProgram(pipe);
                }
            }
            programs.Remove((int)program);
        }
    }
}