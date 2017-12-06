using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day6
{
    class Program
    {
        static void Main(string[] args)
        {
            string input = "5	1	10	0	1	7	13	14	3	12	8	10	7	12	0	6";
            List<string> inputs = input.Split('\t').ToList();

            List<int> banks = inputs.Select(int.Parse).ToList();
            List<List<int>> previousStates = new List<List<int>>();
                
            int cycles = 0;
            while(true)
            {
                previousStates.Add(new List<int>(banks));
                cycles++;
                banks = doCycle(banks);

                if(isRepeated(previousStates, banks))
                {
                    break;
                }
            }
            Console.WriteLine(cycles);
        }

        static List<int> doCycle(List<int> mem)
        {
            List<int> tmp = new List<int>(mem);

            int maxValue = tmp.Max();
            int maxIndex = tmp.IndexOf(maxValue);

            tmp[maxIndex] = 0;
            for (int i = 0; i < maxValue; i++)
            {
                maxIndex++;
                tmp[maxIndex % mem.Count]++;
            }

            return tmp;
        }

        static bool isRepeated(List<List<int>> previousStates, List<int> current)
        {
            int i = 0;
            foreach (var state in previousStates)
            {
                if(state.SequenceEqual(current))
                {
                    Console.WriteLine(previousStates.Count - i);
                    return true;
                }
                i++;
            }
            return false;
        }
    }
}
