using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _1_lab
{
    public partial class Form1 : Form
    {
        enum drMode { drModeLine, drModeRect, drModeEllipse, drModePolygon, drModeBrLine };
        Graphics gr;
        int x = -1, y = -1, x1 = -1, y1 = -1;
        bool isMoving = false, shiftPressed = false, controlPressed = false;
        Pen p;
        drMode dr = drMode.drModeLine;


        public Form1()
        {

            InitializeComponent();
            gr = panel1.CreateGraphics();
            gr.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            p = new Pen(Color.Black);
            p.StartCap = p.EndCap = System.Drawing.Drawing2D.LineCap.Round;
            p.Width = 6;
            comboBox1.SelectedItem = comboBox1.Items[0];

        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
        
            x = e.X;
            y = e.Y;
            isMoving = true;
            if (dr == drMode.drModeLine)
                gr.DrawLine(p, e.Location, new Point(x+1, y+1));

        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            if (!isMoving)
                return;

            if (shiftPressed && dr == drMode.drModeLine )
                gr.DrawLine(p, new Point(x, y), e.Location);
            if (dr == drMode.drModeRect || dr == drMode.drModeEllipse)
            {

                int buf;
                var tmp = new Point(e.X, e.Y);
                if (tmp.X < x)
                {
                    buf = tmp.X;
                    tmp.X = x;
                    x = buf;
                }
                if (tmp.Y < y)
                {
                    buf = tmp.Y;
                    tmp.Y = y;
                    y = buf;
                }
                if (dr == drMode.drModeRect)
                    gr.DrawRectangle(p, x, y, tmp.X - x, tmp.Y - y);
                else
                    gr.DrawEllipse(p, x, y, tmp.X - x, tmp.Y - y);
            }
            if (dr == drMode.drModePolygon)
            {
                int r = (int)(e.Y - y)/2, x1,y1;
              
                var center = new PointF(x+r, y+r);
                int n = (int)numericUpDown1.Value;
                double angle = Math.PI * 2 / n;
                Point[] points = new Point[n];
                for (int i =0; i< n; i++)
                {
                    x1 = (int)(center.X + Math.Cos(i * angle) * r);
                    y1 = (int)(center.Y + Math.Sin(i * angle) * r);
                    points[i].X = x1;
                    points[i].Y = y1;

                }

                
                gr.DrawPolygon(p, points) ;



            }
            isMoving = false;
            
            x = -1; y = -1; 
            



        }

        private void ClearButt_Click(object sender, EventArgs e)
        {
            gr.Clear(panel1.BackColor);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            label1.Visible = false;
            numericUpDown1.Visible = false;
            switch (comboBox1.SelectedIndex)
            {
                case 0:
                    dr = drMode.drModeLine;
                    break;
                case 1:
                    dr = drMode.drModeRect;
                    break;
                case 2:
                    dr = drMode.drModeEllipse;
                    break;
                case 3:
                    dr = drMode.drModePolygon;
                    label1.Visible = true;
                    numericUpDown1.Visible = true;
                    break;
                case 4:
                    dr = drMode.drModeBrLine;
                    break;
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void panel1_MouseClick(object sender, MouseEventArgs e)
        {
            if (dr != drMode.drModeBrLine)
                return;

            if (x1 == -1 || y1 == -1)
            {
                x1 = e.X;
                y1 = e.Y;
                return;
            }
            else
            {
                gr.DrawLine(p, new Point(x1, y1), e.Location);
                x1 = e.X;
                y1 = e.Y;
            }

                
            

        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (!isMoving || dr != drMode.drModeLine)
            { return; }


            if (e.X != x && e.Y!=y && !shiftPressed && !controlPressed)
            {
                gr.DrawLine(p,new Point(x,y), e.Location);
                x = e.X; y = e.Y;
            }

            if ( controlPressed)
            {
                int deltaX = e.X - x, deltaY = e.Y - y;
                double tga = Math.Abs((double)deltaY / (deltaX + 0.01));

                if (tga < Math.Tan(Math.PI/4))
                {
                    gr.DrawLine(p, new Point(x, y), new Point(e.X, y));
                    //x = e.X;
                }


                if (tga >= Math.Tan(Math.PI / 4))
                {
                    gr.DrawLine(p, new Point(x, y), new Point(x, e.Y));
                    //y = e.Y;
                }


            }
            
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            shiftPressed = false;
            controlPressed = false;
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            shiftPressed = e.Shift;
            controlPressed = e.Control;
        }
    }
}
