using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day24
{
    class Program
    {
        static void Main(string[] args)
        {
            var inputFile = File.OpenText("../../input.txt");
            string input = inputFile.ReadToEnd().Trim();

            List<Tuple<int, int>> components = new List<Tuple<int, int>>();
            foreach (var line in input.Split('\n'))
            {
                var ports = line.Split('/');
                components.Add(new Tuple<int, int>(int.Parse(ports[0]), int.Parse(ports[1])));
            }

            var bridges = MakeBridges(components);

            Console.WriteLine(FindMaxStrength(bridges));
            int maxLength = bridges.Max(x => x.Count);
            var theLongestBridges = bridges.Where(x => x.Count == maxLength).ToList();
            Console.WriteLine(FindMaxStrength(theLongestBridges));
        }

        static List<List<Tuple<int, int>>> MakeBridges(List<Tuple<int, int>> components)
        {
            List<List<Tuple<int, int>>> bridges = new List<List<Tuple<int, int>>>();
            AddToBridge(0, new List<Tuple<int, int>>(), components, bridges);

            return bridges;
        }
        static int FindMaxStrength(List<List<Tuple<int, int>>> bridges)
        {
            int max = 0;
            foreach (var bridge in bridges)
            {
                int currentMax = 0;
                foreach (var component in bridge)
                {
                    currentMax += component.Item1 + component.Item2;
                }
                if (currentMax > max)
                {
                    max = currentMax;
                }
            }
            return max;
        }
        static void AddToBridge(int port, List<Tuple<int, int>> bridge, List<Tuple<int, int>> availableComponents, List<List<Tuple<int, int>>> bridges)
        {
            var mayBeAdded = FindComponentWithPort(port, availableComponents);
            if (mayBeAdded.Count == 0)
            {
                bridges.Add(bridge);
                return;
            }

            foreach (var toAdd in mayBeAdded)
            {
                var currentBridge = new List<Tuple<int, int>>(bridge);
                currentBridge.Add(toAdd);
                var currentAvailable = new List<Tuple<int, int>>(availableComponents);
                currentAvailable.Remove(toAdd);
                if (toAdd.Item1 == port)
                {
                    AddToBridge(toAdd.Item2, currentBridge, currentAvailable, bridges);
                }
                else
                {
                    AddToBridge(toAdd.Item1, currentBridge, currentAvailable, bridges);
                }
            }
        }

        static List<Tuple<int, int>> FindComponentWithPort(int port, List<Tuple<int, int>> comp)
        {
            List<Tuple<int, int>> toReturn = new List<Tuple<int, int>>();
            foreach (var component in comp)
            {
                if (component.Item1 == port || component.Item2 == port)
                    toReturn.Add(component);
            }

            return toReturn;
        }
    }
}