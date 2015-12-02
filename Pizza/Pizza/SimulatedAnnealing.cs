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
        float t, a;
        int h, g;
        Random rnd;
        public SimulatedAnnealing(Solution sol, float temperature, float cooling_rate, int heuristic_function, int neighbor_function)
        {
            s = sol;
            t = temperature;
            a = cooling_rate;
            h = heuristic_function;
            g = neighbor_function;
            rnd = new Random();
        }

        public Solution run()
        {
            int cool = 0;
            while (t > 0.1)
            {
                float s_h = s.Heuristic(h);
                int iteraties = 0;
                Console.WriteLine("Temperature: " + t.ToString() + " costs: " + s_h.ToString());

                // get neighbor
                bool accept = false;
                while (!accept && iteraties < 100)
                {
                    Solution next = s.NextNeighbor(g);
                    float next_h = next.Heuristic(h);
                    iteraties++;
                    if (next_h <= s_h)
                    {
                        s = next;
                        accept = true;
                    }
                    else
                    {
                        double p = Math.Exp(-(next_h - s_h) / t);
                        //Console.WriteLine("kans op acceptatie: " + p + "d: " + (next_h - s_h).ToString());
                        double r = rnd.NextDouble();
                        if (p > r)
                        {
                            // accept solution
                            s = next;
                            accept = true;
                        }
                    }                        
                }

                // cooling
                cool++;
                //cool += iteraties;
                if (cool > 16)
                {
                    cool = 0;
                    t = a * t;
                }
            }

            return s;
        }
    }
    /*
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
    */
}
