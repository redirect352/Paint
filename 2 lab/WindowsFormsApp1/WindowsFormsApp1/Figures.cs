using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace WindowsFormsApp1
{
    public abstract class Figure
    {
        public Graphics DrawPanel;
        protected Point startPoint;
        protected Point endPoint = new Point(-1, -1);
        public Pen DrPen;
        public Color FillColor;
        public bool EndOfCurrentFigure = false;



        public Figure(int x0, int y0, Graphics gr, Pen pen, Color Fc)
        {
            startPoint = new Point(x0, y0);
            DrawPanel = gr;
            DrPen = pen;
            FillColor = Fc;
        }


        public virtual Point StartPoint
        {
            get
            {
                return startPoint;
            }
            set
            {
                startPoint = value;

            }
        }

        public virtual Point PreDrawEndPoint
        {
            get
            {
                return endPoint;
            }
            set
            {
                EndPoint= value;

            }

        }

        public virtual Point EndPoint
        {
            get
            {
                return endPoint;
            }
            set
            {
                endPoint = value;

            }

        }

        protected void FindLeftTopPoint(ref Point MainPicture, ref Point TemporaryImage)
        {
            int buf;
            if (TemporaryImage.X < MainPicture.X)
            {
                buf = TemporaryImage.X;
                TemporaryImage.X = MainPicture.X;
                MainPicture.X = buf;
            }
            if (TemporaryImage.Y < MainPicture.Y)
            {
                buf = TemporaryImage.Y;
                TemporaryImage.Y = MainPicture.Y;
                MainPicture.Y = buf;
            }
        }



    }

    public class StraigthLine : Figure
    {
        public StraigthLine(int x0, int y0, Graphics gr, Pen pen, Color Fc) : base(x0, y0, gr, pen, Fc) { }



        public override Point EndPoint
        {
            get => base.StartPoint;
            set
            {
                endPoint = value;
                DrawPanel.DrawLine(DrPen, startPoint, endPoint);

            }
        }



    }

    public class Rectangle : Figure
    {

        public Rectangle(int x0, int y0, Graphics gr, Pen pen, Color Fc) : base(x0, y0, gr, pen, Fc) { }


        public override Point EndPoint
        {
            get => base.StartPoint;
            set
            {
                endPoint = value;
                Point MainPicture = new Point(startPoint.X, startPoint.Y);

                FindLeftTopPoint(ref startPoint, ref endPoint);

                var brush = new SolidBrush(FillColor);
                DrawPanel.DrawRectangle(DrPen, startPoint.X, startPoint.Y, endPoint.X - startPoint.X, endPoint.Y - startPoint.Y);
                DrawPanel.FillRectangle(brush, startPoint.X, startPoint.Y, endPoint.X - startPoint.X, endPoint.Y - startPoint.Y);
                startPoint = MainPicture;
            }
        }

    }

    public class Ellipse : Figure
    {

        public Ellipse(int x0, int y0, Graphics gr, Pen pen, Color Fc) : base(x0, y0, gr, pen, Fc) { }



        public override Point EndPoint
        {
            get => base.StartPoint;
            set
            {
                endPoint = value;
                Point MainPicture = new Point(startPoint.X, startPoint.Y);
                FindLeftTopPoint(ref startPoint, ref endPoint);


                var brush = new SolidBrush(FillColor);
                DrawPanel.DrawEllipse(DrPen, startPoint.X, startPoint.Y, endPoint.X - startPoint.X, endPoint.Y - startPoint.Y);
                DrawPanel.FillEllipse(brush, startPoint.X, startPoint.Y, endPoint.X - startPoint.X, endPoint.Y - startPoint.Y);
                startPoint = MainPicture;
                brush.Dispose();
            }
        }

    }


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
                if ( (n == 0) && (value.X >0))
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

    public class RigthPolygon : Polygon
    {

        public RigthPolygon(int x0, int y0, Graphics gr, Pen pen, Color Fc) : base(x0, y0, gr, pen, Fc) { }
        protected int topAmount = 3;
        protected Point[] points = new Point[3];

      
        public int TopAmount
        {
            get
            {
                return topAmount;
            }
            set
            {
                points = new Point[value];
                topAmount = value;
            }

        }


        public override Point StartPoint
        {
            get
            {
                return startPoint;
            }
            set
            {
                startPoint = value;

            }
        }

        public override Point PreDrawEndPoint
        {
            get
            {
                return endPoint;
            }
            set
            {
                EndPoint = value;

            }

        }

        public override Point EndPoint
        {
            get => base.EndPoint;
            set
            {
                endPoint = value;

                int r = (int)(endPoint.X - startPoint.X) / 2, x1, y1;

                var center = new PointF(StartPoint.X + r, startPoint.Y + r);

                double angle = Math.PI * 2 / TopAmount, shiftAngle = Math.PI * (endPoint.X - startPoint.X) / (endPoint.Y - startPoint.Y) / 20;

                for (int i = 0; i < TopAmount; i++)
                {
                    x1 = (int)(center.X + Math.Cos(i * angle + shiftAngle) * r);
                    y1 = (int)(center.Y + Math.Sin(i * angle + shiftAngle) * r);
                    points[i].X = x1;
                    points[i].Y = y1;

                }

                var brush = new SolidBrush(FillColor);

                DrawPanel.DrawPolygon(DrPen, points);
                DrawPanel.FillPolygon(brush, points);

                brush.Dispose();

            }
        }
    }

    public class BrokenLine : Figure
    {
        public BrokenLine(int x0, int y0, Graphics gr, Pen pen, Color Fc) : base(x0, y0, gr, pen, Fc) { }

        private const int PointArraySize = 10;

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
                
                DrawPanel.DrawLine(DrPen, points.ElementAt<Point>(n-1), value);
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
                if ( n > 0)
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
}
