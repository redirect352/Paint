﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace WindowsFormsApp1
{
    public class FigureNameAttribute : System.Attribute
    {
        public string Name;
        public bool IsDrawing;
        public FigureNameAttribute(string name, bool dr)
        {
            Name = name;
            IsDrawing = dr;
        }
    }


    [FigureNameAttribute("Figure", false)]
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

}
