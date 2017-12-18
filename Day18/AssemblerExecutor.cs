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
        int numberOfProgram;
        List<long> buf;
        public AssemblerExecutor oth { get; set; }

        public Object thisLock = new object();

        long howManySend = 0;
        bool isDeadlock = false;

        public AssemblerExecutor(int numberOfProgram)
        {
            instructions = new List<Instruction>();
            registers = new Dictionary<char, long>();
            registers.Add('p', numberOfProgram);
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
            while (!isDeadlock)
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
            long firstOpValue = 0;
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

            if (toExecute.second.Length > 0)
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
                    int i = 0;
                    while (buf.Count == 0 && ++i < 10E6) ;
                    if (i == 10E6)
                    {
                        Console.WriteLine($"Process {numberOfProgram} has sent {howManySend}");
                        isDeadlock = true;
                        break;
                    }

                    lock (thisLock)
                    {
                        registers[toExecute.first] = buf.First();
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
