using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThePainterFormsTest.Models;
using static ThePainterFormsTest.Models.Canvas;

namespace ThePainterFormsTest.Commands
{
    /// <summary>
    /// Change drawingmode commandclass
    /// </summary>
    class ChangeDrawingMode : ICommand
    {
        private Mode _mode, _oldMode;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="mode">the new drawingmode</param>
        public ChangeDrawingMode(Mode mode)
        {
            _mode = mode;
        }

        /// <summary>
        /// Executes the action
        /// </summary>
        /// <param name="canvas"></param>
        public void Execute(Canvas canvas)
        {
            _oldMode = canvas.DrawingMode;
            canvas.DrawingMode = _mode;
        }

        /// <summary>
        /// Undo's the action
        /// </summary>
        /// <param name="canvas"></param>
        public void Undo(Canvas canvas)
        {
            canvas.DrawingMode = _oldMode;
        }
    }
}
