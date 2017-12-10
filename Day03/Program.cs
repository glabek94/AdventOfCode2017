using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


//HOW UGLY IT IS
namespace Day3
{
    class Program
    {
        static void Main(string[] args)
        {
            int input = 312051;

            const int size = 1000;
            int[,] tab = new int[size, size];

            int lastInserted = 1;
            tab[size / 2, size / 2] = lastInserted;

            for (int i = 1; i < 50; i++) //for each ring
            {
                int startX = (size + 0) / 2 + i;
                int startY = (size + 0) / 2 + i - 1;

                int numsOnSide = i * 2;

                int currX = startX;
                int currY = startY;
                for (int j = 0; j < numsOnSide; j++)
                {
                    lastInserted = toInsert(tab, currX, currY);
                    tab[currX, currY] = lastInserted;
                    if(lastInserted > input)
                    {
                        Console.WriteLine($"{lastInserted}");
                    }
                    currY--;
                }
                currY++;
                currX--;
                for (int j = 0; j < numsOnSide; j++)
                {
                    lastInserted = toInsert(tab, currX, currY);
                    tab[currX, currY] = lastInserted;
                    if (lastInserted > input)
                    {
                        Console.WriteLine($"{lastInserted}");
                    }
                    currX--;
                }
                currX++;
                currY++;
                for (int j = 0; j < numsOnSide; j++)
                {
                    lastInserted = toInsert(tab, currX, currY);
                    tab[currX, currY] = lastInserted;
                    if (lastInserted > input)
                    {
                        Console.WriteLine($"{lastInserted}");
                    }
                    currY++;
                }
                currY--;
                currX++;
                for (int j = 0; j < numsOnSide; j++)
                {
                    lastInserted = toInsert(tab, currX, currY);
                    tab[currX, currY] = lastInserted;
                    if (lastInserted > input)
                    {
                        Console.WriteLine($"{lastInserted}");
                    }
                    currX++;
                }
            }
            //Program.PrintArray(tab);
        }

        public static int toInsert(int[,] tab, int i, int j)
        {
            return tab[i,j] + tab[i-1, j-1] + tab[i+1, j+1] + tab[i-1, j+1] + tab[i+1, j-1] + tab[i + 1, j] + tab[i - 1, j] + tab[i, j + 1] + tab[i, j - 1];
        }

        public static void PrintArray(int[,] array)
        {
            int n = (array.GetLength(0) * array.GetLength(1) - 1).ToString().Length + 1;

            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    Console.Write(array[j, i].ToString().PadLeft(n, ' '));
                }
                Console.WriteLine();
            }
        }
    }
}
