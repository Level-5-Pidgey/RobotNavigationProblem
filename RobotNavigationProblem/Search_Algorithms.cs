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
            Node CurrentNode = CurrentNode = OpenList[0];

            while (OpenList.Count != 0)
            {
                CurrentNode = OpenList[0];
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
                if (CurrentNode.Equals(goalNode))
                {
                    break;
                }

                //For each connected/child node around the currently explored node,
                //generate children
                foreach (Node neighbour in Map.GetNeighbouringNodes(CurrentNode))
                {
                    //If the neighbour's position is already present on the closed list 
                    if (!Map.IsValid(neighbour.Position) || ClosedList.Contains(neighbour))
                    {
                        //If it is, we can ignore this neighbour, it's not in the direction desired towards the desired goal
                        continue;
                    }
                    neighbour.GCost = CurrentNode.GCost + GetManhattanDistance(CurrentNode, neighbour);
                    neighbour.HCost = GetManhattanDistance(neighbour, goalNode);

                    //If the child is not on the open list or if it's costs are cheaper, calculate heuristics w/ Manhattan distance
                    //Update existing node/add new
                    if (CurrentNode.FCost < neighbour.FCost || !OpenList.Contains(neighbour))
                    {
                        neighbour.Parent = CurrentNode;

                        //If the node isn't present on the open list, it should then be added
                        if (!OpenList.Contains(neighbour))
                        {
                            OpenList.Add(neighbour);
                        }
                    }
                }
            }

            if (CurrentNode.Equals(goalNode))
            {
                Console.WriteLine("(A*) Number of explored nodes: " + (ClosedList.Count() + OpenList.Count()));
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

                if (currentNode.Equals(goalNode))
                {
                    break;
                }

                foreach (Node neighbour in Map.GetNeighbouringNodes(currentNode))
                {
                    if (closedList.Contains(neighbour) || !Map.IsValid(neighbour.Position))
                    {
                        continue;
                    }

                    if (!openList.Contains(neighbour))
                    {
                        neighbour.Parent = currentNode;
                        openList.Add(neighbour);
                    }
                }
            }

            if (currentNode.Equals(goalNode))
            {
                Console.WriteLine("(DFS) Number of explored nodes: " + (closedList.Count() + openList.Count()));
                return GetFinalPath(startNode, currentNode);
            }
            else
            {
                throw new Exception("Error with DFS Algorithm.");
            }
        }

        public Path FindBFS(Position start, Position goal)
        {
            Node startNode = new Node(start);
            Node goalNode = new Node(goal);

            Queue<Node> openList = new Queue<Node>();
            HashSet<Node> closedList = new HashSet<Node>();

            Node currentNode = startNode;
            openList.Enqueue(currentNode);

            while (openList.Count != 0)
            {
                currentNode = openList.Dequeue();
                closedList.Add(currentNode);

                if (currentNode.Equals(goalNode))
                {
                    break;
                }

                foreach (Node neighbour in Map.GetNeighbouringNodes(currentNode))
                {
                    if (closedList.Contains(neighbour) || !Map.IsValid(currentNode.Position))
                    {
                        continue;
                    }
                    
                    if (!openList.Contains(neighbour))
                    {
                        neighbour.Parent = currentNode;
                        openList.Enqueue(neighbour);
                    }
                }
            }

            if (currentNode.Equals(goalNode))
            {
                Console.WriteLine("(BFS) Number of explored nodes: " + (closedList.Count() + openList.Count()));
                return GetFinalPath(startNode, currentNode);
            }
            else
            {
                throw new Exception("Error with BFS Algorithm.");
            }
        }

        public Path FindGreedyBest(Position start, Position goal)
        {
            Node startNode = new Node(start);
            Node goalNode = new Node(goal);

            List<Node> openList = new List<Node>();
            HashSet<Node> closedList = new HashSet<Node>();

            Node currentNode = startNode;
            openList.Add(currentNode);
            currentNode.HCost = GetManhattanDistance(currentNode, goalNode);

            while (openList.Count != 0)
            {
                currentNode = openList[0];
                //Get the current node
                //Let the currentNode equal the node with the lowest f value
                for (int i = 0; i < openList.Count; i++)
                {
                    if (openList[i].HCost < currentNode.HCost)
                    {
                        currentNode = openList[i];
                    }
                }

                openList.Remove(currentNode);
                closedList.Add(currentNode);

                if (currentNode.Equals(goalNode))
                {
                    break;
                }

                foreach (Node neighbour in Map.GetNeighbouringNodes(currentNode))
                {
                    if (closedList.Contains(neighbour) || !Map.IsValid(neighbour.Position))
                    {
                        continue;
                    }

                    neighbour.HCost = GetManhattanDistance(neighbour, goalNode);
                    if (currentNode.HCost > neighbour.HCost || !openList.Contains(neighbour))
                    {
                        neighbour.Parent = currentNode;
                        if (!openList.Contains(neighbour))
                        {
                            openList.Add(neighbour);
                        }
                    }
                }
            }

            if (currentNode.Equals(goalNode))
            {
                Console.WriteLine("(GBFS) Number of explored nodes: " + (closedList.Count() + openList.Count()));
                return GetFinalPath(startNode, currentNode);
            }
            else
            {
                throw new Exception("Error with Greedy Best First Algorithm.");
            }
        }

        Path GetFinalPath(Node StartNode, Node FinalNode)
        {
            Path finalPath = new Path();
            Node curr = FinalNode;

            while (curr != null && curr != StartNode)
            {
                finalPath.PathCollection.Add(curr);
                curr = curr.Parent;
            }

            finalPath.PathCollection.Add(StartNode);
            finalPath.PathCollection.Reverse();

            return finalPath;
        }

        float GetManhattanDistance(Node n1, Node n2)
        {
            float distX = Math.Abs(n1.Position.X - n2.Position.X);
            float distY = Math.Abs(n1.Position.Y - n2.Position.Y);

            return distX + distY;
        }
    }
}
