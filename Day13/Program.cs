using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day13
{
    class Program
    {
        static void Main(string[] args)
        {
            var inputFile = File.OpenText("../../input.txt");
            string input = inputFile.ReadToEnd().Trim();

            Dictionary<int, int> firewalls = new Dictionary<int, int>();
            foreach (var line in input.Split('\n'))
            {
                var elems = line.Split(':');
                int level = int.Parse(elems[0]);
                int width = int.Parse(elems[1]);
                firewalls.Add(level, width);
            }

            int severity = 0;
            foreach (var firewall in firewalls)
            {
                int pos = countPosition(firewall.Value, firewall.Key);
                if (pos == 0)
                {
                    severity += firewall.Value * firewall.Key;
                }
            }

            Console.WriteLine(severity);

            int delay = 0;
            bool isGood = false;
            while (!isGood)
            {
                isGood = true;
                foreach (var firewall in firewalls)
                {
                    int pos = countPosition(firewall.Value, firewall.Key + delay);
                    if (pos == 0)
                    {
                        delay++;
                        isGood = false;
                        break;
                    }
                }
            }

            Console.WriteLine(delay);
        }

        static int countPosition(int width, int time)
        {
            int lengthOfCycle = 2 * (width - 1);
            int position = time % lengthOfCycle;

            if (position >= width)
            {
                return lengthOfCycle - position;
            }
            else
            {
                return position;
            }
        }
    }
}