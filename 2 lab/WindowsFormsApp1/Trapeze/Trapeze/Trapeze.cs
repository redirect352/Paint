using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Newtonsoft.Json;
using BasedInterfaces;

namespace Trapeze
{
    class Trapeze : IFigure
    {
        protected Pen drPen = null;
        protected float penWidth = -1;
        protected Point startPoint;
        protected Point endPoint = new Point(-1, -1);
        protected Color penColor;

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
        public Trapeze(int x0, int y0, Graphics gr, Pen pen, Color Fc)
        {
            startPoint = new Point(x0, y0);
            DrawPanel = gr;
            DrPen = pen;
            FillColor = Fc;

        }

        public Color PenColor
        {
            get
            {
                return penColor;
            }

            set
            {
                penColor = value;
                if (drPen != null)
                    drPen.Color = value;
            }

        }

        [JsonIgnore]
        public Pen DrPen
        {
            get
            {
                if ((drPen == null) && (PenWidth >= 0))
                {
                    drPen = new Pen(PenColor, PenWidth);
                    drPen.StartCap = drPen.EndCap = System.Drawing.Drawing2D.LineCap.Round;
                }
                return drPen;
            }
            set
            {
                if (value != null)
                {
                    penWidth = value.Width;
                    penColor = value.Color;
                }
                drPen = value;
            }

        }
        [JsonIgnore]
        public Graphics DrawPanel { get; set; }
        public Color FillColor { get; set; }
        public bool EndOfCurrentFigure { get; set; }
        public float PenWidth
        {
            get
            {
                return penWidth;
            }
            set
            {
                if (value < 0)
                    return;
                penWidth = value;
                if (drPen != null)
                    drPen.Width = value;
            }
        }

        public IFigure Clone()
        {
            Trapeze NewF = new Trapeze(startPoint.X, startPoint.Y, DrawPanel, (Pen)DrPen.Clone(), FillColor);
            NewF.endPoint = new Point(this.endPoint.X, this.endPoint.Y);
            NewF.EndOfCurrentFigure = this.EndOfCurrentFigure;
            return NewF;
        }

        public Point PreDrawEndPoint
        {
            get
            {
                return endPoint;
            }
            set
            {
                endPoint = value;
                if (DrawPanel != null)
                    this.Redraw();
            }

        }

        public Point EndPoint
        {
            get
            {
                return endPoint;
            }
            set
            {
                endPoint = value;
                if (DrawPanel != null)
                    this.Redraw();
                EndOfCurrentFigure = true;
            }
        }

        public void Redraw()
        {

            Point[] points = new Point[4];
            int r = (int)(endPoint.X - startPoint.X) / 2;

            var center = new PointF(StartPoint.X + r, startPoint.Y + r);
            points[0] = startPoint;
            points[1] = new Point(startPoint.X + r, startPoint.Y);
            points[2] = endPoint;
            points[3] = new Point(endPoint.X - r - (int)(2 * r), endPoint.Y);


            var brush = new SolidBrush(FillColor);

            DrawPanel.DrawPolygon(DrPen, points);
            DrawPanel.FillPolygon(brush, points);

            brush.Dispose();

        }

    }


    public class TrapezeCreator : IFiguresCreator
    {
        public IFigure Create(int x0, int y0, Graphics gr, Pen pen, Color Fc)
        {
            return new Trapeze(x0, y0, gr, pen, Fc);
        }
        private IFiguresCreator i;
        
        public string Name
        {
            get
            {
                return " Трапеция";
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
