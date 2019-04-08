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
            Path path = DFS.FindDFS(map.StartPos, map.Goal[1]);
            timer1.Stop();

            Console.WriteLine("\n\rDFS As Positions:");
            path.PrintPositionOutput();

            Console.WriteLine("\n\rDFS As Directions:");
            path.PrintDirectionOutput();

            var timer2 = new System.Diagnostics.Stopwatch();
            timer2.Start();
            Search_Algorithms AStar = new Search_Algorithms(map);
            Path path2 = DFS.FindAStar(map.StartPos, map.Goal[1]);
            timer2.Stop();

            Console.WriteLine("\n\rA* Positions:");
            path2.PrintPositionOutput();

            Console.WriteLine("\n\rA* As Directions:");
            path2.PrintDirectionOutput();

            Console.WriteLine($"\n\rA* Time taken : {timer2.ElapsedMilliseconds} ms");
            Console.WriteLine($"\n\rDFS Time taken : {timer1.ElapsedMilliseconds} ms");
        }
    }
}
