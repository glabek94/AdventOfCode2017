using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day16
{
    class Program
    {
        static void Main(string[] args)
        {
            var inputFile = File.OpenText("../../input.txt");
            string input = inputFile.ReadToEnd().Trim();

            List<char> programs = new List<char>(16);
            for (char i = 'a'; i < 'q'; i++)
            {
                programs.Add(i);
            }

            List<Tuple<char, int, int>> commandsList = new List<Tuple<char, int, int>>();
            foreach (var command in input.Split(','))
            {
                char commandType = command[0];
                switch (commandType)
                {
                    case 's':
                        int param = int.Parse(command.Substring(1));
                        commandsList.Add(new Tuple<char, int, int>(commandType, param, 0));
                        break;
                    case 'x':
                        int[] pos = command.Substring(1).Split('/').Select(x => int.Parse(x)).ToArray();
                        commandsList.Add(new Tuple<char, int, int>(commandType, pos[0], pos[1]));
                        break;
                    case 'p':
                        char[] progs = command.Substring(1).Split('/').Select(x => x.First()).ToArray();
                        commandsList.Add(new Tuple<char, int, int>(commandType, progs[0], progs[1]));
                        break;
                }
            }

            List<string> dances = new List<string>();
            int curStart = 0;
            int idx = 0;
            while (true)
            {
                foreach (var command in commandsList)
                {
                    switch (command.Item1)
                    {
                        case 's':
                            curStart = (curStart + (programs.Count - command.Item2)) % programs.Count;
                            break;
                        case 'x':
                            makeExchange(programs, (command.Item2 + curStart) % programs.Count, (command.Item3 + curStart) % programs.Count);
                            break;
                        case 'p':
                            makePartner(programs, (char)command.Item2, (char)command.Item3);
                            break;
                    }
                }

                StringBuilder gen = new StringBuilder();
                gen.Append(programs.GetRange(curStart, programs.Count - curStart).ToArray());
                gen.Append(programs.GetRange(0, curStart).ToArray());

                dances.Add(gen.ToString());
                if (dances[0] == gen.ToString() && idx != 0)
                {
                    break;
                }
                idx++;
            }

            Console.WriteLine(dances[0]);
            idx = (int)((10E6 - 1) % idx);
            Console.WriteLine(dances[idx]);
        }

        static void makeExchange(List<char> input, int posA, int posB)
        {
            char tmp = input[posA];
            input[posA] = input[posB];
            input[posB] = tmp;
        }

        static void makePartner(List<char> input, char progA, char progB)
        {
            int posA = input.FindIndex(x => x == progA);
            int posB = input.FindIndex(x => x == progB);
            makeExchange(input, posA, posB);
        }
    }
}
