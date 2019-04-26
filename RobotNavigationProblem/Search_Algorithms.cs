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
        //Private variables and properties
        private Map _map;
        private int _collectiveNodeCount;
        private int _frontierCount;
        private int _iterationCount;
        private const int MAX_ITERATIONS = 100000;

        public Search_Algorithms(Map map)
        {
            Map = map;
        }

        public Map Map
        {
            get => _map;
            set => _map = value;
        }

        public int CollectiveNodeCount
        {
            get => _collectiveNodeCount;
            private set => _collectiveNodeCount = value;
        }

        public int FrontierCount
        {
            get => _frontierCount;
            private set => _frontierCount = value;
        }

        public int IterationCount
        {
            get => _iterationCount;
            private set => _iterationCount = value;
        }

        //Pathfinding methods 

        /// <summary>
        /// Finds a route using the Depth first search algorithm. Traverses a tree, starting from a root node (start position) without using a heuristic value.
        /// </summary>
        /// <param name="start">The position the route should start at to find a goal from.</param>
        /// <param name="goal">The position the pathfinding method is creating a route to.</param>
        /// <returns>A finalized path after finding a goal.</returns>
        public Path FindDFS(Position start, Position goal)
        {
            //Reset the nodes searched counter, for diagnostics purposes.
            CollectiveNodeCount = 0;
            FrontierCount = 0;
            IterationCount = 0;

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

                //Checking if the current iteration count has exceeded the max allowed iteration count
                if (IterationCount > MAX_ITERATIONS)
                {
                    //The search has iterated over the maximum number assumed to find a route. This means either there is no valid path, or the path is extremely long
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

                //Update iteration count. For demonstrative purposes + to catch if there's no valid path.
                IterationCount++;
            }

            if (currentNode.Equals(goalNode))
            {
                CollectiveNodeCount = openList.Count + closedList.Count;
                FrontierCount = openList.Count;
                return GetFinalPath(startNode, currentNode);
            }
            else
            {
                throw new Exception("Error with DFS Algorithm. Either there is no valid path, or one cannot be found.");
            }
        }

        /// <summary>
        /// Finds a route using the Breadth first search algorithm. Traverses a tree, starting from a root node (start position) and explores all neighbouring nodes at the same depth prior to exploring the next node without using a heuristic value.
        /// </summary>
        /// <param name="start">The position the route should start at to find a goal from.</param>
        /// <param name="goal">The position the pathfinding method is creating a route to.</param>
        /// <returns>A finalized path after finding a goal.</returns>
        public Path FindBFS(Position start, Position goal)
        {
            //Reset the nodes searched counter, for diagnostics purposes.
            CollectiveNodeCount = 0;
            FrontierCount = 0;
            IterationCount = 0;

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

                //Checking if the current iteration count has exceeded the max allowed iteration count
                if (IterationCount > MAX_ITERATIONS)
                {
                    //The search has iterated over the maximum number assumed to find a route. This means either there is no valid path, or the path is extremely long
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

                //Update iteration count. For demonstrative purposes + to catch if there's no valid path.
                IterationCount++;
            }

            if (currentNode.Equals(goalNode))
            {
                CollectiveNodeCount = openList.Count + closedList.Count;
                FrontierCount = openList.Count;
                return GetFinalPath(startNode, currentNode);
            }
            else
            {
                throw new Exception("Error with BFS Algorithm. Either there is no valid path, or one cannot be found.");
            }
        }

        /// <summary>
        /// Finds a route using a greedy-best first search algorithm. As the nodes in the tree are traversed, the most promising neighbour to find the destination is expanded and the others are disregarded. The Manhattan distance is used as the heuristic cost for picking the most promising node.
        /// </summary>
        /// <param name="start">The position the route should start at to find a goal from.</param>
        /// <param name="goal">The position the pathfinding method is creating a route to.</param>
        /// <returns>A finalized path after finding a goal.</returns>
        public Path FindGreedyBest(Position start, Position goal)
        {
            //Reset the nodes searched counter, for diagnostics purposes.
            CollectiveNodeCount = 0;
            FrontierCount = 0;
            IterationCount = 0;

            Node startNode = new Node(start);
            Node goalNode = new Node(goal);

            Queue<Node> openList = new Queue<Node>();
            HashSet<Node> closedList = new HashSet<Node>();

            Node currentNode = startNode;
            openList.Enqueue(currentNode);
            currentNode.HCost = GetManhattanDistance(currentNode, goalNode);

            while (openList.Count != 0)
            {
                currentNode = openList.Dequeue();
                closedList.Add(currentNode);

                if (currentNode.Equals(goalNode))
                {
                    break;
                }

                //Checking if the current iteration count has exceeded the max allowed iteration count
                if (IterationCount > MAX_ITERATIONS)
                {
                    //The search has iterated over the maximum number assumed to find a route. This means either there is no valid path, or the path is extremely long
                    break;
                }

                foreach (Node neighbour in Map.GetNeighbouringNodes(currentNode))
                {
                    if (closedList.Contains(neighbour) || !Map.IsValid(neighbour.Position))
                    {
                        continue;
                    }

                    neighbour.HCost = GetManhattanDistance(neighbour, goalNode);
                    if (!openList.Contains(neighbour) || currentNode.HCost > neighbour.HCost)
                    {
                        neighbour.Parent = currentNode;
                        openList.Enqueue(neighbour);
                    }
                }

                //Update iteration count. For demonstrative purposes + to catch if there's no valid path.
                IterationCount++;
            }

            if (currentNode.Equals(goalNode))
            {
                CollectiveNodeCount = openList.Count + closedList.Count;
                FrontierCount = openList.Count;
                return GetFinalPath(startNode, currentNode);
            }
            else
            {
                throw new Exception("Error with Greedy Best First Algorithm. Either there is no valid path, or one cannot be found.");
            }
        }

        /// <summary>
        /// A* Search finds a route from a starting node to a goal node using a more complex heuristic -- choosing the most optimal neighbour of each current node, and preferring the shortest possible path to the goal.
        /// </summary>
        /// <param name="start">The position the route should start at to find a goal from.</param>
        /// <param name="goal">The position the pathfinding method is creating a route to.</param>
        /// <returns>A finalized path after finding a goal.</returns>
        public Path FindAStar(Position start, Position goal)
        {
            //Reset the nodes searched counter, for diagnostics purposes.
            CollectiveNodeCount = 0;
            FrontierCount = 0;
            IterationCount = 0;

            Node startNode = new Node(start);
            Node goalNode = new Node(goal);

            //HashSets have generally much more consistent performance compared to lists when dealing with large amount of objects contained within
            //OpenList will be represented as a List as there will not be many items contained within it over time
            //Since ClosedList will contain many many nodes over time as the path is being found, to keep performance fast a HashSet is utilized
            //https://stackoverflow.com/a/10762995 has a great visual representation on the performance differences between Lists and HashSets re. size

            List<Node> openList = new List<Node>();
            HashSet<Node> closedList = new HashSet<Node>();

            openList.Add(startNode);
            Node currentNode = startNode;
            currentNode.HCost = GetManhattanDistance(currentNode, goalNode);

            while (openList.Count != 0)
            {
                //Sort list and place the lowest FCost Node to the start
                openList.Sort((n1, n2) => (n1.FCost).CompareTo(n2.FCost));
                currentNode = openList[0];

                //Remove the currentNode from the openList
                //Add the currentNode to the closedList
                openList.Remove(currentNode);
                closedList.Add(currentNode);

                //Check if current node is actually the goal
                //if so, gratz! We can now backtrack and get the finished path.
                if (currentNode.Equals(goalNode))
                {
                    break;
                }

                //Checking if the current iteration count has exceeded the max allowed iteration count
                if (IterationCount > MAX_ITERATIONS)
                {
                    //The search has iterated over the maximum number assumed to find a route. This means either there is no valid path, or the path is extremely long
                    break;
                }

                //For each connected/child node around the currently explored node,
                //generate children
                foreach (Node neighbour in Map.GetNeighbouringNodes(currentNode))
                {
                    //If the neighbour's position is already present on the closed list 
                    if (!Map.IsValid(neighbour.Position) || closedList.Contains(neighbour))
                    {
                        //If it is, we can ignore this neighbour, it's not in the direction desired towards the desired goal
                        continue;
                    }
                    neighbour.GCost = currentNode.GCost + GetManhattanDistance(currentNode, neighbour);
                    neighbour.HCost = GetManhattanDistance(neighbour, goalNode);

                    //If the child is not on the open list or if it's costs are cheaper, calculate heuristics w/ Manhattan distance
                    //Update existing node/add new
                    if (currentNode.FCost > neighbour.FCost || !openList.Contains(neighbour))
                    {
                        neighbour.Parent = currentNode;

                        //If the node isn't present on the open list, it should then be added
                        if (!openList.Contains(neighbour))
                        {
                            openList.Add(neighbour);
                        }
                    }
                }

                //Update iteration count. For demonstrative purposes + to catch if there's no valid path.
                IterationCount++;
            }

            if (currentNode.Equals(goalNode))
            {
                CollectiveNodeCount = openList.Count + closedList.Count;
                FrontierCount = openList.Count;
                return GetFinalPath(startNode, currentNode);
            }
            else
            {
                throw new Exception("Error with A* Algorithm. Either there is no valid path, or one cannot be found.");
            }
        }

        /// <summary>
        /// Uniform cost search (a.k.a. Dijkstra's Algorithm) optimises the search path by sorting the list of potential nodes to be explored by a heuristic (Manhattan Distance in this case), and then explores the most promising node.
        /// </summary>
        /// <param name="start">The position the route should start at to find a goal from.</param>
        /// <param name="goal">The position the pathfinding method is creating a route to.</param>
        /// <returns>A finalized path after finding a goal.</returns>
        public Path FindUniformCost(Position start, Position goal)
        {
            //Reset the nodes searched counter, for diagnostics purposes.
            CollectiveNodeCount = 0;
            FrontierCount = 0;
            IterationCount = 0;

            Node startNode = new Node(start);
            Node goalNode = new Node(goal);

            Queue<Node> openList = new Queue<Node>();
            HashSet<Node> closedList = new HashSet<Node>();

            Node currentNode = startNode;
            openList.Enqueue(currentNode);
            currentNode.HCost = GetManhattanDistance(currentNode, goalNode);

            while (openList.Count != 0)
            {
                currentNode = openList.Dequeue();
                closedList.Add(currentNode);

                if (currentNode.Equals(goalNode))
                {
                    break;
                }

                //Checking if the current iteration count has exceeded the max allowed iteration count
                if (IterationCount > MAX_ITERATIONS)
                {
                    //The search has iterated over the maximum number assumed to find a route. This means either there is no valid path, or the path is extremely long
                    break;
                }

                foreach (Node neighbour in Map.GetNeighbouringNodes(currentNode))
                {
                    if (closedList.Contains(neighbour) || !Map.IsValid(neighbour.Position))
                    {
                        continue;
                    }

                    neighbour.Parent = currentNode;
                    neighbour.GCost = neighbour.Parent.GCost + 1;

                    if (currentNode.GCost < neighbour.GCost)
                    {

                        if (!openList.Contains(neighbour))
                        {
                            openList.Enqueue(neighbour);
                        }
                    }
                }

                //Update iteration count. For demonstrative purposes + to catch if there's no valid path.
                IterationCount++;
            }

            if (currentNode.Equals(goalNode))
            {
                CollectiveNodeCount = openList.Count + closedList.Count;
                FrontierCount = openList.Count;
                return GetFinalPath(startNode, currentNode);
            }
            else
            {
                throw new Exception("Error with Uniform Cost Search algorithm. Either there is no valid path, or one cannot be found.");
            }
        }

        /// <summary>
        /// A variant of Breadth-first Search that searches from the goal to the start and vice-versa simultaneously.
        /// </summary>
        /// <param name="start">The position the route should start at to find a goal from.</param>
        /// <param name="goal">The position the pathfinding method is creating a route to.</param>
        /// <returns>A finalized path after finding a goal.</returns>
        public Path FindBidirectionalBFS(Position start, Position goal)
        {
            //Reset the nodes searched counter, for diagnostics purposes.
            CollectiveNodeCount = 0;
            FrontierCount = 0;
            IterationCount = 0;

            Node startNode = new Node(start);
            Node goalNode = new Node(goal);

            Queue<Node> openList1 = new Queue<Node>();
            Queue<Node> openList2 = new Queue<Node>();
            HashSet<Node> closedList = new HashSet<Node>();

            openList1.Enqueue(startNode);
            openList2.Enqueue(goalNode);

            Node currentNode1 = startNode;
            Node currentNode2 = goalNode;

            while (openList1.Count != 0 && openList2.Count != 0)
            {
                BidirectionalBFS(openList1, closedList, currentNode1);
                BidirectionalBFS(openList2, closedList, currentNode2);

                if (BidirectionalIntersection(openList1, openList2) != null)
                {
                    break;
                }

                //Checking if the current iteration count has exceeded the max allowed iteration count
                if (IterationCount > MAX_ITERATIONS)
                {
                    //The search has iterated over the maximum number assumed to find a route. This means either there is no valid path, or the path is extremely long
                    break;
                }

                //Update iteration count. For demonstrative purposes + to catch if there's no valid path.
                IterationCount++;
            }

            if (BidirectionalIntersection(openList2, openList1) != null)
            {
                CollectiveNodeCount = openList2.Count + openList1.Count + closedList.Count;
                FrontierCount = openList1.Count + openList2.Count;
                return GetBidirectionalPath(openList1, openList2, startNode, goalNode);
            }
            else
            {
                throw new Exception("Error with UBFS Algorithm. Either there is no valid path, or one cannot be found.");
            }
        }

        //Other methods utilized by one/all pathfinding methods.
        //--
        //Region to encapsulate and hide all Bidirectional-related methods.
        #region
        /// <summary>
        /// A method to perform a single "step" of Breadth-first search.
        /// </summary>
        /// <param name="openList">The current "frontier" or list of potential nodes to explore.</param>
        /// <param name="closedList">A HashSet of nodes that have already been explored by the search function.</param>
        /// <param name="currentNode">The node currently chosen to find neighbours for.</param>
        void BidirectionalBFS(Queue<Node> openList, HashSet<Node> closedList, Node currentNode)
        {
            currentNode = openList.Dequeue();
            closedList.Add(currentNode);

            foreach (Node neighbour in Map.GetNeighbouringNodes(currentNode))
            {
                if (closedList.Contains(neighbour) || !Map.IsValid(neighbour.Position))
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="openList1">The 1st list the Bidirectional search uses. From Start -> Goal</param>
        /// <param name="openList2">The 2nd list the Bidirectional search uses. From Goal -> Start</param>
        /// <returns>The node (and therefore position) that the two lists have met at and intersected.</returns>
        Node BidirectionalIntersection(Queue<Node> openList1, Queue<Node> openList2)
        {
            foreach (Node n in openList1)
            {
                if (openList2.Contains(n))
                {
                    return n;
                }
            }

            return null;
        }

        /// <summary>
        /// Method to create a path from 2 queues, knowing the start and goal nodes.
        /// </summary>
        /// <param name="list1">The 1st list the Bidirectional search uses. From Start -> Goal</param>
        /// <param name="list2">The 2nd list the Bidirectional search uses. From Goal -> Start</param>
        /// <param name="startNode">The node that represents the starting state.</param>
        /// <param name="goalNode">The node that represents the goal state.</param>
        /// <returns>A finalized path after finding a goal.</returns>
        Path GetBidirectionalPath(Queue<Node> list1, Queue<Node> list2, Node startNode, Node goalNode)
        {
            Path finalPath = new Path();
            Node curr = BidirectionalIntersection(list1, list2);

            //Start with the 2nd list -- going from the goal -> middle.
            while (curr != null && curr != goalNode)
            {
                finalPath.PathCollection.Add(curr);
                curr = curr.Parent;
            }

            //We wanna reverse here, rather than at the end to make a readable path.
            finalPath.PathCollection.Reverse();

            //Now we've reacheed the middle we can go from the middle -> start.
            curr = BidirectionalIntersection(list2, list1).Parent;
            while (curr != null && curr != startNode)
            {
                finalPath.PathCollection.Add(curr);
                curr = curr.Parent;
            }

            return finalPath;
        }
        #endregion

        //Methods used by all standard search types.
        //--

        /// <summary>
        /// Method to create a path using the parent nodes of a node object passed by a search algorithm.
        /// </summary>
        /// <param name="startNode">The node that represents the starting state.</param>
        /// <param name="finalNode">The node that represents the goal state. For search algorithms, this would be the "currentNode" once the goal has been reached.</param>
        /// <returns>A finalized path after finding a goal.</returns>
        Path GetFinalPath(Node startNode, Node finalNode)
        {
            Path finalPath = new Path();
            Node curr = finalNode;

            while (curr != null && curr != startNode)
            {
                finalPath.PathCollection.Add(curr);
                curr = curr.Parent;
            }

            finalPath.PathCollection.Add(startNode);
            finalPath.PathCollection.Reverse();

            return finalPath;
        }

        /// <summary>
        /// Calculates the floating-point value of the straight-line distance between two nodes.
        /// </summary>
        /// <param name="n1">1st Node to be compared with the 2nd.</param>
        /// <param name="n2">2nd Node to be compared with the 1st.</param>
        /// <returns>A floating point value of the straight-line distance betwen two nodes.</returns>
        float GetManhattanDistance(Node n1, Node n2)
        {
            float distX = Math.Abs(n1.Position.X - n2.Position.X);
            float distY = Math.Abs(n1.Position.Y - n2.Position.Y);

            return distX + distY;
        }
    }
}
