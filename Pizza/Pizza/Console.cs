using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows.Forms;
using System.IO;

namespace Pizza
{
  class My_Console
  {
	long[] initial_costs;
	long[] gemiddeld_costs;
	double[] gemiddeld_tijd;
	int[] N;
	int[] M;
	double[] cool;
	int[] tem;

	public My_Console()
	{
	  InputTests(); // Testing...
	}

	private void InputTests()
	{
	  // The files used in this example are created in the topic
	  // How to: Write to a Text File. You can change the path and
	  // file name to substitute text files of your own.

	  // Example #2
	  // Read each line of the file into a string array. Each element
	  // of the array is one line of the file.
	  OpenFileDialog sfd = new OpenFileDialog();
	  string s = Path.GetDirectoryName(Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory())));
	  sfd.InitialDirectory = s;
	  sfd.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";

	  DialogResult dr = sfd.ShowDialog();
	  string path = sfd.FileName;
	  string name = Path.GetFileNameWithoutExtension(path);
	  string[] lines = System.IO.File.ReadAllLines(path);
	  
	  int i = 1;
	  gemiddeld_costs = new long[lines.Length];
	  gemiddeld_tijd = new double[lines.Length];
	  initial_costs = new long[lines.Length];
	  N = new int[lines.Length];
	  M = new int[lines.Length];
	  cool = new double[lines.Length];
	  tem = new int[lines.Length];

	  foreach (string line in lines)
	  {
		if (line != "")
		{
		  // Use a tab to indent each line of the file.
		  Test t = new Test(line);
		  RunTest(t, path, name, i, lines.Length);
		  i++;
		}
	  }
	  Logger l = new Logger(7*gemiddeld_tijd.Length+13);
	  l.AddLine("Initial-costs:");
	  foreach(long d in initial_costs)
      {
		l.AddLine(d.ToString());
	  }
	  l.AddLine("");
	  l.AddLine("Gemiddelde costs:");
	  foreach(long d in gemiddeld_costs)
	  {
		l.AddLine(d.ToString());
	  }
	  l.AddLine("");
	  l.AddLine("Gemiddelde tijd: ");
	  foreach (double d in gemiddeld_tijd)
		l.AddLine(d.ToString());

	  l.AddLine("");
	  l.AddLine("N: ");
	  foreach (int n in N)
		l.AddLine(n.ToString());

	  l.AddLine("");
	  l.AddLine("M: ");
        foreach (int m in M)
		l.AddLine(m.ToString());

	  l.AddLine("");
	  l.AddLine("Cool-rate: ");
	  foreach (double m in cool)
		l.AddLine(m.ToString());

	  l.AddLine("");
	  l.AddLine("Temperature: ");
	  foreach (int m in tem)
		l.AddLine(m.ToString());

	  l.Write(path, name);
	}

	private void RunTest(Test t, string path, string name, int nr, int total)
	{
	  int aantal = 30;
	  Logger l = new Logger(2 * aantal + 18);
	  
	  N[nr - 1] = t.custom;
	  M[nr - 1] = t.neigh;
	  if (t.method == 0)
	  { cool[nr - 1] = -1; tem[nr - 1] = -1; }
	  else
	  { cool[nr - 1] = t.cool; tem[nr - 1] = t.temp; }

	  
	  l.AddLine("");
	  Stopwatch s = new Stopwatch();
	  var gemiddelde = 0.0;
	  var tijd = 0.0;

	  if (t.method == 0)
	  {
		l.AddLine("Iterative Improvement");
		l.AddLine("Initial mode: " + t.initial);
		l.AddLine("Neighbor mode: " + t.neigh);
		l.AddLine("Deliverymen: " + t.delivery);
		l.AddLine("Customers: " + t.custom);
		l.AddLine("Iterations: " + t.iterations);
		l.AddLine("Aantal tests: " + aantal);

		long init_gem = 0;
		for (int i = 0; i < aantal; i++)
		{
		  Input p = new Input(t.custom, true);
		  InitialPath init = new InitialPath(p);
		  Solution initial_solution = init.getSolution(t.initial, t.delivery);
		  long init_costs = initial_solution.costs();
		  init_gem += init_costs;
		  

		  s.Start();
		  IterativeImprovement sa = new IterativeImprovement(initial_solution, t.iterations);
		  Solution result_solution = sa.LocalSearch();
		  s.Stop();
		  // Present results  
		  long costs = result_solution.costs();
		  string rescost = costs.ToString();
		  //Console.WriteLine(rescost);
		  gemiddelde += costs;
		  Console.WriteLine($"Test {nr}/{total}: {i + 1}/{aantal}");
		  tijd += s.ElapsedMilliseconds;
		  l.AddLine($"{rescost}");//, tijd: {s.ElapsedMilliseconds}");
								  //Console.WriteLine(s.ElapsedMilliseconds);
		  s.Reset();
		}

		initial_costs[nr - 1] = init_gem /aantal; 
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

		long init_gem = 0;
		for (int i = 0; i < aantal; i++)
		{
		  Input p = new Input(t.custom, true);
		  InitialPath init = new InitialPath(p);
		  Solution initial_solution = init.getSolution(t.initial, t.delivery);
		  long init_costs = initial_solution.costs();
		  init_gem += init_costs;

		  s.Start();
		  SimulatedAnnealing sa = new SimulatedAnnealing(initial_solution, t.temp, t.cool, t.neigh);
		  Solution result_solution = sa.run();
		  s.Stop();
		  // Present results  
		  long costs = result_solution.costs();
		  string rescost = costs.ToString();
		  //Console.WriteLine(rescost);
		  gemiddelde += costs;
		  tijd += s.ElapsedMilliseconds;
		  l.AddLine($"{rescost}");//, tijd: {s.ElapsedMilliseconds}");
		  Console.WriteLine($"Test {nr}/{total}: {i + 1}/{aantal}");
		  //Console.WriteLine(s.ElapsedMilliseconds);
		  s.Reset();
		}
		initial_costs[nr - 1] = init_gem / aantal;
	  }
	  l.AddLine("");
	  l.AddLine($"Gemiddelde costs: ");
	  l.AddLine((gemiddelde / aantal).ToString());
	  l.AddLine("Gemiddelde tijd: ");
	  l.AddLine((tijd / aantal).ToString());
	  gemiddeld_costs[nr - 1] = (long)gemiddelde / aantal;
	  gemiddeld_tijd[nr - 1] = tijd / aantal;
	  l.AddLine("Gemiddeld Initial costs: " + initial_costs[nr - 1]);

	  // Locatie waar tests-map wordt aangemaakt en tests als txt.bestand genummerd worden opgeslagen
	  l.Write(path, name, nr);
	}
  }
}
