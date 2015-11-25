﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pizza
{
    class Solution
    {
        public int n;
        public Customer[] cs;
        public int[][] rs;
        Random rnd;

        public Solution(Customer[] customers, int[][] routes)
        {
            // customers, or 'map' of the problem
            cs = customers;
            // representation of the routes, by customer id's
            rs = routes;
            // number of deliverymen
            n = rs.GetLength(0);
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
            for (int i = 0; i < n; i++)
            {
                // number of customers on the route
                int nodes = rs[i].Length;
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
                    id_b = rs[i][j];
                    // get node position
                    x_b = cs[id_b].X;
                    y_b = cs[id_b].Y;
                    // calculate distance between node a and b
                    dist += Math.Abs(x_a - x_b) + Math.Abs(y_a - y_b);
                    // a = b
                    id_a = id_b;
                    x_a = x_b;
                    y_a = y_b;
                }

                // add route distance to total heuristic value
                costs += dist;
            }
            return costs;
        }            

        public List<Solution> GenerateNeighbors(int g, int num)
        {
            List<Solution> gs = new List<Solution>();
            
            // Neighbor Gen I
            if (g == 0)
            {
                // generate n neighbors
                for (int i = 0; i < num; i++)
                {
                    int[][] rx = rs;

                    // Rnd route and nodes
                    int rnd_route = rnd.Next(0, n);
                    int rnd_nodeA = rnd.Next(0, rx[rnd_route].Length);
                    int rnd_nodeB = rnd.Next(0, rx[rnd_route].Length);

                    // Swap nodes
                    int a = rs[rnd_route][rnd_nodeA];
                    rx[rnd_route][rnd_nodeA] = rx[rnd_route][rnd_nodeB];
                    rx[rnd_route][rnd_nodeB] = a;

                    gs.Add(new Solution(cs, rx));
                }
            }
            return gs;
        }

        public Solution NextNeighbor(int g)
        {            
            if (g == 0)
            {
                // Rnd route and nodes
                int rnd_route = rnd.Next(0, n);
                int rnd_nodeA = rnd.Next(0, rs[rnd_route].Length);
                int rnd_nodeB = rnd.Next(0, rs[rnd_route].Length);

                // Swap nodes
                int a = rs[rnd_route][rnd_nodeA];
                rs[rnd_route][rnd_nodeA] = rs[rnd_route][rnd_nodeB];
                rs[rnd_route][rnd_nodeB] = a;

                // Return new Solution
                return new Solution(cs, rs);
            }
            else
                return new Solution(cs, rs);
        }
    }
}
