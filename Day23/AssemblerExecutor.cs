using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day23
{
    public class AssemblerExecutor
    {
        List<Instruction> instructions;
        Dictionary<char, long> registers;
        int insPtr;

        int howManyMul = 0;

        public AssemblerExecutor()
        {
            instructions = new List<Instruction>();
            registers = new Dictionary<char, long>();
            AddRegister('a');
            insPtr = 0;
        }

        private void AddRegister(char name)
        {
            registers.Add(name, 0);
        }

        public int Execute()
        {
            while (insPtr < instructions.Count)
            {
                ExecuteInstruction(instructions[insPtr]);
            }

            return howManyMul;
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
                    AddRegister(toExecute.first);

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
                        AddRegister(toExecute.second[0]);

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
                case Instruction.instructionType.set:
                    registers[toExecute.first] = secondOpValue;
                    insPtr++;
                    break;
                case Instruction.instructionType.mul:
                    howManyMul++;
                    registers[toExecute.first] = firstOpValue * secondOpValue;
                    insPtr++;
                    break;
                case Instruction.instructionType.jnz:
                    if (firstOpValue != 0)
                    {
                        insPtr += (int)secondOpValue;
                    }
                    else
                    {
                        insPtr++;
                    }
                    break;
                case Instruction.instructionType.sub:
                    registers[toExecute.first] = firstOpValue - secondOpValue;
                    insPtr++;
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

            public enum instructionType { set, mul, sub, jnz };
        }
    }
}