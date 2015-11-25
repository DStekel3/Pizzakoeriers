using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Pizza
{
  public class InitialPath
  {
    public int[] path; // gives the order of customers in the initial path
    bool[] visited; // visited[x] = true when X is visited in path
    public int length; // total length of the initial path

    public InitialPath(Input i)
    {
      path = new int[i.customers.Length + 1]; // initial route. path[0] = first customer we go to, path[n] = depot after we visited each customer
      visited = new bool[i.customers.Length];
      NearestNeighbour(i.customers);
      return;
    }

    public void NearestNeighbour(Customer[] customers)
    {
      Point cur_pos = new Point(0, 0);
      path[path.Length - 1] = -1;
      for (int x = 0; x < path.Length - 1; x++)
      {
        int nearest_neighbour = -1;
        int best_dist = int.MaxValue;
        for (int t = 0; t < customers.Length; t++)
        {

          if (!visited[t])
          {
            int dist = Math.Abs(customers[t].X - cur_pos.X) + Math.Abs(customers[t].Y - cur_pos.Y);
            if (dist < best_dist)
            {
              nearest_neighbour = customers[t].ID;
              best_dist = dist;
            }
          }
        }
        path[x] = nearest_neighbour;
        length += best_dist;
        visited[nearest_neighbour] = true;
        cur_pos = new Point(customers[nearest_neighbour].X, customers[nearest_neighbour].Y);
      }
    }
  }
}
