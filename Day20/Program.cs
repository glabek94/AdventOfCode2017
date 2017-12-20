using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day20
{
    class Program
    {
        static void Main(string[] args)
        {
            var inputFile = File.OpenText("../../input.txt");
            string input = inputFile.ReadToEnd().TrimEnd('\n');

            List<int[]> p = new List<int[]>();
            List<int[]> v = new List<int[]>();
            List<int[]> a = new List<int[]>();
            List<bool> collided = new List<bool>();

            foreach (var line in input.Split('\n'))
            {
                var curLine = line.TrimEnd();
                var vals = curLine.Split('<', '>');

                var pCur = vals[1];
                var vCur = vals[3];
                var aCur = vals[5];

                p.Add(parseVec(pCur));
                v.Add(parseVec(vCur));
                a.Add(parseVec(aCur));
                collided.Add(false);
            }

            int notCollidedYet = p.Count();
            int iters = 0;
            while (true)
            {
                for (int i = 0; i < p.Count; i++)
                {
                    if (collided[i])
                        continue;

                    v[i][0] += a[i][0];
                    v[i][1] += a[i][1];
                    v[i][2] += a[i][2];

                    p[i][0] += v[i][0];
                    p[i][1] += v[i][1];
                    p[i][2] += v[i][2];
                }

                var duplicates = p.Select((t, i) => new { Index = i, Pos = t }) //select new objects - (i, pos)
                    .Where((x) => !collided[x.Index]) //take only such particles that haven't collide yet
                    .GroupBy(g => $"{g.Pos[0].ToString()}{g.Pos[1].ToString()}{g.Pos[2].ToString()}") //xD xD xD - group them by their positions
                    .Where(g => g.Count() > 1); //take only such groups that have more than 0 element - position where more than 1 particles collide

                foreach (var group in duplicates)
                {
                    foreach (var x in group)
                    {
                        collided[x.Index] = true;
                    }
                }

                int currentNotCollided = p.Where((x, i) => !collided[i]).Count();
                if (currentNotCollided < notCollidedYet)
                {
                    notCollidedYet = currentNotCollided;
                    iters = 0;
                }
                else
                {
                    iters++;
                    if (iters > 1000)
                        break;
                }
            }
            Console.WriteLine(notCollidedYet);
        }

        static int[] parseVec(string toParse)
        {
            var cur = toParse.Split(',');
            int x = int.Parse(cur[0]);
            int y = int.Parse(cur[1]);
            int z = int.Parse(cur[2]);

            return new int[] { x, y, z };
        }
    }
}
