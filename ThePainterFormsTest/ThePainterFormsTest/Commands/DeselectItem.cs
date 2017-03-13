using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThePainterFormsTest.Controllers;
using ThePainterFormsTest.Models;

namespace ThePainterFormsTest.Commands
{
    class DeselectItem : ICommand
    {
        private ICanvasItem _item;

        public DeselectItem(ICanvasItem item)
        {
            _item = item;
        }

        public void Execute(Canvas canvas)
        {
            _item.Deselect();
            canvas.SelectedItem = null;

            Controller.Instance.InvalidateCanvas();
        }

        public void Undo(Canvas canvas)
        {
            _item.Select();
            canvas.SelectedItem = _item;

            Controller.Instance.InvalidateCanvas();
        }
    }
}
