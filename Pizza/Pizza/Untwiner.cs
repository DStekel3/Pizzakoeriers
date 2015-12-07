using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pizza
{
    class Untwiner
    {
        Solution s;

        public Untwiner(Solution sol)
        {
            s = sol;
        }

        public Solution Untwineeee()
        {
            for(int i = 0; i < 50; i++)
            {
                Console.WriteLine(s.costs());
                s.Untwine();
            }
            Console.WriteLine(s.costs());
            return s;
        }
    }
}
