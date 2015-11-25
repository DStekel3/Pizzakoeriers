using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pizza
{
    class DeClassVanAndrew
    {
        int iterations = 10; //Amount of neighbouring solutions to look at
        int cost_opt, cost_cur;
        int[][] path;
        Customer[] customers;
        Solution s;

        public DeClassVanAndrew(Customer[] cust, int[][] input)
        {
            path = input;
            customers = cust;
        }
        /*
        public void CalculateInitial()
        {
            s = new Solution(customers, path);
            cost_opt = s.costs();
        }

        public void LocalSearch()
        {
            for(int x = 0; x < iterations; x++)
            {
                s.GenerateNeighbors(iterations);
                cost_cur = s.costs();
                if (cost_cur < cost_opt)
                {
                    cost_opt = cost_cur;
                    path = s.rs;
                }
            }
        }
        */
    }
}
