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
    public class DeselectItemFromTree : ICommand
    {
        private DrawableItem _item;

        public DeselectItemFromTree(DrawableItem item)
        {
            _item = item;
        }

        public void Execute(Canvas canvas)
        {
            _item.Accept(DeselectVisitor.Instance);
            canvas.SelectedItem = null;

            Controller.Instance.InvalidateCanvas();
        }

        public void Undo(Canvas canvas)
        {
            _item.Accept(SelectVisitor.Instance);
            canvas.SelectedItem = _item;

            Controller.Instance.InvalidateCanvas();
        }
    }
}
