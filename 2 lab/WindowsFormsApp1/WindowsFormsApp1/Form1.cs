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
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        LinkedList<Type> UsedTypes = new LinkedList<Type>();
        LinkedList<IFiguresCreator> Creators = new LinkedList<IFiguresCreator>();

        UndoStack FiguresBackBuffer = new UndoStack(), FiguresFrontBuffer = null;
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

            IFiguresCreator CurrentCreator = Creators.ElementAt<IFiguresCreator>(comboBox1.SelectedIndex);   
            CurrentFigure = CurrentCreator.Create(-1, -1, gr, pen, FillColorPanel.BackColor);

            pictureBox1.Image = MainPicture;


        }

        private bool LoadModules()
        {
            bool FiguresExist = false;
            try
            {
                Assembly assembly = Assembly.GetExecutingAssembly();
                Type[] types = assembly.GetTypes();
                int k = 0;
                for (int i = 0; i < types.Length; i++)
                {
                    if (types[i].GetInterface(typeof(IFiguresCreator).FullName) != null)
                    {
                        Creators.AddLast((IFiguresCreator)Activator.CreateInstance(types[i]));


                        comboBox1.Items.Add(Creators.ElementAt<IFiguresCreator>(k).Name);
                        FiguresExist = true;
                        k++;
                    }
                }
            }
            catch
            {
                FiguresExist = false;
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

                if (FiguresBackBuffer.Count < 1)
                {
                    FiguresBackBuffer.Push(CurrentFigure);
                    UndoButton.Enabled = true;
                }
                else
                {
                    if (FiguresBackBuffer.LastEnd())
                    {
                        FiguresBackBuffer.Push(CurrentFigure);
                    }
                    else
                    {
                        FiguresBackBuffer.Pop();
                        FiguresBackBuffer.Push(CurrentFigure);
                    }

                }




                CurrentFigure.StartPoint = new Point(-2,-2);
                pictureBox1.Image = MainPicture;

                if (FiguresFrontBuffer != null)
                {
                    FiguresFrontBuffer = null;
                    RedoButton.Enabled = false;
                }

            }
            catch { }
        }

        private void ClearButton_Click(object sender, EventArgs e)
        {
            gr = Graphics.FromImage(MainPicture);

            gr.Clear(pictureBox1.BackColor);
            pictureBox1.Image = MainPicture;
            FiguresBackBuffer = new UndoStack();
            FiguresFrontBuffer = new UndoStack();
            UndoButton.Enabled = false;
            RedoButton.Enabled = false;


            IFiguresCreator CurrentCreator = Creators.ElementAt<IFiguresCreator>(comboBox1.SelectedIndex);
            CurrentFigure = CurrentCreator.Create(-1, -1, gr, pen, FillColorPanel.BackColor);

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            numericUpDown1.Visible = false;
            TopsLabel.Visible = false;
            label2.Visible = false;
            IFiguresCreator CurrentCreator = Creators.ElementAt<IFiguresCreator>(comboBox1.SelectedIndex);

            CurrentFigure = CurrentCreator.Create(-1, -1, gr, pen, FillColorPanel.BackColor);
                
            if (CurrentCreator.TopsNeeded)
            {
                numericUpDown1.Visible = true;
                TopsLabel.Visible = true;
            }
            if (FiguresBackBuffer.Count>0)
                FiguresBackBuffer.ElementAt(0).EndOfCurrentFigure = true;

        }

        private void PenWidthBar_Scroll(object sender, EventArgs e)
        {

            
            CurrentFigure.DrPen.Width = PenWidthBar.Value;
            pen.Width = PenWidthBar.Value;
            if (FiguresBackBuffer.Count > 1)
            {
                Figure tmp = FiguresBackBuffer.Pop();
                if (!tmp.EndOfCurrentFigure)
                {
                    tmp.DrPen.Width = PenWidthBar.Value;            
                }
                FiguresBackBuffer.Push(tmp);
                gr = Graphics.FromImage(MainPicture);
                gr.Clear(pictureBox1.BackColor);
                FiguresBackBuffer.DrawStack(gr);
                pictureBox1.Image = MainPicture;
            }

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

        private void RedoButton_Click(object sender, EventArgs e)
        {
            Figure tmp = FiguresFrontBuffer.Pop();
            gr = Graphics.FromImage(MainPicture);
            tmp.DrawPanel = gr;
            tmp.Redraw();

            FiguresBackBuffer.Pop();
            bool buf = tmp.EndOfCurrentFigure;
            tmp.EndOfCurrentFigure = true;
            FiguresBackBuffer.Push(tmp);
            tmp.EndOfCurrentFigure = buf;


            UndoButton.Enabled = true;
           



            

            pictureBox1.Image = MainPicture;
            gr.Dispose();
            if (FiguresFrontBuffer.Count == 0)
            {
                RedoButton.Enabled = false;
            }


        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {

            if (e.Control == true )
            {
                if (e.KeyCode == Keys.Z && UndoButton.Enabled)
                {
                    button1_Click(null, null);
                }
                if (e.KeyCode == Keys.B && RedoButton.Enabled)
                {
                    RedoButton_Click(null, null);
                }
            }
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            FileStream F = File.Open("D:/pic.dat", FileMode.Create);
            try
            {
                if (F!= null)
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    for (int i =0; i < FiguresBackBuffer.Count; i++)
                    {
                        Figure Tmp = FiguresBackBuffer.ElementAt(i);

                        //formatter.Serialize(F, FiguresBackBuffer.ElementAt(i));


                    }

                }          
            }
            finally
            {
                F.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int N = FiguresBackBuffer.Count;
            if (N <= 0)
                return;
            if (FiguresFrontBuffer == null)
                FiguresFrontBuffer = new UndoStack();

            
            Figure Last = FiguresBackBuffer.ElementAt(0);
            FiguresFrontBuffer.Push(Last);
            if (Last.OnePointBack())
            {
                            
            }
            else
                FiguresBackBuffer.Pop();

            RedoButton.Enabled = true;

            gr = Graphics.FromImage(MainPicture);     
            gr.Clear(pictureBox1.BackColor);

            FiguresBackBuffer.DrawStack(gr);

            
            pictureBox1.Image = MainPicture;

            if (FiguresBackBuffer.Count <= 0)
                UndoButton.Enabled = false;

           
            IFiguresCreator CurrentCreator = Creators.ElementAt<IFiguresCreator>(comboBox1.SelectedIndex);
            CurrentFigure = CurrentCreator.Create(-1, -1, gr, pen, FillColorPanel.BackColor);
            Last.EndOfCurrentFigure = true;
        }

        private void PreDrawTimer_Tick(object sender, EventArgs e)
        {
            PreDrawTimer.Enabled = false;
        }
    }


}
