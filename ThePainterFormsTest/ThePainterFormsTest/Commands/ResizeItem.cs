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
    /// Resize item commandclass
    /// </summary>
    class ResizeItem : ICommand
    {
        private int _width, _height, _oldWith, _oldHeight;
        private DrawableItem _item;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="item">tob be resized item</param>
        /// <param name="begin">begin point</param>
        /// <param name="end">end point</param>
        public ResizeItem(DrawableItem item, Point begin, Point end)
        {
            _width = end.X - begin.X;
            _height = end.Y - begin.Y;
            _oldWith = item.Width;
            _oldHeight = item.Height;
            _item = item;
        }

        /// <summary>
        /// Executes the action
        /// </summary>
        /// <param name="canvas"></param>
        public void Execute(Canvas canvas)
        {
            canvas.SelectedItem = null;

            _item.Accept(new ResizeVisitor(_width, _height));

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

            _item.Accept(new ResizeVisitor(_oldWith, _oldHeight));

            canvas.SelectedItem = _item;

            Controller.Instance.InvalidateCanvas();
        }
    }
}
