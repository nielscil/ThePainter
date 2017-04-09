using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThePainterFormsTest.Commands;
using ThePainterFormsTest.Controllers;
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
            _tempdata = canvas.Items;
            _selected = canvas.SelectedItem;

            CommandExecuter.GetExecutionState(out _history, out _redoHistory);

            canvas.ClearCanvas();
        }

        public void Undo(Canvas canvas)
        {
            CommandExecuter.SetExecutionState(_history, _redoHistory);
            canvas.SetTempData(_tempdata, _selected);
        }
    }
}
