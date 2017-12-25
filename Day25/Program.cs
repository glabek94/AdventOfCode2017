using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day25
{
    class Program
    {
        static void Main(string[] args)
        {
            int steps = 12172063;
            TuringMachine mach = new TuringMachine(steps);

            Console.WriteLine(mach.MakeSteps(steps));
        }
    }
}
