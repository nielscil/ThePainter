using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThePainterFormsTest.Commands;
using ThePainterFormsTest.Models;

namespace ThePainterFormsTest.Commands
{
    class ClearCanvas : ICommand
    {

        Stack<ICommand> _history;
        Stack<ICommand> _redoHistory;
        List<DrawableItem> _tempdata;
        DrawableItem _selected;

        public void Execute(Canvas canvas)
        {
            _tempdata = canvas.GetTempData(out _history, out _redoHistory, out _selected);

            canvas.ClearCanvas();
        }

        public void Undo(Canvas canvas)
        {
            canvas.SetTempData(_tempdata, _history, _redoHistory, _selected);
        }
    }
}
