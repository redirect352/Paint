using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Newtonsoft.Json;
using WindowsFormsApp1.FugureInterface;

namespace WindowsFormsApp1
{

    [Serializable]
    public class BrokenLine : Figure
    {
        public BrokenLine(int x0, int y0, Graphics gr, Pen pen, Color Fc) : base(x0, y0, gr, pen, Fc) { }
        public LinkedList<Point> points = new LinkedList<Point>();
        private int n = 0;


        public override IFigure Clone()
        {
            BrokenLine NewF = new BrokenLine(startPoint.X, startPoint.Y, null, (Pen)DrPen.Clone(), FillColor);
            NewF.EndOfCurrentFigure = this.EndOfCurrentFigure;

            for (int i = 0; i < points.Count; i++)
            {
                NewF.points.AddLast(points.ElementAt<Point>(i));
               
            }
            NewF.n = this.n;

            return NewF;
        }

        public override Point StartPoint
        {
            get => base.StartPoint;

            set
            {
                if (startPoint.X == -2 &&  this.EndOfCurrentFigure)
                {
                    points = new LinkedList<Point>();
                    n = 0;
                    EndOfCurrentFigure = false;
                }

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
                if (n>=1 &&  (DrawPanel != null))
                    DrawPanel.DrawLine(DrPen, points.ElementAt<Point>(n - 1), value);
            }
        }

        public override bool OnePointBack()
        {
            if (n < 3)
                return false;
            else
            {
                points.RemoveLast();
                this.EndOfCurrentFigure = false;
                n--;
                return true;
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
                if (value.X < 0 || value.Y < 0)
                    return;
                points.AddLast(value);
                if (n > 0 &&  (DrawPanel != null))
                {
                    DrawPanel.DrawLine(DrPen, points.ElementAt<Point>(n - 1), points.ElementAt<Point>(n));

                }
                n++;



            }
        }

        public override void Redraw()
        {
            int N = points.Count;
            if (N > 1)
            {
                for (int i = 0; i < N - 1; i++)
                {
                    DrawPanel.DrawLine(DrPen, points.ElementAt<Point>(i), points.ElementAt<Point>(i + 1));
                }

            }

        }

    }




    public class BrokenLineCreator : IFiguresCreator
    {
        public IFigure Create(int x0, int y0, Graphics gr, Pen pen, Color Fc)
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
