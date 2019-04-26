using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Using a class to give information about the position of objects as using a 1-index array and returning the 1st index of the array for giving position information is both cumbersome and inefficient

namespace RobotNavigationProblem
{
    public class Position
    {
        private int _x;
        private int _y;

        public int X
        {
            get => _x;
            private set => _x = value;
        }

        public int Y
        {
            get => _y;
            private set => _y = value;
        }

        public Position(int x, int y)
        {
            X = x;
            Y = y;
        }

        //Override default toString method to allow for the positions to be printed to the console as an output.
        public override string ToString()
        {
            return "(" + X + "," + Y + ")";
        }
    }
}
