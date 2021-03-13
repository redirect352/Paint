using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace WindowsFormsApp1
{

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

    public class StraigthLineCreator : IFiguresCreator
    {
        public Figure Create(int x0, int y0, Graphics gr, Pen pen, Color Fc)
        {
            return new StraigthLine(x0, y0, gr, pen, Fc);
        }
        public string Name
        {
            get
            {
                return " Прямая";
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
