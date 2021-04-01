using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace WindowsFormsApp1
{
    //----------------------------------------------------------------------------------------

    // Class for undo stack realization.

    public class UndoStack
    {

        private Stack<Figure> LastFig;
        private int n = 0;
        public Graphics gr;


        public UndoStack()
        {
            LastFig = new Stack<Figure>();
        }

        public int Count
        {
            get
            {
                return n;
            }
        }

        public Figure ElementAt(int i)
        {
            if (i < n)
                return LastFig.ElementAt<Figure>(i);
            else
                return null;
        }

        public bool Push(Figure F)
        {
            try
            {


                LastFig.Push(F.Clone());
                n++;
                return true;

            }
            catch
            {
                return false;
            }


        }

        public bool LastEnd()
        {
            if (LastFig.Count <= 0)
                return false;
            bool res;
            res = LastFig.ElementAt<Figure>(0).EndOfCurrentFigure;
            return res;

        }

        public bool DrawStack(Graphics gr)
        {
            if (this.Count < 1)
                return false;
            Figure tmp;
            for (int i = this.Count - 1; i >= 0; i--)
            {
                tmp = this.ElementAt(i);
                tmp.DrawPanel = gr;
                tmp.Redraw();

            }

            return true;
        }


        public Figure Pop()
        {
            if (n == 0)
                return null;
            else
            {
                Figure ret = LastFig.Pop();
                n--;
                return ret;
            }
        }


    }
}
