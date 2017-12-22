using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day22
{
    class Program
    {
        static void Main(string[] args)
        {
            var inputFile = File.OpenText("../../input.txt");
            string input = inputFile.ReadToEnd().TrimEnd('\n');
            var lines = input.Split('\n').Select(l => l.TrimEnd()).ToArray();

            List<Tuple<int, int>> infectedNodes = new List<Tuple<int, int>>();
            List<Tuple<int, int>> weakenedNodes = new List<Tuple<int, int>>();
            List<Tuple<int, int>> flaggedNodes = new List<Tuple<int, int>>();

            for (int i = 0, k = -lines.Length / 2; i < lines.Length; i++, k++)
            {
                for (int j = 0, l = -lines.Length / 2; j < lines.Length; j++, l++)
                {
                    if (lines[i][j] == '#')
                    {
                        infectedNodes.Add(new Tuple<int, int>(k, l));
                    }
                }
            }

            int x = 0;
            int y = 0;
            Direction dir = Direction.up;
            int infections = 0;

            for (int burst = 0; burst < 10000000; burst++)
            {
                if (burst % 100000 == 0)
                {
                    Console.WriteLine(burst);
                }

                Tuple<int, int> tmpInf = null, tmpWeak = null, tmpFlagg = null;
                tmpInf = infectedNodes.Find(n => n.Item1 == x && n.Item2 == y);
                if (tmpInf == null)
                {
                    tmpWeak = weakenedNodes.Find(n => n.Item1 == x && n.Item2 == y);
                    if (tmpWeak == null)
                    {
                        tmpFlagg = flaggedNodes.Find(n => n.Item1 == x && n.Item2 == y);
                    }
                }

                if (tmpInf != null) //is infected so turn right and become flagged
                {
                    infectedNodes.Remove(tmpInf);
                    flaggedNodes.Add(tmpInf);

                    switch (dir)
                    {
                        case Direction.up:
                            dir = Direction.right;
                            break;
                        case Direction.down:
                            dir = Direction.left;
                            break;
                        case Direction.left:
                            dir = Direction.up;
                            break;
                        case Direction.right:
                            dir = Direction.down;
                            break;
                    }
                }

                if (tmpWeak != null) //is weakened so become infected
                {
                    weakenedNodes.Remove(tmpWeak);
                    infectedNodes.Add(tmpWeak);
                    infections++;
                }

                if (tmpFlagg != null) //is flagged so become clean and reverse direction
                {
                    flaggedNodes.Remove(tmpFlagg);

                    switch (dir)
                    {
                        case Direction.up:
                            dir = Direction.down;
                            break;
                        case Direction.down:
                            dir = Direction.up;
                            break;
                        case Direction.left:
                            dir = Direction.right;
                            break;
                        case Direction.right:
                            dir = Direction.left;
                            break;
                    }
                }

                if (tmpInf == null && tmpWeak == null && tmpFlagg == null) //is not infected so tunr left
                {
                    weakenedNodes.Add(new Tuple<int, int>(x, y));

                    switch (dir)
                    {
                        case Direction.up:
                            dir = Direction.left;
                            break;
                        case Direction.down:
                            dir = Direction.right;
                            break;
                        case Direction.left:
                            dir = Direction.down;
                            break;
                        case Direction.right:
                            dir = Direction.up;
                            break;
                    }
                }

                switch (dir)
                {
                    case Direction.up:
                        x--;
                        break;
                    case Direction.down:
                        x++;
                        break;
                    case Direction.left:
                        y--;
                        break;
                    case Direction.right:
                        y++;
                        break;
                }
            }

            Console.WriteLine(infections);
        }

        enum Direction { up, down, left, right }
    }
}
