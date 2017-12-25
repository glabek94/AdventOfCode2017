using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day25
{
    class TuringMachine
    {
        private bool[] tape;
        private char currentState;
        private Dictionary<char, State> states;
        private long cursor;

        public TuringMachine(long steps)
        {
            tape = new bool[steps];
            cursor = tape.Length / 2;

            states = new Dictionary<char, State>
            {
                { 'A', new State(true, 1, 'B', false, -1, 'C') },
                { 'B', new State(true, -1, 'A', true, -1, 'D') },
                { 'C', new State(true, 1, 'D', false, 1, 'C') },
                { 'D', new State(false, -1, 'B', false, 1, 'E') },
                { 'E', new State(true, 1, 'C', true, -1, 'F') },
                { 'F', new State(true, -1, 'E', true, 1, 'A') }
            };

            currentState = 'A';
        }

        public long MakeSteps(long howMany)
        {
            for (int i = 0; i < howMany; i++)
            {
                MakeStep();
            }

            return tape.Count(x => x);
        }

        private void MakeStep()
        {
            State currentState = states[this.currentState];
            if (!tape[cursor])
            {
                tape[cursor] = currentState.ValueIf0;
                cursor += currentState.NextPosIf0;
                this.currentState = currentState.NextStateIf0;
            }
            else
            {
                tape[cursor] = currentState.ValueIf1;
                cursor += currentState.NextPosIf1;
                this.currentState = currentState.NextStateIf1;
            }
        }

        private struct State
        {
            public bool ValueIf0;
            public int NextPosIf0;
            public char NextStateIf0;

            public bool ValueIf1;
            public int NextPosIf1;
            public char NextStateIf1;

            public State(bool valIf0, int posIf0, char stateIf0, bool valIf1, int posIf1, char stateIf1)
            {
                ValueIf0 = valIf0;
                NextPosIf0 = posIf0;
                NextStateIf0 = stateIf0;

                ValueIf1 = valIf1;
                NextPosIf1 = posIf1;
                NextStateIf1 = stateIf1;
            }
        }
    }
}
