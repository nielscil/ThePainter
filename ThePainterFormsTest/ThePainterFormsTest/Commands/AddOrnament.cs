using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThePainterFormsTest.Controllers;
using ThePainterFormsTest.Models;
using ThePainterFormsTest.States;

namespace ThePainterFormsTest.Commands
{
    public class AddOrnament : ICommand
    {
        private DrawableItem _selectedItem;
        private Ornament _ornament;
        private int _index;

        public AddOrnament(DrawableItem selectedItem, string text, IOrnamentState state, int index)
        {
            _index = index;
            _selectedItem = selectedItem;
            _ornament = new Ornament(text, selectedItem, state);
        }

        public void Execute(Canvas canvas)
        {
            canvas.AddOrnament(_ornament, _index);

            Controller.Instance.InvalidateCanvas();
        }

        public void Undo(Canvas canvas)
        {
            canvas.RemoveOrnament(_ornament);

            Controller.Instance.InvalidateCanvas();
        }
    }
}
