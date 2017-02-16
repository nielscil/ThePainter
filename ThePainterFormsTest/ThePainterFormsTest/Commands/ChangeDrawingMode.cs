using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThePainterFormsTest.Models;
using static ThePainterFormsTest.Models.Canvas;

namespace ThePainterFormsTest.Commands
{
    class ChangeDrawingMode : ICommand
    {
        private Mode _mode, _oldMode;

        public ChangeDrawingMode(Mode mode)
        {
            _mode = mode;
        }

        public void Execute(Canvas canvas)
        {
            _oldMode = canvas.DrawingMode;
            canvas.DrawingMode = _mode;
        }

        public void Undo(Canvas canvas)
        {
            canvas.DrawingMode = _oldMode;
        }
    }
}
