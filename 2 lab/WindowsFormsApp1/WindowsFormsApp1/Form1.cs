using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {

        Graphics gr;
        Pen p;
        Figure CurrentFig;
        Bitmap p1= new Bitmap(1000,1000), p2 = new Bitmap(1000, 1000);

        List<Figure> figures = new List<Figure>();

        public Form1()
        {

            InitializeComponent();
            comboBox1.SelectedIndex = 0;
            gr = Graphics.FromImage(p1);


            gr.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            p = new Pen(Color.Black);
            p.StartCap = p.EndCap = System.Drawing.Drawing2D.LineCap.Round;
            p.Width = PenWidthBar.Value;
            CurrentFig = new StraigthLine(-1, -1, gr, p, FillColorPanel.BackColor);

            pictureBox1.Image = p1;


        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {


            CurrentFig.StartPoint = new Point(e.X, e.Y);
            PreDrawTimer.Enabled = true;

        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (CurrentFig.StartPoint.X < 0)
                return;

            if (!PreDrawTimer.Enabled)
            {
                p2.Dispose();

                p2 = (Bitmap)p1.Clone();
                
                pictureBox1.Image = p2;
                gr = Graphics.FromImage(p2);
                CurrentFig.DrawPanel = gr;


                CurrentFig.EndPoint = e.Location;
                gr.Dispose();
                PreDrawTimer.Enabled = true;
               
               
                gr.Dispose();
            }


        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {

            PreDrawTimer.Enabled = false;
            try
            {

                gr = Graphics.FromImage(p1);
                CurrentFig.DrawPanel = gr;
                CurrentFig.EndPoint = new Point(e.X, e.Y);
                CurrentFig.StartPoint = new Point(-2,-2);
                
                pictureBox1.Image = p1;

                if ((e.Button == MouseButtons.Right) && (CurrentFig is BrokenLine))
                {
                    CurrentFig = new BrokenLine(-1, -1, gr, p, FillColorPanel.BackColor);
                    return;
                }
            }
            catch { }
        }

        private void ClearButton_Click(object sender, EventArgs e)
        {
            gr = Graphics.FromImage(p1);

            gr.Clear(pictureBox1.BackColor);
            pictureBox1.Image = p1;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            RigthPolygon.Visible = false;
            numericUpDown1.Visible = false;
            TopsLabel.Visible = false;

            pictureBox1.MouseDown -= panel1_MouseDown;
            pictureBox1.MouseDown += panel1_MouseDown;

            switch (comboBox1.SelectedIndex)
            {
                

                case 0:
                    CurrentFig = new StraigthLine(-1, -1, gr, p, FillColorPanel.BackColor);
                    break;
                case 1 :
                    CurrentFig = new Rectangle(-1, -1, gr, p, FillColorPanel.BackColor);
                    break;
                case 2:
                    CurrentFig = new Ellipse(-1, -1, gr, p, FillColorPanel.BackColor);
                    break;
                case 3:
                    RigthPolygon.Checked = true;
                    RigthPolygon.Visible = true;
                    numericUpDown1.Visible = true;
                    TopsLabel.Visible = true;
                    CurrentFig = new RigthPolygon(-1, -1, gr, p, FillColorPanel.BackColor);
                    break;
                case 4:
                    pictureBox1.MouseDown -= panel1_MouseDown;
                    CurrentFig = new BrokenLine (-1, -1, gr, p, FillColorPanel.BackColor);
                    break;
            }
        }

        private void PenWidthBar_Scroll(object sender, EventArgs e)
        {
            p.Width = PenWidthBar.Value;
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            if (CurrentFig is RigthPolygon)
                (CurrentFig as RigthPolygon).TopAmount = (int)numericUpDown1.Value;
        }

        private void PenColorButton_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                PenColorPanel.BackColor = colorDialog1.Color;
                p.Color = colorDialog1.Color;
            }
        }

        private void FillColorButton_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                FillColorPanel.BackColor = colorDialog1.Color;
                CurrentFig.FillColor = colorDialog1.Color;


            }
        }

        private void PreDrawTimer_Tick(object sender, EventArgs e)
        {
            PreDrawTimer.Enabled = false;
        }
    }







    public abstract class Figure
    {
        public Graphics DrawPanel;
        protected Point startPoint;
        protected Point endPoint = new Point(-1,-1);
        public Pen DrPen;
        public Color FillColor;


        public Figure(int x0, int y0, Graphics gr, Pen p, Color Fc)
        {
            startPoint = new Point(x0, y0);
            DrawPanel = gr;
            DrPen = p;
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

        protected void FindLeftTopPoint( ref Point p1, ref Point p2)
        {
            int buf;
            if (p2.X < p1.X)
            {
                buf = p2.X;
                p2.X = p1.X;
                p1.X = buf;
            }
            if (p2.Y < p1.Y)
            {
                buf = p2.Y;
                p2.Y = p1.Y;
                p1.Y = buf;
            }
        }


    }

    public class StraigthLine : Figure
    {
        public StraigthLine(int x0, int y0, Graphics gr, Pen p, Color Fc) : base(x0, y0, gr, p, Fc) { }

        public override Point EndPoint
        {
            get
            {
                return endPoint;
            }
            set
            {
                endPoint = value;
                DrawPanel.DrawLine(DrPen, startPoint, endPoint);

            }
        }



    }

    public class Rectangle : Figure
    {

        public Rectangle(int x0, int y0, Graphics gr, Pen p, Color Fc) : base(x0, y0, gr, p, Fc) { }


        public override Point EndPoint
        {
            get
            {
                return endPoint;
            }
            set
            {
                endPoint = value;
                Point p1 = new Point(startPoint.X, startPoint.Y);

                FindLeftTopPoint(ref startPoint, ref endPoint);

                var br = new SolidBrush(FillColor);
                DrawPanel.DrawRectangle(DrPen, startPoint.X, startPoint.Y, endPoint.X - startPoint.X, endPoint.Y - startPoint.Y);
                DrawPanel.FillRectangle(br, startPoint.X, startPoint.Y, endPoint.X - startPoint.X, endPoint.Y - startPoint.Y);
                startPoint = p1;
            }
        }

    }

    public class Ellipse : Figure
    {

        public Ellipse(int x0, int y0, Graphics gr, Pen p, Color Fc) : base(x0, y0, gr, p, Fc) { }

        public override Point EndPoint
        {
            get
            {
                return endPoint;
            }
            set
            {
                endPoint = value;
                Point p1 = new Point(startPoint.X, startPoint.Y);
                FindLeftTopPoint(ref startPoint, ref endPoint);


                var br = new SolidBrush(FillColor);
                DrawPanel.DrawEllipse(DrPen, startPoint.X, startPoint.Y, endPoint.X - startPoint.X, endPoint.Y - startPoint.Y);
                DrawPanel.FillEllipse(br, startPoint.X, startPoint.Y, endPoint.X - startPoint.X, endPoint.Y - startPoint.Y);
                startPoint = p1;
                br.Dispose();
            }
        }

    }

    public class RigthPolygon : Figure
    {

        public RigthPolygon(int x0, int y0, Graphics gr, Pen p, Color Fc) : base(x0, y0, gr, p, Fc) { }

        public int TopAmount = 3;

        public override Point EndPoint
        {
            get
            {
                return endPoint;
            }
            set
            {
                endPoint = value;

                int r = (int)(endPoint.X - startPoint.X) / 2, x1, y1;

                var center = new PointF(StartPoint.X + r, startPoint.Y + r);
                
                double angle = Math.PI * 2 / TopAmount, shiftAngle = Math.PI* (endPoint.X - startPoint.X) / (endPoint.Y - startPoint.Y);



                Point[] points = new Point[TopAmount];
                for (int i = 0; i < TopAmount; i++)
                {
                    x1 = (int)(center.X + Math.Cos(i * angle + shiftAngle) * r);
                    y1 = (int)(center.Y + Math.Sin(i * angle+ shiftAngle) * r);
                    points[i].X = x1;
                    points[i].Y = y1;

                }

                var br = new SolidBrush(FillColor);
               
                DrawPanel.DrawPolygon(DrPen,points);
                DrawPanel.FillPolygon(br, points);

                br.Dispose();

            }
        }
    }

    public class BrokenLine: Figure
    {
        public BrokenLine(int x0, int y0, Graphics gr, Pen p, Color Fc) : base(x0, y0, gr, p, Fc) { }

        public override Point StartPoint
        {
            get => base.StartPoint ;

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
                if (startPoint.X>0) 
                 DrawPanel.DrawLine(DrPen, startPoint, endPoint);

            }
        }

    }
}
