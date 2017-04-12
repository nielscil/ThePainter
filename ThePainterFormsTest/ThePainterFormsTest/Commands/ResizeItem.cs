using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThePainterFormsTest.Controllers;
using ThePainterFormsTest.Models;
using ThePainterFormsTest.Visitors;

namespace ThePainterFormsTest.Commands
{
    class ResizeItem : ICommand
    {
        private int _width, _height, _oldWith, _oldHeight;
        private DrawableItem _item;

        public ResizeItem(DrawableItem item, Point begin, Point end)
        {
            _width = end.X - begin.X;
            _height = end.Y - begin.Y;
            _oldWith = item.Width;
            _oldHeight = item.Height;
            _item = item;
        }

        public void Execute(Canvas canvas)
        {
            canvas.SelectedItem = null;

            _item.Accept(new ResizeVisitor(_width, _height));

            canvas.SelectedItem = _item;

            Controller.Instance.InvalidateCanvas();
        }

        public void Undo(Canvas canvas)
        {
            canvas.SelectedItem = null;

            _item.Accept(new ResizeVisitor(_oldWith, _oldHeight));

            canvas.SelectedItem = _item;

            Controller.Instance.InvalidateCanvas();
        }
    }
}
