using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThePainterFormsTest.Models;

namespace ThePainterFormsTest.Visitors
{
    public class TempMoveVisitor : IVisitor
    {

        private Point _begin;
        private Point _end;

        public TempMoveVisitor(Point begin, Point end)
        {
            _begin = begin;
            _end = end;
        }

        public void Visit(Models.Rectangle rectangle)
        {
            rectangle.X += _end.X - _begin.X;
            rectangle.Y += _end.Y - _begin.Y;
        }

        public void Visit(Ellipse ellipse)
        {
            ellipse.X += _end.X - _begin.X;
            ellipse.Y += _end.Y - _begin.Y;
        }

        public void BeforeGroup(Group group)
        {
            //do nothing I guess??
        }

        public void AfterGroup(Group group)
        {
            //do nothing I guess??
        }
    }
}
