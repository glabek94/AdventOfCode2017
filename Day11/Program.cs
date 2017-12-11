using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day11
{
    class Program
    {
        static void Main(string[] args)
        {
            var inputFile = File.OpenText("../../input.txt");
            string input = inputFile.ReadToEnd().Trim();

            //Cube coordinates
            //https://www.redblobgames.com/grids/hexagons/ quality content
            int x = 0;
            int y = 0;
            int z = 0;
            int maxDist = 0;

            foreach (var dir in input.Split(','))
            {
                switch (dir)
                {
                    case "n":
                        x++; z--;
                        break;
                    case "ne":
                        x++; y--;
                        break;
                    case "se":
                        y--; z++;
                        break;
                    case "s":
                        x--; z++;
                        break;
                    case "sw":
                        x--; y++;
                        break;
                    case "nw":
                        y++; z--;
                        break;

                }

                if (Math.Abs(x) > maxDist)
                {
                    maxDist = Math.Abs(x);
                }
                if (Math.Abs(y) > maxDist)
                {
                    maxDist = Math.Abs(y);
                }
                if (Math.Abs(z) > maxDist)
                {
                    maxDist = Math.Abs(z);
                }
            }

            var dist = Math.Max(Math.Abs(x), Math.Abs(y));
            dist = Math.Max(dist, Math.Abs(z));

            Console.WriteLine(dist);
            Console.WriteLine(maxDist);
        }
    }
}
