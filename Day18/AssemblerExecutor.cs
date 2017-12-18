using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day18
{
    public class AssemblerExecutor
    {
        List<Instruction> instructions;
        Dictionary<char, long> registers;
        int insPtr;
        long lastFrequency;
        int numberOfProgram;
        List<long> buf;
        public AssemblerExecutor oth { get; set; }

        public Object thisLock = new object();

        long howManySend = 0;

        public AssemblerExecutor(int numberOfProgram)
        {
            instructions = new List<Instruction>();
            registers = new Dictionary<char, long>();
            registers.Add('p', numberOfProgram);
            registers.Add('a', numberOfProgram);
            registers.Add('b', numberOfProgram);
            registers.Add('f', numberOfProgram);
            registers.Add('i', numberOfProgram);
            lastFrequency = 0;
            insPtr = 0;
            buf = new List<long>();
            this.numberOfProgram = numberOfProgram;
        }

        private void addRegister(char name)
        {
            registers.Add(name, 0);
        }

        public void Execute()
        {
            while (true)
            {
                ExecuteInstruction(instructions[insPtr]);
            }
        }

        public void AddInstruction(Instruction toAdd)
        {
            instructions.Add(toAdd);
        }

        private void ExecuteInstruction(Instruction toExecute)
        {
            long firstOpValue;
            long secondOpValue = 0;

            if (!int.TryParse(toExecute.first.ToString(), out int tmp))
            {
                if (!registers.ContainsKey(toExecute.first))
                    addRegister(toExecute.first);

                firstOpValue = registers[toExecute.first];
            }
            else
            {
                firstOpValue = tmp;
            }

            if (toExecute.second.Length>0)
            {
                if (!int.TryParse(toExecute.second, out tmp))
                {
                    if (!registers.ContainsKey(toExecute.second[0]))
                        addRegister(toExecute.second[0]);

                    if (toExecute.second.Length > 0)
                        secondOpValue = registers[toExecute.second[0]];
                }
                else
                {
                    secondOpValue = tmp;
                } 
            }

            switch (toExecute.name)
            {
                case Instruction.instructionType.snd:
                    lock (oth.thisLock)
                    {
                        oth.buf.Add(firstOpValue);
                        howManySend++;
                        Console.WriteLine($"Process {numberOfProgram} send {firstOpValue} (total {howManySend})");
                    }
                    insPtr++;
                    break;
                case Instruction.instructionType.set:
                    registers[toExecute.first] = secondOpValue;
                    insPtr++;
                    break;
                case Instruction.instructionType.add:
                    registers[toExecute.first] = firstOpValue + secondOpValue;
                    insPtr++;
                    break;
                case Instruction.instructionType.mul:
                    registers[toExecute.first] = firstOpValue * secondOpValue;
                    insPtr++;
                    break;
                case Instruction.instructionType.mod:
                    registers[toExecute.first] = firstOpValue % secondOpValue;
                    insPtr++;
                    break;
                case Instruction.instructionType.rcv:
                    while (buf.Count == 0) ;
                    lock(thisLock)
                    {
                        registers[toExecute.first] = buf.First();
                        Console.WriteLine($"\t\tProcess {numberOfProgram} received {buf.First()}");
                        buf.RemoveAt(0);
                    }
                    insPtr++;
                    break;
                case Instruction.instructionType.jgz:
                    if (firstOpValue > 0)
                    {
                        insPtr += (int)secondOpValue;
                    }
                    else
                    {
                        insPtr++;
                    }
                    break;
                default:
                    break;
            }
        }

        public struct Instruction
        {
            public instructionType name { get; }
            public char first { get; }
            public string second { get; }

            public Instruction(instructionType name, char first, string second = "")
            {
                this.name = name;
                this.first = first;
                this.second = second;
            }

            public enum instructionType { snd, set, add, mul, mod, rcv, jgz };
        }
    }
}
