using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Newtonsoft.Json;
using WindowsFormsApp1.FugureInterface;

namespace WindowsFormsApp1
{

    

    [Serializable]
    public abstract class Figure : IFigure
    {
        
        protected Graphics drawPanel;
        protected Point startPoint;
        protected Point endPoint = new Point(-1, -1);


        protected Pen drPen = null;
        
        protected bool endOfCurrentFigure = false;

        protected float penWidth = -1;
        protected Color penColor;


        public Color FillColor { get;set;}

        [JsonIgnore]
        public Graphics DrawPanel {
            get
            {
                return drawPanel;
            }
            set
            {
                drawPanel = value;
            }
        }

        public bool EndOfCurrentFigure
        {
            get {
                return endOfCurrentFigure;
            }
            set{
                endOfCurrentFigure = value;
            }
        }

        public float PenWidth {
            get {
                return penWidth;
            }
            set {
                if (value < 0)
                    return;      
                penWidth = value;
                if (drPen != null)
                    drPen.Width = value;
            }
        }
        public Color PenColor {
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
                if ((drPen == null) && (PenWidth >=0) )
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

        public Figure(int x0, int y0, Graphics gr, Pen pen, Color Fc)
        {
            startPoint = new Point(x0, y0);
            DrawPanel = gr;
            DrPen = pen;
            FillColor = Fc;
        }
  

        public virtual IFigure Clone()
        {
            return null;

        }

        public virtual bool OnePointBack() { return false; }
        public virtual void Redraw()
        { }

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

    }

}
