using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThePainterFormsTest.Controllers;
using ThePainterFormsTest.Models;
using ThePainterFormsTest.Visitors;

namespace ThePainterFormsTest.Commands
{
    /// <summary>
    /// Deselect commandclass
    /// </summary>
    public class DeselectItem : ICommand
    {
        private DrawableItem _item;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="item">item to deselect</param>
        public DeselectItem(DrawableItem item)
        {
            _item = item;
        }

        /// <summary>
        /// Executes the action
        /// </summary>
        /// <param name="canvas"></param>
        public void Execute(Canvas canvas)
        {
            _item.Accept(DeselectVisitor.Instance);
            canvas.SelectedItem = null;
            Controller.Instance.SelectNode(_item, false);

            Controller.Instance.InvalidateCanvas();
        }

        /// <summary>
        /// Undo's the action
        /// </summary>
        /// <param name="canvas"></param>
        public void Undo(Canvas canvas)
        {
            _item.Accept(SelectVisitor.Instance);
            canvas.SelectedItem = _item;
            Controller.Instance.SelectNode(_item, true);

            Controller.Instance.InvalidateCanvas();
        }
    }
}
