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
    class MoveItem : ICommand
    {
        private int _x, _y, _oldX, _oldY;
        private ICanvasItem _item;

        public MoveItem(ICanvasItem item, Point begin, Point end)
        {
            _x = end.X - begin.X + item.X;
            _y = end.Y - begin.Y + item.Y;
            _oldX = item.X;
            _oldY = item.Y;
            _item = item;
        }

        public void Execute(Canvas canvas)
        {
            canvas.SelectedItem = null;
            _item.Move(_x, _y);
            canvas.SelectedItem = _item;

            Controller.Instance.InvalidateCanvas();
        }

        public void Undo(Canvas canvas)
        {
            canvas.SelectedItem = null;
            _item.Move(_oldX, _oldY);
            canvas.SelectedItem = _item;

            Controller.Instance.InvalidateCanvas();
        }
    }
}
