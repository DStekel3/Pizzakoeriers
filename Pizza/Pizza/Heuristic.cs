using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pizza
{
  class Heuristic
  {
    public int TakeAndRun(Solution s)
    {
      Console.WriteLine("TakeAndRun-Heuristic!");
      // returns the actual costs of the solution
      int total = 0;
      for (int i = 0; i < s.n; i++)
      {
        foreach (Deliveryman d in s.rs)
        {
          int cur_x = 0;
          int cur_y = 0;
          for (int t = 0; t < d.route.Count; t++)
          {
            if (t % 2 == 0)
            {
              int a = Math.Abs(cur_x - s.cs[d.route[t]-1].X);
              int b = Math.Abs(cur_y - s.cs[d.route[t]-1].Y);

              total += a + b;
            }
          }
        }
      }
      return total;
    }

    public int NoHeuristic(Solution s)
    {
      Console.WriteLine("No Heuristic!");
      // returns the actual costs of the solution
      int total_time = 0;
      for (int i = 0; i < s.n; i++)
      {
        // number of customers on the route
        int nodes = s.rs[i].route.Count;
        // total distance of the route
        int dist = 0;
        // customer id's and pos
        int id_a = 0;
        int x_a = 0;
        int y_a = 0;
        int id_b, x_b, y_b;

        for (int j = 0; j < nodes; j++)
        {
          // get node id
          id_b = s.rs[i].route[j];
          // get node position
          x_b = s.cs[id_b - 1].X;
          y_b = s.cs[id_b - 1].Y;
          // calculate distance between node a and b
          dist += Math.Abs(x_a - x_b) + Math.Abs(y_a - y_b);
          total_time += dist;
          // a = b
          id_a = id_b;
          x_a = x_b;
          y_a = y_b;
        }
        // back to depot
        //dist += Math.Abs(x_a - 0) + Math.Abs(y_a - 0);
      }
      // calculate average delivery duration per route
      return total_time;
    }
  }
}
