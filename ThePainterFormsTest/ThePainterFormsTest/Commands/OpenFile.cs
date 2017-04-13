using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThePainterFormsTest.Models;

namespace ThePainterFormsTest.Commands
{
    /// <summary>
    /// Open file commandclass
    /// </summary>
    class OpenFile : ICommand
    {
        string _filePath;

        Stack<ICommand> _history;
        Stack<ICommand> _redoHistory;
        List<DrawableItem> _tempdata;
        DrawableItem _selected;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="filePath">path to the file</param>
        public OpenFile(string filePath)
        {
            _filePath = filePath;
        }

        /// <summary>
        /// Executes the action
        /// </summary>
        /// <param name="canvas"></param>
        public void Execute(Canvas canvas)
        {
            _tempdata = canvas.Items;
            _selected = canvas.SelectedItem;
            CommandExecuter.GetExecutionState(out _history, out _redoHistory);

            canvas.OpenFile(_filePath);
        }

        /// <summary>
        /// Undo's the action
        /// </summary>
        /// <param name="canvas"></param>
        public void Undo(Canvas canvas)
        {
            CommandExecuter.SetExecutionState(_history, _redoHistory);
            canvas.SetTempData(_tempdata, _selected);
        }
    }
}
