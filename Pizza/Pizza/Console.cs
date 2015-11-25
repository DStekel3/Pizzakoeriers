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
            Input p = new Input(10, false);
            InitialPath i = new InitialPath(p);
            int[][] initpath = new int[1][];
            initpath[0] = i.path;
            Console.WriteLine("Done.");
        }

        // Add algorithms and other stuff you need to solve the TSP-problem
    }
}
