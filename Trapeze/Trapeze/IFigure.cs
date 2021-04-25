using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Newtonsoft.Json;

namespace WindowsFormsApp1
{
    public interface IFigure
    {
        float PenWidth { get; set; }
        Color PenColor { get; set; }
        Pen DrPen { get; set; }
        Point StartPoint { get; set; }
        Point PreDrawEndPoint { get; set; }
        Point EndPoint { get; set; }
        void Redraw();
        IFigure Clone();
        Color FillColor { get; set; }
        Graphics DrawPanel { get; set; }
        bool EndOfCurrentFigure { get; set; }
    }
}
