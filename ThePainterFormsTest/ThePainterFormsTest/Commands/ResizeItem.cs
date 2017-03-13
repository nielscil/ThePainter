using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThePainterFormsTest.Controllers;
using ThePainterFormsTest.Models;

namespace ThePainterFormsTest.Commands
{
    class ResizeItem : ICommand
    {
        private int _width, _height, _oldWith, _oldHeight;
        private ICanvasItem _item;

        public ResizeItem(ICanvasItem item, Point begin, Point end)
        {
            _width += end.X - begin.X + item.Width;
            _height += end.Y - begin.Y + item.Height;
            _oldWith = item.Width;
            _oldHeight = item.Height;
            _item = item;
        }

        public void Execute(Canvas canvas)
        {
            canvas.SelectedItem = null;
            _item.Resize(_width, _height);
            canvas.SelectedItem = _item;

            Controller.Instance.InvalidateCanvas();
        }

        public void Undo(Canvas canvas)
        {
            canvas.SelectedItem = null;
            _item.Resize(_oldWith, _oldHeight);
            canvas.SelectedItem = _item;

            Controller.Instance.InvalidateCanvas();
        }
    }
}
