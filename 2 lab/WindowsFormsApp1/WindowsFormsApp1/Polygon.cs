using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace WindowsFormsApp1
{

    public class Polygon : Figure
    {
        public Polygon(int x0, int y0, Graphics gr, Pen pen, Color Fc) : base(x0, y0, gr, pen, Fc) { }

        private LinkedList<Point> points = new LinkedList<Point>();
        protected int n = 0;



        public override Point StartPoint
        {
            get => base.StartPoint;
            set
            {
                startPoint = value;
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
                DrawPanel.DrawLine(DrPen, points.ElementAt<Point>(n - 1), value);
            }
        }


        public override Point EndPoint
        {
            get => base.EndPoint;
            set
            {


                endPoint = value;
                points.AddLast(value);
                if (!this.EndOfCurrentFigure)
                {


                    if (n > 0)
                    {
                        DrawPanel.DrawLine(DrPen, points.ElementAt<Point>(n - 1), points.ElementAt<Point>(n));
                    }
                    n++;

                }
                else
                {
                    n = 0;

                    var brush = new SolidBrush(FillColor);
                    DrawPanel.DrawPolygon(DrPen, points.ToArray());
                    DrawPanel.FillPolygon(brush, points.ToArray());
                    points.Clear();
                    brush.Dispose();
                    this.EndOfCurrentFigure = false;
                }




            }
        }


    }


    public class PolygonCreator : IFiguresCreator
    {
        public Figure Create(int x0, int y0, Graphics gr, Pen pen, Color Fc)
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
