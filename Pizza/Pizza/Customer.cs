using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Pizza
{
    class Customer
    {
        public int X { get; }
        public int Y { get; }

        public Customer(int x, int y)
        {
            X = x; Y = y;
        }
    }
}
