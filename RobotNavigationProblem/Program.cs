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

            Search_Algorithms DFS = new Search_Algorithms(map);
            Path path = DFS.FindAStar(map.StartPos, map.Goal[0]);

            Console.WriteLine("\n\rAs Positions:");
            path.PrintPositionOutput();

            Console.WriteLine("\n\rAs Directions:");
            path.PrintDirectionOutput();
        }
    }
}
