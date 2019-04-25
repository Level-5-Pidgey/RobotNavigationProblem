using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace RobotNavigationProblem
{
    public static class UtilityMethods
    {
        /// <summary>
        /// Static method to allow easy .toInt() of a string, saving on code duplication.
        /// </summary>
        /// <param name="s">String to convert into an int</param>
        /// <returns>An integer, from string form</returns>
        public static int ReturnInt(this string s)
        {
            return int.Parse(s);
        }
    }
}
