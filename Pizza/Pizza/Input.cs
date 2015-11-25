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
            customers = new Customer[amount ];
            customers = Generate(amount, clusters);
        }


        private Customer[] Generate(int amount, bool clustered)
        {
            Random r = new Random();
            int clusters;
            /*
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
            */
            int cnr = 1;
           
                for (int t = 0; t < amount ; t++)
                {
                    //int d = 1000 / clusters;
                    int x = r.Next(-1000, 1000);
                    int y = r.Next(-1000, 1000);
                    customers[cnr-1] = new Customer(x, y, cnr);
                    cnr++;
                    System.Console.WriteLine("Customer " + cnr + ": X pos " + x + " , y pos " + y + " cluster: 1");
                }
            

            return customers;
        }
    }
}
