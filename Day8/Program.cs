using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day8
{
    class Program
    {
        static void Main(string[] args)
        {
            var inputFile = File.OpenText("../../input.txt");
            string input = inputFile.ReadToEnd().Trim();

            Dictionary<string, int> mem = new Dictionary<string, int>();

            foreach (var line in input.Split('\n'))
            {
                var curLine = line.Trim();
                var curMem = curLine.Split(' ')[0];
                mem[curMem] = 0;
            }

            int maxValue = 0;

            foreach (var line in input.Split('\n'))
            {
                var curLine = line.Trim().Split(' ');
                var curMem = curLine[0];
                var act = curLine[1];
                var valueAct = int.Parse(curLine[2]);
                var condVal = curLine[4];
                var cond = curLine[5];
                var condValue = int.Parse(curLine[6]);

                bool isCondOK = false;
                //is cond
                switch(cond)
                {
                    case "<":
                        isCondOK = mem[condVal] < condValue;
                        break;
                    case ">":
                        isCondOK = mem[condVal] > condValue;
                        break;
                    case "<=":
                        isCondOK = mem[condVal] <= condValue;
                        break;
                    case ">=":
                        isCondOK = mem[condVal] >= condValue;
                        break;
                    case "==":
                        isCondOK = mem[condVal] == condValue;
                        break;
                    case "!=":
                        isCondOK = mem[condVal] != condValue;
                        break;
                }

                if(isCondOK)
                {
                    switch(act)
                    {
                        case "inc":
                            mem[curMem] += valueAct;
                            break;
                        case "dec":
                            mem[curMem] -= valueAct;
                            break;
                    }

                    if (mem[curMem] > maxValue)
                        maxValue = mem[curMem];
                }
            }

            Console.WriteLine(mem.Values.Max());
            Console.WriteLine(maxValue);
        }

    }
}
