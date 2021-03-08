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


        protected int topAmount = 3;
        protected Point[] points = new Point[3];
        protected int n = 0;

        public int TopAmount
        {
            get
            {
                return topAmount;
            }
            set
            {
                topAmount = value;
                points = new Point[topAmount];

            }

        }




        public override Point EndPoint
        {
            get => base.EndPoint;
            set
            {


                endPoint = value;

                if (n < (TopAmount - 1))
                {

                    points[n] = value;
                    if (n > 0)
                    {
                        DrawPanel.DrawLine(DrPen, points[n - 1], points[n]);
                    }
                    else
                    {
                        DrawPanel.DrawLine(DrPen, points[n], new Point(points[n].X + 1, points[n].Y + 1));
                    }

                    n++;

                }
                else
                {
                    points[n] = value;
                    n = 0;

                    var brush = new SolidBrush(FillColor);
                    DrawPanel.DrawPolygon(DrPen, points);
                    DrawPanel.FillPolygon(brush, points);

                    brush.Dispose();
                }




            }
        }


    }

    public class RigthPolygon : Polygon
    {

        public RigthPolygon(int x0, int y0, Graphics gr, Pen pen, Color Fc) : base(x0, y0, gr, pen, Fc) { }


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

        public override Point StartPoint
        {
            get => base.StartPoint;

            set
            {
                startPoint = value;
                if (value.X == -2)
                {
                    startPoint = new Point(endPoint.X, endPoint.Y);
                }

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

                endPoint = value;
                if (startPoint.X > 0)
                    DrawPanel.DrawLine(DrPen, startPoint, endPoint);

            }
        }

    }
}
