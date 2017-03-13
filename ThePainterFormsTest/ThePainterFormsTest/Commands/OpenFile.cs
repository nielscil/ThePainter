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
        List<ICanvasItem> _tempdata;
        ICanvasItem _selected;

        public OpenFile(string filePath)
        {
            _filePath = filePath;
        }

        public void Execute(Canvas canvas)
        {
            _tempdata = canvas.GetTempData(out _history, out _redoHistory, out _selected);

            canvas.OpenFile(_filePath);
        }

        public void Undo(Canvas canvas)
        {
            canvas.SetTempData(_tempdata, _history, _redoHistory, _selected);
        }
    }
}
