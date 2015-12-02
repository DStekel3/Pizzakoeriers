using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pizza
{
  public partial class Display : Form
  {
    Solution sol;
    Point depot;
    List<Brush> brushes = new List<Brush>();

    public Display(Solution solution)
    {
      InitializeComponent();
      sol = solution;
      this.Text = "Solution Graph (costs: " + sol.costs() + ")";
      this.Size = new Size(800, 800);

      // depot point
      depot = new Point(this.Width / 2, this.Height / 2);

      // different strokes for different folks
      defineBrushes();

      // paint event
      this.Paint += new PaintEventHandler(drawSolution);
    }

    public void defineBrushes()
    {
      brushes.Add(Brushes.Red);
      brushes.Add(Brushes.Blue);
      brushes.Add(Brushes.Green);
      brushes.Add(Brushes.Yellow);
      brushes.Add(Brushes.Violet);
      brushes.Add(Brushes.Brown);
      brushes.Add(Brushes.Pink);
      brushes.Add(Brushes.Azure);
      brushes.Add(Brushes.DarkBlue);
      brushes.Add(Brushes.White);
    }

    public void drawSolution(object sender, PaintEventArgs pea)
    {
      defineBrushes();
      Pen pen = new Pen(Brushes.Red);
      foreach (Deliveryman d in sol.rs)
      {
        Brush brush = null;
        if (brushes.Count != 0)
        {
           brush = brushes[0];
          brushes.RemoveAt(0);
        }
        else
          brush = Brushes.Red;
        Point p1 = depot;

        foreach (int p in d.route)
        {
          // obtain customer location
          Point p2 = new Point(sol.cs[p - 1].X / 3 + this.Width / 2, sol.cs[p - 1].Y / 3 + this.Height / 2);
          // mark location            
          pea.Graphics.FillEllipse(brush, new Rectangle(p2, new Size(new Point(10, 10))));
          // draw route to customer
          pea.Graphics.DrawLine(new Pen(brush), new Point(p1.X + 5, p1.Y + 5), new Point(p2.X + 5, p2.Y + 5));
          // update p1
          p1 = p2;
        }

        pea.Graphics.FillEllipse(Brushes.Black, new Rectangle(depot, new Size(new Point(10, 10))));
      }
    }
  }
}
