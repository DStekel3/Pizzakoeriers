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
            Input p = new Input(10, true);
            InitialPath init = new InitialPath(p);
      int begin_nodes = init.cs.Length;
            Solution initial_solution = init.getSolution(0);
            while (true)
            {                
                SimulatedAnnealing sa = new SimulatedAnnealing(initial_solution, 1500, 0.95f, 0, 1);
                Solution result_solution = sa.run();
        int end_nodes = result_solution.cs.Length;
        Console.WriteLine("Begin: " + begin_nodes + ", End: " + end_nodes);

                // Present results
                Console.WriteLine("----------------");
                Console.WriteLine("init solution: " + initial_solution.costs());
                Console.WriteLine("result solution: " + result_solution.costs());
                Console.WriteLine("----------------");
                Console.WriteLine("Done.");

                initial_solution.Draw();
                result_solution.Draw();
                Console.ReadLine();
            }
        }

        // Add algorithms and other stuff you need to solve the TSP-problem
    }
}
