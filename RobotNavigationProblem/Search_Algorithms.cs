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

                //For each connected/child node around the currently explored node,
                //generate children
                foreach (Node neighbour in Map.GetNeighbouringNodes(CurrentNode))
                {
                    #region
                    ////If the neighbour's position is already present on the closed list 
                    //if (ClosedList.Any((n1) => (n1.Position.X == neighbour.Position.X && n1.Position.Y == neighbour.Position.Y)))
                    //{
                    //    //We can ignore it and move on as it's already been explored
                    //    continue;
                    //}
                    ////If the child is already on the open list, let's check to see if it's a better
                    ////and more efficient path than currently
                    ////If not, add it and assign the parent node as the currently explored node
                    //else if (OpenList.Any(n1 => n1.Position.X == neighbour.Position.X && n1.Position.Y == neighbour.Position.Y))
                    //{
                    //    neighbour.Parent = CurrentNode;
                    //    float MoveCost = neighbour.Parent.GCost + neighbour.Parent.HCost;

                    //    //If the currentNode's FCost is less than the neighbour's FCost, swap the parents over
                    //    if (CurrentNode.GCost + CurrentNode.HCost < MoveCost)
                    //    {
                    //        //linq expression to find the index of the neighbour within the OpenList
                    //        int index = OpenList.FindIndex((n1) => n1 == neighbour);

                    //        OpenList[index].Parent = CurrentNode;
                    //        OpenList[index].GCost = OpenList[index].Parent.GCost + OpenList[index].Parent.HCost;
                    //    }
                    //}
                    //else
                    //{
                    //    neighbour.Parent = CurrentNode;
                    //    neighbour.GCost = neighbour.Parent.GCost + neighbour.Parent.HCost;
                    //    neighbour.HCost = GetManhattanDistance(neighbour, goalNode);

                    //    OpenList.Add(neighbour);
                    //}
                    #endregion

                    if(!Map.IsValid(neighbour.Position) || ClosedList.Any((n1) => (n1.Position.X == neighbour.Position.X && n1.Position.Y == neighbour.Position.Y)))
                    {
                        continue;
                    }
                    float MoveCost = CurrentNode.GCost + GetManhattanDistance(CurrentNode, neighbour);

                    if(MoveCost < neighbour.FCost || !OpenList.Any((n1) => (n1.Position.X == neighbour.Position.X && n1.Position.Y == neighbour.Position.Y)))
                    {
                        neighbour.GCost = MoveCost;
                        neighbour.HCost = GetManhattanDistance(neighbour, goalNode);
                        neighbour.Parent = CurrentNode;

                        if(!OpenList.Any((n1) => (n1.Position.X == neighbour.Position.X && n1.Position.Y == neighbour.Position.Y)))
                        {
                            OpenList.Add(neighbour);
                        }
                    }
                }
            }

            if (CurrentNode.Position.X == goalNode.Position.X && CurrentNode.Position.Y == goalNode.Position.Y)
            {
                return GetFinalPath(startNode, CurrentNode);
            }
            else
            {
                throw new Exception("Error with A* Algorithm.");
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

            Finalpath.PathCollection.Add(StartNode);
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
