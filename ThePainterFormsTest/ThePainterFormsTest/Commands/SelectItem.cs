using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThePainterFormsTest.Controllers;
using ThePainterFormsTest.Models;

namespace ThePainterFormsTest.Commands
{
    class SelectItem : ICommand
    {
        private DrawableItem _item;
        private DrawableItem _previousSelectedItem;

        public SelectItem(DrawableItem item)
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
                canvas.SelectedItem = _previousSelectedItem;
                _previousSelectedItem.Select();
            }

            Controller.Instance.InvalidateCanvas();
        }
    }
}
