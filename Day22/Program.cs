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

            Dictionary<Tuple<int, int>, State> nodes = new Dictionary<Tuple<int, int>, State>();

            for (int i = 0, k = -lines.Length / 2; i < lines.Length; i++, k++)
            {
                for (int j = 0, l = -lines.Length / 2; j < lines.Length; j++, l++)
                {
                    if (lines[i][j] == '#')
                    {
                        nodes.Add(new Tuple<int, int>(k, l), State.infected);
                    }
                }
            }

            int x = 0;
            int y = 0;
            Direction dir = Direction.up;
            int infections = 0;

            for (int burst = 0; burst < 10000000; burst++)
            {
                State tmp;
                if (!nodes.TryGetValue(new Tuple<int, int>(x, y), out tmp))
                    tmp = State.clean;

                if (tmp == State.infected) //is infected so turn right and become flagged
                {
                    nodes[new Tuple<int, int>(x, y)] = State.flagged;

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
                else if (tmp == State.weakened) //is weakened so become infected
                {
                    nodes[new Tuple<int, int>(x, y)] = State.infected;
                    infections++;
                }
                else if (tmp == State.flagged) //is flagged so become clean and reverse direction
                {
                    nodes.Remove(new Tuple<int, int>(x, y));

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

                else //is not infected so tunr left
                {
                    nodes.Add(new Tuple<int, int>(x, y), State.weakened);

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
        enum State { infected, flagged, weakened, clean }
    }
}
