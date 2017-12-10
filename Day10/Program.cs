using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day10
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> list = new List<int>(256);
            for (int i = 0; i < list.Capacity; i++)
            {
                list.Add(i);
            }

            int currentPos = 0;
            int skip = 0;
            string input = "129,154,49,198,200,133,97,254,41,6,2,1,255,0,191,108";
            //List<int> seq = new List<int> { 129, 154, 49, 198, 200, 133, 97, 254, 41, 6, 2, 1, 255, 0, 191, 108 };
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

            //Console.WriteLine(list[0] * list[1]);
            foreach (var a in denseHash)
            {
                Console.Write(a.ToString("X2"));
            }
        }
    }
}
