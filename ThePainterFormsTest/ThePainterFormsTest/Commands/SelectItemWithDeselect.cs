using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThePainterFormsTest.Controllers;
using ThePainterFormsTest.Models;

namespace ThePainterFormsTest.Commands
{
    class SelectItemWithDeselect : ICommand
    {
        private ICanvasItem _item;
        private ICanvasItem _previousSelectedItem;

        public SelectItemWithDeselect(ICanvasItem item)
        {
            _item = item;
        }

        public void Execute(Canvas canvas)
        {
            _previousSelectedItem = canvas.SelectedItem;
            _previousSelectedItem?.Deselect();

            _item.Select();
            canvas.SelectedItem = _item;

            Controller.Instance.InvalidateCanvas();
        }

        public void Undo(Canvas canvas)
        {
            _item.Deselect();
            canvas.SelectedItem = null;

            if(_previousSelectedItem != null)
            { 
                _previousSelectedItem.Select();
                canvas.SelectedItem = _previousSelectedItem;
            }

            Controller.Instance.InvalidateCanvas();
        }
    }
}
