using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;
using System.Runtime;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        LinkedList<Type> UsedTypes = new LinkedList<Type>();

        UndoStack St = new UndoStack();
        Graphics gr;
        Pen pen;
        Figure CurrentFigure;
        Bitmap MainPicture= new Bitmap(1000,1000), TemporaryImage = new Bitmap(1000, 1000);
        int FpsCounter = 0;
        
        

        public Form1()
        {

            InitializeComponent();

            if (!LoadModules())
            {
                MessageBox.Show("Error. No figures modules found.");
                Application.Exit();
            }
            
            

            comboBox1.SelectedIndex = 0;
            gr = Graphics.FromImage(MainPicture);


            gr.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            pen = new Pen(Color.Black);
            pen.StartCap = pen.EndCap = System.Drawing.Drawing2D.LineCap.Round;
            pen.Width = PenWidthBar.Value;
            CurrentFigure = (Figure)Activator.CreateInstance(UsedTypes.ElementAt<Type>(comboBox1.SelectedIndex), -1, -1, gr, pen, FillColorPanel.BackColor);

            pictureBox1.Image = MainPicture;


        }

        private bool LoadModules()
        {
            bool FiguresExist = false;

            Assembly assembly = Assembly.GetExecutingAssembly();
            Type[] types = assembly.GetTypes();
            for (int i = 0; i < types.Length; i++)
            {
               if (Attribute.IsDefined(types[i], typeof(FigureNameAttribute)))
               {
                    FigureNameAttribute t = (FigureNameAttribute)types[i].GetCustomAttribute(typeof(FigureNameAttribute));
                    if ( (t.IsDrawing))
                    {
                        UsedTypes.AddLast(types[i]);
                        comboBox1.Items.Add(t.Name);
                        FiguresExist = true;
                    }

                }                          
            }
            return FiguresExist;
        }


        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {


            CurrentFigure.StartPoint = new Point(e.X, e.Y);
            PreDrawTimer.Enabled = true;



            FpsCounter = 0;
            timer1.Enabled = true;

        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (CurrentFigure.StartPoint.X < 0)
                return;

            if (!PreDrawTimer.Enabled )
            {
                FpsCounter++;

                TemporaryImage.Dispose();

                TemporaryImage = (Bitmap)MainPicture.Clone();
                
                pictureBox1.Image = TemporaryImage;
                gr = Graphics.FromImage(TemporaryImage);
                CurrentFigure.DrawPanel = gr;


                CurrentFigure.PreDrawEndPoint = e.Location;
                gr.Dispose();
                PreDrawTimer.Enabled = true;
               
               
                
            }


        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {

            timer1.Enabled = false;
            PreDrawTimer.Enabled = false;

            try
            {

                gr = Graphics.FromImage(MainPicture);
                CurrentFigure.DrawPanel = gr;


                if (e.Button == MouseButtons.Right)
                    CurrentFigure.EndOfCurrentFigure = true;
                CurrentFigure.EndPoint = new Point(e.X, e.Y);

                CurrentFigure.StartPoint = new Point(-2,-2);
                pictureBox1.Image = MainPicture;
                
            }
            catch { }
        }

        private void ClearButton_Click(object sender, EventArgs e)
        {
            gr = Graphics.FromImage(MainPicture);

            gr.Clear(pictureBox1.BackColor);
            pictureBox1.Image = MainPicture;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            numericUpDown1.Visible = false;
            TopsLabel.Visible = false;
            label2.Visible = false;
            CurrentFigure = (Figure)Activator.CreateInstance(UsedTypes.ElementAt<Type>(comboBox1.SelectedIndex), -1, -1, gr, pen, FillColorPanel.BackColor);

            
            foreach (PropertyInfo pi in UsedTypes.ElementAt<Type>(comboBox1.SelectedIndex).GetProperties())
            {
                if ((pi.Name == "TopAmount"))
                {
                    numericUpDown1.Visible = true;
                    TopsLabel.Visible = true;
                    break;
                }

            }


        }

        private void PenWidthBar_Scroll(object sender, EventArgs e)
        {
            pen.Width = PenWidthBar.Value;
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            (CurrentFigure as RigthPolygon).TopAmount = (int)numericUpDown1.Value;
        }

        private void PenColorButton_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                PenColorPanel.BackColor = colorDialog1.Color;
                pen.Color = colorDialog1.Color;
            }
        }

        private void FillColorButton_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                FillColorPanel.BackColor = colorDialog1.Color;
                CurrentFigure.FillColor = colorDialog1.Color;


            }
        }




        private void timer1_Tick(object sender, EventArgs e)
        {
            label3.Text = FpsCounter.ToString();
            FpsCounter = 0;
        }

        private void PreDrawTimer_Tick(object sender, EventArgs e)
        {
            PreDrawTimer.Enabled = false;
        }
    }


   

    //----------------------------------------------------------------------------------------

    // Class for undo stack realization.

    public class UndoStack
    {
        private int StackSize = 10;
        private Stack<Figure> LastFig;
        private int n = 0;
        public Graphics gr;

        public UndoStack(int size)
        {
            StackSize = size;
            LastFig = new Stack<Figure>();
        }
        public UndoStack()
        {
            LastFig = new Stack<Figure>();
        }

        public bool Push(Figure F)
        {
            if (n < StackSize)
            {
                LastFig.Push(F);
                n++;
                return true;
            }
            else
                return false;
        }

        public Figure Pop()
        {
            if (n == 0)
                return null;
            else
            {
                Figure ret = LastFig.Pop();
                n--;

                Figure[] Arr = new Figure[StackSize];
                int FigAmount = n, i;


                for ( i = 0; i < n; i++)               
                    Arr[i] = LastFig.Pop();

                for (i = n -1 ; i >=0; i--)
                {
                    LastFig.Push(Arr[i]);
                    Arr[i].DrawPanel = gr;
                    Arr[i].EndPoint = Arr[i].EndPoint;

                }




                return ret;
            }
        }


    }


}
