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
	int temp = 100;
	float cool_rate = 0.99f;
	int aantal = 5;
	int iterations = 1000;

	public My_Console()
	{

	  InputTests();
	  /*
	  Logger l = new Logger(2*aantal + 13);
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
	  l.AddLine("Iterative Improvement:");
	  for (int i = 0; i < aantal; i++)
	  {
		s.Start();
		//SimulatedAnnealing sa = new SimulatedAnnealing(initial_solution, temp, cool_rate,  neighbour_mode);
		IterativeImprovement sa = new IterativeImprovement(initial_solution, iterations);
		Solution result_solution = sa.LocalSearch();
		s.Stop();

		// Present results 
		string rescost = result_solution.costs().ToString();
		Console.WriteLine(rescost);
		Console.WriteLine($"{i+1}/{aantal}");
		l.AddLine($"{rescost}, tijd: {s.ElapsedMilliseconds}");
		Console.WriteLine(s.ElapsedMilliseconds);
		s.Reset();
	  }
	  l.AddLine("");
	  l.AddLine("Simulated Annealing: ");
	  for (int i = 0; i < aantal; i++)
	  {
		s.Start();
		SimulatedAnnealing sa = new SimulatedAnnealing(initial_solution, temp, cool_rate, neighbour_mode);
		Solution result_solution = sa.run();
		s.Stop();
		// Present results  

		string rescost = result_solution.costs().ToString();
		Console.WriteLine(rescost);
		l.AddLine($"{rescost}, tijd: {s.ElapsedMilliseconds}");
		Console.WriteLine($"{i+1}/{aantal}");
		Console.WriteLine(s.ElapsedMilliseconds);
		s.Reset();
	  }
	  Console.WriteLine("Done.");
	  l.Write();
	  */
	}

	private void InputTests()
	{
	  // The files used in this example are created in the topic
	  // How to: Write to a Text File. You can change the path and
	  // file name to substitute text files of your own.

	  // Example #2
	  // Read each line of the file into a string array. Each element
	  // of the array is one line of the file.
	  string[] lines = System.IO.File.ReadAllLines(@"C:\Users\Daniël\Desktop\tests.txt");
	  int test_lines = 0;
	  int i = 1;
	  foreach (string line in lines)
	  {
		if (line != "")
		{
		  // Use a tab to indent each line of the file.
		  Test t = new Test(line);
		  RunTest(t, i);
		  i++;
		}
	  }
	}

	private void RunTest(Test t, int nr)
	{
	  int aantal = 30;
	  Logger l = new Logger(2 * aantal + 16);
	  Input p = new Input(customers, true);
	  InitialPath init = new InitialPath(p);
	  Solution initial_solution = init.getSolution(t.initial, t.delivery);
	  string initcost = "init solution: " + initial_solution.costs();
	  l.AddLine(initcost);
	  l.AddLine("");
	  Stopwatch s = new Stopwatch();
	  var gemiddelde = 0.0;

	  if (t.method == 0)
	  {
		l.AddLine("Iterative Improvement");
		l.AddLine("Initial mode: " + t.initial);
		l.AddLine("Neighbor mode: " + t.neigh);
		l.AddLine("Deliverymen: " + t.delivery);
		l.AddLine("Customers: " + t.custom);
		l.AddLine("Iterations: " + t.iterations);
		l.AddLine("Aantal tests: " + aantal);
		for (int i = 0; i < aantal; i++)
		{
		  s.Start();
		  IterativeImprovement sa = new IterativeImprovement(initial_solution, t.iterations);
		  Solution result_solution = sa.LocalSearch();
		  s.Stop();
		  // Present results  
		  int costs = result_solution.costs();
		  string rescost = costs.ToString();
		  Console.WriteLine(rescost);
		  gemiddelde += costs;
		  Console.WriteLine($"{i + 1}/{aantal}");
		  l.AddLine($"{rescost}");//, tijd: {s.ElapsedMilliseconds}");
		  Console.WriteLine(s.ElapsedMilliseconds);
		  s.Reset();
		}
	  }
	  else if (t.method == 1)
	  {
		l.AddLine("Simulated Annealing");
		l.AddLine("Initial mode: " + t.initial);
		l.AddLine("Neighbor mode: " + t.neigh);
		l.AddLine("Deliverymen: " + t.delivery);
		l.AddLine("Customers: " + t.custom);
		l.AddLine("Temperature: " + t.temp);
		l.AddLine("Cooling rate: " + t.cool);
		l.AddLine("Aantal tests: " + aantal);

		for (int i = 0; i < aantal; i++)
		{
		  s.Start();
		  SimulatedAnnealing sa = new SimulatedAnnealing(initial_solution, t.temp, t.cool, t.neigh);
		  Solution result_solution = sa.run();
		  s.Stop();
		  // Present results  
		  int costs = result_solution.costs();
          string rescost = costs.ToString();
		  Console.WriteLine(rescost);
		  gemiddelde += costs;
		  l.AddLine($"{rescost}");//, tijd: {s.ElapsedMilliseconds}");
		  Console.WriteLine($"{i + 1}/{aantal}");
		  Console.WriteLine(s.ElapsedMilliseconds);
		  s.Reset();
		}
	  }
	  l.AddLine("");
	  l.AddLine($"Gemiddelde costs: ");
	  l.AddLine((gemiddelde / aantal).ToString());

	  // Locatie waar tests-map wordt aangemaakt en tests als txt.bestand genummerd worden opgeslagen
	  l.Write(@"C:\Users\Daniël\Desktop", nr);
	}
  }
}
