using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;


namespace WindowsFormsApp1
{
    public interface IFiguresCreator
    {

        IFigure Create(int x0, int y0, Graphics gr, Pen pen, Color Fc);
        
        string Name { get; }
        bool TopsNeeded { get; }
    }
}
