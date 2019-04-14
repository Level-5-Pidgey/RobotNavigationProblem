using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;

namespace RobotNavigationProblem
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            //Variable assignment
            Map map;
            bool guiDraw = false;

            //Launching program
            if (args.Length < 2)
            {
                throw new ArgumentException("At least 2 arguments expected, please consult the README for additional information");
            }

            //Valid arguments given to run the program in CLI
            string fileDir = "../../../" + args[0] + ".txt";
            string pathfindingMethod = args[1];

            //If "true" (or variants of) are given after valid args, the program will display a GUI format of the pathfinding process
            if (args.Length == 3)
            {
                
                if (args[2].ToLower() == "t" || args[2].ToLower() == "true" || args[2].ToLower() == "yes")
                {
                    guiDraw = true;
                }
            }

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
                        var pathSearchTimer = new System.Diagnostics.Stopwatch();
                        pathSearchTimer.Start();
                        Search_Algorithms pathSearch = new Search_Algorithms(map);
                        Path path = pathSearch.FindDFS(map.StartPos, map.Goal[1]);
                        pathSearchTimer.Stop();

                        Console.WriteLine("DFS As Positions:");
                        path.PrintPositionOutput();

                        Console.WriteLine("DFS As Directions:");
                        path.PrintDirectionOutput();

                        Console.WriteLine($"DFS Time taken : { pathSearchTimer.ElapsedMilliseconds } ms");
                        break;
                    }
                case "bfs":
                    {
                        var pathSearchTimer = new System.Diagnostics.Stopwatch();
                        pathSearchTimer.Start();
                        Search_Algorithms pathSearch = new Search_Algorithms(map);
                        Path path = pathSearch.FindBFS(map.StartPos, map.Goal[1]);
                        pathSearchTimer.Stop();

                        Console.WriteLine("BFS As Positions:");
                        path.PrintPositionOutput();

                        Console.WriteLine("BFS As Directions:");
                        path.PrintDirectionOutput();

                        Console.WriteLine($"BFS Time taken : { pathSearchTimer.ElapsedMilliseconds } ms");
                        break;
                    }
                case "astar":
                    {
                        var pathSearchTimer = new System.Diagnostics.Stopwatch();
                        pathSearchTimer.Start();
                        Search_Algorithms pathSearch = new Search_Algorithms(map);
                        Path path = pathSearch.FindAStar(map.StartPos, map.Goal[1]);
                        pathSearchTimer.Stop();

                        Console.WriteLine("A* As Positions:");
                        path.PrintPositionOutput();

                        Console.WriteLine("A* As Directions:");
                        path.PrintDirectionOutput();

                        Console.WriteLine($"A* Time taken : { pathSearchTimer.ElapsedMilliseconds } ms");
                        break;
                    }
                case "greedybest":
                    {
                        var pathSearchTimer = new System.Diagnostics.Stopwatch();
                        pathSearchTimer.Start();
                        Search_Algorithms pathSearch = new Search_Algorithms(map);
                        Path path = pathSearch.FindGreedyBest(map.StartPos, map.Goal[1]);
                        pathSearchTimer.Stop();

                        Console.WriteLine("Greedy Best First As Positions:");
                        path.PrintPositionOutput();

                        Console.WriteLine("Greedy Best First As Directions:");
                        path.PrintDirectionOutput();

                        Console.WriteLine($"Greedy Best First Time taken : { pathSearchTimer.ElapsedMilliseconds } ms");
                        break;
                    }
                default:
                    {
                        Console.WriteLine("Invalid/Improper pathfinding type provided. Consult the README for additional information. The program will now exit.");
                        break;
                    }
            }

            //if (guiDraw)
            //{
            //    application.enablevisualstyles();
            //    application.setcompatibletextrenderingdefault(false);
            //    application.run(new form1());
            //}
        }
    }
}
