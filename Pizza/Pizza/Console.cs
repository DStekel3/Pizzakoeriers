using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Pizza
{
  class My_Console
  {
	// set variables:
	int initial_mode = 0;
	int deliverymen = 10;
	int customers = 500;
	int neighbour_mode = 2;
	int temp = 500;
	float cool_rate = 0.99f;
	int aantal = 5;
	int iterations = 1000;

	public My_Console()
	{

	  //InputTests();
	  Logger l = new Logger(2*aantal + 10);
	  Input p = new Input(customers, true);
	  InitialPath init = new InitialPath(p);
	  Solution initial_solution = init.getSolution(initial_mode, deliverymen);

	  l.AddLine("Simulated Annealing");
	  l.AddLine("Temperature: " + temp);
	  l.AddLine("Cooling rate: " + cool_rate);
	  l.AddLine("Neighbor mode: " + neighbour_mode);
	  l.AddLine("Deliverymen: " + deliverymen);
	  l.AddLine("Customers: " + customers);
	  l.AddLine("Initial mode: " + initial_mode);
	  l.AddLine("Aantal tests: " + aantal);

	  Console.WriteLine("----------------");
	  string initcost = "init solution: " + initial_solution.costs();
	  Console.WriteLine("----------------");
	  Console.WriteLine(initcost);

	  l.AddLine(initcost);
	  l.AddLine("");
	  Stopwatch s = new Stopwatch();
	  for (int i = 0; i < aantal; i++)
	  {
		s.Start();
		//SimulatedAnnealing sa = new SimulatedAnnealing(initial_solution, temp, cool_rate,  neighbour_mode);
		IterativeImprovement sa = new IterativeImprovement(initial_solution, iterations);
		Solution result_solution = sa.LocalSearch();
		// Present results  
		int nr = i + 1;
		string rescost = result_solution.costs().ToString();
		Console.WriteLine(rescost);
		l.AddLine(rescost);
		Console.WriteLine($"{i}/{aantal}");
		s.Stop();
		Console.WriteLine(s.ElapsedMilliseconds);
		s.Reset();
	  }
	  for (int i = 0; i < aantal; i++)
	  {
		s.Start();
		SimulatedAnnealing sa = new SimulatedAnnealing(initial_solution, temp, cool_rate, neighbour_mode);
		Solution result_solution = sa.run();
		// Present results  
		int nr = i + 1;
		string rescost = result_solution.costs().ToString();
		Console.WriteLine(rescost);
		l.AddLine(rescost);
		Console.WriteLine($"{i}/{aantal}");
		s.Stop();
		Console.WriteLine(s.ElapsedMilliseconds);
		s.Reset();
	  }
	  Console.WriteLine("Done.");
	  l.Write();
	}

	private void InputTests()
	{
	  Console.WriteLine("Hoeveel tests?");
	  int x = int.Parse(Console.ReadLine());

	  for (int t = 0; t < x; t++)
	  {
		Console.WriteLine("Customers: ");
		int c = int.Parse(Console.ReadLine());
	  }
	}



	// Add algorithms and other stuff you need to solve the TSP-problem
  }
}
