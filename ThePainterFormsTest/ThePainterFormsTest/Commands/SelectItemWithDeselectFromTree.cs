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
    public class SelectItemWithDeselectFromTree : ICommand
    {
        private DrawableItem _item;
        private DrawableItem _previousSelectedItem;

        public SelectItemWithDeselectFromTree(DrawableItem item)
        {
            _item = item;
        }

        public void Execute(Canvas canvas)
        {
            _previousSelectedItem = canvas.SelectedItem;
            _previousSelectedItem?.Accept(DeselectVisitor.Instance);

            _item.Accept(SelectVisitor.Instance);
            canvas.SelectedItem = _item;

            Controller.Instance.InvalidateCanvas();
        }

        public void Undo(Canvas canvas)
        {
            _item.Accept(DeselectVisitor.Instance);
            _previousSelectedItem?.Accept(SelectVisitor.Instance);
            canvas.SelectedItem = _previousSelectedItem;

            Controller.Instance.InvalidateCanvas();
        }
    }
}
