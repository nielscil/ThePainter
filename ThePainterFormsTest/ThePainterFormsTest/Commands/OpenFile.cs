using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThePainterFormsTest.Models;

namespace ThePainterFormsTest.Commands
{
    class OpenFile : ICommand
    {
        string _filePath;

        Stack<ICommand> _history;
        Stack<ICommand> _redoHistory;
        List<DrawableItem> _tempdata;
        DrawableItem _selected;

        public OpenFile(string filePath)
        {
            _filePath = filePath;
        }

        public void Execute(Canvas canvas)
        {
            _tempdata = canvas.Items;
            _selected = canvas.SelectedItem;
            CommandExecuter.GetExecutionState(out _history, out _redoHistory);

            canvas.OpenFile(_filePath);
        }

        public void Undo(Canvas canvas)
        {
            CommandExecuter.SetExecutionState(_history, _redoHistory);
            canvas.SetTempData(_tempdata, _selected);
        }
    }
}
