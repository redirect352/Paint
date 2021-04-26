using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using BasedInterfaces;


namespace WindowsFormsApp1
{

    [Serializable]
    public class Polygon : Figure
    {
        public Polygon(int x0, int y0, Graphics gr, Pen pen, Color Fc) : base(x0, y0, gr, pen, Fc) { }
        public LinkedList<Point> points = new LinkedList<Point>();
        protected int n = 0;

        public override IFigure Clone()
        {
            Polygon NewF = new Polygon(startPoint.X, startPoint.Y, null, (Pen)DrPen.Clone(), FillColor);
            NewF.EndOfCurrentFigure = this.EndOfCurrentFigure;
            
            for (int i=0; i< points.Count;i++)
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
                startPoint = value;
                if (startPoint.X == -2 && this.EndOfCurrentFigure)
                {
                    n = 0;
                    points.Clear();
                    this.EndOfCurrentFigure = false;
                }
                if ((n == 0) && (value.X > 0))
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
                if (DrawPanel != null)
                    DrawPanel.DrawLine(DrPen, points.ElementAt<Point>(n - 1), value);
            }
        }


        public override Point EndPoint
        {
            get => base.EndPoint;
            set
            {


                endPoint = value;
                if(value.X > 0)
                    points.AddLast(value);
                if (!this.EndOfCurrentFigure)
                {

                    
                    if (n > 0 && (DrawPanel != null) )
                    {
                        DrawPanel.DrawLine(DrPen, points.ElementAt<Point>(n - 1), points.ElementAt<Point>(n));
                    }
                    n++;

                }
                else if(DrawPanel != null)
                    this.Redraw();
                                                                             
            }
        }

        public override void Redraw()
        {
            if (EndOfCurrentFigure )
            {
                var brush = new SolidBrush(FillColor);
                DrawPanel.DrawPolygon(DrPen, points.ToArray());
                DrawPanel.FillPolygon(brush, points.ToArray());
                brush.Dispose();
            }
            else
            {
                int N = points.Count;
                if (N > 1)
                {
                    for (int i = 0; i < N - 1; i++)
                    {
                        DrawPanel.DrawLine(DrPen, points.ElementAt<Point>(i), points.ElementAt<Point>(i+1));
                    }

                }

            }

        }

        public override bool OnePointBack()
        {
            if (n < 3)
                return false;
            else
            {
                this.EndOfCurrentFigure = false;
                points.RemoveLast();
                n--;
                    return true;
            }
        }


    }


    public class PolygonCreator : IFiguresCreator
    {
        public IFigure Create(int x0, int y0, Graphics gr, Pen pen, Color Fc)
        {
            return new Polygon(x0, y0, gr, pen, Fc);
        }

       

        public string Name
        {
            get
            {
                return " Многоугольник";
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
