using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThePainterFormsTest.Models;

namespace ThePainterFormsTest.Commands
{
    class RemoveGroup : ICommand
    {
        private Group _group;
        private int _index;

        public RemoveGroup(Group group)
        {
            _group = group;
        }

        public void Execute(Canvas canvas)
        {
            _index = canvas.RemoveGroup(_group);
        }

        public void Undo(Canvas canvas)
        {
            canvas.AddGroup(_group, _index);
        }
    }
}
