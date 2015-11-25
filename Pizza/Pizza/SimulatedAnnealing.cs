using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pizza
{
    class SimulatedAnnealing
    {
        Solution s;

        public SimulatedAnnealing(Solution sol)
        {
            s = sol;

            foreach(Deliveryman d in s.rs)
            {
                VerbeterPad(d);
            }
        }

        private void VerbeterPad(Deliveryman d)
        {
            
               
        }
    }
}
