using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day21
{
    class Program
    {
        static void Main(string[] args)
        {
            var inputFile = File.OpenText("../../input.txt");
            string input = inputFile.ReadToEnd().TrimEnd('\n');

            Dictionary<string, char[,]> rules = new Dictionary<string, char[,]>();
            foreach (var line in input.Split('\n'))
            {
                var curLine = line.Trim();
                var splitted = curLine.Split('=', '>');

                string[] beforeRows = splitted[0].Trim().Split('/');
                string[] afterRows = splitted[2].Trim().Split('/');

                string before = "";
                char[,] after = new char[afterRows.Length, afterRows.Length];

                for (int i = 0; i < beforeRows.Length; i++)
                {
                    for (int j = 0; j < beforeRows.Length; j++)
                        before += beforeRows[i][j];
                }

                for (int i = 0; i < afterRows.Length; i++)
                {
                    for (int j = 0; j < afterRows.Length; j++)
                        after[i, j] = afterRows[i][j];
                }

                rules.Add(before, after);
            }

            char[,] startPattern = { { '.', '#', '.' }, { '.', '.', '#' }, { '#', '#', '#' } };

            for (int i = 0; i < 18; i++)
            {
                startPattern = MakeMagic(startPattern, rules);
            }

            int howManyStaysOn = 0;
            foreach (var character in startPattern)
            {
                if (character == '#')
                    howManyStaysOn++;
            }

            Console.WriteLine(howManyStaysOn);
        }

        static char[,] MakeMagic(char[,] current, Dictionary<string, char[,]> rules)
        {
            List<char[,]> foundRules = new List<char[,]>();
            int mult = 0;
            int howMany = 0;
            if (current.GetLength(0) % 2 == 0)
            {
                mult = 2;
                howMany = current.GetLength(0) / 2;
            }
            else
            {
                mult = 3;
                howMany = current.GetLength(0) / 3;
            }

            for (int i = 0; i < howMany; i++)
            {
                for (int j = 0; j < howMany; j++)
                {
                    char[,] toFind = current.Slice(i * mult, (i + 1) * mult - 1, j * mult, (j + 1) * mult - 1);
                    foundRules.Add(FindRule(toFind, rules));
                }
            }

            char[,] toReturn = new char[current.GetLength(0) + howMany, current.GetLength(0) + howMany];
            int whichRule = 0;
            for (int i = 0; i < howMany; i++)
            {
                for (int j = 0; j < howMany; j++)
                {
                    for (int x = 0; x < mult + 1; x++)
                    {
                        for (int y = 0; y < mult + 1; y++)
                        {
                            toReturn[i * (mult + 1) + x, j * (mult + 1) + y] = foundRules[whichRule][x, y];
                        }
                    }
                    whichRule++;
                }
            }

            return toReturn;
        }

        static string PatternToString(char[,] pattern)
        {
            string toReturn = "";

            for (int i = 0; i < pattern.GetLength(0); i++)
            {
                for (int j = 0; j < pattern.GetLength(1); j++)
                    toReturn += pattern[i, j];
            }

            return toReturn;
        }

        static char[,] FindRule(char[,] pattern, Dictionary<string, char[,]> rules)
        {
            for (int x = 0; x < 4; x++)
            {
                var patternToFind = PatternToString(pattern);

                if (rules.TryGetValue(patternToFind, out char[,] toReturn))
                {
                    return toReturn;
                }
                else
                {
                    patternToFind = PatternToString(FlipHor(pattern));

                    if (rules.TryGetValue(patternToFind, out toReturn))
                    {
                        return toReturn;
                    }

                    patternToFind = PatternToString(FlipVer(pattern));

                    if (rules.TryGetValue(patternToFind, out toReturn))
                    {
                        return toReturn;
                    }

                    pattern = RotateLeft(pattern);
                }
            }

            throw new Exception("Rule haven't found");
        }

        static char[,] FlipHor(char[,] toFlip)
        {
            var tmp = toFlip.Clone() as char[,];

            for (int j = 0; j < toFlip.GetLength(1); j++)
            {
                for (int i = 0; i < toFlip.GetLength(0); i++)
                    tmp[i, j] = toFlip[i, toFlip.GetLength(1) - 1 - j];
            }

            return tmp;
        }

        static char[,] FlipVer(char[,] toFlip)
        {
            var tmp = toFlip.Clone() as char[,];

            for (int i = 0; i < toFlip.GetLength(0); i++)
            {
                for (int j = 0; j < toFlip.GetLength(1); j++)
                    tmp[i, j] = toFlip[toFlip.GetLength(0) - 1 - i, j];
            }

            return tmp;
        }

        static char[,] Transpose(char[,] toTrans)
        {
            var tmp = toTrans.Clone() as char[,];

            for (int i = 0; i < toTrans.GetLength(0); i++)
            {
                for (int j = 0; j < toTrans.GetLength(1); j++)
                    tmp[i, j] = toTrans[j, i];
            }

            return tmp;
        }

        static char[,] RotateLeft(char[,] toRotate)
        {
            char[,] toRet = FlipHor(toRotate);
            return Transpose(toRet);
        }
    }
}