using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThePainterFormsTest.Models;

namespace ThePainterFormsTest.Commands
{
    public interface ICommand
    {
        void Execute(Canvas canvas);

        void Undo(Canvas canvas);
    }
}
