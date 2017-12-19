using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day19
{
    public class Diagram
    {
        private enum Direction { up, down, left, right };
        private List<string> diagram;
        private int currX = 0;
        private int currY = 0;
        private Direction lastDirection = Direction.down;

        public int Steps { get; private set; } = 0;
        public string Travel { get; private set; } = "";

        public Diagram(List<string> diagram)
        {
            this.diagram = diagram;
        }

        public void GoGoGo()
        {
            Steps = 0;
            FindStart();
            while (DoMove())
            {
            }
        }

        private void FindStart()
        {
            while (diagram[0][currY] != '|')
            {
                currY++;
            }
        }

        private bool DoMove()
        {
            while ((diagram[currX][currY]) != '+')
            {
                if (diagram[currX][currY] == ' ')
                {
                    return false;
                }

                if (diagram[currX][currY] != '-' && diagram[currX][currY] != '|')
                {
                    Travel += diagram[currX][currY];
                }

                switch (lastDirection)
                {
                    case Direction.up:
                        currX--;
                        break;
                    case Direction.down:
                        currX++;
                        break;
                    case Direction.left:
                        currY--;
                        break;
                    case Direction.right:
                        currY++;
                        break;
                    default:
                        break;
                }
                Steps++;
            }

            if (lastDirection == Direction.left || lastDirection == Direction.right)
            {
                if (diagram[currX - 1][currY] != ' ')
                {
                    currX -= 1;
                    lastDirection = Direction.up;
                }
                else
                {
                    currX += 1;
                    lastDirection = Direction.down;
                }
            }
            else
            {
                if (diagram[currX][currY - 1] != ' ')
                {
                    currY -= 1;
                    lastDirection = Direction.left;
                }
                else
                {
                    currY += 1;
                    lastDirection = Direction.right;
                }
            }
            Steps++;
            return true;
        }
    }
}