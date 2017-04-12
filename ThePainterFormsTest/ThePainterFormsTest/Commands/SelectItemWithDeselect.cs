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
    class SelectItemWithDeselect : ICommand
    {
        private DrawableItem _item;
        private DrawableItem _previousSelectedItem;

        public SelectItemWithDeselect(DrawableItem item)
        {
            _item = item;
        }

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
