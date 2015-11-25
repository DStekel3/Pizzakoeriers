using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pizza
{
    class My_Console
    {
        public My_Console()
        {
            Input p = new Input(50, true);
            InitialPath i = new InitialPath(p);
            int[][] initpath = new int[1][];
            initpath[0] = i.path;
            Solution s = new Solution(p.customers, initpath);
            Console.WriteLine("Heuristic: " + s.Heuristic(0).ToString());
            Console.WriteLine("Done.");
        }

        // Add algorithms and other stuff you need to solve the TSP-problem
    }
}
