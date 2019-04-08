using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotNavigationProblem
{
    public class Path
    {
        private List<Node> _path = new List<Node>();

        internal List<Node> PathCollection
        {
            get => _path;
            set => _path = value;
        }

        private enum Direction
        {
            Up,
            Down,
            Left,
            Right
        }

        //Constructors for a new path with a starting item, or just an empty path
        public Path(Node pos)
        {
            this.PathCollection.Add(pos);
        }

        public Path() { }

        public void PrintDirectionOutput()
        {
            for (int i = 1; i < PathCollection.Count; i++)
            {
                Console.Write(GetDirection(PathCollection[i - 1].Position, PathCollection[i].Position) + "; ");
            }
            Console.WriteLine(); //New line return after complete output of directions
        }

        //REMOVE ME LATER
        public void PrintPositionOutput()
        {
            foreach (Node n in PathCollection)
            {
                Console.Write(n.Position + "; ");
            }
        }

        private Direction GetDirection(Position posA, Position posB)
        {
            //Note: When all else is equal, nodes should be expanded
            //according to the following order: the agent should try to move UP before attempting
            //LEFT, before attempting DOWN, before attempting RIGHT, in that order.
            //(Assignment Report)
            if (posA.Y > posB.Y)
            {
                return Direction.Up;
            }

            if (posA.X > posB.X)
            {
                return Direction.Left;
            }
               
            if (posB.Y > posA.Y)
            {
                return Direction.Down;
            }

            if (posB.X > posA.X)
            {
                return Direction.Right;
            }
                
            //Throw an error otherwise
            throw new Exception("Error returning direction of movement.");
        }
    }
}