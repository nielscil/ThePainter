using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThePainterFormsTest.Models;

namespace ThePainterFormsTest.Visitors
{
    public class ResizeVisitor : IVisitor
    {
        private int _width;
        private int _height;

        private int? _widthDiff;
        private int? _heightDiff;

        public ResizeVisitor(int width, int heigth)
        {
            _width = width;
            _height = heigth;
        }

        public void Visit(Rectangle rectangle)
        {
            rectangle.Width = _widthDiff.HasValue ? rectangle.Width + _widthDiff.Value : _width;
            rectangle.Height = _heightDiff.HasValue ? rectangle.Height + _heightDiff.Value : _height;
        }

        public void Visit(Ellipse ellipse)
        {
            ellipse.Width = _widthDiff.HasValue ? ellipse.Width + _widthDiff.Value : _width;
            ellipse.Height = _heightDiff.HasValue ? ellipse.Height + _heightDiff.Value : _height;
        }

        public void Visit(Group group)
        {
            _widthDiff = _width - group.Width;
            _heightDiff = _height - group.Height;
        }
    }
}
