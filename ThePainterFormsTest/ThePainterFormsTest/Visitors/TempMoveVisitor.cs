using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThePainterFormsTest.Models;

namespace ThePainterFormsTest.Visitors
{
    /// <summary>
    /// Temp move visitor for moving animation
    /// </summary>
    public class TempMoveVisitor : IVisitor
    {

        private Point _begin;
        private Point _end;

        public TempMoveVisitor(Point begin, Point end)
        {
            _begin = begin;
            _end = end;
        }

        public void Visit(BasicFigure figure)
        {
            figure.X += _end.X - _begin.X;
            figure.Y += _end.Y - _begin.Y;
        }

        public void BeforeGroup(Group group)
        {
            //do nothing I guess??
        }

        public void AfterGroup(Group group)
        {
            //do nothing I guess??
        }

        public void BeforeOrnament(Ornament ornament)
        {
            //Do nothing
        }

        public void AfterOrnament(Ornament ornament)
        {
            //Do nothing I guess??
        }
    }
}
