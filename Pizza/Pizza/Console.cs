using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pizza
{
    class My_Console
    {
        public My_Console(int aantal)
        {
            Logger l = new Logger(aantal+4);
            Input p = new Input(10, true);
            InitialPath init = new InitialPath(p);
            Solution initial_solution = init.getSolution(1);

            l.AddLine("Simulated Annealing");
            l.AddLine("Temperature: " + 100);
            l.AddLine("Cooling rate: " + 0.95f);
            l.AddLine("Neighbor functie: "+1);


            Console.WriteLine("----------------");
            string initcost = "init solution: " + initial_solution.costs();
            Console.WriteLine("----------------");
            Console.WriteLine(initcost);

            l.AddLine(initcost);
            for(int i = 0; i<aantal;i++)
            {
                SimulatedAnnealing sa = new SimulatedAnnealing(initial_solution, 100, 0.95f, 0, 1);

                Solution result_solution = sa.run();
                // Present results  
                int nr = i + 1;
                string rescost = "result solution " + nr + " :" + result_solution.costs();
                Console.WriteLine(rescost);
                l.AddLine(rescost);

                //initial_solution.Draw();
                //result_solution.Draw();
            }
            Console.WriteLine("Done.");
            l.Write();
            Console.ReadLine();
        }

        // Add algorithms and other stuff you need to solve the TSP-problem
    }
}
