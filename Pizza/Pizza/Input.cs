using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pizza
{
    public class Input
    {
        public Customer[] customers;
        // generates a (random) set of customers used as input
        public Input(int amount)
        {
            customers = new Customer[amount];
            for(int t =0;t< amount;t++)
            {
                Random r = new Random();
                int x = r.Next(-100, 100);
                int y = r.Next(-100, 100);
                customers[t] = new Customer(x, y, t);
            }
        }
    }
}
