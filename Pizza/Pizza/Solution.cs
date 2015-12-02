using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pizza
{
    public class Solution
    {
        int n;
        public Customer[] cs;
        public Deliveryman[] rs;
        Random rnd;
        
        public Solution(Customer[] customers, Deliveryman[] d)
        {
            // customers, or 'map' of the problem            
            cs = customers;
            // representation of the routes, by customer id's
            rs = d;            
            // number of deliverymen
            n = d.Length;
            // rnd
            rnd = new Random();
        }

        public int Heuristic(int h)
        {
            // Heuristic value
            int heuristic = 0;

            // --- Heuristics ---
            // Heuristic 1:
            if (h == 0)
            {
                heuristic = costs();
                /*
                for (int i = 0; i < n; i++)
                {
                    // number of customers on the route
                    int nodes = rs[i].Length;
                    // total distance of the route
                    int dist = 0;
                    int jump = nodes / 4;

                    for (int node = 0; node < nodes; node += jump)
                    {

                    }
                }
                */
            }
            
            return heuristic;            
        }

        public int costs()
        {
            // returns the actual costs of the solution
            int total_time = 0;
            for (int i = 0; i < n; i++)
            {
                // number of customers on the route
                int nodes = rs[i].route.Count;
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
                    id_b = rs[i].route[j];
                    // get node position
                    x_b = cs[id_b - 1].X;
                    y_b = cs[id_b - 1].Y;
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

        public Solution NextNeighbor(int g)
        {
            if (g == 0)
            {
                Deliveryman[] rx = new Deliveryman[rs.Length];
                for (int i = 0; i < rs.Length; i++)
                {
                    rx[i] = new Deliveryman();
                    foreach (int c in rs[i].route)
                        rx[i].route.Add(c);
                }

                // Rnd route and nodes
                int rnd_route = rnd.Next(0, n);
                int rnd_nodeA = rnd.Next(0, rx[rnd_route].route.Count);
                int rnd_nodeB = rnd.Next(0, rx[rnd_route].route.Count);

                // Swap nodes
                int a = rx[rnd_route].route[rnd_nodeA];
                rx[rnd_route].route[rnd_nodeA] = rx[rnd_route].route[rnd_nodeB];
                rx[rnd_route].route[rnd_nodeB] = a;

                // Return new Solution
                return new Solution(cs, rx);
            }
            else if (g == 1)
            {
                Random r2 = new Random();
                Deliveryman[] rx = new Deliveryman[rs.Length];
                for (int i = 0; i < rs.Length; i++)
                {
                    rx[i] = new Deliveryman();
                    foreach (int c in rs[i].route)
                        rx[i].route.Add(c);
                }

                int rnd_first = r2.Next(0, n); //Deliveryman to take customer from
                int l_first = rx[rnd_first].route.Count();
                int rnd_second = r2.Next(0, n); //Deliveryman to put customer in
                int node_first = r2.Next(0, l_first); //Customer to move

                //Add customer to new route and remove from initial one only if first route would not end up empty
                if (rx[rnd_first].route.Count() - 1 > 1)
                {
                    int r = rnd.Next(0, rx[rnd_second].route.Count);
                    rx[rnd_second].route.Insert(r, rx[rnd_first].route[node_first]);
                    //rx[rnd_second].route.Add(rx[rnd_first].route[node_first]);
                    rx[rnd_first].route.RemoveAt(node_first);
                }

                //Return new solution
                return new Solution(cs, rx);
            }
            else
                return new Solution(cs, rs);
        }

        public void Draw()
        {
            Display display = new Display(this);
            Application.Run(display);
        }
    }
}
