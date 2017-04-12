using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThePainterFormsTest.Models;

namespace ThePainterFormsTest.Visitors
{
    public class TempResizeVisitor : IVisitor
    {

        private Point _begin;
        private Point _end;

        public TempResizeVisitor(Point begin, Point end)
        {
            _begin = begin;
            _end = end;
        }

        public void Visit(BasicFigure figure)
        {
            figure.Height += _end.Y - _begin.Y;
            figure.Width += _end.X - _begin.X;
        }

        public void BeforeGroup(Group group)
        {
            //Do nothing I guess??
        }

        public void AfterGroup(Group group)
        {
            //do nothing I guess??
        }
    }
}
