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
            InitialPath init = new InitialPath(p);
            Solution initial_solution = init.getSolution(1);
      while (Console.ReadLine() == "start")
      {
        int initial_costs = initial_solution.costs();
        SimulatedAnnealing sa = new SimulatedAnnealing(initial_solution, 1500, 0.95f, 0, 1);
        Solution result_solution = sa.run();

        // Present results
        Console.WriteLine("----------------");
        Console.WriteLine("init solution: " + initial_solution.costs());
        Console.WriteLine("result solution: " + result_solution.costs());
        Console.WriteLine("----------------");
        Console.WriteLine("Done.");

        initial_solution.Draw();
        result_solution.Draw();
      }
        }

        // Add algorithms and other stuff you need to solve the TSP-problem
    }
}
