﻿using System;
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
            int costs = 0;
            int longest = 0;
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
                    // a = b
                    id_a = id_b;
                    x_a = x_b;
                    y_a = y_b;
                }
                // back to depot
                dist += Math.Abs(x_a - 0) + Math.Abs(y_a - 0);

                // add route distance to total heuristic value
                costs += dist;

                // update longest
                if (longest < dist)
                    longest = dist;         
            }
            // calculate average distance per route
            costs /= n;

            return costs + longest;
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
                    rx[rnd_second].route.Add(rx[rnd_first].route[node_first]);
                    rx[rnd_first].route.RemoveAt(node_first);
                }

                //Return new solution
                return new Solution(cs, rx);
            }
            else
                return new Solution(cs, rs);
        }

        public void Untwine()
        {
            foreach (Deliveryman d in rs)
            {
                for (int i = 0; i < d.route.Count - 3; i++)
                {
                    Edge e1 = new Edge(cs[d.route[i]], cs[d.route[i + 1]]);
                    Edge e2 = new Edge(cs[d.route[i + 2]], cs[d.route[i + 3]]);

                    if(Intersect(e1, e2))
                    {
                        int temp = d.route[i + 1];
                        d.route[i + 1] = d.route[i + 2];
                        d.route[i + 2] = temp;
                    }
                }
            }
        }

        public bool Intersect(Edge e1, Edge e2)
        {
            int links = e1.A - e2.A;
            int rechts = e2.Y1 - e1.Y1;
            int x;
            if (links == 0)
                return false;
            else
            {
                x = rechts / links;
                if (InRange(x, e1) && InRange(x, e2))
                    return true;

                return false;
            }

        }
        public bool InRange(int x, Edge e)
        {
            if (e.X1 <= e.X2)
                if (e.X1 <= x && x <= e.X2)
                    return true;
            if (e.X2 <= x && x <= e.X1)
                return true;
            return false;
        }
        

        public void Draw()
        {
            Display display = new Display(this);
            Application.Run(display);
        }
    }

    public class Edge
    {
        public int ID1;
        public int ID2;

        public int X1 { get; }
        public int X2 { get; }
        public int Y1 { get; }
        public int Y2 { get; }
        public int A { get; set; }

        public Edge(Customer c1, Customer c2)
        {
            ID1 = c1.ID;
            ID2 = c2.ID;
            X1 = c1.X;
            X2 = c2.X;
            Y1 = c1.Y;
            Y2 = c2.Y;

            Helling();
        }

        private void Helling()
        {
            int dx = X2 - X1;
            int dy = Y2 - Y1;
            A = dy / dx;
        }

       
    }
    
}
