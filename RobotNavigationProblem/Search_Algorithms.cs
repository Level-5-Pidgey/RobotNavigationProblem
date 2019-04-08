using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace RobotNavigationProblem
{
    public class Search_Algorithms
    {
        private Map _map;

        public Search_Algorithms(Map map)
        {
            Map = map;
        }

        public Map Map
        {
            get => _map;
            set => _map = value;
        }

        public Path FindAStar(Position start, Position goal)
        {
            Node startNode = new Node(start);
            Node goalNode = new Node(goal);

            //HashSets have generally much more consistent performance compared to lists when dealing with large amount of objects contained within
            //OpenList will be represented as a List as there will not be many items contained within it over time
            //Since ClosedList will contain many many nodes over time as the path is being found, to keep performance fast a HashSet is utilized
            //https://stackoverflow.com/a/10762995 has a great visual representation on the performance differences between Lists and HashSets re. size

            List<Node> OpenList = new List<Node>();
            HashSet<Node> ClosedList = new HashSet<Node>();

            OpenList.Add(startNode);
            Node CurrentNode = OpenList[0];

            while (OpenList.Count > 0)
            {
                //Get the current node
                //Let the currentNode equal the node with the lowest f value
                for (int i = 0; i < OpenList.Count; i++)
                {
                    if (OpenList[i].FCost < CurrentNode.FCost || (OpenList[i].FCost == CurrentNode.FCost && OpenList[i].HCost < CurrentNode.HCost))
                    {
                        CurrentNode = OpenList[i];
                    }
                }

                //Remove the currentNode from the openList
                //Add the currentNode to the closedList
                OpenList.Remove(CurrentNode);
                ClosedList.Add(CurrentNode);

                //Check if current node is actually the goal
                //if so, gratz! We can now backtrack and get the finished path.
                if (CurrentNode.Position.X == goalNode.Position.X && CurrentNode.Position.Y == goalNode.Position.Y)
                {
                    break;
                }

                //Generate neighbour/child nodes
                //Let the neighbours of the current nodes equal the adjacent nodes
                foreach (Node neighbour in Map.GetNeighbouringNodes(CurrentNode))
                {
                    //Child is on the closedList
                    //if child is in the closedList
                    //continue to beginning of for loop
                    //.Add() in an if statement for a Hash Set works like .Contains()
                    if (ClosedList.Any(n1 => n1.Position.X == CurrentNode.Position.X && n1.Position.Y == CurrentNode.Position.Y) || !Map.IsValid(neighbour.Position))
                    {
                        continue;
                    }
                    else if (OpenList.Any(n1 => n1.Position.X == CurrentNode.Position.X && n1.Position.Y == CurrentNode.Position.Y))
                    {
                        float MoveCost = neighbour.Parent.GCost + neighbour.Parent.HCost;

                        if (CurrentNode.GCost + CurrentNode.HCost < MoveCost)
                        {
                            int index = OpenList.FindIndex((n1) => n1 == neighbour);

                            OpenList[index].Parent = CurrentNode;
                            OpenList[index].GCost = OpenList[index].Parent.GCost + OpenList[index].Parent.HCost;
                        }
                    }
                    else
                    {
                        neighbour.Parent = CurrentNode;
                        neighbour.GCost = neighbour.Parent.GCost + neighbour.Parent.HCost;
                        neighbour.HCost = GetManhattanDistance(neighbour, goalNode);

                        OpenList.Add(neighbour);
                    }
                }
            }

            if (CurrentNode.Position.X == goalNode.Position.X && CurrentNode.Position.Y == goalNode.Position.Y)
            {
                return GetFinalPath(startNode, CurrentNode);
            }
            else
            {
                throw new Exception("Shit is fucky wucky :(");
            }
                       
        }

        public Path FindDFS(Position start, Position goal)
        {
            Node startNode = new Node(start);
            Node currentNode = new Node(start);
            Node goalNode = new Node(goal);

            List<Node> openList = new List<Node>();
            HashSet<Node> closedList = new HashSet<Node>();

            openList.Add(startNode);

            while (openList.Count != 0)
            {
                currentNode = openList[0];
                openList.RemoveAt(0);
                closedList.Add(currentNode);

                if (currentNode.Position.X == goalNode.Position.X && currentNode.Position.Y == goalNode.Position.Y)
                {
                    break;
                }

                foreach (Node neighbour in Map.GetNeighbouringNodes(currentNode))
                {
                    if (Map.IsValid(neighbour.Position))
                    {
                        if(closedList.Add(neighbour))
                        { 
                            neighbour.Parent = currentNode;
                            closedList.Add(neighbour);
                            openList.Add(neighbour);
                        }
                    }
                }
            }

            return GetFinalPath(startNode, currentNode);
        }

        Path GetFinalPath(Node StartNode, Node FinalNode)
        {
            Path Finalpath = new Path();
            Node curr = FinalNode;

            while (curr != null && curr != StartNode)
            {
                Finalpath.PathCollection.Add(curr);
                curr = curr.Parent;
            }

            Finalpath.PathCollection.Reverse();

            return Finalpath;
        }

        float GetManhattanDistance(Node n1, Node n2)
        {
            float distX = Math.Abs(n1.Position.X - n2.Position.X);
            float distY = Math.Abs(n1.Position.Y - n2.Position.Y);

            return distX + distY;
        }
    }
}
