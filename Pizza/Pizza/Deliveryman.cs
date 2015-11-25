using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pizza
{
  public class Deliveryman
  {
    public int[] route;
    public int length;

    public Deliveryman(int path_length)
    {
      route = new int[path_length];
    }
  }
}
