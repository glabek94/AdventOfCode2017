using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Day14
{
    class Program
    {
        static void Main(string[] args)
        {
            string input = "oundnydw";
            List<string> disk = new List<string>();
            for (int i = 0; i < 128; i++)
            {
                disk.Add(makeKnotHash(input + $"-{i}"));
            }

            int used = 0;
            foreach (var line in disk)
            {
                used += line.Where(c => c == '1').Count();
            }

            Bitmap diskImage = new Bitmap(128, 128);
            for (int x = 0; x < 128; x++)
            {
                for (int y = 0; y < 128; y++)
                {
                    if (disk[x][y] == '1')
                    {
                        diskImage.SetPixel(x, y, Color.White);
                    }
                    else
                    {
                        diskImage.SetPixel(x, y, Color.Black);
                    }
                }
            }
            diskImage.Save("../../diskImage.png");
            Console.WriteLine(used);

            int[,] regions = new int[128, 128];
            int curRegion = 1;
            for (int x = 0; x < 128; x++)
            {
                for (int y = 0; y < 128; y++)
                {
                    if (disk[x][y] == '1' && regions[x, y] == 0)
                    {
                        makeRegions(disk, regions, x, y, curRegion++);
                    }
                }
            }

            Dictionary<int, Color> colors = new Dictionary<int, Color>();
            colors.Add(0, Color.Black);
            Random rnd = new Random();
            for (int i = 1; i < curRegion; i++)
            {
                int r = rnd.Next(0, 256);
                int g = rnd.Next(0, 256);
                int b = rnd.Next(0, 256);

                Color toAdd = Color.FromArgb(r, g, b);
                colors.Add(i, toAdd);
            }

            Bitmap regionImage = new Bitmap(128, 128);
            for (int x = 0; x < 128; x++)
            {
                for (int y = 0; y < 128; y++)
                {
                    regionImage.SetPixel(x, y, colors[regions[x, y]]);
                }
            }
            regionImage.Save("../../regionImage.png");

            Console.WriteLine(curRegion - 1);
        }

        static string makeKnotHash(string inputString)
        {
            List<int> list = new List<int>(256);
            for (int i = 0; i < list.Capacity; i++)
            {
                list.Add(i);
            }

            int currentPos = 0;
            int skip = 0;
            string input = inputString;
            List<int> seq = new List<int>();
            foreach (char c in input)
            {
                seq.Add((int)c);
            }

            List<int> postfix = new List<int> { 17, 31, 73, 47, 23 };
            foreach (int extraL in postfix)
            {
                seq.Add(extraL);
            }

            for (int round = 0; round < 64; round++)
            {
                foreach (int length in seq)
                {
                    for (int i = 0; i < length / 2; i++)
                    {
                        int curIndex = (currentPos + i) % list.Count;
                        int curToSwap = (currentPos + length - 1 - i) % list.Count;

                        var tmp = list[curToSwap];
                        list[curToSwap] = list[curIndex];
                        list[curIndex] = tmp;
                    }

                    currentPos = (currentPos + length + skip) % list.Count;
                    skip++;
                }
            }

            List<int> denseHash = new List<int>();
            for (int i = 0; i < 256; i += 16)
            {
                int result = 0;
                for (int j = 0; j < 16; j++)
                {
                    result = result ^ list[i + j];
                }
                denseHash.Add(result);
            }

            string inHex = "";
            foreach (var a in denseHash)
            {
                inHex += Convert.ToString(a, 16).PadLeft(2, '0');
            }

            string toReturn = "";
            foreach (var digit in inHex)
            {
                int num = Convert.ToInt32(digit.ToString(), 16);
                toReturn += Convert.ToString(num, 2).PadLeft(4, '0');
            }
            return toReturn;
        }

        static void makeRegions(List<string> disk, int[,] regions, int x, int y, int curRegion)
        {
            int[] xNeigh = { -1, 0, 0, 1 };
            int[] yNeigh = { 0, 1, -1, 0 };

            regions[x, y] = curRegion;

            for (int i = 0; i < 4; i++)
            {
                int newX = x + xNeigh[i];
                int newY = y + yNeigh[i];

                if (newX < 0 || newY < 0 || newX >= 128 || newY >= 128)
                {
                    continue;
                }

                if (disk[newX][newY] == '1' && regions[newX, newY] == 0)
                {
                    makeRegions(disk, regions, newX, newY, curRegion);
                }
            }
        }
    }
}