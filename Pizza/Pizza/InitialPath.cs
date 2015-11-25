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
      visited = new bool[i.customers.Length];
      NearestNeighbour(i.customers, 1);
      return;
    }

    public Solution NearestNeighbour(Customer[] customers, int amount)
    {
      Deliveryman[] d = new Deliveryman[amount];
      for(int t =0;t<d.Length;t++)
      {
        d[t] = new Deliveryman();
      }

      Point cur_pos = new Point(0, 0);

      for (int x = 0; x < customers.Length/amount; x++)
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
        d[0].route.Add(nearest_neighbour);
        visited[nearest_neighbour] = true;
        cur_pos = new Point(customers[nearest_neighbour].X, customers[nearest_neighbour].Y);
      }
      return new Solution(customers, d);
    }

    public Solution RandomMultiple(Customer[] customers, int amount)
    {
      Deliveryman[] work = new Deliveryman[amount];

      int path_length = 0;
      if (customers.Length % amount == 0)
        path_length = customers.Length / amount;
      else
        path_length = customers.Length / amount + 1;

      for(int t =0;t<work.Length;t++)
      {
        work[t] = new Deliveryman();
      }

      int cur = 0;
      int pos = 0;
      for (int t = 0; t < customers.Length; t++)
      {
        work[cur].route.Add(customers[t].ID);
        cur++;
        if (cur >= amount)
        {
          cur = 0;
          pos++;
        }
      }

      return new Solution(customers, work);
    }
  }
}
