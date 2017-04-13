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
    /// Select item with deselect commandclass
    /// </summary>
    class SelectItemWithDeselect : ICommand
    {
        private DrawableItem _item;
        private DrawableItem _previousSelectedItem;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="item">to be selected item</param>
        public SelectItemWithDeselect(DrawableItem item)
        {
            _item = item;
        }

        /// <summary>
        /// Executes the action
        /// </summary>
        /// <param name="canvas"></param>
        public void Execute(Canvas canvas)
        {
            _previousSelectedItem = canvas.SelectedItem;
            _previousSelectedItem?.Accept(DeselectVisitor.Instance);
            Controller.Instance.SelectNode(_previousSelectedItem, false);

            _item.Accept(SelectVisitor.Instance);
            canvas.SelectedItem = _item;
            Controller.Instance.SelectNode(_item, true);

            Controller.Instance.InvalidateCanvas();
        }

        /// <summary>
        /// Undo's the action
        /// </summary>
        /// <param name="canvas"></param>
        public void Undo(Canvas canvas)
        {
            _item.Accept(DeselectVisitor.Instance);
            Controller.Instance.SelectNode(_item, false);

            _previousSelectedItem?.Accept(SelectVisitor.Instance);
            canvas.SelectedItem = _previousSelectedItem;
            Controller.Instance.SelectNode(_previousSelectedItem, true);

            Controller.Instance.InvalidateCanvas();
        }
    }
}
