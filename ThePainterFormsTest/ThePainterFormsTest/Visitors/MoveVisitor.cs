using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThePainterFormsTest.Models;

namespace ThePainterFormsTest.Visitors
{
    public class MoveVisitor : IVisitor
    {
        private int _x;
        private int _y;

        private int? _xDiff;
        private int? _yDiff;
            
        public MoveVisitor(int x, int y)
        {
            _x = x;
            _y = y;
        }

        public void Visit(Rectangle rectangle)
        {
            rectangle.X = _xDiff.HasValue ? rectangle.X + _xDiff.Value : _x;
            rectangle.Y = _yDiff.HasValue ? rectangle.Y + _yDiff.Value : _y;
        }

        public void Visit(Ellipse ellipse)
        {
            ellipse.X = _xDiff.HasValue ? ellipse.X + _xDiff.Value : _x;
            ellipse.Y = _yDiff.HasValue ? ellipse.Y + _yDiff.Value : _y;
        }

        public void Visit(Group group)
        {
            _xDiff = _x - group.X;
            _yDiff = _y - group.Y;
        }
    }
}
