using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace RobotNavigationProblem
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Form1());

            Map map = new Map("../../../RobotNav-Test.txt");

            var timer1 = new System.Diagnostics.Stopwatch();
            timer1.Start();
            Search_Algorithms DFS = new Search_Algorithms(map);
            Path path = DFS.FindBFS(map.StartPos, map.Goal[0]);
            timer1.Stop();

            Console.WriteLine("\n\rBFS As Positions:");
            path.PrintPositionOutput();

            Console.WriteLine("\n\rBFS As Directions:");
            path.PrintDirectionOutput();

            var timer2 = new System.Diagnostics.Stopwatch();
            timer2.Start();
            Search_Algorithms AStar = new Search_Algorithms(map);
            Path path2 = DFS.FindAStar(map.StartPos, map.Goal[0]);
            timer2.Stop();

            Console.WriteLine("\n\rA* Positions:");
            path2.PrintPositionOutput();

            Console.WriteLine("\n\rA* As Directions:");
            path2.PrintDirectionOutput();

            var timer3 = new System.Diagnostics.Stopwatch();
            timer3.Start();
            Search_Algorithms BestFirst = new Search_Algorithms(map);
            Path path3 = BestFirst.FindGreedyBest(map.StartPos, map.Goal[0]);
            timer3.Stop();

            Console.WriteLine("\n\rGBFS Positions:");
            path3.PrintPositionOutput();

            Console.WriteLine("\n\rGBFS As Directions:");
            path3.PrintDirectionOutput();

            Console.WriteLine($"\n\rA* Time taken : {timer2.ElapsedMilliseconds} ms");
            Console.WriteLine($"\n\rBFS Time taken : {timer1.ElapsedMilliseconds} ms");
            Console.WriteLine($"\n\rGBFS Time taken : {timer3.ElapsedMilliseconds} ms");
        }
    }
}
