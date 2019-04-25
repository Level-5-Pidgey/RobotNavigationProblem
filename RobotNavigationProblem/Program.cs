using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;

namespace RobotNavigationProblem
{
    static class Program
    {
        static void Main(string[] args)
        {
            //Variable assignment
            Map map;

            //Launching program
            if (args.Length != 2)
            {
                throw new ArgumentException("2 arguments are expected, please consult the README for additional information");
            }

            //Valid arguments given to run the program in CLI
            //Uses a relative filepath from the .exe!!
            //i.e. users must put the file being searched in the same directory as the .exe, or use escapes to move up folders!
            string fileDir = args[0] + ".txt";
            string pathfindingMethod = args[1];

            if(!File.Exists(fileDir))
            {
                throw new FileNotFoundException("Your file cannot be located, please check the directory and consult the README for additional information");
            }
            else
            {
                map = new Map(fileDir);
            }

            switch (pathfindingMethod.ToLower())
            {
                case "dfs":
                    {
                        Console.WriteLine("+======================================+");
                        Console.WriteLine("+= Initializing Depth First Search... =+");
                        Console.WriteLine("+======================================+");
                        Console.WriteLine("");
                        Console.WriteLine("From start position to goal 1...");
                        Console.WriteLine("");

                        var pathSearchTimer = new Stopwatch();
                        Search_Algorithms pathSearch = new Search_Algorithms(map);
                        pathSearchTimer.Start();
                        Path path = pathSearch.FindDFS(map.StartPos, map.Goal[0]);
                        pathSearchTimer.Stop();

                        //Output number of nodes on both open/closed list to find a result.
                        Console.WriteLine("Number of Nodes Explored: " + pathSearch.NumNodes);

                        Console.WriteLine("Positions:");
                        path.PrintPositionOutput();

                        Console.WriteLine("Directions:");
                        path.PrintDirectionOutput();

                        pathSearchTimer.Start();
                        path = pathSearch.FindDFS(map.Goal[0], map.Goal[1]);
                        pathSearchTimer.Stop();
                        Console.WriteLine("");
                        Console.WriteLine("From goal 1 to goal 2...");
                        Console.WriteLine("");

                        //Output number of nodes on both open/closed list to find a result.
                        Console.WriteLine("Number of Nodes Explored: " + pathSearch.NumNodes);

                        Console.WriteLine("Positions:");
                        path.PrintPositionOutput();

                        Console.WriteLine("Directions:");
                        path.PrintDirectionOutput();

                        Console.WriteLine("");
                        Console.WriteLine($"DFS Time taken (Total): { pathSearchTimer.ElapsedMilliseconds } ms");
                        break;
                    }
                case "bfs":
                    {
                        Console.WriteLine("+========================================+");
                        Console.WriteLine("+= Initializing Breadth First Search... =+");
                        Console.WriteLine("+========================================+");
                        Console.WriteLine("");
                        Console.WriteLine("From start position to goal 1...");
                        Console.WriteLine("");

                        var pathSearchTimer = new Stopwatch();
                        Search_Algorithms pathSearch = new Search_Algorithms(map);
                        pathSearchTimer.Start();
                        Path path = pathSearch.FindBFS(map.StartPos, map.Goal[0]);
                        pathSearchTimer.Stop();

                        //Output number of nodes on both open/closed list to find a result.
                        Console.WriteLine("Number of Nodes Explored: " + pathSearch.NumNodes);

                        Console.WriteLine("Positions:");
                        path.PrintPositionOutput();

                        Console.WriteLine("Directions:");
                        path.PrintDirectionOutput();

                        pathSearchTimer.Start();
                        path = pathSearch.FindBFS(map.Goal[0], map.Goal[1]);
                        pathSearchTimer.Stop();
                        Console.WriteLine("");
                        Console.WriteLine("From goal 1 to goal 2...");
                        Console.WriteLine("");

                        //Output number of nodes on both open/closed list to find a result.
                        Console.WriteLine("Number of Nodes Explored: " + pathSearch.NumNodes);

                        Console.WriteLine("Positions:");
                        path.PrintPositionOutput();

                        Console.WriteLine("Directions:");
                        path.PrintDirectionOutput();

                        Console.WriteLine("");
                        Console.WriteLine($"BFS Time taken (Total): { pathSearchTimer.ElapsedMilliseconds } ms");
                        break;
                    }
                case "astar":
                    {
                        Console.WriteLine("+=============================+");
                        Console.WriteLine("+= Initializing A* Search... =+");
                        Console.WriteLine("+=============================+");
                        Console.WriteLine("");
                        Console.WriteLine("From start position to goal 1...");
                        Console.WriteLine("");

                        var pathSearchTimer = new Stopwatch();
                        Search_Algorithms pathSearch = new Search_Algorithms(map);
                        pathSearchTimer.Start();
                        Path path = pathSearch.FindAStar(map.StartPos, map.Goal[0]);
                        pathSearchTimer.Stop();

                        //Output number of nodes on both open/closed list to find a result.
                        Console.WriteLine("Number of Nodes Explored: " + pathSearch.NumNodes);

                        Console.WriteLine("Positions:");
                        path.PrintPositionOutput();

                        Console.WriteLine("Directions:");
                        path.PrintDirectionOutput();

                        pathSearchTimer.Start();
                        path = pathSearch.FindAStar(map.Goal[0], map.Goal[1]);
                        pathSearchTimer.Stop();
                        Console.WriteLine("");
                        Console.WriteLine("From goal 1 to goal 2...");
                        Console.WriteLine("");

                        //Output number of nodes on both open/closed list to find a result.
                        Console.WriteLine("Number of Nodes Explored: " + pathSearch.NumNodes);

                        Console.WriteLine("Positions:");
                        path.PrintPositionOutput();

                        Console.WriteLine("Directions:");
                        path.PrintDirectionOutput();

                        Console.WriteLine("");
                        Console.WriteLine($"A* Search Time taken (Total): { pathSearchTimer.ElapsedMilliseconds } ms");
                        break;
                    }
                case "greedybest":
                    {
                        Console.WriteLine("+======================================+");
                        Console.WriteLine("+= Initializing Greedy Best Search... =+");
                        Console.WriteLine("+======================================+");
                        Console.WriteLine("");
                        Console.WriteLine("From start position to goal 1...");
                        Console.WriteLine("");

                        var pathSearchTimer = new Stopwatch();
                        Search_Algorithms pathSearch = new Search_Algorithms(map);
                        pathSearchTimer.Start();
                        Path path = pathSearch.FindGreedyBest(map.StartPos, map.Goal[0]);
                        pathSearchTimer.Stop();

                        //Output number of nodes on both open/closed list to find a result.
                        Console.WriteLine("Number of Nodes Explored: " + pathSearch.NumNodes);

                        Console.WriteLine("Positions:");
                        path.PrintPositionOutput();

                        Console.WriteLine("Directions:");
                        path.PrintDirectionOutput();

                        pathSearchTimer.Start();
                        path = pathSearch.FindGreedyBest(map.Goal[0], map.Goal[1]);
                        pathSearchTimer.Stop();
                        Console.WriteLine("");
                        Console.WriteLine("From goal 1 to goal 2...");
                        Console.WriteLine("");

                        //Output number of nodes on both open/closed list to find a result.
                        Console.WriteLine("Number of Nodes Explored: " + pathSearch.NumNodes);

                        Console.WriteLine("Positions:");
                        path.PrintPositionOutput();

                        Console.WriteLine("Directions:");
                        path.PrintDirectionOutput();

                        Console.WriteLine("");
                        Console.WriteLine($"Greedy Best Time taken (Total): { pathSearchTimer.ElapsedMilliseconds } ms");
                        break;
                    }
                case "uniformcost":
                    {
                        Console.WriteLine("+=======================================+");
                        Console.WriteLine("+= Initializing Uniform Cost Search... =+");
                        Console.WriteLine("+=======================================+");
                        Console.WriteLine("");
                        Console.WriteLine("From start position to goal 1...");
                        Console.WriteLine("");

                        var pathSearchTimer = new Stopwatch();
                        Search_Algorithms pathSearch = new Search_Algorithms(map);
                        pathSearchTimer.Start();
                        Path path = pathSearch.FindUniformCost(map.StartPos, map.Goal[0]);
                        pathSearchTimer.Stop();

                        //Output number of nodes on both open/closed list to find a result.
                        Console.WriteLine("Number of Nodes Explored: " + pathSearch.NumNodes);

                        Console.WriteLine("Positions:");
                        path.PrintPositionOutput();

                        Console.WriteLine("Directions:");
                        path.PrintDirectionOutput();

                        pathSearchTimer.Start();
                        path = pathSearch.FindUniformCost(map.Goal[0], map.Goal[1]);
                        pathSearchTimer.Stop();
                        Console.WriteLine("");
                        Console.WriteLine("From goal 1 to goal 2...");
                        Console.WriteLine("");

                        //Output number of nodes on both open/closed list to find a result.
                        Console.WriteLine("Number of Nodes Explored: " + pathSearch.NumNodes);

                        Console.WriteLine("Positions:");
                        path.PrintPositionOutput();

                        Console.WriteLine("Directions:");
                        path.PrintDirectionOutput();

                        Console.WriteLine("");
                        Console.WriteLine($"Uniform Cost Search Time taken (Total): { pathSearchTimer.ElapsedMilliseconds } ms");
                        break;
                    }
                case "bidirectional":
                    {
                        Console.WriteLine("+=====================================+");
                        Console.WriteLine("+= Initializing Bidirectional BFS... =+");
                        Console.WriteLine("+=====================================+");
                        Console.WriteLine("");
                        Console.WriteLine("From start position to goal 1...");
                        Console.WriteLine("");

                        var pathSearchTimer = new Stopwatch();
                        Search_Algorithms pathSearch = new Search_Algorithms(map);
                        pathSearchTimer.Start();
                        Path path = pathSearch.FindBidirectionalBFS(map.StartPos, map.Goal[0]);
                        pathSearchTimer.Stop();

                        //Output number of nodes on both open/closed list to find a result.
                        Console.WriteLine("Number of Nodes Explored: " + pathSearch.NumNodes);

                        Console.WriteLine("Positions:");
                        path.PrintPositionOutput();

                        Console.WriteLine("Directions:");
                        path.PrintDirectionOutput();

                        pathSearchTimer.Start();
                        path = pathSearch.FindBidirectionalBFS(map.Goal[0], map.Goal[1]);
                        pathSearchTimer.Stop();
                        Console.WriteLine("");
                        Console.WriteLine("From goal 1 to goal 2...");
                        Console.WriteLine("");

                        //Output number of nodes on both open/closed list to find a result.
                        Console.WriteLine("Number of Nodes Explored: " + pathSearch.NumNodes);

                        Console.WriteLine("Positions:");
                        path.PrintPositionOutput();

                        Console.WriteLine("Directions:");
                        path.PrintDirectionOutput();

                        Console.WriteLine("");
                        Console.WriteLine($"Bidirectional BFS taken (Total): { pathSearchTimer.ElapsedMilliseconds } ms");
                        break;
                    }
                default:
                    {
                        Console.WriteLine("Invalid/Improper pathfinding type provided. Consult the README for additional information. The program will now exit.");
                        break;
                    }
            }
        }
    }
}
