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
    public Customer[] cs;

    public InitialPath(Input i)
    {
        cs = i.customers;
        visited = new bool[cs.Length];
    }

    public Solution getSolution(int mode)
    {
            if (mode == 0)
                return EqualNeighbour(cs, 2);
            else
                return RandomMultiple(cs, 4);
        }

    public Solution NearestNeighbour(Customer[] customers, int deliverers)
    {
      Deliveryman[] d = new Deliveryman[deliverers];
      for(int t =0;t<d.Length;t++)
      {
        d[t] = new Deliveryman();
      }

      Point cur_pos = new Point(0, 0);

      for (int x = 0; x < customers.Length/ deliverers; x++)
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
        visited[nearest_neighbour - 1] = true;
        cur_pos = new Point(customers[nearest_neighbour - 1].X, customers[nearest_neighbour - 1].Y);
      }
      return new Solution(customers, d);
    }

    public Solution EqualNeighbour(Customer[] customers, int deliverers)
    {
      Deliveryman[] ds = new Deliveryman[deliverers];
      for (int t = 0; t < ds.Length; t++)
      {
        ds[t] = new Deliveryman();
      }

      for (int x = 0; x < customers.Length; x++)
      {
        int laziest = int.MaxValue;
        Deliveryman l = null;
        foreach (Deliveryman d in ds)
        {
          int score = 0;
          Point cur_pos = new Point(0, 0);
          foreach (int z in d.route)
          {
            int dist = Math.Abs(cs[z - 1].X - cur_pos.X) + Math.Abs(cs[z - 1].Y - cur_pos.Y);
            score += dist;
            cur_pos = new Point(cs[z - 1].X, cs[z - 1].Y);
          }

          score += Math.Abs(cur_pos.X - 0) + Math.Abs(cur_pos.Y - 0);
          if (score < laziest)
          { laziest = score; l = d; }
        }

        for(int t = 0; t < ds.Length; t++)
        {
          if (ds[t] == l)
            ds[t].route.Add(customers[x].ID);
        }
      }

      return new Solution(customers, ds);
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
