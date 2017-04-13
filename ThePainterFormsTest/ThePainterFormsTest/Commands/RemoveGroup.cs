using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThePainterFormsTest.Models;

namespace ThePainterFormsTest.Commands
{
    /// <summary>
    /// Remove group commandclass
    /// </summary>
    class RemoveGroup : ICommand
    {
        private Group _group;
        private int _index;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="group">to be removed group</param>
        public RemoveGroup(Group group)
        {
            _group = group;
        }

        /// <summary>
        /// Executes the action
        /// </summary>
        /// <param name="canvas"></param>
        public void Execute(Canvas canvas)
        {
            _index = canvas.RemoveGroup(_group);
        }

        /// <summary>
        /// Undo's the action
        /// </summary>
        /// <param name="canvas"></param>
        public void Undo(Canvas canvas)
        {
            canvas.AddItem(_group, _index);
        }
    }
}
