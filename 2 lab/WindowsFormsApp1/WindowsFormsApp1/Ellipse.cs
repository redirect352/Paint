using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace WindowsFormsApp1
{
    [Serializable]
    public class Ellipse : Figure
    {

        public Ellipse(int x0, int y0, Graphics gr, Pen pen, Color Fc) : base(x0, y0, gr, pen, Fc) { }

        public override Figure Clone()
        {

            Ellipse NewF = new Ellipse(startPoint.X, startPoint.Y, DrawPanel, (Pen)DrPen.Clone(), FillColor);
            NewF.endPoint = new Point(this.endPoint.X, this.endPoint.Y);
            NewF.EndOfCurrentFigure = this.EndOfCurrentFigure;
            return NewF;

        }

        public override Point EndPoint
        {
            get => base.EndPoint;
            set
            {
                endPoint = value;
                this.Redraw();
                EndOfCurrentFigure = true;
            }
        }

        public override void Redraw()
        {
         
            var brush = new SolidBrush(FillColor);
            DrawPanel.DrawEllipse(DrPen, Math.Min(startPoint.X, endPoint.X) , Math.Min(startPoint.Y, endPoint.Y), Math.Abs(endPoint.X - startPoint.X), Math.Abs(endPoint.Y - startPoint.Y));

            DrawPanel.FillEllipse(brush, Math.Min(startPoint.X, endPoint.X) , Math.Min(startPoint.Y, endPoint.Y), Math.Abs(endPoint.X - startPoint.X), Math.Abs(endPoint.Y - startPoint.Y));
            brush.Dispose();

        }


    }

    public class EllipseCreator : IFiguresCreator
    {
        public Figure Create(int x0, int y0, Graphics gr, Pen pen, Color Fc)
        {
            return new Ellipse(x0, y0, gr, pen, Fc);
        }


        public string Name
        {
            get
            {
                return " Овал";
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
