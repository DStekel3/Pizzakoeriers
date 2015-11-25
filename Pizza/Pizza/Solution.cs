using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pizza
{
    class Solution
    {
        int n;
        Customer[] cs;
        int[][] rs;
        
        public Solution(Customer[] customers, int[][] routes)
        {
            // customers, or 'map' of the problem
            cs = customers;
            // representation of the routes, by customer id's
            rs = routes;
            // number of deliverymen
            n = rs.Length;
            // use heuristic function...
        }

        public int Heuristic(int h)
        {
            // Heuristic value
            int heuristic = 0;

            // Heuristics
            // ...     
            
            return heuristic;            
        }

        public int costs()
        {
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
            

        public Solution[] GenerateNeighbors(int g)
        {
            // Neighbor Gen I
            if (g == 0)
                return new Solution[1];
            else return new Solution[1];
        }
    }
}
