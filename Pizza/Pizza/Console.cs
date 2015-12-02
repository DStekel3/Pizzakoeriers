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
            Input p = new Input(200, true);
            InitialPath init = new InitialPath(p);
            Solution initial_solution = init.getSolution(1);
            int inicost = initial_solution.costs();
            while (true)
            {
                //SimulatedAnnealing sa = new SimulatedAnnealing(initial_solution, 1500, 0.95f, 0, 1);
                Untwiner utw = new Untwiner(initial_solution);
                Solution result_solution = utw.Untwineeee();
                // Present results
                Console.WriteLine("----------------");
                Console.WriteLine("init solution: " + inicost);
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
