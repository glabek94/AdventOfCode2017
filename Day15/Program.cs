using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day15
{
    class Program
    {
        static void Main(string[] args)
        {
            long prevA = 277;
            long prevB = 349;
            int factorA = 16807;
            int factorB = 48271;
            long divider = 2147483647;

            int judge = 0;
            //int tries = (int)40E6
            int tries = (int)5E6;

            for (int i = 0; i < tries; i++)
            {
                prevA = (prevA * factorA) % divider;
                while (prevA % 4 != 0)
                {
                    prevA = (prevA * factorA) % divider;
                }

                prevB = (prevB * factorB) % divider;
                while (prevB % 8 != 0)
                {
                    prevB = (prevB * factorB) % divider;
                }

                if ((prevA & 0xFFFF) == (prevB & 0xFFFF))
                {
                    judge++;
                }
            }

            Console.WriteLine(judge);
        }
    }
}