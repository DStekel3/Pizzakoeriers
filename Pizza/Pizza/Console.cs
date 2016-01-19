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
	int nr_II, nr_SA;
	public Dictionary<Tuple<string, string>, Solution[]> initsolutions = new Dictionary<Tuple<string, string>, Solution[]>();

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
	  nr_II = int.Parse(lines[0].Split(' ')[0]);
	  nr_SA = int.Parse(lines[0].Split(' ')[1]);
	  lines[0] = "";

	  int i = 1;
	  gemiddeld_costs = new long[lines.Length - 1];
	  gemiddeld_tijd = new double[lines.Length - 1];
	  initial_costs = new long[lines.Length - 1];
	  N = new int[lines.Length - 1];
	  M = new int[lines.Length - 1];
	  cool = new double[nr_SA];
	  tem = new int[nr_SA];

	  foreach (string line in lines)
	  {
		if (line != "")
		{
		  // Use a tab to indent each line of the file.
		  Test t = new Test(line);

		  // create initial solutions
		  if (!initsolutions.ContainsKey(new Tuple<string, string>(t.custom.ToString(), t.delivery.ToString())))
		  {
			// blalj
			// if initial tests do not exist, create them
			int aantaltests = 30;
			Solution[] inits = new Solution[aantaltests];
			Random rnd = new Random();
			for (int a = 0; a < aantaltests; a++)
			{
			  Input p = new Input(t.custom, rnd);
			  InitialPath init = new InitialPath(p, rnd);
			  inits[a] = init.getSolution(t.initial, t.delivery);
			}
			initsolutions.Add(new Tuple<string, string>(t.custom.ToString(), t.delivery.ToString()), inits);
		  }

		  // run test
		  RunTest(t, path, name, i, lines.Length - 1);
		  i++;
		}
	  }
	  Logger l = new Logger(7 * gemiddeld_tijd.Length + 16);
	  l.AddLine("Initial-costs:");
	  var x = 0;
	  foreach (long d in initial_costs)
	  {
		if (x == nr_II)
		  l.AddLine("");
		l.AddLine(d.ToString());
		x++;
	  }
	  l.AddLine("");
	  x = 0;
	  l.AddLine("Gemiddelde costs:");
	  foreach (long d in gemiddeld_costs)
	  {
		if (x == nr_II)
		  l.AddLine("");
		l.AddLine(d.ToString());
		x++;
	  }
	  l.AddLine("");
	  x = 0;
	  l.AddLine("Gemiddelde tijd: ");
	  foreach (double d in gemiddeld_tijd)
	  {
		if (x == nr_II)
		  l.AddLine("");
		l.AddLine(d.ToString());
		x++;
	  }

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
	  Logger l = new Logger(4 * aantal + 21);
	  Solution[] inits = initsolutions[new Tuple<string, string>(t.custom.ToString(), t.delivery.ToString())];

	  N[nr - 1] = t.custom;
	  M[nr - 1] = t.delivery;
	  if (t.method == 1)
	  {
		cool[nr - 1 - nr_II] = t.cool;
		tem[nr - 1 - nr_II] = t.temp;
	  }


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
		l.AddLine("Costs: ");
		long[] resultTime = new long[aantal];
		for (int i = 0; i < aantal; i++)
		{
		  //Input p = new Input(t.custom, true);
		  //InitialPath init = new InitialPath(p);
		  //Solution initial_solution = init.getSolution(t.initial, t.delivery);

		  // init solution
		  Solution initial_solution = inits[i];
		  long init_costs = initial_solution.costs();
		  init_gem += init_costs;

		  s.Start();
		  IterativeImprovement sa = new IterativeImprovement(initial_solution, t.iterations);
		  Solution result_solution = sa.LocalSearch();
		  s.Stop();
		  // Present results  
		  long costs = result_solution.costs();
		  string rescost = costs.ToString();
		  gemiddelde += costs;
		  Console.WriteLine($"Test {nr}/{total}: {i + 1}/{aantal}");
		  tijd += s.ElapsedMilliseconds;
		  l.AddLine($"{rescost}");
		  resultTime[i] = s.ElapsedMilliseconds;
		  s.Reset();
		}
		l.AddLine("");
		l.AddLine("Time:");
		for(int i =0;i< aantal;i++)
		  l.AddLine(resultTime[i].ToString());

		initial_costs[nr - 1] = init_gem / aantal;
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
		l.AddLine("Costs:");
		long[] resultTime = new long[aantal];
		for (int i = 0; i < aantal; i++)
		{
		  // init solution
		  Solution initial_solution = inits[i];
		  long init_costs = initial_solution.costs();
		  init_gem += init_costs;

		  s.Start();
		  SimulatedAnnealing sa = new SimulatedAnnealing(initial_solution, t.temp, t.cool, t.neigh);
		  Solution result_solution = sa.run();
		  s.Stop();
		  // Present results  
		  long costs = result_solution.costs();
		  string rescost = costs.ToString();
		  gemiddelde += costs;
		  tijd += s.ElapsedMilliseconds;
		  l.AddLine($"{rescost}");
		  
		  Console.WriteLine($"Test {nr}/{total}: {i + 1}/{aantal}");
		  s.Reset();
		}
		l.AddLine("");
		l.AddLine("Time:");
		for (int i = 0; i < aantal; i++)
		  l.AddLine(resultTime[i].ToString());
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
