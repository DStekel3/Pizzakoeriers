using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pizza
{
  public class Test
  {
	public int method, initial, delivery, custom, neigh, iterations, temp;
	public float cool;

	public Test(string s)
	{
	  string[] tests = s.Split(' ');

	  initial = int.Parse(tests[1]);
	  delivery = int.Parse(tests[2]);
	  custom = int.Parse(tests[3]);
	  neigh = int.Parse(tests[4]);
	  method = -1;
	  iterations = 0;
	  cool = 0.99f;
	  temp = 100;
	  if (tests[0] == "Iterative")
	  {
		method = 0;
		iterations = int.Parse(tests[5]);
	  }
	  else if (tests[0] == "Simulated")
	  {
		method = 1;
		cool = float.Parse(tests[5]);
		temp = int.Parse(tests[6]);
	  }
	}
  }
}
