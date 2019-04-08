using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotNavigationProblem
{
    public class Node
    {
        //Need to add depth of node

        private float _gCost;
        private float _hCost;
        private Position _position;

        public Node(Position pos, Node parent = null)
        {
            Position = pos;
            Parent = parent;
        }

        //To assign the node "previous" to it -- the parent node leading from the origin
        public Node Parent
        {
            get;
            set;
        }

        public float GCost
        {
            get => _gCost;
            set => _gCost = value;
        }

        public float HCost
        {
            get => _hCost;
            set => _hCost = value;
        }

        public Position Position
        {
            get => _position;
            private set => _position = value;
        }

        public double FCost
        {
            get
            {
                return (float)(GCost + HCost);
            }
        }
    }
}
