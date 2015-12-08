using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pizza
{
    class IterativeImprovement
    {
        int iterations = 10; //Amount of neighbouring solutions to look at
        long cost_opt, cost_cur;
        Solution s;
        
        public IterativeImprovement(Solution initial_solution, int iter)
        {
            s = initial_solution;
            iterations = iter;
            cost_opt = s.costs(); //Initial optimal cost
        }

        public Solution LocalSearch()
        {
            for(int x = 0; x < iterations; x++)
            {
                Solution s_next = s.NextNeighbor(0); //Neighboring state by swapping two customers in route
                cost_cur = s_next.costs(); //Calculate cost of neighboring state
                if (cost_cur < cost_opt) //If cost of neighboring state is less than current optimal...
                {
                    cost_opt = cost_cur; //...update optimal cost...
                    s = s_next; //...and update best route
                    x = 0;
                }
            }
            return s;
        }      
    }
}
