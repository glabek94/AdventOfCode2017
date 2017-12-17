using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day17
{
    class Program
    {
        static void Main(string[] args)
        {
            int steps = 386;

            List<int> spinlocks = new List<int>() { 0 };
            int currentPos = 0;
            for (int i = 1; i < 2018; i++)
            {
                currentPos = (currentPos + steps) % spinlocks.Count + 1;
                spinlocks.Insert(currentPos, i);
            }

            Console.WriteLine(spinlocks[(currentPos + 1) % spinlocks.Count]);

            int whatInserted = 0;
            int currentCount = 1;
            currentPos = 0;

            for (int i = 1; i < 50000000; i++)
            {
                currentPos = (currentPos + steps) % currentCount + 1;
                currentCount++;
                if (currentPos == 1)
                {
                    whatInserted = i;
                }
            }

            Console.WriteLine(whatInserted);
        }
    }
}
