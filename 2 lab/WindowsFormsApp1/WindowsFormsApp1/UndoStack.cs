using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using BasedInterfaces;

namespace WindowsFormsApp1
{
    //----------------------------------------------------------------------------------------

    // Class for undo stack realization.

    public class UndoStack
    {

        private Stack<IFigure> LastFig;
        private int n = 0;
        public Graphics gr;


        public UndoStack()
        {
            LastFig = new Stack<IFigure>();
        }

        public int Count
        {
            get
            {
                return n;
            }
        }

        public IFigure ElementAt(int i)
        {
            if (i < n)
                return LastFig.ElementAt<IFigure>(i);
            else
                return null;
        }

        public bool Push(IFigure F)
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
            res = LastFig.ElementAt<IFigure>(0).EndOfCurrentFigure;
            return res;

        }

        public bool DrawStack(Graphics gr)
        {
            if (this.Count < 1)
                return false;
            IFigure tmp;
            for (int i = this.Count - 1; i >= 0; i--)
            {
                tmp = this.ElementAt(i);
                tmp.DrawPanel = gr;
                tmp.Redraw();

            }

            return true;
        }


        public IFigure Pop()
        {
            if (n == 0)
                return null;
            else
            {
                IFigure ret = LastFig.Pop();
                n--;
                return ret;
            }
        }


    }
}
