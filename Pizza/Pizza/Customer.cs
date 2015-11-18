using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Pizza
{
    public class Customer
    {
        public int X { get; }
        public int Y { get; }
        public int ID { get; }

        public Customer(int x, int y, int id)
        {
            X = x; Y = y; ID = id;
        }
    }
}
