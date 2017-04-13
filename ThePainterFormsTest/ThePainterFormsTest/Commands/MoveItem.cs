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
    /// <summary>
    /// Move item commandclass
    /// </summary>
    class MoveItem : ICommand
    {
        private int _x, _y, _oldX, _oldY;
        private DrawableItem _item;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="item">to be moved item</param>
        /// <param name="begin">begin point</param>
        /// <param name="end">end point</param>
        public MoveItem(DrawableItem item, Point begin, Point end)
        {
            _x = end.X - begin.X;
            _y = end.Y - begin.Y;
            _oldX = item.X;
            _oldY = item.Y;
            _item = item;
        }

        /// <summary>
        /// Executes the action
        /// </summary>
        /// <param name="canvas"></param>
        public void Execute(Canvas canvas)
        {
            canvas.SelectedItem = null;

            _item.Accept(new MoveVisitor(_x, _y));

            canvas.SelectedItem = _item;

            Controller.Instance.InvalidateCanvas();
        }

        /// <summary>
        /// Undo's the action
        /// </summary>
        /// <param name="canvas"></param>
        public void Undo(Canvas canvas)
        {
            canvas.SelectedItem = null;

            _item.Accept(new MoveVisitor(_oldX, _oldY));

            canvas.SelectedItem = _item;

            Controller.Instance.InvalidateCanvas();
        }
    }
}
