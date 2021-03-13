using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace WindowsFormsApp1
{

    public class BrokenLine : Figure
    {
        public BrokenLine(int x0, int y0, Graphics gr, Pen pen, Color Fc) : base(x0, y0, gr, pen, Fc) { }





        private LinkedList<Point> points = new LinkedList<Point>();
        private int n = 0;


        public override Point StartPoint
        {
            get => base.StartPoint;

            set
            {


                startPoint = value;
                if ((n == 0) && (value.X >= 0))
                {
                    points.AddLast(value);
                    n++;
                }
            }

        }

        public override Point PreDrawEndPoint
        {
            get => base.PreDrawEndPoint;
            set
            {

                DrawPanel.DrawLine(DrPen, points.ElementAt<Point>(n - 1), value);
            }
        }


        public override Point EndPoint
        {
            get
            {
                return endPoint;
            }
            set
            {
                points.AddLast(value);
                if (n > 0)
                {
                    DrawPanel.DrawLine(DrPen, points.ElementAt<Point>(n - 1), points.ElementAt<Point>(n));

                }
                n++;

                if (this.EndOfCurrentFigure)
                {
                    points = new LinkedList<Point>();
                    n = 0;
                    EndOfCurrentFigure = false;
                }

            }
        }

    }


    public class BrokenLineCreator : IFiguresCreator
    {
        public Figure Create(int x0, int y0, Graphics gr, Pen pen, Color Fc)
        {
            return new BrokenLine(x0, y0, gr, pen, Fc);
        }
        public string Name
        {
            get
            {
                return "Ломанная";
            }
        }
        public bool TopsNeeded
        {
            get
            {
                return false;
            }
        }
    }

}
