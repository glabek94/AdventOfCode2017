using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Day18
{
    class Program
    {
        static void Main(string[] args)
        {
            var inputFile = File.OpenText("../../input.txt");
            string input = inputFile.ReadToEnd().Trim();

            AssemblerExecutor executor0 = new AssemblerExecutor(0);
            AssemblerExecutor executor1 = new AssemblerExecutor(1);
            executor0.oth = executor1;
            executor1.oth = executor0;

            foreach (var line in input.Split('\n'))
            {
                var currentLine = line.Trim();
                AssemblerExecutor.Instruction.instructionType instructionType = AssemblerExecutor.Instruction.instructionType.snd;
                char firstOperand;
                string secondOperand = "";

                var splitted = currentLine.Split(' ');
                switch (splitted[0])
                {
                    case "snd":
                        instructionType = AssemblerExecutor.Instruction.instructionType.snd;
                        break;
                    case "set":
                        instructionType = AssemblerExecutor.Instruction.instructionType.set;
                        break;
                    case "add":
                        instructionType = AssemblerExecutor.Instruction.instructionType.add;
                        break;
                    case "mul":
                        instructionType = AssemblerExecutor.Instruction.instructionType.mul;
                        break;
                    case "mod":
                        instructionType = AssemblerExecutor.Instruction.instructionType.mod;
                        break;
                    case "rcv":
                        instructionType = AssemblerExecutor.Instruction.instructionType.rcv;
                        break;
                    case "jgz":
                        instructionType = AssemblerExecutor.Instruction.instructionType.jgz;
                        break;
                }
                firstOperand = splitted[1][0];
                if (splitted.Length > 2)
                {
                    secondOperand = splitted[2];
                }

                executor0.AddInstruction(new AssemblerExecutor.Instruction(instructionType, firstOperand, secondOperand));
                executor1.AddInstruction(new AssemblerExecutor.Instruction(instructionType, firstOperand, secondOperand));
            }

            Thread ex0 = new Thread(() => executor0.Execute());
            ex0.Name = "ex0";
            Thread ex1 = new Thread(() => executor1.Execute());
            ex1.Name = "ex1";
            ex0.Start();
            ex1.Start();
        }
    }
}
