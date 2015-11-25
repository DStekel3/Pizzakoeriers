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
        public Input(int amount, bool clusters)
        {
            customers = new Customer[amount+1];
            customers = Generate(amount, clusters);
        }


        private Customer[] Generate(int amount, bool clustered)
        {
            Random r = new Random();
            int clusters;
            if (clustered)
            {
                clusters = r.Next(2, 6);
                System.Console.WriteLine("Clusters: " + clusters);
            }
            else
            {
                clusters = 1;
                System.Console.WriteLine("Clusters: 1 grote cluster");
            }

            int cnr = 1;
            for (int i = 0; i < clusters; i++)
            {
                for (int t = 0; t < amount / clusters; t++)
                {
                    int d = 1000 / clusters;
                    int x = r.Next(d * i, d * i + 300);
                    int y = r.Next(d * i, d * i + 300);
                    customers[cnr] = new Customer(x, y, cnr);
                    cnr++;
                    System.Console.WriteLine("Customer " + cnr + ": X pos " + x + " , y pos " + y + " cluster: " + i);
                }
            }

            return customers;
        }
    }
}
