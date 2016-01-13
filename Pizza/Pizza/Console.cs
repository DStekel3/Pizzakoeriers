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
            int test_lines = 0;
            int i = 1;
            foreach (string line in lines)
            {
                if (line != "")
                {
                    // Use a tab to indent each line of the file.
                    Test t = new Test(line);

                    // create initial solutions
                    if (!initsolutions.ContainsKey(new Tuple<string, string>(t.custom.ToString(), t.delivery.ToString())))
                    {
                        // if initial tests do not exist, create them
                        int aantaltests = 30;
                        Solution[] inits = new Solution[aantaltests];
                        for (int a = 0; a < aantaltests; a++)
                        {
                            Input p = new Input(t.custom, true);
                            InitialPath init = new InitialPath(p);
                            inits[a] = init.getSolution(t.initial, t.delivery);                            
                        }
                        initsolutions.Add(new Tuple<string, string>(t.custom.ToString(), t.delivery.ToString()), inits);
                    }

                    RunTest(t, path, name, i, lines.Length);
                    i++;
                }
            }
        }

        private void RunTest(Test t, string path, string name, int nr, int total)
        {
            int aantal = 30;
            Logger l = new Logger(2 * aantal + 18);
            //Input p = new Input(t.custom, true);
            //InitialPath init = new InitialPath(p);
            //Solution initial_solution = init.getSolution(t.initial, t.delivery);
            Solution[] inits = initsolutions[new Tuple<string, string>(t.custom.ToString(), t.delivery.ToString())];
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
                for (int i = 0; i < aantal; i++)
                {
                    // init solution
                    Solution initial_solution = inits[i];

                    // log shit
                    string initcost = "init solution: " + initial_solution.costs();
                    l.AddLine(initcost);

                    // run test
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
                    // init solution
                    Solution initial_solution = inits[i];

                    // log shit
                    string initcost = "init solution: " + initial_solution.costs();
                    l.AddLine(initcost);

                    // run test
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
            }
            l.AddLine("");
            l.AddLine($"Gemiddelde costs: ");
            l.AddLine((gemiddelde / aantal).ToString());
            l.AddLine("Gemiddelde tijd: ");
            l.AddLine((tijd / aantal).ToString());

            // Locatie waar tests-map wordt aangemaakt en tests als txt.bestand genummerd worden opgeslagen
            l.Write(path, name, nr);
        }
    }
}
