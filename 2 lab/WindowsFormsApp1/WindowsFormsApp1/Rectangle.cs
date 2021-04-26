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
    public class Rectangle : Figure
    {

        public Rectangle(int x0, int y0, Graphics gr, Pen pen, Color Fc) : base(x0, y0, gr, pen, Fc) { }

        public override IFigure Clone()
        {

            Rectangle NewF = new Rectangle(startPoint.X, startPoint.Y, DrawPanel, (Pen)DrPen.Clone(), FillColor);
            NewF.endPoint = new Point(this.endPoint.X, this.endPoint.Y);
            NewF.EndOfCurrentFigure = this.EndOfCurrentFigure;
            return NewF;

        }

        public override Point EndPoint
        {
            get => base.StartPoint;
            set
            {
                endPoint = value;
                if (DrawPanel != null)
                    this.Redraw();
                EndOfCurrentFigure = true;
            }
        }


        public override void Redraw()
        {




            var brush = new SolidBrush(FillColor);

            DrawPanel.DrawRectangle(DrPen, Math.Min(startPoint.X, endPoint.X), Math.Min(startPoint.Y, endPoint.Y), Math.Abs(endPoint.X - startPoint.X), Math.Abs(endPoint.Y - startPoint.Y));

            DrawPanel.FillRectangle(brush, Math.Min(startPoint.X, endPoint.X), Math.Min(startPoint.Y, endPoint.Y), Math.Abs(endPoint.X - startPoint.X), Math.Abs(endPoint.Y - startPoint.Y));


        }


    }



    public class RectangleCreator : IFiguresCreator
    {
        public IFigure Create(int x0, int y0, Graphics gr, Pen pen, Color Fc)
        {
            return new Rectangle(x0, y0, gr, pen, Fc);
        }



        public string Name
        {
            get
            {
                return " Прямоугольник";
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
