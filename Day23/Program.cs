using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day23
{
    class Program
    {
        static void Main(string[] args)
        {
            var inputFile = File.OpenText("../../input.txt");
            string input = inputFile.ReadToEnd().Trim();

            AssemblerExecutor executor = new AssemblerExecutor();

            foreach (var line in input.Split('\n'))
            {
                var currentLine = line.Trim();
                AssemblerExecutor.Instruction.instructionType instructionType = AssemblerExecutor.Instruction.instructionType.set;
                char firstOperand;
                string secondOperand = "";

                var splitted = currentLine.Split(' ');
                switch (splitted[0])
                {
                    case "set":
                        instructionType = AssemblerExecutor.Instruction.instructionType.set;
                        break;
                    case "mul":
                        instructionType = AssemblerExecutor.Instruction.instructionType.mul;
                        break;
                    case "jnz":
                        instructionType = AssemblerExecutor.Instruction.instructionType.jnz;
                        break;
                    case "sub":
                        instructionType = AssemblerExecutor.Instruction.instructionType.sub;
                        break;
                }
                firstOperand = splitted[1][0];
                if (splitted.Length > 2)
                {
                    secondOperand = splitted[2];
                }

                executor.AddInstruction(new AssemblerExecutor.Instruction(instructionType, firstOperand, secondOperand));
            }

            Console.WriteLine(executor.Execute());

            //Part 2
            int b = 106500;
            int c = 123500;
            int h = 0;

            for (; b <= c; b += 17)
            {
                bool isPrime = true;
                for (int t = 2; t < Math.Ceiling(Math.Sqrt(b)); t++)
                {
                    if (b % t == 0) //b is not a prime number
                    {
                        isPrime = false;
                        break;
                    }
                }

                if (!isPrime)
                {
                    h++;
                }
            }

            Console.WriteLine(h);
        }
    }
}
