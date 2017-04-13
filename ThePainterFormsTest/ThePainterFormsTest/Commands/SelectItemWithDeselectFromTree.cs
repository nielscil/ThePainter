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
    /// Select item with deselect from tree commandclass
    /// </summary>
    public class SelectItemWithDeselectFromTree : ICommand
    {
        private DrawableItem _item;
        private DrawableItem _previousSelectedItem;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="item">to be selected item</param>
        public SelectItemWithDeselectFromTree(DrawableItem item)
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

            _item.Accept(SelectVisitor.Instance);
            canvas.SelectedItem = _item;

            Controller.Instance.InvalidateCanvas();
        }

        /// <summary>
        /// Undo's the action
        /// </summary>
        /// <param name="canvas"></param>
        public void Undo(Canvas canvas)
        {
            _item.Accept(DeselectVisitor.Instance);
            _previousSelectedItem?.Accept(SelectVisitor.Instance);
            canvas.SelectedItem = _previousSelectedItem;

            Controller.Instance.InvalidateCanvas();
        }
    }
}
