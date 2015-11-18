using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pizza
{
    public class Input
    {
        List<Customer> customers;
        // generates a (random) set of customers used as input
        public Input(int amount, bool clusters)
        {
            customers = new List<Customer>();

            customers = Generate(amount, clusters);
        }


        private List<Customer> Generate(int amount, bool clustered)
        {
            Random r = new Random();
            int clusters;
            if (clustered)
                clusters = r.Next(2, 6);
            else
                clusters = 1;

            List<Customer> c = new List<Customer>();
            int cnr = 0;
            for (int i = 0; i < clusters; i++)
            {
                for (int t = 0; t < amount; t++)
                {
                    int x = r.Next(-100, 100);
                    int y = r.Next(-100, 100);
                    customers[cnr] = new Customer(x, y);
                    cnr++;
                }
            }

            return c;
        }
    }
}
