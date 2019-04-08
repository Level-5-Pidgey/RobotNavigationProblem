using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotNavigationProblem
{
    public class Node : IEquatable<Node>
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


        //To allow for more easy comparison between nodes, we can implement IEquatable<T> to it -- which allows us to define that when comparing two nodes, we actually want to compare
        //two positions.
        //Previously, doing OpenList.Contains(node) would check if there was a *specific* node already present, which meant that in algorithms such as A* where nodes had attached cost values
        //these comparisons would fail.
        public bool Equals(Node other)
        {
            return null != other && (Position.X == other.Position.X && Position.Y == other.Position.Y);
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Node);
        }

        public override int GetHashCode()
        {
            return -425505606 + EqualityComparer<Position>.Default.GetHashCode(Position);
        }
    }
}
